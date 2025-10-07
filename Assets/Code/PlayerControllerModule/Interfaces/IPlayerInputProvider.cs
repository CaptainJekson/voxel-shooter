using UnityEngine;

namespace Code.PlayerControllerModule.Interfaces
{
    public interface IPlayerInputProvider
    {
        Vector3 GetMoveDirection();
        float GetRotationX();
        float GetRotationY();
        bool GetJumpInput();
        bool GetSprintInput();
    }
}