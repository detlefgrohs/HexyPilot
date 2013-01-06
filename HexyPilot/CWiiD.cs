using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct BluetoothAddress
{
    public static BluetoothAddress Any = new BluetoothAddress();
    public static BluetoothAddress All = new BluetoothAddress(0xff, 0xff, 0xff, 0xff, 0xff, 0xff);
    public static BluetoothAddress Local = new BluetoothAddress(0, 0, 0, 0xff, 0xff, 0xff);

    public byte b0;
    public byte b1;
    public byte b2;
    public byte b3;
    public byte b4;
    public byte b5;

    public BluetoothAddress(byte bd5, byte bd4, byte bd3, byte bd2, byte bd1, byte bd0)
    {
        this.b0 = bd0;
        this.b1 = bd1;
        this.b2 = bd2;
        this.b3 = bd3;
        this.b4 = bd4;
        this.b5 = bd5;
    }

    public override string ToString()
    {
        return b5.ToString("X2") + ":" + b4.ToString("X2") + ":" + b3.ToString("X2") + ":" + b2.ToString("X2") + ":" + b1.ToString("X2") + ":" + b0.ToString("X2");
    }
}

[Flags]
public enum CWIID_FLAG
{
    MESG_IFC = 0x01,
    CONTINUOUS = 0x02,
    REPEAT_BTN = 0x04,
    NONBLOCK = 0x08
}

public enum CWIID_CMD
{
    STATUS,
    LED,
    RUMBLE,
    RPT_MODE
}

[Flags]
public enum CWIID_LED
{
    NONE = 0x00,
    LED1 = 0x01,
    LED2 = 0x02,
    LED3 = 0x04,
    LED4 = 0x08
}

internal class CWiiD : IDisposable
{
    private IntPtr wiimotePtr;

    public CWiiD()
    {
        wiimotePtr = cwiid_open(ref BluetoothAddress.Any, CWIID_FLAG.MESG_IFC);
    }

    public void Close()
    {
        if (wiimotePtr != IntPtr.Zero)
        {
            cwiid_close(wiimotePtr);
            wiimotePtr = IntPtr.Zero;
        }
    }

    public void SetLed(bool led1, bool led2, bool led3, bool led4)
    {
        var flag = CWIID_LED.NONE;
        if (led1) flag |= CWIID_LED.LED1;
        if (led2) flag |= CWIID_LED.LED2;
        if (led3) flag |= CWIID_LED.LED3;
        if (led4) flag |= CWIID_LED.LED4;
        cwiid_command(wiimotePtr, CWIID_CMD.LED, (int)flag);
    }

    public void SetRumble(bool rumble)
    {
        cwiid_command(wiimotePtr, CWIID_CMD.RUMBLE, rumble ? 1 : 0);
    }

    #region IDisposable Members

    private bool disposed;

    ~CWiiD()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Clean up managed resources.
            }

            // Clean up unmanaged resources.
            Close();

            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        System.GC.SuppressFinalize(this);
    }

    #endregion IDisposable Members

    [DllImport("libcwiid.so.1")]
    private static extern IntPtr cwiid_open(ref BluetoothAddress bdaddr, CWIID_FLAG flags);

    [DllImport("libcwiid.so.1")]
    private static extern int cwiid_close(IntPtr wiimote);

    [DllImport("libcwiid.so.1")]
    private static extern int cwiid_command(IntPtr wiimote, CWIID_CMD command, int flags);

    [DllImport("libcwiid.so.1")]
    private static extern int cwiid_enable(IntPtr wiimote, CWIID_FLAG flags);

    [DllImport("libcwiid.so.1")]
    private static extern int cwiid_disable(IntPtr wiimote, CWIID_FLAG flags);
}