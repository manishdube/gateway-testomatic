module RestrictedContent_PQAC
open canopy
open runner
open etconfig
open lib
open gwlib
open System

let core _ = 
    context "RestrictedContent - Professional Qualifications Advisory Committee"
    once (fun _ -> loginGateway ())
    lastly (fun _ -> logoutGateway ())

    "Calendar- 2014 Committee Meeting Schedule" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        click "Calendar- 2014 Committee Meeting Schedule"
        assertUrl (gatewayUrl + "msrb1/control/pqac/calendar.asp")

    "Series 51 Committee Assignment" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        assertDisplayed "a[href='file.asp?file=Series-51-February-Meeting-Assignment-and-Exhibits.pdf&path=/pqac/&option=1']"
        assertFieldContains "a[href='file.asp?file=Series-51-February-Meeting-Assignment-and-Exhibits.pdf&path=/pqac/&option=1']" "Series 51 Committee Assignment"

    "Series 52/53 Committee Assignment" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        assertDisplayed "a[href='file.asp?file=Series-52-53-Committee-Assignment.pdf&path=/pqac/&option=1']"
        assertFieldContains "a[href='file.asp?file=Series-52-53-Committee-Assignment.pdf&path=/pqac/&option=1']" "Series 52/53 Committee Assignment"

    "Work Made for Hire Agreement" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        click "Work Made for Hire Agreement"
        assertUrl (gatewayUrl + "msrb1/control/pqac/forhire.asp")

    "Non-Staff Travel and Other Expense Reimbursement Policy and Voucher" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        assertDisplayed "a[href='file.asp?file=TravelExpenses.pdf&path=/pqac/&option=1']"
        assertFieldContains "a[href='file.asp?file=TravelExpenses.pdf&path=/pqac/&option=1']" "Non-Staff Travel and Other Expense Reimbursement Policy and Voucher"
        
    "MSRB Glossary" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        click "MSRB Glossary"
        locateWindowWithTitleText "Glossary"
        assertUrl ("www.msrb.org/Glossary.aspx")
        safeCloseWindow()
        locateWindowWithTitleText "Rulemaking Board"

    "Municipal Fund Securities Limited Principal Qualification Examination (Series 51)" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        assertDisplayed "a[href='file.asp?file=Series51StudyOutlinewithcodes.pdf&path=/pqac/&option=1']"
        assertFieldContains "a[href='file.asp?file=Series51StudyOutlinewithcodes.pdf&path=/pqac/&option=1']" "Municipal Fund Securities Limited Principal Qualification Examination (Series 51)"

    "Municipal Securities Representative Qualification Examination (Series 52)" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        assertDisplayed "a[href='file.asp?file=CodedOutlineApr10vFINAL1.pdf&path=/pqac/&option=1']"
        assertFieldContains "a[href='file.asp?file=CodedOutlineApr10vFINAL1.pdf&path=/pqac/&option=1']" "Municipal Securities Representative Qualification Examination (Series 52)"

    "Municipal Securities Principal Qualification Examination (Series 53)" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        assertDisplayed "a[href='file.asp?file=Series53StudyOutlinewithcodes.pdf&path=/pqac/&option=1']"
        assertFieldContains "a[href='file.asp?file=Series53StudyOutlinewithcodes.pdf&path=/pqac/&option=1']" "Municipal Securities Principal Qualification Examination (Series 53)"

    "Guide to Item Writing and Review" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        assertDisplayed "a[href='file.asp?file=PQACHandbook.pdf&path=/pqac/&option=1']"
        assertFieldContains "a[href='file.asp?file=PQACHandbook.pdf&path=/pqac/&option=1']" "Guide to Item Writing and Review" 

    "Committee Roster" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        assertDisplayed "a[href='file.asp?file=PQAC_ROSTER.pdf&path=/pqac/&option=1']"
        assertFieldContains "a[href='file.asp?file=PQAC_ROSTER.pdf&path=/pqac/&option=1']" "Committee Roster" 

    "PQAC Administrator Area - PQAC User Stats Report - Page View Statistics" &&& fun _ ->
        objective "Display the Page View Statistucs for PQAC Users"
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        click "a[href='admin/default.asp']" 
        assertFieldContains  "PQ Admin Restricted Content" (read "h1")
        click "a[href='pqacquery.asp']"                 //PQAC User Stats Report
        assertFieldContains "h1" "Restricted PQAC Page View Statistics Query"
        setFieldValue "#member" "scook"
        click "Submit"
        assertFieldContains "h1" "Restricted PQAC Site Page View Statistics" 

    "PQAC Administrator Area - PQAC User Info" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee"  
        assertFieldContains "h1" "PQ Member Restricted Content"
        click "a[href='admin/default.asp']" 
        assertFieldContains  "h1" "PQ Admin Restricted Content"
        click "a[href='userinfo.asp']"                 //PQAC User Info
        assertFieldContains "h1" "PQAC User Details"

    "PQAC Administrator Area - Archives" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "Professional Qualifications Advisory Committee" 
        assertFieldContains "h1" "PQ Member Restricted Content" 
        click "a[href='events/default.asp']"            //Archives
        assertFieldContains "h1" "PQAC Events"
        click "a[href='07/default.asp']"                //2007 Sedona, AZ
        assertUrl (gatewayUrl + "msrb1/control/pqac/events/07/default.asp")
        assertFieldContains "h3" "PQWeb Sedona Arizona Event - 2007" 
         