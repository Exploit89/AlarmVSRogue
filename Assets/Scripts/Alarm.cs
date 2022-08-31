using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;

    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _alarmSound.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(_alarmSound.volume <= 1f)
            _alarmSound.volume += 0.001f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
            _alarmSound.Stop();
    }
}
