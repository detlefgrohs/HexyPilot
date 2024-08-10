using System;
using System.Runtime.Serialization;

namespace HexyPilot.Wii
{
    /// <summary>
    /// Argument sent through the WiimoteExtensionChangedEvent
    /// </summary>
    public class WiimoteExtensionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The extenstion type inserted or removed
        /// </summary>
        public ExtensionType ExtensionType;

        /// <summary>
        /// Whether the extension was inserted or removed
        /// </summary>
        public bool Inserted;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">The extension type inserted or removed</param>
        /// <param name="inserted">Whether the extension was inserted or removed</param>
        public WiimoteExtensionChangedEventArgs(ExtensionType type, bool inserted)
        {
            ExtensionType = type;
            Inserted = inserted;
        }
    }

    /// <summary>
    /// Argument sent through the WiimoteChangedEvent
    /// </summary>
    public class WiimoteChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The current state of the Wiimote and extension controllers
        /// </summary>
        public WiimoteState WiimoteState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ws">Wiimote state</param>
        public WiimoteChangedEventArgs(WiimoteState ws)
        {
            WiimoteState = ws;
        }
    }

    /// <summary>
    /// Represents errors that occur during the execution of the Wiimote library
    /// </summary>
    [Serializable]
    public class WiimoteException : ApplicationException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public WiimoteException()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Error message</param>
        public WiimoteException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="innerException">Inner exception</param>
        public WiimoteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Streaming context</param>
        protected WiimoteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}