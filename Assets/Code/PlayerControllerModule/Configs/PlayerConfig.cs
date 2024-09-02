using Code.PlayerControllerModule.Views;
using UnityEngine;

namespace Code.PlayerControllerModule.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerControllerModule/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerView PlayerView { get; private set; }
        [field: SerializeField] public Vector3 StartPosition { get; private set; }
        
        [field: Header("Stats:")]
        [field: SerializeField] public float Gravity { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float SprintMultiplier { get; private set; }
        [field: SerializeField] public float SlidePower { get; private set; }
        [field: SerializeField] public float SlopeLimit { get; private set; }
        [field: SerializeField] public float RaycastGroundCheckDistance { get; private set; }
        [field: SerializeField] public float RotateSpeed { get; private set; }
        [field: SerializeField] public float MinRotationX { get; private set; }
        [field: SerializeField] public float MaxRotationX { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public float StaminaDrainRate { get; private set; }
        [field: SerializeField] public float StaminaRecoveryRate { get; private set; }
        [field: SerializeField] public float StaminaMinStartSprint { get; private set; }
        [field: SerializeField] public float JumpStaminaCost { get; private set; }
        [field: SerializeField] public float MaxStamina { get; private set; }
        [field: SerializeField] public float PushPower { get; private set; }
    }
}