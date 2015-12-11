module testmain
open canopy
open canopy.configuration
open canopy.reporters
open canopy.types
open runner
open etconfig
open etreporter
open summaryreporter
open commandlineargs
open warmup
open System

let appName = AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "")

let usage = "usage: {appName} [args]\n" +
            "where args is optional and can be a combination of the following:\n" +
            "    -Firefox (use Firefox browser*)\n" +
            "    -Chrome (use Chrome browser)\n" +
            "    -IE (use Internet Explorer browser)\n" +
            "    -WarmUp (run prepare both emma and dataport for testing*)\n" +
            "    -NoWarmUp (do NOT run prepare both emma and dataport for testing)\n" +
            "    -Log (create log file of results, use logFileTemplate entry in config.yaml to specify file name*)\n" +
            "    -NoLog (do NOT create log file of results)\n" +
            "    -TeamCity (create output for Team City, all other output OFF)\n" +
            "    -SqlInit (execute sql init file with parameters specified in config.yaml)\n" +
            "    -Dev (run only the dev test suite)\n" +
            "    -PressEnter (require entering return key at end of tests)\n" +
            "    -CoreOnly (Ignore any tests that are under construction)\n" +
            "    -Help or -? (display this usage text and run no tests)\n" +
            "parameters are not case sensitive\n" +
            "* default\n" +
            "running without specifying any args is equivalent to \"-Firefox -WarmUp -Log\"\n" +
            "test case parameters are specified in the \"config.yaml\" file\n" +
            "this file can be edited with notepad or any other text editor"

let configReporters (cla : CommandLineArgs) =
    if cla.teamCity then
        let teamCityReporter = new TeamCityReporter ()
        canopy.configuration.reporter <- new TeamCityReporter()
    else if cla.log then
        let t = new TeeReporter ()
        canopy.configuration.reporter <- t
        //t.Add (new ConsoleReporter ()) // part of LiveHTMLReporter

        let executionTime =  DateTime.Now
        let dtString = executionTime.ToString "yyyy-MM-dd-HHmm"
        let logFileName = config.logFileTemplate.Replace("{dt}", dtString)
        let summaryFileName = config.summaryFileTemplate.Replace("{dt}", dtString)

        let logReporter = new LogFileReporter (logFileName) :> IReporter
        t.Add logReporter
        logReporter.describe (sprintf "%s version %s" appName versioninfo.versionString)
        logReporter.describe (sprintf "tests executed on %s %s" (executionTime.ToShortDateString()) (executionTime.ToShortTimeString()))
        let browser = match cla.browser with
                      | Firefox -> "Firefox"
                      | Chrome -> "Chrome"
                      | IE -> "IE"
        logReporter.describe (sprintf "browser: %s" browser)
        logReporter.describe "parameters:"
        logReporter.write ("gatewayUrl: " + gatewayUrl)

        let summaryReporter = new SummaryReporter (summaryFileName)
        t.Add summaryReporter
        summaryReporter.Info appName versioninfo.versionString executionTime browser ["gatewayUrl"; gatewayUrl]

        let liveHtmlReporter = new LiveHtmlReporter ()
        liveHtmlReporter.reportPath <- Some(config.htmlFileTemplate.Replace("{dt}", dtString))
        t.Add liveHtmlReporter

[<EntryPoint>]
let main argv =
    let cla = parseCommandLine argv
    if cla.versionOnly then
        printfn "%s" versioninfo.versionString
        0
    else if cla.showUsage || not cla.isValid then
        printfn "%s" (usage.Replace("{appName}", appName))
        -1
    else
        printfn "%s version %s" appName versioninfo.versionString
        if cla.sqlInit then
            let sqlRunner = TestomaticSupport.SqlRunner()
            sqlRunner.Execute(config.sqlInit.tnsName, config.sqlInit.userId, config.sqlInit.password, AppDomain.CurrentDomain.BaseDirectory, config.sqlInit.fileName)
        configReporters cla
        chromeDir <- (AppDomain.CurrentDomain.BaseDirectory + @"\BrowserSupport")
        ieDir <- (AppDomain.CurrentDomain.BaseDirectory + @"\BrowserSupport")
        if cla.devMode then
            devtests.all ()
        else
//            AccountOrganizationManagement.core ()
//            Administration_GeneralSystem.core ()
//            Administration_User.core ()
//            AgentRelationshipManagement.core ()
//            DealerFeedbackSystem.core ()
//            FormA12Registrant.core ()
//            FormA12Registration.core ()
//            FormA12RegistrationMisc.core ()
//            MarketTransparencyServices.core ()
            MarketTransparencySystems.core ()
//            OrganizationAdministration.core ()
            RegistrationApproval_IssuersObligorIndividualAgent.core ()
//            RegistrantInformationForms.core () 
//            RestrictedContent_MSRBDepartmentHeadContent.core ()
//            RestrictedContent_PQAC.core ()
//            Reports_OutputConfirmation.core ()
//            Reports_RQS_Disclosure.core ()
//            Reports_RQS_TradeData.core ()
//            RightsManagementAndAuthentication.core ()
//            StaffContentMiscellaneous.core ()
//            Subscriptions.core ()
            FormA12RelatedLinks.core ()
            Reports_RegulatorWeb_RegistrationReports.core ()
//            if cla.coreOnly = false then
//                MarketTransparencySystems.underConstruction()
//                Reports_RegulatorWeb_RegistrationReports.underConstruction()
////                Reports_OutputConfirmation.underConstruction()
//            UserProfile.core ()
        match cla.browser with
            | Firefox -> start firefox
//            | Chrome -> istart chrome
            | IE -> start ie
        if cla.warmUp then
            warmupGateway ()
            warmupRQS ()
            warmupMsrbOrg ()
            warmupEmmaDataport ()
            warmupDQDashboard ()
        run()
        if cla.pressEnter then
            printfn "press [enter] to exit"
            System.Console.ReadLine() |> ignore
        quit()
        0
