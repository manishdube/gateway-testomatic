module DealerFeedbackSystem
open canopy
open runner
open etconfig
open lib
open gwlib
open System

let core _ = 
    context "Dealer Feedback System"
    once (fun _ -> loginGateway ())
    lastly (fun _ -> logoutGateway ())

    "Transaction Data Request Form - New Transaction Data Request (D1) - Invalid MSRBID" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#dfstog" 
        click "Transaction Data Request Form"
        assertFieldContains "h2" "Dealer Feedback System"
        click "a[href='/QueryReports/DFS/D1']"
        assertFieldContains "h2" "Transaction Data Request Form (D1)"
        setFieldValue "#MsrbId" "MSRB"
        let opt = (element "select[name='SelectedMonthItemId'] option:last-of-type").Text
        setFieldValue "select[name='SelectedMonthItemId']" opt
        click "Continue"
        assertFieldContains "div.o2info:nth-of-type(3)" "MSRB ID is invalid."
        
    "Transaction Data Request Form - New Transaction Data Request (D1) Valid MSRBID" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#dfstog" 
        click "Transaction Data Request Form"
        assertFieldContains "h2" "Dealer Feedback System"
        click "a[href='/QueryReports/DFS/D1']"
        assertFieldContains "h2" "Transaction Data Request Form (D1)"
        setFieldValue "#MsrbId" "A0278"
        let opt = (element "select[name='SelectedMonthItemId'] option:last-of-type").Text
        setFieldValue "select[name='SelectedMonthItemId']" opt
        click "Continue"
        assertFieldContains "div.o2info" "Request ID"

    "Transaction Data Request Form - D99" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#dfstog" 
        click "Transaction Data Request Form"
        click "a[href='/QueryReports/PSO/D99']" 
        assertFieldContains "h2:nth-of-type(2)" "Non Standard Report Upload (D99)"
        setFieldValue "#MsrbId" "MSRB"
        setFieldValue "#RequestorId" "SCOOK"
        click "Submit"
        assertFieldContains "h2" "Dealer Feedback System — Queued Report Details"
        click "Upload"
        assertFieldContains "div.o2info" "Please select a file to upload."
        
    "View Request Queue" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#dfstog" 
        click "Transaction Data Request Form"
        click "a[href='/QueryReports/PSO/Search']" 
        assertFieldContains "h2" "Dealer Feedback System — View Request Queue"
        setFieldValue "input[name='psoSearchModel.QueryId']" "200001"
        setFieldValue "input[name='psoSearchModel.ExaminerName']" "SCOOK"
        setFieldValue "input[name='psoSearchModel.TargetCrd']" "12345"
        setFieldValue "input[name='psoSearchModel.TargetMsrbId']" "MSRB"
        setFieldValue "input[name='psoSearchModel.ReceivedDateFrom']" "11/02/2014"
        setFieldValue "input[name='psoSearchModel.ReceivedDateTo']" "11/30  /2014"
        setFieldValue "select[name='psoSearchModel.Status']" "Any"
        click "Search"
        assertFieldContains "div.mainContent" "No data was found."

                