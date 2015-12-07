module commandlineargs

type Browser =
    | Firefox
    | Chrome
    | IE

type CommandLineArgs () = class
    member val isValid : bool = true with get, set
    member val browser : Browser = Chrome with get, set
    member val warmUp : bool = true with get, set
    member val log : bool = true with get, set
    member val teamCity : bool = false with get, set
    member val sqlInit : bool = false with get, set
    member val devMode : bool = false with get, set
    member val pressEnter : bool = false with get, set
    member val versionOnly : bool = false with get, set
    member val showUsage : bool = false with get, set
    member val coreOnly : bool = false with get, set
end

let parseCommandLine (args: string[]) =
    let cla = new CommandLineArgs()
    for item in args do
        match item.ToLower() with
            | "-firefox" -> cla.browser <- Firefox
            | "-chrome" -> cla.browser <- Chrome
            | "-ie" -> cla.browser <- IE
            | "-warmup" -> cla.warmUp <- true
            | "-nowarmup" -> cla.warmUp <- false
            | "-log" -> cla.log <- true
            | "-nolog" -> cla.log <- false
            | "-teamcity" -> cla.teamCity <- true
            | "-sqlinit" -> cla.sqlInit <- true
            | "-dev" -> cla.devMode <- true
            | "-pressenter" -> cla.pressEnter <- true
            | "-version" -> cla.versionOnly <- true
            | "-coreonly" -> cla.coreOnly <- true
            | "-help" | "-?" -> cla.showUsage <- true
            | other -> cla.isValid <- false
    cla

