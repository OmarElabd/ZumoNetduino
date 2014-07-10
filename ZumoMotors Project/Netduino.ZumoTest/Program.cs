using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using Netduino.Zumo.Motors;

namespace Netduino.ZumoTest
{
    public class Program
    {
        public static void Main()
        {
            // Test Zumo Motors
            ZumoMotors motors = new ZumoMotors(); //Initialize New Zumo Motors
            motors.SetSpeeds(50, 50); //Set Left and Right Motor speeds to 50% each
        }
    }
}
