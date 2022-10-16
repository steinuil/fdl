module FDL.Tests.WindowTests


open Xunit
open FDL


[<Fact>]
let ``Get and set window name`` () =
    System.init SubSystem.Video

    let title = "Get and set window name"

    use win =
        Window.create title Window.Position.Undefined 100 100 Window.Flags.Hidden

    let got = Window.title win
    Assert.Equal(title, got)

    let title = "ゲームオーバー"

    win |> Window.setTitle title
    let got = Window.title win

    System.quit ()

    Assert.Equal(title, got)
