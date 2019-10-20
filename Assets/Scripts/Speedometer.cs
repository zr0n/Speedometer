using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

namespace Speedometer
{
    [RequireComponent(typeof(ProgressBarCircle))]
    public class Speedometer : MonoBehaviour
    {
        [SerializeField]
        ShipMovement ship;

        [SerializeField]
        string portName = "COM3";

        SerialPort arduino;

        float speedPerc;

        ProgressBarCircle pb;

        // Start is called before the first frame update
        void Start()
        {
            pb = GetComponent<ProgressBarCircle>();
            arduino = new SerialPort(portName, 9600);
            arduino.Open();
        }

        // Update is called once per frame
        void Update()
        {
            if (!ship)
                return;

            speedPerc = (ship.velocity.magnitude / ship.maxSpeed) * 100f;

            pb.BarValue = speedPerc;

            if(speedPerc > 1f)
            {
                arduino.Write("1");

                if (speedPerc > 25f)
                {
                    arduino.Write("2");
                    if (speedPerc > 88f)
                        arduino.Write("3");
                    else
                        arduino.Write("6");
                        
                }
                else
                {
                    arduino.Write("5");
                    arduino.Write("6");
                }
            }
            else
            {
                arduino.Write("4");
                arduino.Write("5");
                arduino.Write("6");
            }
        }
    }

}
