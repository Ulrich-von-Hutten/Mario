using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D bc2d;
    [SerializeField] private LayerMask platformLayerMask;


    public float x = 0;

    public float moveSpeed = 5f;

    public float jumpForce = 5f;
    public float jumpTime = 0.2f;

    private float jumpTimeCounter = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButton("Jump") && jumpTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
        }
    }

    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, bc2d.bounds.extents.y + 0.1f, platformLayerMask);
        return raycastHit.collider != null;
    }
}
