using System;

namespace HexyPilot.Wii
{
    public interface IWiimote
    {
        /// <summary>
        /// Event raised when Wiimote state is changed
        /// </summary>
        event EventHandler<WiimoteChangedEventArgs> WiimoteChanged;

        /// <summary>
        /// Event raised when an extension is inserted or removed
        /// </summary>
        event EventHandler<WiimoteExtensionChangedEventArgs> WiimoteExtensionChanged;

        /// <summary>
        /// Connect to a Wiimote paired to the PC via Bluetooth
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnect from the controller and stop reading data from it
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Set Wiimote reporting mode (if using an IR report type, IR sensitivity is set to WiiLevel3)
        /// </summary>
        /// <param name="type">Report type</param>
        /// <param name="continuous">Continuous data</param>
        void SetReportType(InputReport type, bool continuous);

        /// <summary>
        /// Set Wiimote reporting mode
        /// </summary>
        /// <param name="type">Report type</param>
        /// <param name="irSensitivity">IR sensitivity</param>
        /// <param name="continuous">Continuous data</param>
        void SetReportType(InputReport type, IRSensitivity irSensitivity, bool continuous);

        /// <summary>
        /// Set the LEDs on the Wiimote
        /// </summary>
        /// <param name="led1">LED 1</param>
        /// <param name="led2">LED 2</param>
        /// <param name="led3">LED 3</param>
        /// <param name="led4">LED 4</param>
        void SetLEDs(bool led1, bool led2, bool led3, bool led4);

        /// <summary>
        /// Set the LEDs on the Wiimote
        /// </summary>
        /// <param name="leds">The value to be lit up in base2 on the Wiimote</param>
        void SetLEDs(int leds);

        /// <summary>
        /// Toggle rumble
        /// </summary>
        /// <param name="on">On or off</param>
        void SetRumble(bool on);

        /// <summary>
        /// Retrieve the current status of the Wiimote and extensions.  Replaces GetBatteryLevel() since it was poorly named.
        /// </summary>
        void GetStatus();

        /// <summary>
        /// Read data or register from Wiimote
        /// </summary>
        /// <param name="address">Address to read</param>
        /// <param name="size">Length to read</param>
        /// <returns>Data buffer</returns>
        byte[] ReadData(int address, short size);

        /// <summary>
        /// Write a single byte to the Wiimote
        /// </summary>
        /// <param name="address">Address to write</param>
        /// <param name="data">Byte to write</param>
        void WriteData(int address, byte data);

        /// <summary>
        /// Write a byte array to a specified address
        /// </summary>
        /// <param name="address">Address to write</param>
        /// <param name="size">Length of buffer</param>
        /// <param name="buff">Data buffer</param>
        void WriteData(int address, byte size, byte[] buff);

        /// <summary>
        /// Current Wiimote state
        /// </summary>
        WiimoteState WiimoteState { get; }
    }
}