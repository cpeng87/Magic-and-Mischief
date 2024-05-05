using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpell : MonoBehaviour
{
    public Projectile bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CastSpell(int manaCost)
    {
        if (GameManager.instance.player.mana.currVal < manaCost)
        {
            return false;
        }
        GameManager.instance.player.pa.AnimateSpellcast();
        Projectile newBullet = Instantiate(bullet);
        newBullet.SetDirection();
        GameManager.instance.player.mana.SubtractVal(manaCost);
        return true;
    }
}
