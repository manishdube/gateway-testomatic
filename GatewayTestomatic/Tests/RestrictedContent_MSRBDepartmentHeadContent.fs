module RestrictedContent_MSRBDepartmentHeadContent
open canopy
open runner
open etconfig
open lib
open gwlib
open System

let core _ = 
    context "RestrictedContent - MSRB Department Head Content"
    once (fun _ -> loginGateway ())
    lastly (fun _ -> logoutGateway ())

    "Market Emergency Contact Lists - SEC Contacts" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "MSRB Department Head Content" 
        assertFieldContains "h1" "MSRB Department Head Restricted Content"
        click "a[href='/msrb1/control/MSRBDeptHeads/mecontact/default.asp']"            //Market Emergency Contact Lists
        assertFieldContains "h1" "MSRB Department Heads - Market Emergency Contacts"
        click "a[href='ContactsSECMarketReg.asp']"                //SEC Contacts
        assertUrl (gatewayUrl + "msrb1/control/MSRBDeptHeads/mecontact/ContactsSECMarketReg.asp")
        assertFieldContains "h1" "Market Regulation" 

    "Market Emergency Contact Lists - Clearing Contacts" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "MSRB Department Head Content" 
        assertFieldContains "h1" "MSRB Department Head Restricted Content" 
        click "a[href='/msrb1/control/MSRBDeptHeads/mecontact/default.asp']"            //Market Emergency Contact Lists
        assertFieldContains "h1" "MSRB Department Heads - Market Emergency Contacts"
        click "a[href='ContactsClearing.asp']"                //Clearing Contacts
        assertUrl (gatewayUrl + "msrb1/control/MSRBDeptHeads/mecontact/ContactsClearing.asp")
        assertFieldContains "h1" "Clearing"

    "Market Emergency Contact Lists - Market and Related Contacts" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#restrictedtog" 
        click "MSRB Department Head Content" 
        assertFieldContains "h1" "MSRB Department Head Restricted Content"
        click "a[href='/msrb1/control/MSRBDeptHeads/mecontact/default.asp']"            //Market Emergency Contact Lists
        assertFieldContains "h1" "MSRB Department Heads - Market Emergency Contacts" 
        click "a[href='ContactsMarkets.asp']"                //Market and Related Contacts
        assertUrl (gatewayUrl + "msrb1/control/MSRBDeptHeads/mecontact/ContactsMarkets.asp")
        assertFieldContains "h1" "HEADS OF MARKETS & RELATED ORGANIZATIONS"
         