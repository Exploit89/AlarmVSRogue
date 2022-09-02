using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Alarm))]

public class EntryExitTrigger : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    [SerializeField] public UnityEvent DoorTriggerActivated { get; private set; }
    [SerializeField] public UnityEvent DoorTriggerDeactivated { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            DoorTriggerActivated?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            DoorTriggerDeactivated?.Invoke();
        }
    }
}
