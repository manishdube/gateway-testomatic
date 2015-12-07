module lib
open System
open canopy
open canopy.configuration
open canopy.types
open etconfig
open etreporter
open OpenQA.Selenium

let rnd = System.Random()

let isIE () =
    (browser :? OpenQA.Selenium.IE.InternetExplorerDriver)

let isChrome () =
    (browser :? OpenQA.Selenium.Chrome.ChromeDriver)

let isFirefox () =
    (browser :? OpenQA.Selenium.Firefox.FirefoxDriver)

let setFieldValue (f : string) (v : string) =
    f << v

let assertFieldValue (f : string) (v : string) =
    f == v

let assertFieldContains (f : string) (v : string) =
    contains v (read f)

let assertUrl (u : string) =
    on u

let assertDisplayed (f : string) =
    displayed f

let assertNotDisplayed (f : string) =
    notDisplayed f

let assertElementExists (f : string) =
    ignore(browser.FindElement(OpenQA.Selenium.By.Id(f)))

let assertEqual expected actual =
    is expected actual

let getRandomNumberString (size : int) =
    // only good for size between 1 and 9
    let max = pown 10 size
    let fmt = new String ('0', size)
    (rnd.Next max).ToString(fmt)

let getRandomString (size : int) = 
    let alpha = [| "A"; "B"; "C"; "D"; "E"; "F"; "G"; "H"; "I"; "J"; "K"; "L"; "M"; "N"; "O"; "P"; "Q"; "R"; "S"; "T"; "U"; "V"; "W"; "X"; "Y"; "Z" |]
    let mutable ret:string = ""
    for i = 0 to size do
        ret <- ret + alpha.[rnd.Next 26]
    ret

let fileUploadSelectPdf (value1 : string) = 
    let fileName = (AppDomain.CurrentDomain.BaseDirectory  + @"sample.pdf")
    (element value1).SendKeys(fileName)

let waitForAjax () =
    let t0 () =
        sleep 0.01
        let result = js "return $.active;" |> Convert.ToInt32
        result = 0
    waitFor t0

let safeCloseWindow () =
    if browser.WindowHandles.Count > 1 then browser.Close()
    ()

let locateWindowWithTitleText(text:String) =
    let t0 () =
        sleep 0.2
        let mutable retWindow=null
        let mutable retVal=false
        try
            for window in browser.WindowHandles do
                browser.SwitchTo().Window(window) |> ignore
                printf "Current browser title %s \n" browser.Title
                if browser.Title.Contains(text) then
                    printf "String was found in browser handle with title %s \n" browser.Title
                    retWindow <- browser.CurrentWindowHandle
                    retVal <- true
                    ()
        with
            | ex -> printfn "%s" (ex.ToString())
        if retWindow <> null then
            browser.SwitchTo().Window(retWindow) |> ignore
        else 
            printf "  Browser was not found in current handle list of size (%i) \n" browser.WindowHandles.Count
        printf "  Retval is %b \n" retVal
        retVal        
    waitFor t0
        
let extendTimeoutMS (fn : unit -> unit, timeoutValueMS : float) =
    let origCompareTimeout = configuration.compareTimeout
    try
        configuration.compareTimeout <- 60.0 
        fn ()
    finally
        configuration.compareTimeout <- origCompareTimeout

let extendTimeout (fn : unit -> unit) =
    extendTimeoutMS(fn,  60.0)

let objective (s : string) =
    match box reporter with
    | :? IReporterEx as rx -> rx.objective s
    | _ -> ()

let precondition (s : string) =
    match box reporter with
    | :? IReporterEx as rx -> rx.precondition s
    | _ -> ()

let postcondition (s : string) =
    match box reporter with
    | :? IReporterEx as rx -> rx.postcondition s
    | _ -> ()

let assertFieldIsReadOnly (f : string) =
    let v = (element f).GetAttribute("readonly")
    is "true" v

let assertElementIsAlphaNumeric (s : string) =
    s =~ "^[a-zA-Z0-9]+$"
    is "true" s

let assertStringIsAlphaNumeric (s : string) =
    let m = System.Text.RegularExpressions.Regex.Match( s , "^[a-zA-Z0-9]+$" ).Success
    is true m
