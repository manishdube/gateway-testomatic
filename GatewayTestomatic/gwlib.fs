module gwlib
open System
open canopy
open canopy.configuration
open etconfig

let login (userId : string) (password: string) =
    try
        url (gatewayUrl + "msrb1/control/default.asp")
        "#UserName" << userId
        "#UPass" << password
        click "#LoginButton"
        on (gatewayUrl + "msrb1/control/selection.asp")
    with
    | _ ->
        reporter.write "loginGateway failed"

let logout () =
    try
        sleep 15
        url (gatewayUrl + "msrb1/control/default.asp")
        displayed "#UserName"
    with
    | _ ->
        reporter.write "logoutGateway failed"

let loginStaff () = login config.staff.userId config.staff.password

let loginetester22 () = login config.etester22.userId config.etester22.password

let loginGateway () = login config.userId config.password

let logoutGateway () = logout ()

let loginConfIssuer () = login config.confIssuer.userId config.confIssuer.password

let loginUnConfIssuer () = login config.unconfIssuer.userId config.unconfIssuer.password

let loginFormalIssuer () = login config.formalIssuer.userId config.formalIssuer.password

let loginDealerMAA () = login config.dealerMAA.userId config.dealerMAA.password

let loginDealer () = login config.dealer.userId config.dealer.password

let logoutDealer () = logout ()

let loginRegulator () = login config.regulator.userId config.regulator.password

let loginMuniAdvisor () = login config.muniAdvisor.userId config.muniAdvisor.password
