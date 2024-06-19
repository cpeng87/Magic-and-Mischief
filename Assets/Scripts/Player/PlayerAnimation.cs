using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Vector3 currDir;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void AnimateMovement(Vector3 dir)
    {
        if (anim != null)
        {
            if (dir.magnitude > 0)
            {
                anim.SetBool("isMoving", true);
                anim.SetFloat("Horizontal", dir.x);
                anim.SetFloat("Vertical", dir.y);
                currDir = dir;
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
    }
    public void AnimateSpellcast()
    {
        if (anim != null)
        {
            anim.SetTrigger("isSpellcasting");
        }
    }
    public void AnimateFlight()
    {
        anim.SetBool("isFlying", true);
    }
    public void StopAnimateFlight()
    {
        anim.SetBool("isFlying", false);
    }
}
