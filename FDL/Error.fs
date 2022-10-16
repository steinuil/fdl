module FDL.Error


open System
open System.Runtime.InteropServices


module private Native =
    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    extern IntPtr SDL_GetError()

    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    extern void SDL_ClearError()

    [<DllImport(nativeLib, CallingConvention = CallingConvention.Cdecl)>]
    extern void SDL_SetError([<MarshalAs(UnmanagedType.LPUTF8Str)>] string _str)


let get () =
    Native.SDL_GetError() |> Marshal.PtrToStringUTF8


let set = Native.SDL_SetError


let clear = Native.SDL_ClearError


exception SdlError of string


let inline toExn result =
    if result <> 0 then
        let err = get ()
        clear ()
        err |> SdlError |> raise


let toResult result =
    if result = 0 then
        Ok(())
    else
        let error = get ()
        Error(error)
