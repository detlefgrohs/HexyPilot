using System;

namespace HexyPilot
{
    public class WiimoteMono : IWiimote, IDisposable
    {
        public event EventHandler<WiimoteChangedEventArgs> WiimoteChanged;

        public event EventHandler<WiimoteExtensionChangedEventArgs> WiimoteExtensionChanged;

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void SetLEDs(int leds)
        {
            throw new NotImplementedException();
        }

        public void SetRumble(bool on)
        {
            throw new NotImplementedException();
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