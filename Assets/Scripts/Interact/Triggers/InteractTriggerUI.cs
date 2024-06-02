using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractTriggerUI<T> : MonoBehaviour, Interactable where T : MonoBehaviour
{
    public T ui;

    protected virtual void Start()
    {
        ui = FindObjectOfType<T>();
    }

    public abstract void Interact();
}
