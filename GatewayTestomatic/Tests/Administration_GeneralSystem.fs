module Administration_GeneralSystem
open canopy
open runner
open etconfig
open lib
open gwlib
open System

let core _ = 
    context "Administration - General System Administration"
    once (fun _ -> loginGateway ())
    lastly (fun _ -> logoutGateway ())

    "Modify System Parameters - Login Message" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "General System Administration"
        click "Modify System Parameters"
        click "Login Message"
        assertFieldContains "h1" "General Site Maintenance"
            
    "Modify System Parameters - Application URLs" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "General System Administration"
        click "Modify System Parameters"
        click "Application URLs"
        assertFieldContains "h1" "General Site Maintenance"

    "Modify System Parameters - Application Lockout Settings" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "General System Administration"
        click "Modify System Parameters"
        click "Application Lockout Settings"
        assertFieldContains "h1" "General Site Maintenance"     

    "Modify System Parameters - Application Group Settings" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "General System Administration"
        click "Modify System Parameters"
        click "Application Group Settings"
        assertFieldContains "h1" "General Site Maintenance"               

    "Modify System Parameters - Account Lockout Settings" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "General System Administration"
        click "Modify System Parameters"
        click "Account Lockout Settings"
        assertFieldContains "h1" "General Site Maintenance"   

    "Modify System Parameters - Hot Site iLog Database Blackout Window" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "General System Administration"
        click "Modify System Parameters"
        click "Hot Site iLog Database Blackout Window"
        assertFieldContains "h1" "iLog Hot Site Blackout Window Maintenance"    

    "Upload iLog Business Holidays from MSILDex" &&! fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "General System Administration"
        click "Upload iLog Business Holidays from MSILDex"
        assertFieldContains "h1" "iLog Business Holidays"  

    "Reports" &&& fun _ ->
        url (gatewayUrl + "msrb1/control/selection.asp")
        click "#admintog"
        click "General System Administration"
        click "Reports"
        click "MSRBNet Page Stats"
        assertFieldContains "h1" "Restricted MSRBNet Page View Statistics Query"  
