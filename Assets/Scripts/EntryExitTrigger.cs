using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Alarm))]

public class EntryExitTrigger : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    public bool IsPlayerInside { get; private set; }

    public event Action DoorTriggerActivated;
    public UnityEvent DoorTriggerDeactivated = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            IsPlayerInside = true;
            DoorTriggerActivated?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            IsPlayerInside = false;
            DoorTriggerDeactivated?.Invoke();
        }
    }
}
