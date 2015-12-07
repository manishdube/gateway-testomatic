module MarketTransparencyServices
open canopy
open runner
open etconfig
open lib
open gwlib
open System

let core _ = 
    context "Market Transparency Services"
    once (fun _ -> loginGateway ())
    lastly (fun _ -> logoutGateway ())

    "Process CD Registrations" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        click "Process CD Registrations"
        setFieldValue "input[name='msrbId']" "A0005"
        setFieldValue "input[name='confid']" "0000ACDQ"
        click "Find"
        let t1 () =
            sleep 2.0
            someElement("No match was found for given Confirmation ID and MSRB ID.").IsSome
        extendTimeout (fun _ -> waitFor t1)         
        assertFieldContains "span.error" "No match was found for given Confirmation ID and MSRB ID."

    "Process CD Registrations with NULL Confirmation Numbers" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        click "Process CD Registrations with Null Confirmation Numbers"
        setFieldValue "input[name='msrbId']" "MSRB999"
        setFieldValue "input[name='userId']" "GWSTAFF"
        click " Confirm User "
        assertFieldContains "table" "Confirmation Requested On"

    "Submit Ad-Hoc Requests for Data from Regulators" &&! fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        click "Submit Ad-Hoc Requests for Data from Regulators"
        setFieldValue "select[name='organization']" "MSRB"
        sleep 0.01
        setFieldValue "select[name='location']" "Headquarters"
        setFieldValue "input[name='examiner']" "MSRB"
        setFieldValue "input[name='phone']" "7032251111"
        setFieldValue "input[name='email']" "email@email.com"
        setFieldValue "input[name='start_date']" "01/01/2014"  
        setFieldValue "input[name='stop_date']" "01/01/2014"
        click "Submit Form"
        assertFieldContains "h3" "Form Confirmation for Q99"

    "RTRS Report Usage Log" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/rtrswebusage/lookup.asp")
        setFieldValue "input[name='start_date']" "09/01/2010"
        setFieldValue "input[name='end_date']" "09/01/2010"
        click "    Submit    "
        let t1 () =
            sleep 2.0
            someElement("Report Name").IsSome
        extendTimeout (fun _ -> waitFor t1)         
        assertDisplayed "Report Name"

    "View TRA Reports/iLog Account Details" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/reports/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/find/finduser.asp")
        setFieldValue "#userid" "MASTERUSER"
        click "Locate Account"
        assertFieldContains "td" "Accounts matching your search criteria"            

    "View TRA Reports/RTRS Testing Reports/RTRS Web Testing/Mandatory Testing Report" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/reports/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/mandatory.asp?pop=yes")
        setFieldValue "input[name='ebs']" "PWPR" 
        click "        Submit        "
        assertFieldContains "td" "No records located matching your search criteria"       

    "View TRA Reports/RTRS Testing Reports/RTRS Web Testing/As Appropriate Testing Report" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/reports/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/asappropriate.asp?pop=yes")
        setFieldValue "input[name='ebs']" "PWJC"
        click "        Submit        "
        assertFieldContains "td" "No records located matching your search criteria"  

    "View TRA Reports/RTRS Testing Reports/Interactive Messaging/RTRS Web Testing/Participant Trades Report" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/reports/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/default2.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/participant.asp?pop=yes")
        setFieldValue "input[name='ebs']" "EBS"
        click "        Submit        "
        assertFieldContains "td" "No records located matching your search criteria"  
 
    "View TRA Reports/RTRS Testing Reports/Interactive Messaging/RTRS Web Testing/Correspondent Trades Report" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/reports/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/default2.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/correspondent.asp?pop=yes")
        setFieldValue "input[name='ebs']" "EBS"
        click "        Submit        "
        assertFieldContains "td" "No records located matching your search criteria"  
 
    "View TRA Reports/RTRS Testing Reports/Interactive Messaging/RTRS Web Testing/As Appropriate Trades Report" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/reports/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/default2.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/asappropriatetest.asp?pop=yes")
        setFieldValue "input[name='ebs']" "EBS"
        click "        Submit        "
        assertFieldContains "td" "No records located matching your search criteria"
 
    "View TRA Reports/RTRS Testing Reports/Ad-Hoc Queries/General Query" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/reports/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/default3.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/adhoc.asp?pop=yes")
        setFieldValue "input[name='ebs']" "EBS"
        click "        Submit        "
        assertFieldContains "td" "No records located matching your search criteria"
 
    "View TRA Reports/RTRS Testing Reports/Ad-Hoc Queries/Late Query" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/reports/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/default3.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/testing/adhoclate.asp?pop=yes")
        setFieldValue "input[name='ebs']" "EBS"
        click "        Submit        "
        assertFieldContains "td" "No records located matching your search criteria"     

    "View TRA Reports/RTRSWeb Login Statistics/RTRSWeb Login Summary Statistics" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/reports/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/logon_stats/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/logon_stats/summary.asp")
        setFieldValue "#startdate" "01/15/2014"
        setFieldValue "#enddate" "01/15/2014"
        click "      Go      "
        assertFieldContains "form[name='form2']" "  Page:"

    "View TRA Reports/RTRSWeb Login Statistics/RTRSWeb Login Statistics by Firm by Day" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/reports/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/logon_stats/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/logon_stats/logons_by_firm.asp")
        setFieldValue "#startdate" "01/15/2014"
        setFieldValue "#enddate" "01/15/2014"
        click "      Go      "
        assertFieldContains "td" "No Statistics In This Date Range"    

    "View TRA Reports/RTRSWeb Login Statistics/RTRSWeb Login Statistics by Day/Time" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/reports/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/logon_stats/default.asp")
        url (gatewayUrl + "msrb1/control/tra/reports/logon_stats/logons_by_day_time.asp")
        setFieldValue "#startdate" "01/15/2014"
        setFieldValue "#enddate" "01/15/2014"
        click "      Go      "
        assertFieldContains "td" "RTRSWeb Logons by Firm by Day"

    "Dealer Outage Reporting" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/send.asp?target=SYSOUTAGEUSER&auth=1")
        assertFieldContains "h3" "All Reported Outages In Past 31 Days"  
    
    "(ARCHIVE) View Checklists from Processed Form RTRS Submissions - Approved by Manager" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/rtrsforms/checklistreview.asp")
        assertFieldContains "td" "Checklists from Processed Forms - Approved by Manager"      

    "(ARCHIVE) View Checklists from Processed Form RTRS Submissions - Processed Forms with Checklists Removed - Rejected by TRA or Approved by Manager" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/rtrsforms/formonlyreview.asp")
        assertFieldContains "td" "Processed Forms with Checklists Removed - Rejected by TRA or Approved by Manager"   
        
    "(ARCHIVE) View Checklists from Processed Form RTRS Submissions - Processed Forms - Rejected by TRA or Approved by Manager" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/rtrsforms/comprehensivereview.asp")
        assertFieldContains "td" "Processed Forms - Rejected by TRA or Approved by Manager"                                                    

    "Migrate User Accounts" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/migrate/findusers.asp  ")
        setFieldValue "#msrbid" "MSRB"
        click "Locate Accounts"
        assertFieldContains "td" "The following firms match your search criteria"   

    "Connect to RTRS Web As Another User" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        url (gatewayUrl + "msrb1/control/tra/rtrswebmgt/finduser.asp")
        setFieldValue "#userid" "scook"
        click "Locate Account"
        assertFieldContains "td" "Accounts matching your search criteria"   
    
    "Data Quality Dashboard" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "Market Transparency Services"
        click "Data Quality Dashboard"
        assertFieldContains "div.page" "Data Quality Assurance" 
        assertNotDisplayed "Login Failed"             
