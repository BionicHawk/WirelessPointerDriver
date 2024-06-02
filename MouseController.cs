using System.Runtime.InteropServices;

namespace WirelessPointerDriver;

public static class MouseController
{

    private static ushort _sensibility = 50;
    private const ushort _upscaleMovement = 1;
    private static double _x = 0, _y = 0;
    private static int Width = 0, Height = 0;
    public static bool DebugMode {get; set;} = false; 

    public static ushort Sensibility
    {
        get => _sensibility;
        set
        {
            if (value >= 1 && value <= 200)
                _sensibility = value;
            else
                Console.WriteLine("Command /set_sensibility expected a number greater than 0 and lower or equal than 200");
        }
    }

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);
    private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
    private const uint MOUSEEVENTF_LEFTUP = 0x04;
    private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const uint MOUSEEVENTF_RIGHTUP = 0x10;

    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hwnd);

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

    public const int SM_CXSCREEN = 0;
    public const int SM_CYSCREEN = 1;

    public static void SetDisplayResolution()
    {
        IntPtr hdc = GetDC(IntPtr.Zero);
        int screenWidth = GetSystemMetrics(SM_CXSCREEN);
        int screenHeight = GetSystemMetrics(SM_CYSCREEN);
        ReleaseDC(IntPtr.Zero, hdc);

        Width = screenWidth;
        Height = screenHeight;
    }

    public static void MoveMouse(Position3D position)
    {
        var calc_x = position.X * _sensibility * _upscaleMovement;
        var calc_y = position.Y * _sensibility * _upscaleMovement;

        _x = Math.Clamp(_x - calc_x, 0, Width);
        _y = Math.Clamp(_y - calc_y, 0, Height);

        Console.WriteLine($"\r{_x} : {_y}");

        if (!DebugMode)
            SetCursorPos((int)_x, (int)_y);
    }

    public static void Click()
    {
        if (!DebugMode)
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP,
            (uint)_x,
            (uint)_y,
            0,
            0);
        else
            Console.WriteLine("Doing Primary Click Action...");
    }

    public static void SecondaryClick()
    {
        if (!DebugMode)
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP,
            (uint)_x,
            (uint)_y,
            0,
            0);
        else
            Console.WriteLine("Doing Secondary Click Action...");
    }
}