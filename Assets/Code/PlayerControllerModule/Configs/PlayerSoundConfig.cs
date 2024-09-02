using UnityEngine;

namespace Code.PlayerControllerModule.Configs
{
    [CreateAssetMenu(fileName = "PlayerSoundConfig", menuName = "Configs/PlayerControllerModule/PlayerSoundConfig")]
    public class PlayerSoundConfig : ScriptableObject
    {
        [field: Header("Steps")]
        [field: SerializeField] public AudioClip Steps { get; private set; }
        [field: SerializeField] public AudioClip StepsFast { get; private set; }
        
        [field: Header("Jump")]
        [field: SerializeField] public AudioClip JumpSound { get; private set; }
    }
}