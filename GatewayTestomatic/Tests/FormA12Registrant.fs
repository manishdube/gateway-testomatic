module FormA12Registrant
open canopy
open runner
open etconfig
open lib
open gwlib
open System
open TestomaticSupport

let core _ = 
    context "FORM-A12 Registrant Form Checks"
    once (fun _ ->  
        logoutGateway ()
        loginMuniAdvisor ()
        )
    lastly (fun _ -> logoutGateway ())

    "A12 Contact Information - Approved Registration - ReadOnly - Contact FirstName, MiddleName, LastName, Suffix" &&& fun _ ->
        click "#formstog"
        click "Form A-12"
        url (gatewayUrl + "RegistrationManager/A12/Overview")
        assertUrl (gatewayUrl + "RegistrationManager/A12/Overview")
        click "a[href='/RegistrationManager/A12/GeneralInformation']"
        assertDisplayed "MSRB Registration-Form A-12: General Firm Information"
        click "Contact Information"
        assertDisplayed "MSRB Registration-Form A-12: Contact Information"
        click "Details"
        assertFieldIsReadOnly "#ContactsModel_EditContact_FirstName"
        assertFieldIsReadOnly "#ContactsModel_EditContact_MiddleName"
        assertFieldIsReadOnly "#ContactsModel_EditContact_LastName"
        assertFieldIsReadOnly "#ContactsModel_EditContact_NameSuffix"



