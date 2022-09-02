using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumeStep;

    private EntryExitTrigger _entryExitTrigger;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        _entryExitTrigger.DoorTriggerActivated += VolumeChange(_maxVolume);
    }

    void OnEnable()
    {
        EntryExitTrigger.DoorTriggerActivated += VolumeChange(_maxVolume);
    }

    void OnDisable()
    {
        
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
