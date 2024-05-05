using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public bool isMoving;
    private Vector2 input;
    private Rigidbody2D rb;

    private Vector3 dir;
    public Vector3 currDirection;
    private bool canMove = true;

    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        // anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        dir = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (canMove)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            dir = new Vector3(x, y).normalized;
            if (dir.magnitude > 0)
            {
                currDirection = dir;
            }
            GameManager.instance.player.pa.AnimateMovement(dir);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currDirection, 64f, layerMask);
        if (hit)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hit.collider.gameObject.GetComponent<Interactable>().Interact();
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // Perform movement
            rb.MovePosition(transform.position + dir * moveSpeed * Time.fixedDeltaTime);
        }
    }

    // private void AnimateMovement()
    // {
    //     if (anim != null)
    //     {
    //         if (dir.magnitude > 0)
    //         {
    //             anim.SetBool("isMoving", true);
    //             anim.SetFloat("Horizontal", dir.x);
    //             anim.SetFloat("Vertical", dir.y);
    //             currDirection = dir;
    //         }
    //         else
    //         {
    //             anim.SetBool("isMoving", false);
    //         }
    //     }
    // }
    // public void AnimateSpellcast()
    // {
    //     if (anim != null)
    //     {
    //         anim.SetTrigger("isSpellcasting");
    //     }
    // }

    public void SetNotMoving()
    {
        canMove = false;
        rb.velocity = Vector3.zero;
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }
}
