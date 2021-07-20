using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geo : MonoBehaviour
{
    //==================================================================================================================
    // Variables 
    //==================================================================================================================

    //====================================================== Misc  =====================================================
    private float _xInput; //The Horizontal Movement Input 
    private const float Speed = 7.5f; //The Speed at which the player moves 

    private Rigidbody2D _rigidbody2D;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;


    public Sprite[] shapes;

    private int _shapeIndex;

    public Transform respawnPoint;

    //================================================= Collision  =====================================================
    private bool _isGrounded;
    public float checkRadius;
    public Transform leftGroundCheck;
    public Transform rightGroundCheck;
    public Transform leftCheck;
    public Transform rightCheck;
    public LayerMask whatIsGround;

    private bool _isTouchingLeft;
    private bool _isTouchingRight;

    // Start is called before the first frame update
    void Start()
    {
        _shapeIndex = 0;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.sharedMaterial.bounciness = 1;
        circleCollider2D.enabled = true;
        boxCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates Horizontal Movement 
        if (Input.GetKeyDown(KeyCode.E))
        {
            _shapeIndex++;
            if (_shapeIndex == 2)
            {
                _shapeIndex = 0;
            }

            spriteRenderer.sprite = shapes[_shapeIndex];

            if (_shapeIndex == 0)
            {
                circleCollider2D.enabled = true;
                boxCollider2D.enabled = false;
                _rigidbody2D.velocity =
                    new Vector2(_rigidbody2D.velocity.x, _isGrounded ? 10 : _rigidbody2D.velocity.y);
            }
            else
            {
                circleCollider2D.enabled = false;
                boxCollider2D.enabled = true;
            }

        }

        _isGrounded = (Physics2D.OverlapCircle(leftGroundCheck.position, checkRadius/2f, whatIsGround)) ||
                      (Physics2D.OverlapCircle(rightGroundCheck.position, checkRadius/2f, whatIsGround));
        _isTouchingLeft = Physics2D.OverlapCircle(leftCheck.position, checkRadius, whatIsGround);
        _isTouchingRight = Physics2D.OverlapCircle(rightCheck.position, checkRadius, whatIsGround);
            
    }
    
    
    //Purpose: Updates inputs 
    private void FixedUpdate()
    {
       UpdateMovement();
    }

    //Purpose: Updates player movement 
    private void UpdateMovement()
    {
        //Updates Horizontal Movement 
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){ _xInput = -Speed; }
        else if(Input.GetKey(KeyCode.D) ||Input.GetKey(KeyCode.RightArrow)){ _xInput = Speed; }
        else{ _xInput = 0; }

        var xVelocity = _xInput;
        if (_isTouchingLeft && _xInput < 0 || _isTouchingRight && _xInput > 0) { xVelocity = 0; }

        var yVelocity = _rigidbody2D.velocity.y > 20 ?  20 : _rigidbody2D.velocity.y;
        if (_shapeIndex == 1)
        {
            yVelocity = _isGrounded ? 0 : yVelocity > -7.5f ? -7.5f : yVelocity;
        }

        _rigidbody2D.velocity = new Vector2(xVelocity, yVelocity); 
    }

    /**
    * Input: hitBox
    * Purpose: Check if the player enters into any triggering hitBoxes   
    */
    private void OnTriggerEnter2D(Collider2D hitBox)
    {
        if (hitBox.CompareTag($"Spike"))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        var position = respawnPoint.position;
        _rigidbody2D.velocity = new Vector2(0, 0);
        _rigidbody2D.position = new Vector2(position.x, position.y);
        spriteRenderer.color = Color.black;
    }
    

    //================================================= Functions Called in Other Scripts ==============================
    
    public void Teleport(float x, float y) { _rigidbody2D.position = new Vector3(x, y); }

    public void UpdateColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void UpdateCheckpoint(Vector3 position)
    {
        respawnPoint.position = position;
    }
    
    public Vector2 GetPosition()
    {
        return _rigidbody2D.position;
    }
    
    public Vector2 GetVelocity()
    {
        return _rigidbody2D.velocity;
    }

    public void SetVelocity(float x, float y)
    {
        _rigidbody2D.velocity = new Vector2(x, y);
    }

}
