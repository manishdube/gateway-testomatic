module RegistrantInformationForms
open canopy
open runner
open etconfig   
open lib
open gwlib
open System

let core _ = 
    context "Registrant Information Forms"

    "Form A-12 - Proceed to Form A-12 (Confirmed Issuer)" &&& fun _ ->
        loginConfIssuer ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Form A-12"
        assertFieldContains "h2" "MSRB Registration: Form A-12"
        click "Proceed to Form A-12"
        assertUrl (gatewayUrl + "msrb1/control/default.asp?login=expired")

    "Form A-12 - View Current Form A-12 Information (Confirmed Issuer)" &&& fun _ ->
        loginConfIssuer ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Form A-12"
        sleep 0.1
        assertFieldContains  "h2" "MSRB Registration: Form A-12"
        click "View Current Form A-12 Information"
        sleep 0.1
        assertUrl (gatewayUrl + "msrb1/control/default.asp?login=expired")

    "Form A-12 - Withdraw or Add Registration Category (Confirmed Issuer)" &&& fun _ ->
        loginConfIssuer ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Form A-12"
        assertFieldContains "h2" "MSRB Registration: Form A-12"
        click "#OrgRolePopup"
        click "Continue"
        click "Cancel"
        assertFieldContains "h2" "MSRB Registration: Form A-12"

    "Form A-12 - Proceed to Form A-12 (DealerMAA)" &&& fun _ ->
        loginDealerMAA ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Form A-12"
        click "Proceed to Form A-12"
        assertUrl (gatewayUrl + "msrb1/control/default.asp?login=expired")

    "Form A-12 - View Current Form A-12 Information (DealerMAA)" &&& fun _ ->
        loginDealerMAA ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Form A-12"
        click "View Current Form A-12 Information"
        assertUrl (gatewayUrl + "msrb1/control/default.asp?login=expired")

    "Form A-12 - Withdraw or Add Registration Category (DealerMAA)" &&& fun _ ->
        loginDealerMAA ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "Form A-12"
        click "#OrgRolePopup" // Current Registration: Withdraw or Add Registration Category 
        click "Continue"
        click "Cancel"
        assertFieldContains "h2" "MSRB Registration: Form A-12"

    "Form A-12 - Dealer is not allowed this function (Dealer)" &&& fun _ ->
        loginDealer ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        assertNotDisplayed "Form A-12"

    "Edit or Affirm Your Organization's Information and Master Account Administrator Designation (FormalIssuer)" &&& fun _ ->
        loginFormalIssuer ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#regformstog"
        click "a[href='/msrb1/control/registration/formalioa/internallanding.asp']"
        assertFieldContains "span.headingMain" "MSRB Organization Account Set-up - Organization Summary"
        click "Confirm"
        assertFieldContains "span.headingMain" "Congratulations, you have successfully updated your account information with the MSRB."
