module Administration_User
open canopy
open runner
open etconfig
open lib
open gwlib
open System

let core _ = 
    context "Administration - User Administration"
    once (fun _ -> loginGateway ())
    lastly (fun _ -> logoutGateway ())

    "Create new users" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "User Administration"
        click "Create new users"
        setFieldValue "#fname" "SCOOK_X"
        setFieldValue "#lname" "Last Name"
        setFieldValue "#email" "email@email.com"
        setFieldValue "#email2" "email@email.com"
        click "Create Account"  
        assertFieldContains "h1" "Account Creation"
        assertFieldContains "b" "At least some rights must be given."  

    "Maintain existing users - Master Account Administrator - Add / Remove Right" &&& fun _ ->
        objective "Master Account Administrator can add and remove rights for an existing user"
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "User Administration"
        click "Maintain existing users"
        setFieldValue "#userid" "GWSTAFF"
        click "Locate Account"
        assertFieldContains "h1" "Search Results"
        assertFieldContains "table tr:nth-of-type(2) td:nth-of-type(2)" "SHANE PARKER"
        assertFieldContains "h1" "Search Results"
        click "a[href='maintainuser.asp?userid=GWSTAFF']"
        assertFieldContains "h1" "Account Information"
        click "input[value='  Change Info.  ']"
        deselected "#TRASUPER"
        check "#TRASUPER"
        click "input[value='Update Account']"
        assertFieldContains "h1" "Supplemental Information"
        click "input[value='Update Account']"
        assertFieldContains "h1" "Account Confirmation"
        click "input[value='Confirm Account']"
        assertFieldContains "h1" "Account Maintenance Complete"
        click "input[value='Return to Search']"  
        setFieldValue "#userid" "GWSTAFF"
        click "Locate Account"
        assertFieldContains "h1" "Search Results"
        assertFieldContains "table tr:nth-of-type(2) td:nth-of-type(2)" "SHANE PARKER"
        assertFieldContains "h1" "Search Results"
        click "a[href='maintainuser.asp?userid=GWSTAFF']"
        assertFieldContains "h1" "Account Information"
        click "input[value='  Change Info.  ']"
        selected "#TRASUPER"
        uncheck "#TRASUPER"
        click "input[value='Update Account']"
        assertFieldContains "h1" "Supplemental Information"
        click "input[value='Update Account']"
        assertFieldContains "h1" "Account Confirmation"
        click "input[value='Confirm Account']"
        assertFieldContains "h1" "Account Maintenance Complete"

    "Maintain existing users - Master Account Administrator - Send User ID" &&& fun _ ->
        objective "Master Account Administrator send email a user the userID"
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "User Administration"
        click "Maintain existing users"
        setFieldValue "#msrbid" "A9995"
        click "Locate Account"
        assertFieldContains "h1" "Search Results"
        assertFieldContains "table tr:nth-of-type(2) td:nth-of-type(2)" "JENNIFER BRUND"
        assertFieldContains "h1" "Search Results"
        click "a[href='maintainuser.asp?userid=DEALER']"
        assertFieldContains "h1" "Account Information"
        click "input[value='  Send UserID   ']"
        assertFieldContains "h1" "Send UserID"
        assertFieldContains "td" "The userid has been sent to the address on record for this account along with instructions on how to obtain the matching password"

    "Manually publish x509 revocation lists" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "User Administration"
        click "Manually publish x509 revocation lists"
        assertFieldContains "h1" "Manual CRL Publication (Expires in 1 Year)"

    "Event Log " &&& fun _ ->
        objective "View the event log for a user"
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "User Administration"
        click "Event Log"
        setFieldValue "input[name='user_id']" "GWSTAFF"
        click "input[value='View Events']"  
        assertFieldContains "h1" "iLog Activity Log"

    