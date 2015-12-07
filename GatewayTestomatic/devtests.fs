module devtests
open canopy
open runner
open etconfig
open lib
open gwlib
open System

let all _ = 
    context "dev tests"
    //once (fun _ -> loginGateway())

    wip ( fun _ ->
        assertStringIsAlphaNumeric "fred"
        assertStringIsAlphaNumeric "fred123"
        assertStringIsAlphaNumeric "fredZZZ111"
        assertStringIsAlphaNumeric "fred-d"
        //url (gatewayUrl + "msrb1/control/selection.asp")
    )
