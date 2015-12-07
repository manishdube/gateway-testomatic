module Subscriptions
open canopy
open runner
open etconfig
open lib
open gwlib
open System

let core _ = 
    context "Subscriptions - Subscription File Retrieval"
    once (fun _ -> loginGateway ())
    lastly (fun _ -> logoutGateway ())

    "SUB_TFILE_T1_TODAY&fileName=ArchiveT1.txt" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Subscription File Retrieval"
        assertFieldContains "h3" "Transaction Data Subscription Service File Download"

    "TestT5Today.txt" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Subscription File Retrieval"
        assertFieldContains "h3" "Transaction Data Subscription Service File Download"

    "SUB_TFILE_T1_ARCHIVE&fileName=ArchiveT1.txt" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Subscription File Retrieval"
        assertFieldContains "h3" "Transaction Data Subscription Service File Download"

    "TestT20Archive.txt" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Subscription File Retrieval"
        assertFieldContains "h3" "Transaction Data Subscription Service File Download"

    "SUB_RTRS_REPLAY_ARCHIVE_PATH&fileName=replay%20archive.txt" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Subscription File Retrieval"
