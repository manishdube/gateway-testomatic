module etconfig
open FSharp.Configuration

type ETConfig = YamlConfig<"config.yaml">
let config = ETConfig()
config.Load ".\config.yaml"

let mutable gatewayUrl = config.gatewayUrl.ToString()
let mutable shortUrl = config.shortUrl.ToString()
let mutable rtrsUrl = config.rtrsUrl.ToString()
let mutable emmaUrl = config.emmaUrl.ToString()
let mutable msrbOrgUrl = config.msrbOrgUrl.ToString()

