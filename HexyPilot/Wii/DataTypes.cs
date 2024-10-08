﻿using System.Drawing;

namespace HexyPilot.Wii
{
    /// <summary>
    /// Point structure for floating point 3D positions (X, Y, Z)
    /// </summary>
    public struct Point3F
    {
        /// <summary>
        /// X, Y, Z coordinates of this point
        /// </summary>
        public float X, Y, Z;

        /// <summary>
        /// Convert to human-readable string
        /// </summary>
        /// <returns>A string that represents the point</returns>
        public override string ToString()
        {
            return string.Format("{{X={0}, Y={1}, Z={2}}}", X, Y, Z);
        }
    }

    /// <summary>
    /// Point structure for int 3D positions (X, Y, Z)
    /// </summary>
    public struct Point3
    {
        /// <summary>
        /// X, Y, Z coordinates of this point
        /// </summary>
        public int X, Y, Z;

        /// <summary>
        /// Convert to human-readable string
        /// </summary>
        /// <returns>A string that represents the point.</returns>
        public override string ToString()
        {
            return string.Format("{{X={0}, Y={1}, Z={2}}}", X, Y, Z);
        }
    }

    /// <summary>
    /// Current overall state of the Wiimote and all attachments
    /// </summary>
    public class WiimoteState
    {
        /// <summary>
        /// Current calibration information
        /// </summary>
        public AccelCalibrationInfo AccelCalibrationInfo = new AccelCalibrationInfo();

        /// <summary>
        /// Current state of accelerometers
        /// </summary>
        public AccelState AccelState = new AccelState();

        /// <summary>
        /// Current state of buttons
        /// </summary>
        public ButtonState ButtonState = new ButtonState();

        /// <summary>
        /// Current state of IR sensors
        /// </summary>
        public IRState IRState = new IRState();

        /// <summary>
        /// Current battery level
        /// </summary>
        public byte Battery;

        /// <summary>
        /// Current state of rumble
        /// </summary>
        public bool Rumble;

        /// <summary>
        /// Is an extension controller inserted?
        /// </summary>
        public bool Extension;

        /// <summary>
        /// Extension controller currently inserted, if any
        /// </summary>
        public ExtensionType ExtensionType;

        /// <summary>
        /// Current state of Nunchuk extension
        /// </summary>
        public NunchukState NunchukState = new NunchukState();

        /// <summary>
        /// Current state of Classic Controller extension
        /// </summary>
        public ClassicControllerState ClassicControllerState = new ClassicControllerState();

        /// <summary>
        /// Current state of Guitar extension
        /// </summary>
        public GuitarState GuitarState = new GuitarState();

        /// <summary>
        /// Current state of LEDs
        /// </summary>
        public LEDState LEDState;

        /// <summary>
        /// Constructor for WiimoteState class
        /// </summary>
        public WiimoteState()
        {
            IRState.IRSensors = new IRSensor[4];
        }
    }

    /// <summary>
    /// Current state of LEDs
    /// </summary>
    public struct LEDState
    {
        /// <summary>
        /// LED on the Wiimote
        /// </summary>
        public bool LED1, LED2, LED3, LED4;
    }

    /// <summary>
    /// Calibration information stored on the Nunchuk
    /// </summary>
    public struct NunchukCalibrationInfo
    {
        /// <summary>
        /// Accelerometer calibration data
        /// </summary>
        public AccelCalibrationInfo AccelCalibration;

        /// <summary>
        /// Joystick X-axis calibration
        /// </summary>
        public byte MinX, MidX, MaxX;

        /// <summary>
        /// Joystick Y-axis calibration
        /// </summary>
        public byte MinY, MidY, MaxY;
    }

    /// <summary>
    /// Calibration information stored on the Classic Controller
    /// </summary>
    public struct ClassicControllerCalibrationInfo
    {
        /// <summary>
        /// Left joystick X-axis
        /// </summary>
        public byte MinXL, MidXL, MaxXL;

        /// <summary>
        /// Left joystick Y-axis
        /// </summary>
        public byte MinYL, MidYL, MaxYL;

        /// <summary>
        /// Right joystick X-axis
        /// </summary>
        public byte MinXR, MidXR, MaxXR;

        /// <summary>
        /// Right joystick Y-axis
        /// </summary>
        public byte MinYR, MidYR, MaxYR;

        /// <summary>
        /// Left analog trigger
        /// </summary>
        public byte MinTriggerL, MaxTriggerL;

        /// <summary>
        /// Right analog trigger
        /// </summary>
        public byte MinTriggerR, MaxTriggerR;
    }

    /// <summary>
    /// Current state of the Nunchuk extension
    /// </summary>
    public struct NunchukState
    {
        /// <summary>
        /// Calibration data for Nunchuk extension
        /// </summary>
        public NunchukCalibrationInfo CalibrationInfo;

        /// <summary>
        /// State of accelerometers
        /// </summary>
        public AccelState AccelState;

        /// <summary>
        /// Raw joystick position before normalization.  Values range between 0 and 255.
        /// </summary>
        public Point RawJoystick;

        /// <summary>
        /// Normalized joystick position.  Values range between -0.5 and 0.5
        /// </summary>
        public PointF Joystick;

        /// <summary>
        /// Digital button on Nunchuk extension
        /// </summary>
        public bool C, Z;
    }

    /// <summary>
    /// Curernt button state of the Classic Controller
    /// </summary>
    public struct ClassicControllerButtonState
    {
        /// <summary>
        /// Digital button on the Classic Controller extension
        /// </summary>
        public bool A, B, Plus, Home, Minus, Up, Down, Left, Right, X, Y, ZL, ZR;

        /// <summary>
        /// Analog trigger - false if released, true for any pressure applied
        /// </summary>
        public bool TriggerL, TriggerR;
    }

    /// <summary>
    /// Current state of the Classic Controller
    /// </summary>
    public struct ClassicControllerState
    {
        /// <summary>
        /// Calibration data for Classic Controller extension
        /// </summary>
        public ClassicControllerCalibrationInfo CalibrationInfo;

        /// <summary>
        /// Current button state
        /// </summary>
        public ClassicControllerButtonState ButtonState;

        /// <summary>
        /// Raw value of left joystick.  Values range between 0 - 255.
        /// </summary>
        public Point RawJoystickL;

        /// <summary>
        /// Raw value of right joystick.  Values range between 0 - 255.
        /// </summary>
        public Point RawJoystickR;

        /// <summary>
        /// Normalized value of left joystick.  Values range between -0.5 - 0.5
        /// </summary>
        public PointF JoystickL;

        /// <summary>
        /// Normalized value of right joystick.  Values range between -0.5 - 0.5
        /// </summary>
        public PointF JoystickR;

        /// <summary>
        /// Raw value of analog trigger.  Values range between 0 - 255.
        /// </summary>
        public byte RawTriggerL, RawTriggerR;

        /// <summary>
        /// Normalized value of analog trigger.  Values range between 0.0 - 1.0.
        /// </summary>
        public float TriggerL, TriggerR;
    }

    /// <summary>
    /// Current state of the Guitar controller
    /// </summary>
    public struct GuitarState
    {
        /// <summary>
        /// Current button state of the Guitar
        /// </summary>
        public GuitarButtonState ButtonState;

        /// <summary>
        /// Raw joystick position.  Values range between 0 - 63.
        /// </summary>
        public Point RawJoystick;

        /// <summary>
        /// Normalized value of joystick position.  Values range between 0.0 - 1.0.
        /// </summary>
        public PointF Joystick;

        /// <summary>
        /// Raw whammy bar position.  Values range between 0 - 10.
        /// </summary>
        public byte RawWhammyBar;

        /// <summary>
        /// Normalized value of whammy bar position.  Values range between 0.0 - 1.0.
        /// </summary>
        public float WhammyBar;
    }

    /// <summary>
    /// Current button state of the Guitar controller
    /// </summary>
    public struct GuitarButtonState
    {
        /// <summary>
        /// Strum bar
        /// </summary>
        public bool StrumUp, StrumDown;

        /// <summary>
        /// Fret buttons
        /// </summary>
        public bool Green, Red, Yellow, Blue, Orange;

        /// <summary>
        /// Other buttons
        /// </summary>
        public bool Minus, Plus;
    }

    /// <summary>
    /// Current state of a single IR sensor
    /// </summary>
    public struct IRSensor
    {
        /// <summary>
        /// Raw values of individual sensor.  Values range between 0 - 1023 on the X axis and 0 - 767 on the Y axis.
        /// </summary>
        public Point RawPosition;

        /// <summary>
        /// Normalized values of the sensor position.  Values range between 0.0 - 1.0.
        /// </summary>
        public PointF Position;

        /// <summary>
        /// Size of IR Sensor.  Values range from 0 - 15
        /// </summary>
        public int Size;

        /// <summary>
        /// IR sensor seen
        /// </summary>
        public bool Found;

        /// <summary>
        /// Convert to human-readable string
        /// </summary>
        /// <returns>A string that represents the point.</returns>
        public override string ToString()
        {
            return string.Format("{{{0}, Size={1}, Found={2}}}", Position, Size, Found);
        }
    }

    /// <summary>
    /// Current state of the IR camera
    /// </summary>
    public struct IRState
    {
        /// <summary>
        /// Current mode of IR sensor data
        /// </summary>
        public IRMode Mode;

        /// <summary>
        /// Current state of IR sensors
        /// </summary>
        public IRSensor[] IRSensors;

        /// <summary>
        /// Raw midpoint of IR sensors 1 and 2 only.  Values range between 0 - 1023, 0 - 767
        /// </summary>
        public Point RawMidpoint;

        /// <summary>
        /// Normalized midpoint of IR sensors 1 and 2 only.  Values range between 0.0 - 1.0
        /// </summary>
        public PointF Midpoint;
    }

    /// <summary>
    /// Current state of the accelerometers
    /// </summary>
    public struct AccelState
    {
        /// <summary>
        /// Raw accelerometer data.
        /// <remarks>Values range between 0 - 255</remarks>
        /// </summary>
        public Point3 RawValues;

        /// <summary>
        /// Normalized accelerometer data.  Values range between 0 - ?, but values > 3 and &lt; -3 are inaccurate.
        /// </summary>
        public Point3F Values;
    }

    /// <summary>
    /// Accelerometer calibration information
    /// </summary>
    public struct AccelCalibrationInfo
    {
        /// <summary>
        /// Zero point of accelerometer
        /// </summary>
        public byte X0, Y0, Z0;

        /// <summary>
        /// Gravity at rest of accelerometer
        /// </summary>
        public byte XG, YG, ZG;
    }

    /// <summary>
    /// Current button state
    /// </summary>
    public struct ButtonState
    {
        /// <summary>
        /// Digital button on the Wiimote
        /// </summary>
        public bool A, B, Plus, Home, Minus, One, Two, Up, Down, Left, Right;
    }

    /// <summary>
    /// The extension plugged into the Wiimote
    /// </summary>
    public enum ExtensionType : byte
    {
        /// <summary>
        /// No extension
        /// </summary>
        None = 0x00,

        /// <summary>
        /// Nunchuk extension
        /// </summary>
        Nunchuk = 0xfe,

        /// <summary>
        /// Classic Controller extension
        /// </summary>
        ClassicController = 0xfd,

        // hmm...what's 0xfc?

        /// <summary>
        /// Guitar controller from Guitar Hero
        /// </summary>
        Guitar = 0xfb
    };

    /// <summary>
    /// The mode of data reported for the IR sensor
    /// </summary>
    public enum IRMode : byte
    {
        /// <summary>
        /// IR sensor off
        /// </summary>
        Off = 0x00,

        /// <summary>
        /// Basic mode
        /// </summary>
        Basic = 0x01,	// 10 bytes

        /// <summary>
        /// Extended mode
        /// </summary>
        Extended = 0x03,	// 12 bytes

        /// <summary>
        /// Full mode (unsupported)
        /// </summary>
        Full = 0x05,	// 16 bytes * 2 (format unknown)
    };

    /// <summary>
    /// The report format in which the Wiimote should return data
    /// </summary>
    public enum InputReport : byte
    {
        /// <summary>
        /// Status report
        /// </summary>
        Status = 0x20,

        /// <summary>
        /// Read data from memory location
        /// </summary>
        ReadData = 0x21,

        /// <summary>
        /// Button data only
        /// </summary>
        Buttons = 0x30,

        /// <summary>
        /// Button and accelerometer data
        /// </summary>
        ButtonsAccel = 0x31,

        /// <summary>
        /// IR sensor and accelerometer data
        /// </summary>
        IRAccel = 0x33,

        /// <summary>
        /// Button and extension controller data
        /// </summary>
        ButtonsExtension = 0x34,

        /// <summary>
        /// Extension and accelerometer data
        /// </summary>
        ExtensionAccel = 0x35,

        /// <summary>
        /// IR sensor, extension controller and accelerometer data
        /// </summary>
        IRExtensionAccel = 0x37,
    };

    /// <summary>
    /// Sensitivity of the IR camera on the Wiimote
    /// </summary>
    public enum IRSensitivity
    {
        /// <summary>
        /// Equivalent to level 1 on the Wii console
        /// </summary>
        WiiLevel1,

        /// <summary>
        /// Equivalent to level 2 on the Wii console
        /// </summary>
        WiiLevel2,

        /// <summary>
        /// Equivalent to level 3 on the Wii console (default)
        /// </summary>
        WiiLevel3,

        /// <summary>
        /// Equivalent to level 4 on the Wii console
        /// </summary>
        WiiLevel4,

        /// <summary>
        /// Equivalent to level 5 on the Wii console
        /// </summary>
        WiiLevel5,

        /// <summary>
        /// Maximum sensitivity
        /// </summary>
        Maximum
    }
}