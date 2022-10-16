module rec FDL.Geometry


open FSharp.NativeInterop
open System.Runtime.InteropServices


#nowarn "9"
#nowarn "51"


module private Native =
    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    [<return: MarshalAs(UnmanagedType.Bool)>]
    extern bool SDL_HasIntersection([<In>] Rectangle _a, [<In>] Rectangle _b)

    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    [<return: MarshalAs(UnmanagedType.Bool)>]
    extern bool SDL_IntersectRect([<In>] Rectangle _a, [<In>] Rectangle _b, [<Out>] Rectangle* _out)

    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    extern void SDL_UnionRect([<In>] Rectangle _a, [<In>] Rectangle _b, [<Out>] Rectangle* _out)

    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    [<return: MarshalAs(UnmanagedType.Bool)>]
    extern bool SDL_EnclosePoints([<In>] Point [] _points, int32 _count, [<In>] Rectangle* _clip, [<Out>] Rectangle* _out)

    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    [<return: MarshalAs(UnmanagedType.Bool)>]
    extern bool SDL_IntersectRectAndLine([<In>] Rectangle _rect, int32* _x1, int32* _y1, int32* _x2, int32* _y2)

// [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
// [<return: MarshalAs(UnmanagedType.Bool)>]
// extern bool SDL_HasIntersectionF([<In>] FRectangle _a, [<In>] FRectangle _b)

// [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
// [<return: MarshalAs(UnmanagedType.Bool)>]
// extern bool SDL_IntersectFRect([<In>] FRectangle _a, [<In>] FRectangle _b, [<Out>] FRectangle* _out)

// [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
// extern void SDL_UnionFRect([<In>] FRectangle _a, [<In>] FRectangle _b, [<Out>] FRectangle* _out)

// [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
// [<return: MarshalAs(UnmanagedType.Bool)>]
// extern bool SDL_EncloseFPoints([<In>] FPoint [] _points, int32 _count, [<In>] FRectangle* _clip, [<Out>] FRectangle* _out)

// [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
// [<return: MarshalAs(UnmanagedType.Bool)>]
// extern bool SDL_IntersectFRectAndLine([<In>] FRectangle _rect, float32* _x1, float32* _y1, float32* _x2, float32* _y2)


[<Struct; StructLayout(LayoutKind.Sequential)>]
type Point = { X: int32; Y: int32 }


module Point =
    let enclose points =
        let mutable out: Rectangle = { X = 0; Y = 0; Width = 0; Height = 0 }

        Native.SDL_EnclosePoints(points, points.Length, NativePtr.nullPtr, &&out)
        |> ignore

        out

    let tryEncloseWithin rect points =
        let mutable out: Rectangle = { X = 0; Y = 0; Width = 0; Height = 0 }

        let mutable rect = rect

        if Native.SDL_EnclosePoints(points, points.Length, &&rect, &&out) then
            ValueSome out
        else
            ValueNone


// module FPoint =
//     let enclose points =
//         let mutable out: FRectangle =
//             { X = 0.f
//               Y = 0.f
//               Width = 0.f
//               Height = 0.f }

//         Native.SDL_EncloseFPoints(points, points.Length, NativePtr.nullPtr, &&out)
//         |> ignore

//         out

//     let tryEncloseWithin rect points =
//         let mutable out: FRectangle =
//             { X = 0.f
//               Y = 0.f
//               Width = 0.f
//               Height = 0.f }

//         let mutable rect = rect

//         if Native.SDL_EncloseFPoints(points, points.Length, &&rect, &&out) then
//             Some out
//         else
//             None


[<Struct; StructLayout(LayoutKind.Sequential)>]
type Rectangle =
    { X: int32
      Y: int32
      Width: int32
      Height: int32 }

    static member (|||)(a: Rectangle, b: Rectangle) =
        let mutable out = { X = 0; Y = 0; Width = 0; Height = 0 }
        Native.SDL_UnionRect(a, b, &&out)
        out


module Rectangle =
    let inline isEmpty (r: Rectangle) = r.Width <= 0 || r.Height <= 0

    let inline contains (p: Point) (r: Rectangle) =
        (p.X >= r.X)
        && (p.X < (r.X + r.Width))
        && (p.Y >= r.Y)
        && (p.Y < (r.Y + r.Height))

    let intersects a b = Native.SDL_HasIntersection(a, b)

    let tryIntersect a b =
        let mutable out: Rectangle = { X = 0; Y = 0; Width = 0; Height = 0 }

        if Native.SDL_IntersectRect(a, b, &&out) then
            ValueSome out
        else
            ValueNone

    let tryIntersectLine (p1: Point) (p2: Point) rect =
        let mutable x1 = p1.X
        let mutable y1 = p1.Y
        let mutable x2 = p2.X
        let mutable y2 = p2.Y

        if Native.SDL_IntersectRectAndLine(rect, &&x1, &&y1, &&x2, &&y2) then
            ValueSome(struct ({ Point.X = x1; Y = y1 }, { Point.X = x2; Y = y2 }))
        else
            ValueNone

    let union (a: Rectangle) (b: Rectangle) = a ||| b


[<Struct; StructLayout(LayoutKind.Sequential)>]
type FPoint = { X: float32; Y: float32 }


[<Struct; StructLayout(LayoutKind.Sequential)>]
type FRectangle =
    { X: float32
      Y: float32
      Width: float32
      Height: float32 }


// static member (|||)(a: FRectangle, b: FRectangle) =
//     let mutable out: FRectangle =
//         { X = 0.f
//           Y = 0.f
//           Width = 0.f
//           Height = 0.f }

//     Native.SDL_UnionFRect(a, b, &&out)
//     out


module FRectangle =
    let inline contains (p: FPoint) (r: FRectangle) =
        (p.X >= r.X)
        && (p.X < (r.X + r.Width))
        && (p.Y >= r.Y)
        && (p.Y < (r.Y + r.Height))

    let inline equalsEpsilon epsilon (a: FRectangle) (b: FRectangle) =
        (a = b)
        || (abs (a.X - b.X) <= epsilon
            && abs (a.Y - b.Y) <= epsilon
            && abs (a.Width - b.Width) <= epsilon
            && abs (a.Height - b.Height) <= epsilon)

    [<Literal>]
    let defaultEpsilon = 1.1920928955078125e-07f

    let equals = equalsEpsilon defaultEpsilon

    let inline isEmpty (r: FRectangle) = r.Width <= 0.f || r.Height <= 0.f

// let intersects a b = Native.SDL_HasIntersectionF(a, b)

// let tryIntersect a b =
//     let mutable out: FRectangle =
//         { X = 0.f
//           Y = 0.f
//           Width = 0.f
//           Height = 0.f }

//     if Native.SDL_IntersectFRect(a, b, &&out) then
//         Some out
//     else
//         None

// let tryIntersectLine (p1: FPoint) (p2: FPoint) rect =
//     let mutable x1 = p1.X
//     let mutable y1 = p1.Y
//     let mutable x2 = p2.X
//     let mutable y2 = p2.Y

//     if Native.SDL_IntersectFRectAndLine(rect, &&x1, &&y1, &&x2, &&y2) then
//         Some({ FPoint.X = x1; Y = y1 }, { FPoint.X = x2; Y = y2 })
//     else
//         None
