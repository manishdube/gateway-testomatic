module warmup
open canopy
open configuration
open etconfig
open lib
open gwlib
open System
open runner

let warmupTimeout = 120.0

let warmupGateway () =
    reporter.describe "warmupGateway"
    try
        extendTimeoutMS (( fun _ ->
            loginStaff ()
            logout ()
            ), warmupTimeout)
    with
    | _ -> reporter.write "warmupGateway failed"

let warmupRQS () =
    reporter.describe "warmupRQS"
    if config.regWebV2 = true then
        try
            extendTimeoutMS ((fun _ ->
                loginGateway ()
                url (gatewayUrl + "msrb1/control/selection.asp")
                click "a[href='/msrb1/control/send.asp?target=REGWEB&auth=1']"
                click "a[href='/Regulator/RegWeb/RedirectToQueryReports']"
                waitFor ( fun _ ->
                    sleep 3.0
                    someElement("All Trades by One Dealer (Q1)").IsSome
                    )
                logout ()
                ), warmupTimeout)
        with
        | _ -> reporter.write "warmupRQS failed"

    if config.regWebV2 = false then
        try
            extendTimeoutMS ((fun _ ->
                loginGateway ()
                url (gatewayUrl + "msrb1/control/selection.asp")
                click "#regulatortog"
                click "All Enforcement Agencies"
                click "Regulator Query System"
                waitFor ( fun _ ->
                    sleep 3.0
                    someElement("All Trades by One Dealer (Q1)").IsSome
                    )
                logout ()
                ), warmupTimeout)
        with
        | _ -> reporter.write "warmupRQS failed"

let warmupMsrbOrg () =
    reporter.describe "warmupMsrbOrg"
    try
        extendTimeoutMS (( fun _ ->
            url (msrbOrgUrl)
            waitFor ( fun _ ->
                someElement("[href='/EducationCenter.aspx']").IsSome
                )
            ), warmupTimeout)
    with
    | _ -> reporter.write "warmupMsrbOrg failed"

let warmupEmmaDataport () =
    reporter.describe "warmupEmmaDataport"
    try
        extendTimeoutMS (( fun _ ->
            loginGateway ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "#systemstog"
            click "a[href='send.asp?target=EMMAEOS']" 
            assertUrl (gatewayUrl + "Submission/SubmissionPortal.aspx") 
            ), warmupTimeout)
    with
    | _ -> reporter.write "warmupEmmaDataport failed"

let warmupDQDashboard () =
    reporter.describe "warmupDQDashboard"
    try
        extendTimeoutMS (( fun _ ->
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "#stafftog"
            click "Market Transparency Services"
            click "Data Quality Dashboard"
            assertFieldContains "div.page" "Data Quality Assurance"  
            ), warmupTimeout)
    with
    | _ -> reporter.write "warmupDQDashboard failed"
