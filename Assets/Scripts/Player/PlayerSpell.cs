using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpell : MonoBehaviour
{
    public Projectile bullet;
    private Camera mainCam;
    // Start is called before the first frame update
    void Awake()
    {
        mainCam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CastSpell(int manaCost)
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        if (GameManager.instance.player.mana.currVal < manaCost)
        {
            return false;
        }
        GameManager.instance.player.pa.AnimateSpellcast();
        Projectile newBullet = Instantiate(bullet);
        newBullet.SetDirection(mousePos);
        GameManager.instance.player.mana.SubtractVal(manaCost);
        return true;
    }
}
