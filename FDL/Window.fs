namespace rec FDL


open System
open System.Runtime.InteropServices


#nowarn "51"


module private WindowNative =
    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    extern void SDL_DestroyWindow(IntPtr _window)


type Window() =
    inherit SafeHandle(0, true)

    override this.ReleaseHandle() =
        WindowNative.SDL_DestroyWindow(this.handle)
        true

    override this.IsInvalid = this.handle = IntPtr.Zero


module Window =
    module private Native =
        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern Window SDL_CreateWindow([<MarshalAs(UnmanagedType.LPUTF8Str)>] string _title, int _x, int _y, int _w, int _h, Flags _flags)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern U32Status SDL_GetWindowID([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern Window SDL_GetWindowFromID(uint32 _id)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern Flags SDL_GetWindowFlags([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_SetWindowTitle([<In>] Window _window, [<MarshalAs(UnmanagedType.LPUTF8Str)>] string _title)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern IntPtr SDL_GetWindowTitle([<In>] Window _window)

        // [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        // extern IntPtr SDL_SetWindowIcon([<In>] Window _window, [<In>] Surface _icon)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern IntPtr SDL_GetWindowData([<In>] Window _window, [<MarshalAs(UnmanagedType.LPUTF8Str)>] string _name)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_SetWindowPosition([<In>] Window _window, int32 _x, int32 _y)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_GetWindowPosition([<In>] Window _window, [<Out>] int32* _x, [<Out>] int32* _y)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_SetWindowSize([<In>] Window _window, int32 _x, int32 _y)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_GetWindowSize([<In>] Window _window, [<Out>] int32* _x, [<Out>] int32* _y)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_GetWindowBordersSize([<In>] Window _window, [<Out>] int32* _top, [<Out>] int32* _left, [<Out>] int32* _bottom, [<Out>] int32* _right)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_GetWindowSizeInPixels([<In>] Window _window, [<Out>] int32* _w, [<Out>] int32* _h)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_SetWindowMinimumSize([<In>] Window _window, int32 _minW, int32 _minH)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_GetWindowMinimumSize([<In>] Window _window, [<Out>] int32* _w, [<Out>] int32* _h)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_SetWindowMaximumSize([<In>] Window _window, int32 _maxW, int32 _maxH)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_GetWindowMaximumSize([<In>] Window _window, [<Out>] int32* _w, [<Out>] int32* _h)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_SetWindowBordered([<In>] Window _window, [<MarshalAs(UnmanagedType.Bool)>] bool _bordered)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_SetWindowResizable([<In>] Window _window, [<MarshalAs(UnmanagedType.Bool)>] bool _resizable)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_SetWindowAlwaysOnTop([<In>] Window _window, [<MarshalAs(UnmanagedType.Bool)>] bool _onTop)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_ShowWindow([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_HideWindow([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_RaiseWindow([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_MaximizeWindow([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_MinimizeWindow([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_RestoreWindow([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_SetWindowFullscreen([<In>] Window _window, FullscreenMode _flag)

        // [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        // extern Surface SDL_GetWindowSurface([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_UpdateWindowSurface([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_UpdateWindowSurfaceRects([<In>] Window _window, [<In>] Geometry.Rectangle [] _rects, int _numRects)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_SetWindowGrab([<In>] Window _window, [<MarshalAs(UnmanagedType.Bool)>] bool _grabbed)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_SetWindowKeyboardGrab([<In>] Window _window, [<MarshalAs(UnmanagedType.Bool)>] bool _grabbed)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern void SDL_SetWindowMouseGrab([<In>] Window _window, [<MarshalAs(UnmanagedType.Bool)>] bool _grabbed)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        [<return: MarshalAs(UnmanagedType.Bool)>]
        extern bool SDL_GetWindowGrab([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        [<return: MarshalAs(UnmanagedType.Bool)>]
        extern bool SDL_GetWindowKeyboardGrab([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        [<return: MarshalAs(UnmanagedType.Bool)>]
        extern bool SDL_GetWindowMouseGrab([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern Window SDL_GetGrabbedWindow()

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_SetWindowMouseRect([<In>] Window _window, [<In>] Geometry.Rectangle _rect)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern Geometry.Rectangle SDL_GetWindowMouseRect([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_SetWindowBrightness([<In>] Window _window, float32 _brightness)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern float32 SDL_GetWindowBrightness([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_SetWindowOpacity([<In>] Window _window, float32 _opacity)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_GetWindowOpacity([<In>] Window _window, [<Out>] float32* _opacity)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_SetWindowModalFor([<In>] Window _modal, [<In>] Window _parent)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_SetWindowInputFocus([<In>] Window _window)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_SetWindowGammaRamp([<In>] Window _window, [<In;
                                                                         MarshalAs(UnmanagedType.LPArray,
                                                                                   SizeConst = 256)>] uint16 [] _red, [<In;
                                                                                                                        MarshalAs(UnmanagedType.LPArray,
                                                                                                                                  SizeConst = 256)>] uint16 [] _green, [<In;
                                                                                                                                                                         MarshalAs(UnmanagedType.LPArray,
                                                                                                                                                                                   SizeConst = 256)>] uint16 [] _blue)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_GetWindowGammaRamp([<In>] Window _window, [<Out>] IntPtr _red, [<Out>] IntPtr _green, [<Out>] IntPtr _blue)

        [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
        extern I32Status SDL_FlashWindow([<In>] Window _window, FlashOperation _operation)



    [<RequireQualifiedAccess>]
    type FullscreenMode =
        | Exclusive = 0x1u
        | BorderlessWindow = 0x1001u
        | Windowed = 0u


    [<RequireQualifiedAccess>]
    type FlashOperation =
        | Cancel = 0
        | Briefly = 1
        | UntilFocused = 2


    [<Flags>]
    type Flags =
        | None = 0u
        | Fullscreen = 0x00000001u
        | OpenGL = 0x00000002u
        | Shown = 0x00000004u
        | Hidden = 0x00000008u
        | Borderless = 0x00000010u
        | Resizable = 0x00000020u
        | Minimized = 0x00000040u
        | Maximized = 0x00000080u
        | MouseGrabbed = 0x00000100u
        | InputFocus = 0x00000200u
        | MouseFocus = 0x00000400u
        | FullscreenDesktop = 0x00001001u
        | Foreign = 0x00000800u
        | AllowHighDPI = 0x00002000u
        | Mousecapture = 0x00004000u
        | AlwaysOnTop = 0x00008000u
        | SkipTaskbar = 0x00010000u
        | Utility = 0x00020000u
        | Tooltip = 0x00040000u
        | PopupMenu = 0x00080000u
        | KeyboardGrabbed = 0x00100000u
        | Vulkan = 0x10000000u
        | Metal = 0x2000000u


    [<Literal>]
    let UndefinedMask = 0x1FFF0000

    [<Literal>]
    let CenteredMask = 0x2FFF0000


    [<RequireQualifiedAccess>]
    type Position =
        | Undefined
        | Centered
        | Absolute of int * int


    let create title pos width height flags =
        let x, y =
            match pos with
            | Position.Undefined -> UndefinedMask, UndefinedMask
            | Position.Centered -> CenteredMask, CenteredMask
            | Position.Absolute (x, y) -> x, y

        let window =
            Native.SDL_CreateWindow(title, x, y, width, height, flags)

        if window.IsInvalid then
            Error.get () |> Error.SdlError |> raise
        else
            window


    let id window =
        let id = Native.SDL_GetWindowID window

        if id = 0u then
            Error.get () |> Error.SdlError |> raise
        else
            id


    let fromId id =
        let window = Native.SDL_GetWindowFromID id

        if window.IsInvalid then
            Error.get () |> Error.SdlError |> raise
        else
            window


    let flags = Native.SDL_GetWindowFlags


    let setTitle title win = Native.SDL_SetWindowTitle(win, title)


    let title win =
        Native.SDL_GetWindowTitle win
        |> Marshal.PtrToStringUTF8


    let setPosition x y win = Native.SDL_SetWindowPosition(win, x, y)


    let position win =
        let mutable x = 0
        let mutable y = 0

        Native.SDL_GetWindowPosition(win, &&x, &&y)

        { Geometry.X = x; Geometry.Y = y }


    let setSize w h win = Native.SDL_SetWindowSize(win, w, h)


    let size win =
        let mutable w = 0
        let mutable h = 0

        Native.SDL_GetWindowSize(win, &&w, &&h)

        struct (w, h)


    let bordersSize win =
        let mutable top = 0
        let mutable left = 0
        let mutable bottom = 0
        let mutable right = 0

        if
            Native.SDL_GetWindowBordersSize(win, &&top, &&left, &&bottom, &&right)
            <> 0
        then
            ValueSome(struct (top, left, bottom, right))
        else
            ValueNone


    let sizeInPixels win =
        let mutable w = 0
        let mutable h = 0

        Native.SDL_GetWindowSizeInPixels(win, &&w, &&h)

        struct (w, h)


    let setMinimumSize w h win =
        Native.SDL_SetWindowMinimumSize(win, w, h)

    let setMaximumSize w h win =
        Native.SDL_SetWindowMaximumSize(win, w, h)


    let minimumSize win =
        let mutable w = 0
        let mutable h = 0

        Native.SDL_GetWindowMinimumSize(win, &&w, &&h)

        struct (w, h)

    let maximumSize win =
        let mutable w = 0
        let mutable h = 0

        Native.SDL_GetWindowMaximumSize(win, &&w, &&h)

        struct (w, h)


    let setBordered bordered win =
        Native.SDL_SetWindowBordered(win, bordered)

    let setResizable resizable win =
        Native.SDL_SetWindowResizable(win, resizable)

    let setAlwaysOnTop alwaysOnTop win =
        Native.SDL_SetWindowAlwaysOnTop(win, alwaysOnTop)


    let show = Native.SDL_ShowWindow

    let hide = Native.SDL_HideWindow

    let raiseWin = Native.SDL_RaiseWindow

    let maximize = Native.SDL_MaximizeWindow

    let minimize = Native.SDL_MinimizeWindow

    let restore = Native.SDL_RestoreWindow


    let setFullscreenMode mode win =
        Native.SDL_SetWindowFullscreen(win, mode)
        |> Error.toExn


    let updateSurface win =
        Native.SDL_UpdateWindowSurface win |> Error.toExn

    let updateSurfaceRects rects win =
        Native.SDL_UpdateWindowSurfaceRects(win, rects, rects.Length)
        |> Error.toExn


    let grabInput grabbed win = Native.SDL_SetWindowGrab(win, grabbed)

    let grabKeyboard grabbed win =
        Native.SDL_SetWindowKeyboardGrab(win, grabbed)

    let grabMouse grabbed win =
        Native.SDL_SetWindowMouseGrab(win, grabbed)


    let grabsInput = Native.SDL_GetWindowGrab

    let grabsKeyboard = Native.SDL_GetWindowKeyboardGrab

    let grabsMouse = Native.SDL_GetWindowMouseGrab


    let findGrabbedWindow () =
        let win = Native.SDL_GetGrabbedWindow()
        if win.IsInvalid then None else Some win


    let setMouseConfinementRect rect win =
        Native.SDL_SetWindowMouseRect(win, rect)
        |> Error.toExn


    let mouseConfinementRect win =
        let rect = Native.SDL_GetWindowMouseRect win

        if win.IsInvalid then
            ValueNone
        else
            ValueSome(rect)


    let setOpacity opacity win =
        Native.SDL_SetWindowOpacity(win, opacity)
        |> Error.toExn


    let opacity win =
        let mutable opacity = 0.f

        Native.SDL_GetWindowOpacity(win, &&opacity)
        |> Error.toExn


    let setModalFor parent modal =
        Native.SDL_SetWindowModalFor(modal, parent)
        |> Error.toExn


    let setInputFocus win =
        Native.SDL_SetWindowInputFocus win |> Error.toExn

    let flash operation win = Native.SDL_FlashWindow(win, operation)


    let setDisplayBrightness brightness win =
        Native.SDL_SetWindowBrightness(win, brightness)
        |> Error.toExn


    let displayBrightness = Native.SDL_GetWindowBrightness


    let setDisplayGammaRamp (red: uint16 []) (green: uint16 []) (blue: uint16 []) win =
        if red.Length <> 256 then
            invalidArg "Expected an array of 256 elements" "red"

        if green.Length <> 256 then
            invalidArg "Expected an array of 256 elements" "green"

        if blue.Length <> 256 then
            invalidArg "Expected an array of 256 elements" "blue"

        Native.SDL_SetWindowGammaRamp(win, red, green, blue)
        |> Error.toExn


    let displayGammaRamp win =
        let mutable red = Array.create 256 0us
        let mutable green = Array.create 256 0us
        let mutable blue = Array.create 256 0us

        let r = GCHandle.Alloc red
        let g = GCHandle.Alloc blue
        let b = GCHandle.Alloc green

        let result =
            Native.SDL_GetWindowGammaRamp(win, GCHandle.ToIntPtr r, GCHandle.ToIntPtr g, GCHandle.ToIntPtr b)

        r.Free()
        g.Free()
        b.Free()

        Error.toExn result

        (red, green, blue)
