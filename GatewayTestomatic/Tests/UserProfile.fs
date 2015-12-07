module UserProfile
open canopy
open runner
open etconfig
open lib
open gwlib
open System

//let mutable lastMsrbId = ""

let core _ = 
    context "My Profile"
    once (fun _ -> loginDealer ())
    lastly (fun _ -> logoutGateway ())

    "Edit My Profile" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Edit"
        assertFieldContains "span.heading" "User Account Profile"
        click "Edit User Account"
        setFieldValue "#title" "Vice President of Importance"
        click "Continue"
        assertDisplayed "Confirm User Account"
        click "Confirm User Account"
        assertFieldContains "span.heading" "User Account Update Results"
        click "Return to Main Menu"

    "Change Password - Twice" &&& fun _ ->
        objective "User can change and re-change password in the same session, then logout and log back in."
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Change Password"
        assertFieldContains "h1" "Change Password"
        setFieldValue "input[name='password']" "gw*loginMSRB1"
        setFieldValue "input[name=newpwd1]" "Password1"
        setFieldValue "input[name=newpwd2]" "Password1"
        click "Continue"
        assertFieldContains "h1" "Transaction Confirmation"
        click "input[value='   Return   ']"    
        click "Change Password"
        assertFieldContains "h1" "Change Password"
        setFieldValue "input[name='password']" "Password1"
        setFieldValue "input[name=newpwd1]" "gw*loginMSRB1"
        setFieldValue "input[name=newpwd2]" "gw*loginMSRB1"
        click "Continue"
        assertFieldContains "h1" "Transaction Confirmation"
        click "input[value='   Return   ']"  
        logoutGateway ()
        loginDealer ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        assertFieldContains "h1" "MSRB Gateway Main Menu"




