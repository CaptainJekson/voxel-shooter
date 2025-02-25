using UnityEngine;

namespace Code
{
    public class CharacterAnimatedTest : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private void Update()
        {
            var position = transform.position;
            
            position = Vector3.MoveTowards(position, position + Vector3.forward,_speed * Time.deltaTime);
            
            transform.position = position;
        }
    }
}