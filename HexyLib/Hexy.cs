using System;
using System.IO;
using System.Text.RegularExpressions;

namespace HexyLib
{
    public class Hexy : IDisposable
    {
        private readonly ServotorComms servotor;
        private readonly int[] offsets;

        public Hexy()
        {
            offsets = new int[32];
            servotor = new ServotorComms();
        }

        public void LoadOffsets(string offsetsFile)
        {
            if (String.IsNullOrEmpty(offsetsFile))
                throw new ArgumentException("offsetsFile is null or empty.", "offsetsFile");

            using (var stream = new StreamReader(offsetsFile))
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

        public void SetNeck(double degrees)
        {
            SetServo(31, degrees);
        }

        private void SetServo(int servo, double degrees)
        {
            // Clamp angle between -90 and 90
            degrees = Math.Max(-90, Math.Min(90, degrees));

            var servoPos = (int)Math.Round(1500.0 + degrees * (1000.0 / 90.0));

            // Adjust pos value with offset and clamp
            servoPos = Math.Max(500, Math.Min(2500, servoPos + offsets[servo]));

            servotor.SetServo(servo, servoPos);
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

                    if (servotor != null)
                        servotor.Dispose();
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