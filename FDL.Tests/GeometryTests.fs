module FDL.Tests.GeometryTests


open Xunit
open FDL.Geometry


module Int =
    [<Fact>]
    let ``Rectangle intersection`` () =
        let a =
            { Rectangle.X = 0
              Y = 1
              Width = 10
              Height = 11 }

        let b =
            { Rectangle.X = 5
              Y = 6
              Width = 10
              Height = 11 }

        Assert.Equal(
            ValueSome
                { Rectangle.X = 5
                  Y = 6
                  Width = 5
                  Height = 6 },
            Rectangle.tryIntersect a b
        )


    [<Fact>]
    let ``Rectangles with no intersection`` () =
        let a =
            { Rectangle.X = 0
              Y = 0
              Width = 10
              Height = 10 }

        let b =
            { Rectangle.X = 20
              Y = 20
              Width = 10
              Height = 10 }

        Assert.Equal(ValueNone, Rectangle.tryIntersect a b)


    [<Fact>]
    let ``Rectangles have intersection`` () =
        let a =
            { Rectangle.X = 0
              Y = 1
              Width = 10
              Height = 11 }

        let b =
            { Rectangle.X = 5
              Y = 6
              Width = 10
              Height = 11 }

        Assert.True(a |> Rectangle.intersects b)


    [<Fact>]
    let ``Rectangles have no intersection`` () =
        let a =
            { Rectangle.X = 0
              Y = 0
              Width = 10
              Height = 10 }

        let b =
            { Rectangle.X = 20
              Y = 20
              Width = 10
              Height = 10 }

        Assert.False(Rectangle.intersects a b)

    [<Fact>]
    let ``Rectangle union`` () =
        let a =
            { Rectangle.X = 2
              Y = 2
              Width = 10
              Height = 10 }

        let b =
            { Rectangle.X = 10
              Y = 10
              Width = 10
              Height = 10 }

        Assert.Equal(
            { Rectangle.X = 2
              Y = 2
              Width = 18
              Height = 18 },
            a ||| b
        )


    [<Fact>]
    let ``Enclose points`` () =
        let points: Point array = [| { X = 1; Y = 1 }; { X = 9; Y = 9 } |]

        Assert.Equal(
            { Rectangle.X = 1
              Y = 1
              Width = 9
              Height = 9 },
            Point.enclose points
        )


    [<Fact>]
    let ``Enclose points within a rectangle`` () =
        let points: Point array = [| { X = 1; Y = 1 }; { X = 9; Y = 9 } |]

        let clip =
            { Rectangle.X = 0
              Y = 0
              Width = 11
              Height = 11 }

        Assert.Equal(
            ValueSome
                { Rectangle.X = 1
                  Y = 1
                  Width = 9
                  Height = 9 },
            Point.tryEncloseWithin clip points
        )


    [<Fact>]
    let ``Intersect line with rectangle`` () =
        let rect =
            { Rectangle.X = 0
              Y = 0
              Width = 11
              Height = 11 }

        let p1 = { Point.X = -5; Y = -2 }

        let p2 = { Point.X = 14; Y = 25 }

        Assert.Equal(
            ValueSome(struct ({ Point.X = 0; Y = 5 }, { Point.X = 3; Y = 10 })),
            Rectangle.tryIntersectLine p1 p2 rect
        )
