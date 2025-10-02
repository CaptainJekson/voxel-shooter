using Code.PlayerModule.Interfaces;
using UnityEngine;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerDesktopInputProvider : IPlayerInputProvider
    {
        public Vector3 GetMoveDirection()
        {
            var x = 0f;
            var z = 0f;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                x = -1f;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                x = 1f;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                z = -1f;
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                z = 1f;
            }

            return new Vector3(x, 0, z);
        }

        public float GetRotationX()
        {
            return Input.GetAxis("Mouse Y") * Time.deltaTime;
        }

        public float GetRotationY()
        {
            return Input.GetAxis("Mouse X") * Time.deltaTime;
        }

        public bool GetJumpInput()
        {
            return Input.GetButton("Jump");
        }

        public bool GetSprintInput()
        {
            return Input.GetButton("Fire3");
        }
    }
}