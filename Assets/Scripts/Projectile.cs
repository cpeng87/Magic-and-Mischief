using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool friendly;
    public float duration;
    public float speed;
    public Vector3 direction;
    private float timer;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Move the projectile forward
        transform.position += direction * speed * Time.deltaTime;

        // Increment the timer
        timer += Time.deltaTime;

        // Check if the projectile has expired
        if (timer >= duration)
        {
            // Destroy the projectile
            Destroy(gameObject);
        }
    }
    public void SetDirection()
    {
        transform.position = GameManager.instance.player.transform.position;
        direction = GameManager.instance.player.pm.currDirection;

        spriteRenderer.flipX = direction.x > 0;
        if (direction.y > 0)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        // Rotate the image -90 degrees if moving down
        else if (direction.y < 0)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else
        {
            // Reset rotation if not moving in any significant direction
            spriteRenderer.transform.rotation = Quaternion.identity;
        }

    }
}
