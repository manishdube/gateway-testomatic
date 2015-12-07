module StaffContentMiscellaneous
open canopy
open runner
open etconfig
open lib
open gwlib
open System

let core _ = 
    context "Staff Content"
    once (fun _ -> loginGateway ())
    lastly (fun _ -> logoutGateway ())

    "Dealer Outage Reporting" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Transaction Reporting"
        click "a[href='/msrb1/control/send.asp?target=SYSOUTAGEUSER&auth=1']"
        let t1 () =
            sleep 2.0
            someElement("Dealer System Outage Report").IsSome
        extendTimeout (fun _ -> waitFor t1)
        assertFieldContains "span.heading" "Dealer System Outage Report" 
        assertFieldContains "h3" "All Reported Outages In Past 31 Days"

    "Board Application Admin" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Board Application Admin"
        let t2 () =
            sleep 2.0
            someElement("Board Administration Portal").IsSome
        extendTimeout (fun _ -> waitFor t2)
        assertUrl (gatewayUrl + "BoardApplication/Admin/Applicants")
        assertFieldContains "span.heading" "Board Administration Portal" 

