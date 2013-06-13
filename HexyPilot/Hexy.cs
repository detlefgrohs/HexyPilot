using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace HexyPilot
{
    public class Hexy : IDisposable
    {
        private SerialPort port;
        private int[] offsets;

        public Hexy()
        {
            LoadOffsets();

            string hexyPort = "";

            // Find the serial port
            var ports = SerialPort.GetPortNames();
            foreach (var portName in ports)
            {
                using (var serial = new SerialPort(portName, 9600))
                {
                    serial.WriteTimeout = 1000;
                    serial.ReadTimeout = 1000;
                    serial.DtrEnable = true;

                    string check = "";

                    serial.DataReceived += (s, e) =>
                    {
                        check = serial.ReadExisting();
                    };

                    try
                    {
                        serial.Open();

                        serial.Write("V");

                        Thread.Sleep(100);

                        if (check.StartsWith("SERVOTOR"))
                        {
                            hexyPort = portName;
                            break;
                        }
                    }
                    catch (TimeoutException)
                    { }
                    catch (IOException)
                    { }
                    finally
                    {
                        if (serial.IsOpen)
                            serial.Close();
                    }
                }
            }

            if (string.IsNullOrEmpty(hexyPort))
                throw new Exception("Hexy port not found");

            port = new SerialPort(hexyPort, 9600);
            port.DtrEnable = true;
            port.Open();
        }

        public void SetNeck(double degrees)
        {
            // Clamp angle between -90 and 90
            degrees = Math.Max(-90, Math.Min(90, degrees));

            var servoPos = (int)Math.Round(1500.0 + degrees * (1000.0 / 90.0));
            SetServo(31, servoPos);
        }

        private void SetServo(int servo, int pos)
        {
            port.WriteLine(String.Format("#{0}P{1}", servo, pos + offsets[servo]));
        }

        private void KillServo(int servo)
        {
            port.WriteLine(String.Format("#{0}L", servo));
        }

        private void LoadOffsets()
        {
            offsets = new int[32];

            var config = Directory.GetFiles(Environment.CurrentDirectory, "*.cfg").FirstOrDefault();

            if (string.IsNullOrEmpty(config))
                return;

            using (var stream = new StreamReader(config))
            {
                var line = stream.ReadLine();

                if (line != "[offsets]")
                    return;

                while (!stream.EndOfStream)
                {
                    line = stream.ReadLine();

                    var match = Regex.Match(line, @"0(\d\d) = (\d+)");
                    if (match.Success)
                    {
                        var servo = Convert.ToInt32(match.Groups[1].Value);
                        var offset = Convert.ToInt32(match.Groups[2].Value);

                        offsets[servo] = offset;
                    }
                }
            }
        }

        #region IDisposable Members

        private bool disposed;

        ~Hexy()
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

                    // Kill all the servos
                    for (int i = 0; i < 32; i++)
                        KillServo(i);

                    if (port != null && port.IsOpen)
                        port.Close();
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