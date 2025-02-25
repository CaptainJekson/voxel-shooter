using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;


    private int _count = 30;
    
    private float _time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_count <= 0)
        {
            return;
        }
        
        _time += Time.deltaTime;
        
        if (_time < 0.15f)
        {
            return;
        }

        _time = 0;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            _count--;
        
            Debug.LogError($"{_count}");
            _audioSource.Play();
        }
    }
}
