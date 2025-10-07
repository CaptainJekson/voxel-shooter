using Code.PlayerControllerModule.Interfaces;
using UnityEngine;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerDesktopInputProvider : IPlayerInputProvider
    {
        public Vector3 GetMoveDirection()
        {
            return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
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