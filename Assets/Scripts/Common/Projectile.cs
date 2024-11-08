using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool friendly;
    public float duration;
    public float speed;
    public Vector3 direction;
    // public Vector3 rotation;
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
        transform.position += direction * speed * UnityEngine.Time.deltaTime;

        // Increment the timer
        timer += UnityEngine.Time.deltaTime;

        // Check if the projectile has expired
        if (timer >= duration)
        {
            // Destroy the projectile
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 mousePos)
    {
        transform.position = GameManager.instance.player.transform.position;
        direction = mousePos - transform.position;
        direction = new Vector3(direction.x, direction.y, 0).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the GameObject to face the direction
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
