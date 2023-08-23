using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;

    public float DashVelocity;

    public float DashCooldown;
    public float GroundedRememberTime;
    public float JumpPressedRememberTime;

    public float MaxPlayerSpeed;
    public float PlayerSpeed;

    [Range(0, 1)]
    public float HorizontalDampingBasic;
    [Range(0, 1)]
    public float HorizontalDampingWhenTurning;
    [Range(0, 1)]
    public float HorizontalDampingWhenStopping;


    [Range(0, 1)]
    public float JumpStopMultiplier;
    public float JumpForce;
    private bool DidJumped;

    public float GroundCheckDistance;
    
    private float JumpPressedTime;
    private float GroundedTime;
    private float LastDashTime;

    private float OriginalSpeed;

    public Vector2 BoxCastSize = new Vector2(1, 1);

    [SerializeField]
    private bool IsGrounded;
    public LayerMask GroundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        OriginalSpeed = PlayerSpeed;
    }

    void FixedUpdate()
    {
        #region GroundCheck
RaycastHit2D GroundCheckRay = Physics2D.BoxCast(transform.position, BoxCastSize, 0f, -Vector2.up, GroundCheckDistance, GroundLayer);

        if(GroundCheckRay.collider != null)
        {
            IsGrounded = true;
        } 
        else
        {
            IsGrounded = false;
        }   
        #endregion 

        
        float HorizontalVelocity = rb.velocity.x;
        HorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            HorizontalVelocity *= Mathf.Pow(1f - HorizontalDampingWhenStopping, Time.deltaTime * 10f);
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(HorizontalVelocity))
            HorizontalVelocity *= Mathf.Pow(1f - HorizontalDampingWhenTurning, Time.deltaTime * 10f);
        else
            HorizontalVelocity *= Mathf.Pow(1f - HorizontalDampingBasic, Time.deltaTime * 10f);
        rb.velocity = new Vector2(HorizontalVelocity, rb.velocity.y);


    }



    void Update()
    {
        JumpPressedTime -= Time.deltaTime;
        GroundedTime -= Time.deltaTime;
        LastDashTime -= Time.deltaTime;


        if(Input.GetKeyDown(KeyCode.Space))
        {
            JumpPressedTime = JumpPressedRememberTime;
        }

        if(Input.GetKeyUp(KeyCode.Space) && DidJumped)
        {
            DidJumped = false;
            if(rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * JumpStopMultiplier);
            }
        }

        if(IsGrounded)
        {
            GroundedTime = GroundedRememberTime;
        }

        if(JumpPressedTime > 0 && GroundedTime > 0) 
        {
            JumpPressedTime = 0;
            GroundedTime = 0;
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            DidJumped = true;
        }

        if(Input.GetKey(KeyCode.X) && LastDashTime <= 0)
        {
            LastDashTime = DashCooldown;
            rb.velocity = new Vector2(DashVelocity, rb.velocity.y);
        }

    }

}
