module Reports_RQS_Disclosure
open canopy
open runner
open etconfig   
open lib
open gwlib
open System

let core _ = 
    context "Regulatory Query System Reports - Disclosures"

    if config.regWebV2 = true then
        "Primary Market Disclosures (Q20) - By Underwriter" &&&    fun _ ->
            loginRegulator ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "a[href='/msrb1/control/send.asp?target=REGWEB&auth=1']"
            click "a[href='/Regulator/RegWeb/RedirectToQueryReports']"
            click "Disclosure"
            click "a[href='/QueryReports/EMMA/Q20Underwriter']"    
            setFieldValue "#BeginDate" "01/01/2010"
            setFieldValue "#EndDate" "06/01/2010"
            setFieldValue "#MsrbId" "A0278"
            click "Submit"
            let t1 () =
                sleep 2.0
                someElement("Regulator Query System — Request Submitted").IsSome
            extendTimeout (fun _ -> waitFor t1)
            assertDisplayed "Regulator Query System — Request Submitted"

        "Primary Market Disclosures (Q20) - By Security CUSIP6 Number" &&& fun _ ->
            loginRegulator ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "a[href='/msrb1/control/send.asp?target=REGWEB&auth=1']"
            click "a[href='/Regulator/RegWeb/RedirectToQueryReports']"
            click "Disclosure"
            click "a[href='/QueryReports/EMMA/Q20Security']"  
            setFieldValue "#BeginDate" "01/01/2010"
            setFieldValue "#EndDate" "06/01/2010"
            setFieldValue "#Cusip6" "59259R"
            click "Submit"
            let t2 () =
                sleep 2.0
                someElement("Regulator Query System — Request Submitted").IsSome
            extendTimeout (fun _ -> waitFor t2)
            assertDisplayed "Regulator Query System — Request Submitted"

        "Primary Market Disclosures (Q20) - By Security CUSIP9 Number" &&& fun _ ->
            loginRegulator ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "a[href='/msrb1/control/send.asp?target=REGWEB&auth=1']"
            click "a[href='/Regulator/RegWeb/RedirectToQueryReports']"
            click "Disclosure"
            click "a[href='/QueryReports/EMMA/Q20Security']"  
            setFieldValue "#BeginDate" "01/01/2010"
            setFieldValue "#EndDate" "06/01/2010"
            setFieldValue "#Cusip9" "66272RAC6 66272RAD4"
            click "Submit"
            let t3 () =
                sleep 2.0
                someElement("Regulator Query System — Request Submitted").IsSome
            extendTimeout (fun _ -> waitFor t3)
            assertDisplayed "Regulator Query System — Request Submitted"

        "Continuing Disclosures (Q30) - By Obligor Name" &&&   fun _ ->
            loginRegulator ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "a[href='/msrb1/control/send.asp?target=REGWEB&auth=1']"
            click "a[href='/Regulator/RegWeb/RedirectToQueryReports']"
            click "Disclosure"  
            click "a[href='/QueryReports/EMMA/Q30Obligor']"
            setFieldValue "#BeginDate" "01/01/2010"
            setFieldValue "#EndDate" "06/01/2010"
            setFieldValue "#ObligorName" "HARBOR"
            click "Submit"
            let t4 () =
                sleep 2.0
                someElement("Regulator Query System — Request Submitted").IsSome
            extendTimeout (fun _ -> waitFor t4)
            assertDisplayed "Regulator Query System — Request Submitted"

        "Continuing Disclosures (Q30) - By Security CUSIP6 Number" &&& fun _ ->
            loginRegulator ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "a[href='/msrb1/control/send.asp?target=REGWEB&auth=1']"
            click "a[href='/Regulator/RegWeb/RedirectToQueryReports']"
            click "Disclosure"  
            click "a[href='/QueryReports/EMMA/Q30Securities']"
            setFieldValue "#BeginDate" "01/01/2010"
            setFieldValue "#EndDate" "06/01/2010"
            setFieldValue "#Cusip6" "59259R"
            click "Submit"
            let t5 () =
                sleep 2.0
                someElement("Regulator Query System — Request Submitted").IsSome
            extendTimeout (fun _ -> waitFor t5)
            assertDisplayed "Regulator Query System — Request Submitted"

        "Continuing Disclosures (Q30) - By Security CUSIP9 Number" &&& fun _ ->
            loginRegulator ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "a[href='/msrb1/control/send.asp?target=REGWEB&auth=1']"
            click "a[href='/Regulator/RegWeb/RedirectToQueryReports']"
            click "Disclosure"  
            click "a[href='/QueryReports/EMMA/Q30Securities']"
            setFieldValue "#BeginDate" "01/01/2010"
            setFieldValue "#EndDate" "06/01/2010"
            setFieldValue "#Cusip9" "66272RAC6 66272RAD4"
            click "Submit"
            let t6 () =
                sleep 2.0
                someElement("Regulator Query System — Request Submitted").IsSome
            extendTimeout (fun _ -> waitFor t6)
            assertDisplayed "Regulator Query System — Request Submitted"

        "Access Reports " &&& fun _ ->
            loginStaff ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "a[href='/msrb1/control/send.asp?target=REGWEB&auth=1']"
            click "a[href='/Regulator/RegWeb/RedirectToQueryReports']"
            click "Access Reports"  
            let t8 () =
                sleep 2.0
                someElement("Access Reports").IsSome
            extendTimeout (fun _ -> waitFor t8)    
            assertDisplayed "Access Reports"
        
                // Administration //
        "Staff - Administration Tab Function" &&& fun _ ->
            loginStaff ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "a[href='/msrb1/control/send.asp?target=REGWEB&auth=1']"
            click "a[href='/Regulator/RegWeb/RedirectToQueryReports']"
            click "Administration"
            let t7 () =
                sleep 2.0
                someElement("Query Reports System — View Request Queue").IsSome
            extendTimeout (fun _ -> waitFor t7)          
            assertDisplayed "Query Reports System — View Request Queue"


    if config.regWebV2 = false then
        "Primary Market Disclosures (Q20) - By Underwriter" &&&    fun _ ->
            loginRegulator ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "All Enforcement Agencies"
            url (gatewayUrl + "/msrb1/control/send.asp?target=REGULATOR&auth=1")
            click "Disclosure"
            click "a[href='/QueryReports/EMMA/Q20Underwriter']"    
            setFieldValue "#BeginDate" "01/01/2010"
            setFieldValue "#EndDate" "06/01/2010"
            setFieldValue "#MsrbId" "A0278"
            click "Submit"
            let t1 () =
                sleep 2.0
                someElement("Regulator Query System — Request Submitted").IsSome
            extendTimeout (fun _ -> waitFor t1)
            assertDisplayed "Regulator Query System — Request Submitted"

        "Primary Market Disclosures (Q20) - By Security CUSIP6 Number" &&& fun _ ->
            loginRegulator ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "All Enforcement Agencies"
            url (gatewayUrl + "/msrb1/control/send.asp?target=REGULATOR&auth=1")
            click "Disclosure"
            click "a[href='/QueryReports/EMMA/Q20Security']"  
            setFieldValue "#BeginDate" "01/01/2010"
            setFieldValue "#EndDate" "06/01/2010"
            setFieldValue "#Cusip6" "59259R"
            click "Submit"
            let t2 () =
                sleep 2.0
                someElement("Regulator Query System — Request Submitted").IsSome
            extendTimeout (fun _ -> waitFor t2)
            assertDisplayed "Regulator Query System — Request Submitted"

        "Primary Market Disclosures (Q20) - By Security CUSIP9 Number" &&& fun _ ->
            loginRegulator ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "All Enforcement Agencies"
            url (gatewayUrl + "/msrb1/control/send.asp?target=REGULATOR&auth=1")
            click "Disclosure"
            click "a[href='/QueryReports/EMMA/Q20Security']"  
            setFieldValue "#BeginDate" "01/01/2010"
            setFieldValue "#EndDate" "06/01/2010"
            setFieldValue "#Cusip9" "66272RAC6 66272RAD4"
            click "Submit"
            let t3 () =
                sleep 2.0
                someElement("Regulator Query System — Request Submitted").IsSome
            extendTimeout (fun _ -> waitFor t3)
            assertDisplayed "Regulator Query System — Request Submitted"

        "Continuing Disclosures (Q30) - By Obligor Name" &&&   fun _ ->
            loginRegulator ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "All Enforcement Agencies"
            url (gatewayUrl + "/msrb1/control/send.asp?target=REGULATOR&auth=1")
            click "Disclosure"  
            click "a[href='/QueryReports/EMMA/Q30Obligor']"
            setFieldValue "#BeginDate" "01/01/2010"
            setFieldValue "#EndDate" "06/01/2010"
            setFieldValue "#ObligorName" "HARBOR"
            click "Submit"
            let t4 () =
                sleep 2.0
                someElement("Regulator Query System — Request Submitted").IsSome
            extendTimeout (fun _ -> waitFor t4)
            assertDisplayed "Regulator Query System — Request Submitted"

        "Continuing Disclosures (Q30) - By Security CUSIP6 Number" &&& fun _ ->
            loginRegulator ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "All Enforcement Agencies"
            url (gatewayUrl + "/msrb1/control/send.asp?target=REGULATOR&auth=1")
            click "Disclosure"  
            click "a[href='/QueryReports/EMMA/Q30Securities']"
            setFieldValue "#BeginDate" "01/01/2010"
            setFieldValue "#EndDate" "06/01/2010"
            setFieldValue "#Cusip6" "59259R"
            click "Submit"
            let t5 () =
                sleep 2.0
                someElement("Regulator Query System — Request Submitted").IsSome
            extendTimeout (fun _ -> waitFor t5)
            assertDisplayed "Regulator Query System — Request Submitted"

        "Continuing Disclosures (Q30) - By Security CUSIP9 Number" &&& fun _ ->
            loginRegulator ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "All Enforcement Agencies"
            url (gatewayUrl + "/msrb1/control/send.asp?target=REGULATOR&auth=1")
            click "Disclosure"  
            click "a[href='/QueryReports/EMMA/Q30Securities']"
            setFieldValue "#BeginDate" "01/01/2010"
            setFieldValue "#EndDate" "06/01/2010"
            setFieldValue "#Cusip9" "66272RAC6 66272RAD4"
            click "Submit"
            let t6 () =
                sleep 2.0
                someElement("Regulator Query System — Request Submitted").IsSome
            extendTimeout (fun _ -> waitFor t6)
            assertDisplayed "Regulator Query System — Request Submitted"

        "Access Reports " &&& fun _ ->
            loginStaff ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "#regulatortog"
            click "All Enforcement Agencies"
            url (gatewayUrl + "/msrb1/control/send.asp?target=REGULATOR&auth=1")
            click "Access Reports"  
            let t8 () =
                sleep 2.0
                someElement("Access Reports").IsSome
            extendTimeout (fun _ -> waitFor t8)    
            assertDisplayed "Access Reports"
        
                // Administration //
        "Staff - Administration Tab Function" &&& fun _ ->
            loginStaff ()
            url (gatewayUrl + "msrb1/control/selection.asp")
            click "#regulatortog"
            click "All Enforcement Agencies"
            url (gatewayUrl + "/msrb1/control/send.asp?target=REGULATOR&auth=1")
            click "Administration"
            let t7 () =
                sleep 2.0
                someElement("Query Reports System — View Request Queue").IsSome
            extendTimeout (fun _ -> waitFor t7)          
            assertDisplayed "Query Reports System — View Request Queue"
   