using System;
using UnityEngine;

[RequireComponent(typeof(Alarm))]

public class EntryExitTrigger : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    public event Action DoorTriggerActivated;
    public event Action DoorTriggerDeactivated;

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
