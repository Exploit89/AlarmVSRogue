using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumeStep;
    [SerializeField] private EntryExitTrigger _trigger;

    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
        _trigger.DoorTriggerActivated += VolumeChange;
        _trigger.DoorTriggerDeactivated.AddListener(VolumeChange);
    }

    public void VolumeChange()
    {
        _alarmSound.Play();

        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        if(_trigger.IsPlayerInside)
            _currentCoroutine = StartCoroutine(ChangeSoundVolume(_maxVolume));
        else
            _currentCoroutine = StartCoroutine(ChangeSoundVolume(_minVolume));
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
