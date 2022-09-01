using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _alarmSound.volume = 0.1f;
            _alarmSound.Play();

            if(_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(SoundVolumeIncrease());
            }
            else
            {
                _currentCoroutine = StartCoroutine(SoundVolumeIncrease());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            StopCoroutine(_currentCoroutine);
            StartCoroutine(SoundVolumeDecrease());
        }
    }

    private IEnumerator SoundVolumeIncrease()
    {
        var _waitingTime = new WaitForSeconds(1f);

        while(_alarmSound.volume < 1)
        {
            _alarmSound.volume += 0.1f;
            yield return _waitingTime;
        }
    }

    private IEnumerator SoundVolumeDecrease()
    {
        var _waitingTime = new WaitForSeconds(1f);

        while (_alarmSound.volume > 0)
        {
            _alarmSound.volume -= 0.1f;
            yield return _waitingTime;
        }

        StopCoroutine(_currentCoroutine);
    }
}
