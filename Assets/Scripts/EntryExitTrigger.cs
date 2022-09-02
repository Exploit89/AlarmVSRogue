using UnityEngine;

public class EntryExitTrigger : MonoBehaviour
{
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private Alarm _alarm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
            _alarm.VolumeChange(_maxVolume);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
            _alarm.VolumeChange(_minVolume);
    }
}
