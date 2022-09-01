using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumeStep;

    private Coroutine _currentCoroutine;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;

    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _alarmSound.Play();

            if(_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(ChangeSoundVolume(_maxVolume));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            StopCoroutine(_currentCoroutine);
            StartCoroutine(ChangeSoundVolume(_minVolume));
        }
    }

    private IEnumerator ChangeSoundVolume(float volume)
    {
        var _waitingTime = new WaitForSeconds(1f);

        while(_alarmSound.volume != volume)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, volume, _volumeStep);
            yield return _waitingTime;
        }
    }
}
