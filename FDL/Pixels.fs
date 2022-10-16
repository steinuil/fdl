namespace FDL


open System
open System.Runtime.InteropServices


[<Struct; StructLayout(LayoutKind.Sequential)>]
type Color =
    { R: uint8
      G: uint8
      B: uint8
      A: uint8 }


module private PixelFormatNative =
    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    extern void SDL_FreeFormat(IntPtr _format)


type PixelFormat() =
    inherit SafeHandle(0, true)

    override this.ReleaseHandle() =
        PixelFormatNative.SDL_FreeFormat(this.handle)
        true

    override this.IsInvalid = this.handle = IntPtr.Zero


module Pixel =
    type Type =
        | Unknown = 0
        | Index1 = 1
        | Index4 = 2
        | Index8 = 3
        | Packed8 = 4
        | Packed16 = 5
        | Packed32 = 6
        | ArrayU8 = 7
        | ArrayU16 = 8
        | ArrayU32 = 9
        | ArrayF16 = 10
        | ArrayF32 = 11


    [<RequireQualifiedAccess>]
    type BitmapOrder =
        | None = 0
        | _1234 = 1
        | _4321 = 2


    [<RequireQualifiedAccess>]
    type PackedOrder =
        | None = 0
        | XRGB = 1
        | RGBX = 2
        | ARGB = 3
        | RGBA = 4
        | XBGR = 5
        | BGRX = 6
        | ABGR = 7
        | BGRA = 8


    [<RequireQualifiedAccess>]
    type ArrayOrder =
        | None = 0
        | RGB = 1
        | RGBA = 2
        | ARGB = 3
        | BGR = 4
        | BGRA = 5
        | ABGR = 6


    [<RequireQualifiedAccess>]
    type PackedLayout =
        | None = 0
        | _332 = 1
        | _4444 = 2
        | _1555 = 3
        | _5551 = 4
        | _565 = 5
        | _8888 = 6
        | _2101010 = 7
        | _1010102 = 8


    [<RequireQualifiedAccess>]
    type PixelFormat =
        | Unknown = 0x00000000
        | Index1LSB = 0x11200100
        | Index1MSB = 0x11100100
        | Index4LSB = 0x12200400
        | Index4MSB = 0x12100400
        | Index8 = 0x13000800
        | RGB332 = 0x14110801
        | XRGB4444 = 0x15120c02
        | RGB444 = 0x15120c02
        | XBGR4444 = 0x15520c02
        | BGR444 = 0x15520c02
        | XRGB1555 = 0x15130f02
        | RGB555 = 0x15130f02
        | XBGR1555 = 0x15530f02
        | BGR555 = 0x15530f02
        | ARGB4444 = 0x15321002
        | RGBA4444 = 0x15421002
        | ABGR4444 = 0x15721002
        | BGRA4444 = 0x15821002
        | ARGB1555 = 0x15331002
        | RGBA5551 = 0x15441002
        | ABGR1555 = 0x15731002
        | BGRA5551 = 0x15841002
        | RGB565 = 0x15151002
        | BGR565 = 0x15551002
        | RGB24 = 0x17101803
        | BGR24 = 0x17401803
        | XRGB8888 = 0x16161804
        | RGB888 = 0x16161804
        | XBGR8888 = 0x16561804
        | BGR888 = 0x16561804
        | BGRX8888 = 0x16661804
        | ARGB8888 = 0x16362004
        | RGBA8888 = 0x16462004
        | ABGR8888 = 0x16762004
        | BGRA8888 = 0x16862004
        | ARGB2101010 = 0x16372004
        | RGBA32 = 0x16762004
        | ARGB32 = 0x16862004
        | BGRA32 = 0x16362004
        | ABGR32 = 0x16462004
        | YV12 = 0x32315659
        | IYUV = 0x56555949
        | YUY2 = 0x32595559
        | UYVY = 0x59565955
        | YUVU = 0x55565559
        | NV12 = 0x3231564e
        | NV21 = 0x3132564e
        | ExternalOES = 0x2053454f
