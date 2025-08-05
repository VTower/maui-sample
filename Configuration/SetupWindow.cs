
#if WINDOWS
using Microsoft.UI;
using Windows.Graphics;
using Microsoft.UI.Windowing;
#endif

using Microsoft.Maui.LifecycleEvents;

namespace maui_sample.Configuration;

public static class SetupWindow
{
    // Aka i take this from https://stackoverflow.com/questions/72128525/net-maui-how-to-maximize-application-on-startup 
    // Credits https://stackoverflow.com/users/2969122/spencer
    public static MauiAppBuilder StartAppFullSize(this MauiAppBuilder appBuilder)
    => appBuilder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                    AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
                    if (winuiAppWindow.Presenter is OverlappedPresenter p)
                        p.Maximize();
                    else
                    {
                        const int width = 1200;
                        const int height = 800;
                        winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
                    }
                });
            });
        });

    // Aka i take this from https://learn.microsoft.com/en-us/answers/questions/1470337/how-do-i-maximize-the-windows-screen-in-a-net-maui
    // Credits https://learn.microsoft.com/en-us/users/yonglunliu-msft/
    public static MauiAppBuilder StartAppFullScreen(this MauiAppBuilder appBuilder)
     => appBuilder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(w =>
                {
                    w.OnWindowCreated(window =>
                    {
                        window.ExtendsContentIntoTitleBar = false; //If you need to completely hide the minimized maximized close button, you need to set this value to false.
                        IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
                        var _appWindow = AppWindow.GetFromWindowId(myWndId);
                        _appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
                    });
                });
        });
}