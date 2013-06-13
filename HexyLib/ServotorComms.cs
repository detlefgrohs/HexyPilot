using System;
using System.IO.Ports;
using System.Threading;

namespace HexyLib
{
    public class ServotorComms : IDisposable
    {
        private readonly SerialPort port;

        public ServotorComms()
        {
            var hexyPort = FindHexyPort();

            if (string.IsNullOrEmpty(hexyPort))
                throw new Exception("Hexy Port Not Found");

            port = new SerialPort(hexyPort, 9600);
            port.DtrEnable = true;
            port.Open();
        }

        public string Version()
        {
            port.WriteLine("V");

            return port.ReadLine();
        }

        public void KillAllServos()
        {
            port.WriteLine("K");
        }

        public void CentreAllServos()
        {
            port.WriteLine("C");
        }

        public void SetServo(int servo, int pos)
        {
            if (servo < 0 || servo >= 32)
                throw new IndexOutOfRangeException("servo number is out of range");
            if (pos < 500 || pos > 2500)
                throw new IndexOutOfRangeException("position is out of range");

            port.WriteLine(String.Format("#{0}P{1}", servo, pos));
        }

        public void KillServo(int servo)
        {
            if (servo < 0 || servo >= 32)
                throw new IndexOutOfRangeException("servo number is out of range");

            port.WriteLine(String.Format("#{0}L", servo));
        }

        private string FindHexyPort()
        {
            var ports = SerialPort.GetPortNames();
            foreach (var portName in ports)
            {
                SerialPortFixer.Execute(portName);
                using (var serial = new SerialPort(portName, 9600))
                {
                    serial.WriteTimeout = 1000;
                    serial.ReadTimeout = 1000;
                    serial.DtrEnable = true;

                    try
                    {
                        serial.Open();

                        serial.Write("V");

                        // Give board time to respond
                        Thread.Sleep(100);

                        var check = serial.ReadLine();

                        if (check.StartsWith("SERVOTOR"))
                        {
                            return portName;
                        }
                    }
                    catch (TimeoutException) // Timeout just means the port didn't respond in time
                    { }
                    finally
                    {
                        if (serial.IsOpen)
                            serial.Close();
                    }
                }
            }

            return "";
        }

        #region IDisposable Members

        private bool disposed;

        ~ServotorComms()
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

                    if (port != null)
                        port.Dispose();
                }

                // Clean up unmanaged resources.

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}