using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MarioCharacter : MonoBehaviour
{

    // Unfortunately named, fortunately glitched script
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 4;

    private BoxCollider2D boxCollider;

    private Vector2 velocity;

    /// <summary>
    /// Set to true when the character intersects a collider beneath
    /// them in the previous frame.
    /// </summary>
    private bool grounded;

    public bool controlsInverted = false;
    public bool jumpsLimited = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        
        
            float moveInput = 0;
        if (GameController.Instance.allowMovement == true)
        {
            if (controlsInverted == false)
                moveInput = Input.GetAxisRaw("Horizontal");
            else
                moveInput = -Input.GetAxisRaw("Horizontal");
        }
          

            if (moveInput < 0)
            {
                gameObject.GetComponent<Mario>().skeletonSprite.GetComponent<SpriteRenderer>().flipX = true;
                gameObject.GetComponent<Mario>().headSprite.flipX = true;
                gameObject.GetComponent<Mario>().bodySprite.flipX = true;
                gameObject.GetComponent<Mario>().feetSprite.flipX = true;

            }
            else
            {
                gameObject.GetComponent<Mario>().skeletonSprite.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponent<Mario>().headSprite.flipX = false;
                gameObject.GetComponent<Mario>().bodySprite.flipX = false;
                gameObject.GetComponent<Mario>().feetSprite.flipX = false;
            }

            if (grounded)
            {
                velocity.y = 0;

                if (Input.GetButtonDown("Jump"))
                {
                if (GameController.Instance.allowMovement == true)
                {
                    SoundControllerScript.PlaySound("jump");
                    // Calculate the velocity required to achieve the target jump height.
                    if (jumpsLimited == true)
                    { 
                        jumpHeight *= 0.8f;
                        MarioController.Instance.marioAI.CallTrigger(MarioAI.Trigger.JumpEverShorter);
                    }
                    velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
                }
                 
                }
            }

            float acceleration = grounded ? walkAcceleration : airAcceleration;
            float deceleration = grounded ? groundDeceleration : 0;

            if (moveInput != 0)
            { 
                velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime); 
            }
            else
            { 
                    velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime); 
            }

            velocity.y += Physics2D.gravity.y * Time.deltaTime;

            // if no button pressed and grounded are both true then don't
            if ((moveInput == 0 && grounded) == false)
                transform.Translate(velocity * Time.deltaTime);

            
            grounded = false;

            // Retrieve all colliders we have intersected after velocity has been applied.
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

            foreach (Collider2D hit in hits)
            {
                // Ignore our own collider.
                if (hit == boxCollider)
                    continue;

                ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

                // Ensure that we are still overlapping this collider.
                // The overlap may no longer exist due to another intersected collider
                // pushing us out of this one.
                if (colliderDistance.isOverlapped)
                {
                    transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                    // If we intersect an object beneath us, set grounded to true. 
                    if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                    {
                        grounded = true;
                    }
                }
            }
        
      
    }
    public bool IsGrounded()
    {
        return grounded;
    }
}