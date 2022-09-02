using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumeStep;

    private Coroutine _currentCoroutine;


    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    public void VolumeChange(float targetVolume)
    {
        _alarmSound.Play();

        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(ChangeSoundVolume(targetVolume));
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
