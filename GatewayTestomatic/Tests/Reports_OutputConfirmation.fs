module Reports_OutputConfirmation
open canopy
open runner
open etconfig   
open lib
open gwlib
open System

let mutable lastRequestId = ""

let core _ = 
    context "Reports Output Confirmation"
    once (fun _ -> loginRegulator ())
    lastly (fun _ -> logoutGateway ())

let underConstruction _ = 
    context "Reports Output Confirmation (Under Construction)"
    once (fun _ -> loginRegulator ())
    lastly (fun _ -> logoutGateway ())

    "Report Data Response Confirmation - Trade Data Report (Q6)" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "All Enforcement Agencies"
        url (gatewayUrl + "/msrb1/control/send.asp?target=REGULATOR&auth=1")
        click "For Multiple CUSIPs, All Trades by Dealers (Q6)"
        setFieldValue "#BeginDate" "01/04/2012"
        setFieldValue "#EndDate" "01/04/2012"
        setFieldValue "#CusipsRawString" "016066BH4"
        click "Submit"
        let t1 () =
            sleep 2.0
            someElement("Regulator Query System — Request Submitted").IsSome
        extendTimeout (fun _ -> waitFor t1)
        assertDisplayed "Regulator Query System — Request Submitted"
        assertFieldContains "div.o2info p:nth-of-type(1)" "Request for Q6 report has been submitted." 
        assertFieldContains "div.o2info p:nth-of-type(2)" "Request ID:"
        lastRequestId <- (read "div.o2info p:nth-of-type(2)").Replace("Request ID: ", "")
        Console.WriteLine("Request ID: = " + lastRequestId)
        click "Return to Query Menu"
        let f1 () =
            sleep 2
            click "#completedQueriesTab"
            setFieldValue "#completedQueriesTable_filter input" lastRequestId
            someElement("a[href='/QueryReports/Pickup/GetFiles?requestId=" + lastRequestId + "&reportType=Q6']").IsSome
        extendTimeout (fun _ -> waitFor f1)
        assertFieldContains ("a[href='/QueryReports/Pickup/GetFiles?requestId=" + lastRequestId + "&reportType=Q6']") lastRequestId

    "Report Data Response Confirmation - Primary Market Disclosures (Q20)" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "All Enforcement Agencies"
        url (gatewayUrl + "/msrb1/control/send.asp?target=REGULATOR&auth=1")
        click "Disclosure"
        click "a[href='/QueryReports/EMMA/Q20Underwriter']"    
        setFieldValue "#BeginDate" "01/04/2012"
        setFieldValue "#EndDate" "01/04/2012"
        setFieldValue "#MsrbId" "A0278"
        click "Submit"
        let t1 () =
            sleep 2.0
            someElement("Regulator Query System — Request Submitted").IsSome
        extendTimeout (fun _ -> waitFor t1)
        assertDisplayed "Regulator Query System — Request Submitted"
        assertFieldContains "div.o2info p:nth-of-type(1)" "Request for Q20 report has been submitted." 
        assertFieldContains "div.o2info p:nth-of-type(2)" "Request ID:"
        lastRequestId <- (read "div.o2info p:nth-of-type(2)").Replace("Request ID: ", "")
        Console.WriteLine("Request ID: = " + lastRequestId)
        click "Return to Query Menu"
        let f1 () =
            sleep 2
            click "#completedQueriesTab"
            setFieldValue "#completedQueriesTable_filter input" lastRequestId
            someElement("a[href='/QueryReports/Pickup/GetFiles?requestId=" + lastRequestId + "&reportType=Q20']").IsSome
        extendTimeout (fun _ -> waitFor f1)
        assertFieldContains ("a[href='/QueryReports/Pickup/GetFiles?requestId=" + lastRequestId + "&reportType=Q20']") lastRequestId




