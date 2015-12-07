module summaryreporter
open canopy
open reporters
open etreporter
open System
open System.Collections.Generic
open TestomaticSupport

type SummaryReporter (outputFile : string) =
    let mutable currentContextItem : SummaryReportItem = null
    let mutable currentTestItem : SummaryReportItem = null
    let reportItems = new List<SummaryReportItem>()
    let reportInfo = new ReportInfo()

    member this.Info (appName : string) (version : string) (executionTime : DateTime) (browser : string) (param : string list ) =
        reportInfo.TestApplicationName <- appName
        reportInfo.Version <- version
        reportInfo.ExecutionTime <- System.Nullable executionTime
        reportInfo.Browser <- browser
        reportInfo.Parameters.AddRange param

    interface IReporter with
        member this.contextStart c =
            currentContextItem <- new SummaryReportItem "Context"
            reportItems.Add currentContextItem
            currentContextItem.Name <- c
            currentContextItem.BeginTime <- System.Nullable DateTime.Now
        member this.contextEnd c =
            currentContextItem.EndTime <- System.Nullable DateTime.Now

        member this.testStart id =
            currentTestItem <- new SummaryReportItem "Test"
            reportItems.Add currentTestItem
            currentTestItem.Name <- id
            currentTestItem.BeginTime <- System.Nullable DateTime.Now
        member this.pass () =
            currentTestItem.Pass <- System.Nullable true
        member this.fail ex id ss sss =
            currentTestItem.Pass <- System.Nullable false
        member this.testEnd id = 
            currentTestItem.EndTime <- System.Nullable DateTime.Now

        member this.suiteBegin () = ()
        member this.suiteEnd () = 
            use xl = new ExcelReporter()
            xl.Init "SummaryReportTemplate.xlsx"
            xl.FormatReport (reportInfo, reportItems)
            xl.Save outputFile

        member this.summary minutes seconds passed failed skipped =
            reportInfo.DurationMinutes <- minutes
            reportInfo.DurationSeconds <- seconds
            reportInfo.PassCount <- passed
            reportInfo.FailCount <- failed

        member this.describe d = ()
        member this.write w = ()
        member this.suggestSelectors selector suggestions = ()
        member this.quit () = ()
        member this.coverage url ss = ()
        member this.todo () = ()
        member this.skip cnt = ()
        member this.setEnvironment s = ()

    interface IReporterEx with
        member this.objective s =
            currentTestItem.Objective <- s
        member this.precondition s =
            currentTestItem.Precondition <- s
        member this.postcondition s =
            currentTestItem.Postcondition <- s


