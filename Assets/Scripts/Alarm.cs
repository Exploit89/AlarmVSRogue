using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumeStep;
    [SerializeField] private EntryExitTrigger _entryExitTrigger;


    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _entryExitTrigger.DoorTriggerActivated += VolumeChange(_maxVolume);
    }

    private void VolumeChange(float targetVolume)
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
