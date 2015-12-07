module etreporter
open canopy
open reporters
open System
open System.Collections.Generic

type IReporterEx =
    abstract member objective : string -> unit
    abstract member precondition : string -> unit
    abstract member postcondition : string -> unit


type LogFileReporter (logFileName : string) =
    
    let openOutputStream () =
        let outputFileInfo = new IO.FileInfo (logFileName)
        if not (System.IO.Directory.Exists (outputFileInfo.Directory.FullName)) then
            System.IO.Directory.CreateDirectory (outputFileInfo.Directory.FullName) |> ignore
        new System.IO.StreamWriter (outputFileInfo.FullName)

    let tw = openOutputStream ()

    interface IReporter with               
        member this.pass () = tw.WriteLine " PASSED"

        member this.fail ex id ss sss =
            tw.WriteLine " FAILED"
            tw.WriteLine ("    " + ex.Message)
            tw.WriteLine "    stack:"
            ex.StackTrace.Split([| "\r\n"; "\n" |], StringSplitOptions.None)
            |> Array.iter (fun trace -> 
                if trace.Contains(".FSharp.") || trace.Contains("canopy.core") || trace.Contains("OpenQA.Selenium") 
                    || trace.Contains("canopy.runner") then
                    ()
                else
                    tw.WriteLine ("    " + trace)
                )

        member this.describe d = tw.WriteLine d
          
        member this.contextStart c = tw.WriteLine ("context \"" + c + "\"")

        member this.contextEnd c = ()

        member this.summary minutes seconds passed failed skipped =
            tw.WriteLine ()
            tw.WriteLine ("{0} minutes {1} seconds to execute", minutes, seconds)
            tw.WriteLine ("{0} passed", passed)
            tw.WriteLine ("{0} failed", failed)
        
        member this.write w = tw.WriteLine ("    " + w)
        
        member this.suggestSelectors selector suggestions = ()

        member this.testStart id = tw.Write ("test \"" + id + "\"")
            
        member this.testEnd id = ()

        member this.quit () =
            tw.Close ()
            tw.Dispose ()
        
        member this.suiteBegin () = ()

        member this.suiteEnd () = ()

        member this.coverage url ss = ()

        member this.todo () = ()

        member this.skip cnt = ()

        member this.setEnvironment s = ()

type TeeReporter () =
    let _reporters = new List<IReporter>()

    member this.Add (r : IReporter) =
        _reporters.Add r

    interface IReporter with               
        member this.pass () =
            _reporters.ForEach (fun i -> i.pass ())

        member this.fail ex id ss sss =
            _reporters.ForEach (fun i -> i.fail ex id ss sss)

        member this.describe d = 
            _reporters.ForEach (fun i -> i.describe d)
          
        member this.contextStart c = 
            _reporters.ForEach (fun i -> i.contextStart c)

        member this.contextEnd c = 
            _reporters.ForEach (fun i -> i.contextEnd c)

        member this.summary minutes seconds passed failed skipped =  
            _reporters.ForEach (fun i -> i.summary minutes seconds passed failed skipped)
        
        member this.write w = 
            _reporters.ForEach (fun i -> i.write w)
        
        member this.suggestSelectors selector suggestions = 
            _reporters.ForEach (fun i -> i.suggestSelectors selector suggestions)

        member this.testStart id = 
            _reporters.ForEach (fun i -> i.testStart id)
            
        member this.testEnd id = 
            _reporters.ForEach (fun i -> i.testEnd id)

        member this.quit () = 
            _reporters.ForEach (fun i -> i.quit ())
        
        member this.suiteBegin () = 
            _reporters.ForEach (fun i -> i.suiteBegin ())

        member this.suiteEnd () = 
            _reporters.ForEach (fun i -> i.suiteEnd ())

        member this.coverage url ss = 
            _reporters.ForEach (fun i -> i.coverage url ss)

        member this.todo () = 
            _reporters.ForEach (fun i -> i.todo ())

        member this.skip cnt = 
            _reporters.ForEach (fun i -> i.skip cnt)

        member this.setEnvironment s =
            _reporters.ForEach (fun i -> i.setEnvironment s)
            
    interface IReporterEx with               
        member this.objective s =
            _reporters.ForEach (fun i -> 
                match box i with
                | :? IReporterEx as rx -> rx.objective s
                | _ -> ()
                )

        member this.precondition s =
            _reporters.ForEach (fun i -> 
                match box i with
                | :? IReporterEx as rx -> rx.precondition s
                | _ -> ()
                )

        member this.postcondition s =
            _reporters.ForEach (fun i -> 
                match box i with
                | :? IReporterEx as rx -> rx.postcondition s
                | _ -> ()
                )
