namespace FDL


open System
open System.Runtime.InteropServices


/// These are the flags which may be passed to FDL.init.
/// You should specify the subsystems which you will be using in your application.
[<Flags; RequireQualifiedAccess>]
type SubSystem =
    | Timer = 0x1u
    | Audio = 0x10u
    /// Implies Flags.Events
    | Video = 0x20u
    /// Implies Flags.Events
    | Joystick = 0x200u
    | Haptic = 0x1000u
    | GameController = 0x2000u
    | Events = 0x4000u
    | Sensor = 0x8000u


module private Native =
    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    extern int SDL_Init(SubSystem _flags)

    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    extern int SDL_InitSubSystem(SubSystem _flags)

    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    extern void SDL_Quit()

    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    extern void SDL_QuitSubSystem(SubSystem _flags)

    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    extern SubSystem SDL_WasInit(SubSystem _flags)


module System =
    let init flags = Native.SDL_Init flags |> Error.toExn

    let quit = Native.SDL_Quit


module SubSystem =
    [<Literal>]
    let Everything =
        SubSystem.Timer
        ||| SubSystem.Audio
        ||| SubSystem.Video
        ||| SubSystem.Joystick
        ||| SubSystem.Haptic
        ||| SubSystem.GameController
        ||| SubSystem.Events
        ||| SubSystem.Sensor

    let init flags =
        Native.SDL_InitSubSystem flags |> Error.toExn

    let quit = Native.SDL_QuitSubSystem

    let wasInit = Native.SDL_WasInit
