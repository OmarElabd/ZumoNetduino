using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Netduino.Zumo.Motors
{
    public class ZumoMotors
    {
        #region Motor Pins
        private readonly Cpu.Pin PWM_L_PIN = Pins.GPIO_PIN_D10;
        private readonly Cpu.Pin PWM_R_PIN = Pins.GPIO_PIN_D9;
        private readonly Cpu.Pin DIR_L_PIN = Pins.GPIO_PIN_D8;
        private readonly Cpu.Pin DIR_R_PIN = Pins.GPIO_PIN_D7;
        #endregion

        #region Motor Ports
        private SecretLabs.NETMF.Hardware.PWM PWM_L;
        private SecretLabs.NETMF.Hardware.PWM PWM_R;
        private OutputPort DIR_L;
        private OutputPort DIR_R;
        #endregion

        #region Flip Flags
        private static bool flipLeft = false;
        private static bool flipRight = false;
        #endregion

        /// <summary>
        /// Constructor initalizes the motor pins.
        /// </summary>
        public ZumoMotors()
        {
            Init();
        }

        /// <summary>
        /// Initalize the Directional and PWM (Speeds) Ports.
        /// </summary>
        private void Init()
        {
            PWM_L = new SecretLabs.NETMF.Hardware.PWM(PWM_L_PIN);
            PWM_R = new SecretLabs.NETMF.Hardware.PWM(PWM_R_PIN);
            DIR_L = new OutputPort(DIR_L_PIN, false);
            DIR_R = new OutputPort(DIR_R_PIN, false);
        }

        /// <summary>
        /// Enable or Disable the Flipping of Left Motor
        /// </summary>
        /// <param name="flip"></param>
        public void FlipLeftMotor(bool flip)
        {
            flipLeft = flip;
        }


        /// <summary>
        /// Enable or Disable the Flipping of Right Motor
        /// </summary>
        /// <param name="flip"></param>
        public void FlipRightMotor(bool flip)
        {
            flipRight = flip;
        }


        /// <summary>
        /// Set the speed for the left motor any negative value will reverse direction and set the speed. A value of 0 will stop a motor.
        /// </summary>
        /// <param name="speed">Value from -100 to 100 described as a percentage of the maximum speed of the left motor.</param>
        public void SetLeftSpeed(int speed)
        {
            //init(); // initialize if necessary

            bool reverse = false;

            if (speed < 0)
            {
                speed = -speed; // make speed a positive quantity
                reverse = true;    // preserve the direction
            }
            if (speed > 100)  // Max 
                speed = 100;

            PWM_L.SetDutyCycle((uint)speed);

            if (reverse ^ flipLeft) // flip if speed was negative or flipLeft setting is active, but not both. Xor Operation.
                DIR_L.Write(true);
            else
                DIR_L.Write(false);
        }

        /// <summary>
        /// Set the speed for the right motor any negative value will reverse direction and set the speed. A value of 0 will stop a motor.
        /// </summary>
        /// <param name="speed">Value from -100 to 100 described as a percentage of the maximum speed of the right motor.</param>
        public void SetRightSpeed(int speed)
        {
            bool reverse = false;

            if (speed < 0)
            {
                speed = -speed;  // Make speed a positive quantity
                reverse = true;  // Preserve the direction
            }
            if (speed > 100)  // Max PWM dutycycle
                speed = 100;

            PWM_R.SetDutyCycle((uint)speed);

            if (reverse ^ flipRight) // flip if speed was negative or flipRight setting is active, but not both. Xor Operation.
                DIR_R.Write(true);
            else
                DIR_R.Write(false);
        }

        /// <summary>
        /// Set the speed for the right and left motors any negative value will reverse direction and set the speed. A value of 0 will stop a motor.
        /// </summary>
        /// <param name="leftSpeed">Value from -100 to 100 described as a percentage of the maximum speed of the left motor.</param>
        /// <param name="rightSpeed">Value from -100 to 100 described as a percentage of the maximum speed of the right motor.</param>
        public void SetSpeeds(int leftSpeed, int rightSpeed)
        {
            SetLeftSpeed(leftSpeed);
            SetRightSpeed(rightSpeed);
        }
    }
}



