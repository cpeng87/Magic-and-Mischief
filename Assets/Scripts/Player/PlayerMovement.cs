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
        GameObject raycastResult = CheckRaycast();
        if (raycastResult != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Interacting(raycastResult);
            }
        }
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, currDirection, 64f, layerMask);
        // if (hit)
        // {
        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         Interacting(hit.collider.gameObject);
        //         // hit.collider.gameObject.GetComponent<Interactable>().Interact();
        //     }
        // }
    }

    public GameObject CheckRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currDirection, 64f, layerMask);
        if (hit)
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private void Interacting(GameObject interactedObj)
    {
        NPCCharacter npcCharacter = interactedObj.GetComponent<NPCCharacter>();
        // if (npcCharacter != null)
        // {   InventoryManager inventory = GetComponent<InventoryManager>();
        //     if (inventory.GetInventory("Toolbar").selectedSlot != null)
        //     {
        //         Item item = GameManager.instance.itemManager.GetItemByName(inventory.GetInventory("Toolbar").selectedSlot.itemName);
        //         bool result = npcCharacter.GiveGift(item);
        //         if (result)
        //         {

        //             GameManager.instance.itemManager.UseItem(inventory.GetInventory("Toolbar").selectedSlot.itemName, inventory.GetInventory("Toolbar").selectedSlot);
        //         }
        //         Debug.Log(npcCharacter.GetAffectionLevel());
        //         return;
        //     }
        // }
        interactedObj.GetComponent<Interactable>().Interact();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // Perform movement
            rb.MovePosition(transform.position + dir * moveSpeed * UnityEngine.Time.fixedDeltaTime);
        }
    }

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

    public void IncreaseSpeed(float amount)
    {
        moveSpeed += amount;
    }
    public void DecreaseSpeed(float amount)
    {
        moveSpeed -= amount;
        if (moveSpeed < 0.1)
        {
            moveSpeed = 0.1f;
        }
    }
    public void SetCurrDirection(Vector3 newDir)
    {
        currDirection = newDir;
    }
    public Vector3 GetCurrDirection()
    {
        return currDirection;
    }
}
