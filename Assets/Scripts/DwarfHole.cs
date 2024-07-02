using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfHole : MonoBehaviour
{
    public DialogueTrigger trigger;
    public BoxCollider2D bc;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        BuffEventHandler.OnFlightChanged += CheckCollider;
        trigger = GetComponent<DialogueTrigger>();
        CheckCollider();
    }

    private void CheckCollider()
    {
        if (GameManager.instance.player.bm.isFlying)
        {
            bc.enabled = false;
        }
        else
        {
            bc.enabled = true;
        } 
    }

    private void OnDestroy()
    {
        BuffEventHandler.OnFlightChanged -= CheckCollider;
    }

}
