using Code.GlobalUtils.MonoProviders;
using UnityEngine;

namespace Code.PlayerControllerModule.Views
{
    public class PlayerView : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] public CharacterController characterController;
        [SerializeField] public Transform headTransform;
        [SerializeField] public OnControllerColliderHitProvider colliderHitProvider;
        [SerializeField] public AudioSource mouthAudioSource;

        [HideInInspector] public bool isGrounded;
        [HideInInspector] public float verticalVelocity;
        [HideInInspector] public float currentXRotation; 
        [HideInInspector] public float currentYRotation;
        [HideInInspector] public bool isSlide;
    }
}