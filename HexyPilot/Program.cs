﻿using System;
using System.Threading;

namespace HexyPilot
{
    public class Program
    {
        private static bool exit;
        private static int leds = 1;

        public static void Main(string[] args)
        {
            Console.WriteLine("Press button 1 + 2 on your Wii Remote...");

            Thread.Sleep(1000);

#if NET
            using (var wm = new WiimoteWin32())
#elif MONO
            using (var wm = new WiimoteMono())
#endif
            {
                wm.Connect();

                wm.WiimoteChanged += wm_WiimoteChanged;

                Console.WriteLine("Wii Remote connected...");
                Console.WriteLine("Press the HOME button to disconnect the Wii and end the application");

                wm.SetLEDs(leds);

                while (!exit)
                {
                }
            }
        }

        private static void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            var wiimote = (IWiimote)sender;
            WiimoteState ws = e.WiimoteState;

            if (ws.ButtonState.A) Console.WriteLine("A");
            if (ws.ButtonState.B) Console.WriteLine("B");
            if (ws.ButtonState.Down) Console.WriteLine("Down");
            if (ws.ButtonState.Home) Console.WriteLine("Home");
            if (ws.ButtonState.Left) Console.WriteLine("Left");
            if (ws.ButtonState.Minus) Console.WriteLine("Minus");
            if (ws.ButtonState.One) Console.WriteLine("One");
            if (ws.ButtonState.Plus) Console.WriteLine("Plus");
            if (ws.ButtonState.Right) Console.WriteLine("Right");
            if (ws.ButtonState.Two) Console.WriteLine("Two");
            if (ws.ButtonState.Up) Console.WriteLine("Up");

            if (ws.ButtonState.Home)
                exit = true;

            wiimote.SetRumble(ws.ButtonState.B);

            if (ws.ButtonState.Plus && leds < 8)
                leds = leds << 1;

            if (ws.ButtonState.Minus && leds > 1)
                leds = leds >> 1;

            Console.WriteLine(leds);

            wiimote.SetLEDs(leds);
        }

        private static void wm_WiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs e)
        {
        }
    }
}