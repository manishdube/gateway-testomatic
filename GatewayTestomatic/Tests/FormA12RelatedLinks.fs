module FormA12RelatedLinks
open canopy
open runner
open etconfig
open lib
open gwlib
open System

let core _ = 
    context "Form A-12 Related Links"

    "A12/GuidelinesResource" &&& fun _ ->
        objective "Verify A-12 Related Links are active and populated with content"
        url (gatewayUrl + "msrb1/control/registration/")
        click "input[id='registrationTypeDealers']"
        click "#nextButton"
        click "#Continue"    
        click "Registration Guidelines"
        locateWindowWithTitleText "GuidelinesResource"
        assertUrl (gatewayUrl + "RegistrationManager/A12/GuidelinesResource")
        safeCloseWindow()
        locateWindowWithTitleText "Form A-12"
        
    "MSRB Registration-Manual" &&& fun _ ->
        objective "Verify A-12 Related Links are active and populated with content"
        url (gatewayUrl + "msrb1/control/registration/")
        click "input[id='registrationTypeDealers']"
        click "#nextButton"
        click "#Continue"
        click "MSRB Registration Manual"
        locateWindowWithTitleText "Registration-Manual"
        safeCloseWindow()
        locateWindowWithTitleText "Form A-12"

    "MSRB Gateway User Manual" &&& fun _ ->
        objective "Verify A-12 Related Links are active and populated with content"
        url (gatewayUrl + "msrb1/control/registration/")
        click "input[id='registrationTypeDealers']"
        click "#nextButton"
        click "#Continue"
        click "Gateway Manual"
        locateWindowWithTitleText "MSRBGatewayUserManual"
        safeCloseWindow()
        locateWindowWithTitleText "Form A-12"

    "Rules-and-Interpretations/MSRB-Rules/Administrative/Rule-A-12" &&& fun _ ->
        objective "Verify A-12 Related Links are active and populated with content"
        url (gatewayUrl + "msrb1/control/registration/")
        click "input[id='registrationTypeDealers']"
        click "#nextButton"
        click "#Continue"
        click "MSRB Rule A-12" 
        locateWindowWithTitleText "MSRB Rule A-12"
        assertUrl (msrbOrgUrl + "Rules-and-Interpretations/MSRB-Rules/Administrative/Rule-A-12.aspx")
        safeCloseWindow()
        locateWindowWithTitleText "Form A-12"