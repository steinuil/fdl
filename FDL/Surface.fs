namespace rec FDL


open System
open System.Runtime.InteropServices


// [<Struct; StructLayout(LayoutKind.Sequential)>]
// type Surface =
//     { flags: Surface.Flags
//       format: Pixel.Format }


module Surface =
    [<Flags>]
    type Flags =
        | Prealloc = 0x1
        | RleAccel = 0x2
        | DontFree = 0x4
        | SimdAligned = 0x8
