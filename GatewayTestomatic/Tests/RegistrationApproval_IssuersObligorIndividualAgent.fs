module RegistrationApproval_IssuersObligorIndividualAgent
open canopy
open runner
open etconfig
open lib
open gwlib
open System
open warmup

let mutable lastMsrbId = ""
let mutable lastTaxIDPrefix = ""
let mutable lastTaxIDSuffix = ""

let core _ = 
    context "Registration And Approval Issuers, Obligor, Individual and Agent"
//    once (fun _ -> warmupMsrbOrg ())

    //Formal Issuer - Registration and Approval--------------------------------------------------------------------
    "Formal Issuer - Registration" &&& fun _ ->
        objective "A Formal Issuer completes the registration process including the upload of necessary documents"
        postcondition "Formal Issuer receives and MSRBID at the conclusion of the registration process"
        url (gatewayUrl + "msrb1/control/registration/default.asp")
        click "#registrationTypeFormal"
        click "#nextButton"
        click "#TOCContinue"
        click "#bSubmit"
        click "#accountDoesNotExist"
        click "#btnOk"
        setFieldValue "#name" "Formal Issuer Organization Name"
        setFieldValue "#phoneAreaCode" "111"
        setFieldValue "#phonePrefix" "111"
        setFieldValue "#phoneSuffix" "1111"
        setFieldValue "#address1" "Address 1"
        setFieldValue "#city" "City"
        setFieldValue "#state" "VA"
        setFieldValue "#zip" "11111"
        click "#btnSubmit"
        setFieldValue "#PCFName" "First Name"
        setFieldValue "#PCLName" "Last Name"
        setFieldValue "#PCEmail" "Email@email.com"
        setFieldValue "#PCEmail2" "Email@email.com"
        setFieldValue "#PCPhoneAreaCode" "111"
        setFieldValue "#PCPhonePrefix" "111"
        setFieldValue "#PCPhoneSuffix" "1111"
        setFieldValue "#PCAddress1" "Address 1"
        setFieldValue "#PCCity" "City"
        setFieldValue "#PCState" "VA"
        setFieldValue "#PCZip" "11111"
        click "#btnSubmitUser"
        click "#orgroleIssuer"
        setFieldValue "#taxIDPrefix" (getRandomNumberString 2)
        setFieldValue "#taxIDSuffix" (getRandomNumberString 7)
        lastTaxIDPrefix <- read "#taxIDPrefix"
        Console.WriteLine("Tax ID Prefix = " + lastTaxIDPrefix)
        lastTaxIDSuffix <- read "#taxIDSuffix"
        Console.WriteLine("Tax ID Suffix = " + lastTaxIDSuffix)
        click "#btnSubmitOrgRole"
        fileUploadSelectPdf "#letterhead"
        fileUploadSelectPdf "#otherForm"
        click "#btnSubmitDoc"
        click "#btnConfirm"
        setFieldValue "#fname" "First Name"
        setFieldValue "#lname" "Last Name"
        setFieldValue "#title" "Title"
        setFieldValue "#areaCode" "111"
        setFieldValue "#phonePrefix" "111"
        setFieldValue "#phoneSuffix" "1111"
        click "#btnSubmitSignature"
        assertFieldContains "span.headingMain" "Your MSRB Number (MSRB ID) is"
        lastMsrbId <- read "span.headingMain u span"
        Console.WriteLine("msrb id = " + lastMsrbId)
        extendTimeout ( fun _ ->
            click "input[value='Exit']"
            waitFor ( fun _ ->
                someElement("[href='/EducationCenter.aspx']").IsSome
                )
            )
        assertDisplayed "[href='/EducationCenter.aspx']"

    "Staff Content - Organization Administration - Registration Formal Issuer - Approval" &&& fun _ ->
        objective "An MSRB staff approves a Formal Issuer registration"
        postcondition "Formal Issuer registration is approved"
        loginStaff ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "#organizationAdministration"
        click "#formalIssuerRegistration"
        setFieldValue "#role" "Issuer"
        setFieldValue "#search" lastMsrbId
        click "#SearchButton"
        let aId = "#" + lastMsrbId + "0"
        click aId
        click ("a[href=\"DocumentProcessing.asp?msrbid=" + lastMsrbId + "&id=1\"]")
        click "Approve"
        click "#prevPage"
        click ("a[href=\"DocumentProcessing.asp?msrbid=" + lastMsrbId + "&id=2\"]")
        click "Approve"
        click "#prevPage"
        assertFieldValue "#authorizationAction" "Authorize"
        click "#authorizationAction"
        acceptAlert ()
        assertFieldValue "#authorizationStatus u span" "Authorized"
        logoutGateway ()

    //Formal Obligor - Registration and Approval--------------------------------------------------------------------
    "Formal Obligor - Registration" &&& fun _ ->
        objective "A Formal Obligor completes the MSRB Online Registration and Organization Account Set-up process including the upload of necessary documents"
        postcondition "Formal Obligor receives and MSRBID at the conclusion of the registration process"
        url (gatewayUrl + "msrb1/control/registration/default.asp")
        click "#registrationTypeFormal"
        click "#nextButton"
        click "#TOCContinue"
        click "#bSubmit"
        click "#accountDoesNotExist"
        click "#btnOk"
        setFieldValue "#name" "Formal Obligor Organization Name"
        setFieldValue "#phoneAreaCode" "111"
        setFieldValue "#phonePrefix" "111"
        setFieldValue "#phoneSuffix" "1111"
        setFieldValue "#address1" "Address 1"
        setFieldValue "#city" "City"
        setFieldValue "#state" "VA"
        setFieldValue "#zip" "11111"
        click "#btnSubmit"
        setFieldValue "#PCFName" "First Name"
        setFieldValue "#PCLName" "Last Name"
        setFieldValue "#PCEmail" "Email@email.com"
        setFieldValue "#PCEmail2" "Email@email.com"
        setFieldValue "#PCPhoneAreaCode" "111"
        setFieldValue "#PCPhonePrefix" "111"
        setFieldValue "#PCPhoneSuffix" "1111"
        setFieldValue "#PCAddress1" "Address 1"
        setFieldValue "#PCCity" "City"
        setFieldValue "#PCState" "VA"
        setFieldValue "#PCZip" "11111"
        click "#btnSubmitUser"
        click "#orgroleObligor"
        setFieldValue "#taxIDPrefix" (getRandomNumberString 2)
        setFieldValue "#taxIDSuffix" (getRandomNumberString 7)
        lastTaxIDPrefix <- read "#taxIDPrefix"
        Console.WriteLine("Tax ID Prefix = " + lastTaxIDPrefix)
        lastTaxIDSuffix <- read "#taxIDSuffix"
        Console.WriteLine("Tax ID Suffix = " + lastTaxIDSuffix)
        click "#btnSubmitOrgRole"
        fileUploadSelectPdf "#letterhead"
        fileUploadSelectPdf "#otherForm"
        click "#btnSubmitDoc"
        click "#btnConfirm"
        setFieldValue "#fname" "First Name"
        setFieldValue "#lname" "Last Name"
        setFieldValue "#title" "Title"
        setFieldValue "#areaCode" "111"
        setFieldValue "#phonePrefix" "111"
        setFieldValue "#phoneSuffix" "1111"
        click "#btnSubmitSignature"
        assertFieldContains "span.headingMain" "Your MSRB Number (MSRB ID) is"
        lastMsrbId <- read "span.headingMain u span"
        Console.WriteLine("msrb id = " + lastMsrbId)
        click "input.button[value='Exit']"

    "Staff Content - Organization Administration - Registration Formal Obligor - Approval" &&& fun _ ->
        objective "An MSRB staff approves a Formal Obligor registration"
        postcondition "Formal Obligor registration is approved"
        loginStaff ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "#organizationAdministration"
        click "#formalIssuerRegistration"
        setFieldValue "#role" "Obligor"
        setFieldValue "#search" lastMsrbId
        click "#SearchButton"
        let aId = "#" + lastMsrbId + "0"
        click aId
        click ("a[href=\"DocumentProcessing.asp?msrbid=" + lastMsrbId + "&id=1\"]")
        click "Approve"
        click "#prevPage"
        click ("a[href=\"DocumentProcessing.asp?msrbid=" + lastMsrbId + "&id=2\"]")
        click "Approve"
        click "#prevPage"
        assertFieldValue "#authorizationAction" "Authorize"
        click "#authorizationAction"
        acceptAlert ()
        assertFieldValue "#authorizationStatus u span" "Authorized"
        logoutGateway ()


    //Individual User Account - Registration and Approval--------------------------------------------------------------------
    "Individual User Account - Registration" &&& fun _ ->
        objective "A Individual User completes the MSRB Online Registration and Organization Account Set-up process"
        postcondition "Individual User receives and MSRBID at the conclusion of the registration process"
        url (gatewayUrl + "msrb1/control/registration/default.asp")
        click "#registrationTypeInformal"
        click "#nextButton"
        let emailAddr = ("email" + (getRandomNumberString 4) + "@email.com")
        setFieldValue "input[name='email']" emailAddr
        click "input.button_main"
        setFieldValue "#fName" "First Name"
        setFieldValue "#lName" "Last Name"
        setFieldValue "#email" emailAddr
        setFieldValue "#email2" emailAddr
        setFieldValue "#title" "Title"
        setFieldValue "#department" "Department"
        setFieldValue "#phoneAreaCode" "111"
        setFieldValue "#phonePrefix" "111"
        setFieldValue "#phoneSuffix" "1111"
        setFieldValue "#faxAreaCode" "111"
        setFieldValue "#faxPrefix" "111"
        setFieldValue "#faxSuffix" "1111"
        setFieldValue "#address1" "Address 1"
        setFieldValue "#address2" "Address 2"
        setFieldValue "#city" "City"
        setFieldValue "#state" "VA"
        setFieldValue "#zip" "11111"
        setFieldValue "#country" "US"
        click "input.button_main"
        setFieldValue "#name" "Organization Name"
        setFieldValue "#phoneAreaCode" "111"
        setFieldValue "#phonePrefix" "111"
        setFieldValue "#phoneSuffix" "1111"
        setFieldValue "#extension" "111"
        setFieldValue "#faxAreaCode" "111"
        setFieldValue "#faxPrefix" "111"
        setFieldValue "#faxSuffix" "1111"
        setFieldValue "#address1" "Address 1"
        setFieldValue "#address2" "Address 2"
        setFieldValue "#city" "City"
        setFieldValue "#state" "VA"
        setFieldValue "#zip" "11111"
        setFieldValue "#country" "US"
        click "input.button_main"
        click "input[name=SMAGENT]"
        click "input[name=OBLIGOR]"
        click "input[name=ISSUER]"
        click "input.button_main"
        click "input.button_main"
        click "input.button_main"
        lastMsrbId <- read "td.leftArea tr:nth-of-type(2) td:nth-of-type(2)"
        Console.WriteLine("msrb id = " + lastMsrbId)
        assertFieldContains "span.heading" "Continuing Disclosure Registration - Account Created"

    "Staff Content - Organization Administration - Registration Informal - Individual User Account - Approval" &&& fun _ ->
        objective "An MSRB staff approves a Individual User registration"
        postcondition "Individual User registration is approved"
        loginStaff ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "#organizationAdministration"
        click "#organizationLookup"
        setFieldValue "#search" lastMsrbId
        click "Search"
        assertFieldContains "#OrgLookupResults tbody tr:nth-of-type(1) td:nth-of-type(2)" "Issuer"
        assertFieldContains "#OrgLookupResults tbody tr:nth-of-type(2) td:nth-of-type(2)" "Obligor"
        assertFieldContains "#OrgLookupResults tbody tr:nth-of-type(3) td:nth-of-type(2)" "Secondary Market Agent"
        assertFieldContains "#OrgLookupResults tbody tr:nth-of-type(1) td:nth-of-type(4)" "Authorized"
        assertFieldContains "#OrgLookupResults tbody tr:nth-of-type(2) td:nth-of-type(4)" "Authorized"
        assertFieldContains "#OrgLookupResults tbody tr:nth-of-type(3) td:nth-of-type(4)" "Authorized"
        assertFieldContains "#OrgLookupResults tbody tr:nth-of-type(1) td:nth-of-type(6)" "Active"
        assertFieldContains "#OrgLookupResults tbody tr:nth-of-type(2) td:nth-of-type(6)" "Active"
        assertFieldContains "#OrgLookupResults tbody tr:nth-of-type(3) td:nth-of-type(6)" "Active"
        logoutGateway ()


    //Agent - Registration and Approval--------------------------------------------------------------------
    "Agent - Registration" &&& fun _ ->
        objective "An Agent completes the MSRB Online Registration and Organization Account Set-up process"
        postcondition "Agent receives and MSRBID at the conclusion of the registration process"
        url (gatewayUrl + "msrb1/control/registration/default.asp")
        click "#registrationTypeAgent"
        click "#nextButton"
        click "Continue"
        click "Accept"
        setFieldValue "#Org_OrganizationName" "AGENT Organization Name"
        setFieldValue "#Org_PhoneAreaCode" "111"
        setFieldValue "#Org_PhonePrefix" "111"
        setFieldValue "#Org_PhoneSuffix" "1111"
        setFieldValue "#Org_PhoneExtension" "111"
        setFieldValue "#Org_FaxAreaCode" "111"
        setFieldValue "#Org_FaxPrefix" "111"
        setFieldValue "#Org_FaxSuffix" "1111"
        setFieldValue "#Org_Address1" "Address 1"
        setFieldValue "#Org_Address2" "Address 2"
        setFieldValue "#Org_City" "City"
        setFieldValue "#Org_State" "VA"
        setFieldValue "#Org_Zip" "11111"
        setFieldValue "#Org_Country" "US"
        click "OK"
        setFieldValue "#MAA_FirstName" "MAAFirstName"
        setFieldValue "#MAA_LastName" "Name"
        let emailAddr = ("email" + (getRandomNumberString 4) + "@email.com")
        setFieldValue "#MAA_EmailAddress" emailAddr
        setFieldValue "#MAA_ConfirmEmailAddress" emailAddr
        setFieldValue "#MAA_PhoneAreaCode" "111"
        setFieldValue "#MAA_PhonePrefix" "111"
        setFieldValue "#MAA_PhoneSuffix" "1111"
        setFieldValue "#MAA_PhoneExtension" "111"
        setFieldValue "#MAA_Address1" "Address 1"
        setFieldValue "#MAA_Address2" "Address 2"
        setFieldValue "#MAA_City" "City"
        setFieldValue "#MAA_State" "VA"
        setFieldValue "#MAA_Zip" "11111"
        setFieldValue "#MAA_Country" "US"
        click "OK"
        setFieldValue "#TaxIdPrefix" (getRandomNumberString 2)
        setFieldValue "#TaxIdSuffix" (getRandomNumberString 7)
        click "OK"
        sleep 1.0
        fileUploadSelectPdf "#Files_uploadLetterheadControl"
        sleep 1.0
        fileUploadSelectPdf "#Files_uploadSecondaryFormControl"
        click "OK"
        click "Confirm"
        setFieldValue "#Submitter_FirstName" "SubmitterFirstName"
        setFieldValue "#Submitter_MiddleName" "MiddleName"
        setFieldValue "#Submitter_LastName" "SubmitterLastName"
        setFieldValue "#Submitter_Title" "Title"
        setFieldValue "#Submitter_PhoneAreaCode" "111"
        setFieldValue "#Submitter_PhonePrefix" "111"
        setFieldValue "#Submitter_PhoneSuffix" "1111"
        setFieldValue "#Submitter_PhoneExtension" "111"
        click "OK"
        assertFieldContains "fieldset.winfo" "Your MSRB Number (MSRB ID) is"
        lastMsrbId <- read "fieldset.winfo b"
        Console.WriteLine("msrb id = " + lastMsrbId)
        click "input[value='Exit']"

    "Staff Content - Organization Administration - Agent Registration - Approval" &&& fun _ ->
        objective "An MSRB staff approves an Agent registration"
        postcondition "Agent registration is approved"
        loginStaff ()
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#stafftog"
        click "#organizationAdministration"
        click "#formalIssuerRegistration"
        setFieldValue "#role" "Agent"
        setFieldValue "#search" lastMsrbId
        click "#SearchButton"
        let aId = "#" + lastMsrbId + "0"
        click aId
        click ("a[href=\"DocumentProcessing.asp?msrbid=" + lastMsrbId + "&id=1\"]")
        click "Approve"
        click "#prevPage"
        click ("a[href=\"DocumentProcessing.asp?msrbid=" + lastMsrbId + "&id=2\"]")
        click "Approve"
        click "#prevPage"
        assertFieldValue "#authorizationAction" "Authorize"
        click "#authorizationAction"
        acceptAlert ()
        assertFieldValue "#authorizationStatus u span" "Authorized"
        logoutGateway ()



   