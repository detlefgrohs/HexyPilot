using System;
using System.Runtime.InteropServices;

namespace HexyPilot
{
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
        NONBLOCK = 0x08,
        MOTIONPLUS = 0x10
    }

    public enum CWIID_CMD
    {
        STATUS,
        LED,
        RUMBLE,
        RPT_MODE
    }

    [Flags]
    public enum CWIID_RPT
    {
        STATUS = 0x01,
        BTN = 0x02,
        ACC = 0x04,
        IR = 0x08,
        NUNCHUK = 0x10,
        CLASSIC = 0x20,
        BALANCE = 0x40,
        MOTIONPLUS = 0x80,
        EXT = (NUNCHUK | CLASSIC | BALANCE | MOTIONPLUS)
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct timespec
    {
        /// <summary>
        /// Seconds
        /// </summary>
        public System.Int32 tv_sec;

        /// <summary>
        /// Nanoseconds
        /// </summary>
        public System.Int32 tv_nsec;
    }

    public delegate void CWiiDMessageCallback(IntPtr wiimote, int mesg_count, IntPtr mesgArray, ref timespec timestamp);

    public static class CWiiD
    {
        [DllImport("libcwiid.so.1")]
        public static extern IntPtr cwiid_open(ref BluetoothAddress bdaddr, CWIID_FLAG flags);

        [DllImport("libcwiid.so.1")]
        public static extern int cwiid_close(IntPtr wiimote);

        [DllImport("libcwiid.so.1")]
        public static extern int cwiid_command(IntPtr wiimote, CWIID_CMD command, int flags);

        [DllImport("libcwiid.so.1")]
        public static extern int cwiid_enable(IntPtr wiimote, CWIID_FLAG flags);

        [DllImport("libcwiid.so.1")]
        public static extern int cwiid_disable(IntPtr wiimote, CWIID_FLAG flags);

        [DllImport("libcwiid.so.1")]
        public static extern int cwiid_set_mesg_callback(IntPtr wiimote, CWiiDMessageCallback callback);

        [DllImport("libcwiid.so.1")]
        public static extern int cwiid_set_rpt_mode(IntPtr wiimote, CWIID_RPT rpt_mode);
    }
}