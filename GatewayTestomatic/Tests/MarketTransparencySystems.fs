module MarketTransparencySystems
open canopy
open runner
open etconfig
open lib
open gwlib
open System
open warmup

let core _ = 
    context "MarketTransparencySystems"
    once (fun _ -> loginGateway ())
    lastly (fun _ -> logoutGateway ())
//    once (fun _ -> warmupEMMA ())

    "Rule G-37 Submission" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#systemstog" 
        click "a[href='send.asp?target=eg37']"
        assertUrl (gatewayUrl + "Submission/SubmissionPortal.aspx")

    "EMMA Dataport" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#systemstog"
        click "a[href='send.asp?target=EMMAEOS']" 
        assertUrl (gatewayUrl + "Submission/SubmissionPortal.aspx") 

let underConstruction _ = 
    context "MarketTransparencySystems (Under Construction)"

    "SHORT System Web User Interface – Data Submissions" &&& fun _ ->
        loginGateway ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#systemstog" 
        click "SHORT System Web User Interface – Data Submissions"
        assertUrl (shortUrl + "msrb_avts_ui/Blotter.action")
        assertFieldContains "td.header" "SUBMISSION MANAGEMENT"

    "SHORT System Web User Interface – Data Submissions Test Environment" &&& fun _ ->
        loginGateway ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#systemstog" 
        click "SHORT System Web User Interface – Data Submissions Test Environment"
        assertUrl (gatewayUrl + "msrb1/control/?guid=")

    "RTRS Web Interface - Production System" &&& fun _ ->
        loginGateway ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#systemstog" 
        click "a[href='/msrb1/control/send.asp?target=RTRSWEB']"
        assertUrl (gatewayUrl + "msrb1/control/default.asp?Login=expired")
               
