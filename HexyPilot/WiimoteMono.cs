using System;

namespace HexyPilot
{
    public class WiimoteMono : IWiimote, IDisposable
    {
        private IntPtr wiimotePtr;

        public event EventHandler<WiimoteChangedEventArgs> WiimoteChanged;

        public event EventHandler<WiimoteExtensionChangedEventArgs> WiimoteExtensionChanged;

        public void Connect()
        {
            wiimotePtr = CWiiD.cwiid_open(ref BluetoothAddress.Any, CWIID_FLAG.MESG_IFC);

            CWiiD.cwiid_set_mesg_callback(wiimotePtr, MessageCallback);

            CWiiD.cwiid_set_rpt_mode(wiimotePtr, CWIID_RPT.BTN | CWIID_RPT.STATUS);
        }

        public void Disconnect()
        {
            if (wiimotePtr != IntPtr.Zero)
            {
                CWiiD.cwiid_close(wiimotePtr);
                wiimotePtr = IntPtr.Zero;
            }
        }

        public void SetReportType(InputReport type, bool continuous)
        {
            throw new NotImplementedException();
        }

        public void SetReportType(InputReport type, IRSensitivity irSensitivity, bool continuous)
        {
            throw new NotImplementedException();
        }

        public void SetLEDs(bool led1, bool led2, bool led3, bool led4)
        {
            var flag = 0;
            if (led1) flag += 0x01;
            if (led2) flag += 0x02;
            if (led3) flag += 0x04;
            if (led4) flag += 0x08;
            SetLEDs((int)flag);
        }

        public void SetLEDs(int leds)
        {
            CWiiD.cwiid_command(wiimotePtr, CWIID_CMD.LED, leds);
        }

        public void SetRumble(bool on)
        {
            CWiiD.cwiid_command(wiimotePtr, CWIID_CMD.RUMBLE, on ? 1 : 0);
        }

        public void GetStatus()
        {
            throw new NotImplementedException();
        }

        public byte[] ReadData(int address, short size)
        {
            throw new NotImplementedException();
        }

        public void WriteData(int address, byte data)
        {
            throw new NotImplementedException();
        }

        public void WriteData(int address, byte size, byte[] buff)
        {
            throw new NotImplementedException();
        }

        public WiimoteState WiimoteState
        {
            get { throw new NotImplementedException(); }
        }

        private void MessageCallback(IntPtr wiimote, int mesg_count, IntPtr mesgArray, ref timespec timestamp)
        {
            Console.Write("{0}:{1} ", timestamp.tv_sec, timestamp.tv_nsec);
            Console.WriteLine(mesg_count);
        }

        #region IDisposable Members

        /// <summary>
        /// Dispose Wiimote
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose wiimote
        /// </summary>
        /// <param name="disposing">Disposing?</param>
        protected virtual void Dispose(bool disposing)
        {
            // close up our handles
            if (disposing)
                Disconnect();
        }

        #endregion IDisposable Members
    }
}