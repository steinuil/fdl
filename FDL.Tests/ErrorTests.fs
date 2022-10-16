module FDL.Tests.ErrorTests


open Xunit


[<Fact>]
let ``Error.set and Error.get`` () =
    let message = "わたし夜に泳ぐの"
    FDL.Error.set message
    let result = FDL.Error.get ()
    Assert.Equal(message, result)


[<Fact>]
let ``Error.clear`` () =
    let message = "わたし夜に泳ぐの"
    FDL.Error.set message
    FDL.Error.clear ()
    let error = FDL.Error.get ()
    Assert.Equal("", error)
