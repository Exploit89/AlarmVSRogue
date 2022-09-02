using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Alarm))]

public class EntryExitTrigger : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;
    [SerializeField] private UnityEvent _doorTriggerActivated;

    private bool _isPlayerInside;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isPlayerInside = true;
            _doorTriggerActivated?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isPlayerInside = false;
            _doorTriggerActivated?.Invoke();
        }
    }
}
