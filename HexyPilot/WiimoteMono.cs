using System;
using System.Runtime.InteropServices;

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
            var msgSize = Marshal.SizeOf(typeof(CWIIDMessage));

            for (int i = 0; i < mesg_count; i++)
            {
                IntPtr p = new IntPtr(mesgArray.ToInt64() + i * msgSize);

                var msg = (CWIIDMessage)Marshal.PtrToStructure(p, typeof(CWIIDMessage));

                switch (msg.Type)
                {
                    case CWIID_MESG_TYPE.STATUS:
                        Console.WriteLine("Status Update: " + msg.StatusMessage.battery);
                        break;
                    case CWIID_MESG_TYPE.BTN:
                        DoButtonUpdate(msg.ButtonMessage.buttons);
                        break;
                    case CWIID_MESG_TYPE.ACC:
                    case CWIID_MESG_TYPE.IR:
                    case CWIID_MESG_TYPE.NUNCHUK:
                    case CWIID_MESG_TYPE.CLASSIC:
                    case CWIID_MESG_TYPE.ERROR:
                    case CWIID_MESG_TYPE.UNKNOWN:
                        Console.WriteLine("Message Type: " + msg.Type);
                        break;
                }
            }
        }

        private void DoButtonUpdate(CWIID_BTN buttons)
        {
            var state = new WiimoteState();
            state.ButtonState.A = (buttons & CWIID_BTN.A) != 0;
            state.ButtonState.B = (buttons & CWIID_BTN.B) != 0;
            state.ButtonState.One = (buttons & CWIID_BTN.BTN1) != 0;
            state.ButtonState.Two = (buttons & CWIID_BTN.BTN2) != 0;
            state.ButtonState.Up = (buttons & CWIID_BTN.UP) != 0;
            state.ButtonState.Down = (buttons & CWIID_BTN.DOWN) != 0;
            state.ButtonState.Left = (buttons & CWIID_BTN.LEFT) != 0;
            state.ButtonState.Right = (buttons & CWIID_BTN.RIGHT) != 0;
            state.ButtonState.Plus = (buttons & CWIID_BTN.PLUS) != 0;
            state.ButtonState.Minus = (buttons & CWIID_BTN.MINUS) != 0;
            state.ButtonState.Home = (buttons & CWIID_BTN.HOME) != 0;
            var handler = WiimoteChanged;
            if (handler != null)
                handler(this, new WiimoteChangedEventArgs(state));
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