using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Speedometer
{
    [RequireComponent(typeof(Rigidbody))]
    public class ShipMovement : MonoBehaviour
    {
        [SerializeField]
        float acceleration = 1f;

        [SerializeField]
        float deceleration = 1f;

        [SerializeField]
        float rotationSpeed = 1f;

        public float maxSpeed = 25f;

        public Vector3 velocity {
            get
            {
                return rb.velocity;
            }
        }

        Rigidbody rb;

        Vector3 lastPosition;
        // Start is called before the first frame update
        void Start()
        {
            lastPosition = transform.position;

            rb = GetComponent<Rigidbody>();

        }

        // Update is called once per frame
        void Update()
        {
            Steering(Input.GetAxis("Horizontal"), Input.GetAxis("Pitch"));

            if (velocity.magnitude > maxSpeed)
                NormalizeSpeed();
        }
        private void FixedUpdate()
        {
            Acceleration(Input.GetAxis("Vertical"));
            

            Debug.Log("Speed: " + velocity.magnitude);
            lastPosition = transform.position;
        }
        void Steering(float horizontalAxis, float pitchAxis)
        {
            if (Mathf.Abs(horizontalAxis) > 0f || Mathf.Abs(pitchAxis) > 0f)
                transform.Rotate(rotationSpeed * pitchAxis, rotationSpeed * horizontalAxis, 0, Space.Self);


        }

        void Acceleration(float verticalAxis)
        {
            if (Mathf.Abs(verticalAxis) == 0f)
            {
                Decelerate();
                return;
            }

            rb.AddForce(transform.forward * acceleration * verticalAxis);
        }

        void NormalizeSpeed()
        {
            rb.velocity = velocity.normalized * maxSpeed;
        }
        
        void Decelerate()
        {
            rb.AddForce(velocity * -1f * deceleration);
        }
    }
}
