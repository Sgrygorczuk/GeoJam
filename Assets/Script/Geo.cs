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
    public SpriteRenderer animationRender;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;


    public Sprite[] shapes;

    public int _shapeIndex = 0;

    public Transform respawnPoint;
    private Vector3 origin;

    public Animator animatorGeo;
    public Animator animationFade;

    //================================================= Collision  =====================================================
    private bool _isGrounded;
    private bool _isColored;
    public float checkRadius;
    public Transform leftGroundCheck;
    public Transform rightGroundCheck;
    public Transform leftCheck;
    public Transform rightCheck;
    public LayerMask whatIsGround;
    public LayerMask whatIsColor;

    public AudioSource dieSFX;
    public AudioSource splashSFX;
    
    private bool _isTouchingLeft;
    private bool _isTouchingRight;
    public bool _stopMovement = true;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.sharedMaterial.bounciness = 1;
        circleCollider2D.enabled = true;
        boxCollider2D.enabled = false;
        print(_shapeIndex);
        if(_shapeIndex == 1){animatorGeo.SetInteger("State", 1);}
        spriteRenderer.sprite = shapes[_shapeIndex];

        origin = new Vector3(respawnPoint.position.x, respawnPoint.position.y, respawnPoint.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        //Updates Horizontal Movement 
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(changeShape());
            
            _shapeIndex++;
            if (_shapeIndex == 2) { _shapeIndex = 0; }

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
        
        if (Input.GetKeyDown(KeyCode.R)) { Restart(); }
        

        _isGrounded = (Physics2D.OverlapCircle(leftGroundCheck.position, checkRadius / 2f, whatIsGround)) ||
                      (Physics2D.OverlapCircle(rightGroundCheck.position, checkRadius / 2f, whatIsGround));
        _isTouchingLeft = Physics2D.OverlapCircle(leftCheck.position, checkRadius, whatIsGround);
        _isTouchingRight = Physics2D.OverlapCircle(rightCheck.position, checkRadius, whatIsGround);

        _isColored = Physics2D.OverlapCircle(_rigidbody2D.position, 2, whatIsGround);
    }

    private IEnumerator changeShape()
    {
        animatorGeo.SetInteger("State", _shapeIndex == 0 ? 1 : 0);
        yield return new WaitForSeconds(1f);
        print("Change");
    }
    
    //Purpose: Updates inputs 
    private void FixedUpdate()
    {
        if(_stopMovement){UpdateMovement();}
    }

    //Purpose: Updates player movement 
    private void UpdateMovement()
    {
        //Updates Horizontal Movement 
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _xInput = -Speed;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _xInput = Speed;
        }
        else
        {
            _xInput = 0;
        }

        var xVelocity = _xInput;
        if (_isTouchingLeft && _xInput < 0 || _isTouchingRight && _xInput > 0)
        {
            xVelocity = 0;
        }

        var yVelocity = _rigidbody2D.velocity.y > 20 ? 20 : _rigidbody2D.velocity.y;
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
            StartCoroutine(Respawn());
        }

        if (hitBox.CompareTag($"Colored"))
        {
            _isColored = hitBox.gameObject.GetComponent<SpriteRenderer>().color == spriteRenderer.color;
        }
    }

    private IEnumerator Respawn()
    {
        if(!dieSFX.isPlaying){dieSFX.Play(0);}
        animationFade.Play("Fade");
        _rigidbody2D.velocity = new Vector2(0, 0);
        
        yield return new WaitForSeconds(0.4f);
        
        var position = respawnPoint.position;
        _rigidbody2D.position = new Vector2(position.x, position.y);
        circleCollider2D.enabled = true;
        boxCollider2D.enabled = false;
        spriteRenderer.color = Color.black;
        animationRender.color = Color.black;
    }

    private void Restart()
    {
        respawnPoint.position = origin;
        StartCoroutine(Respawn());
    }

    
//================================================= Functions Called in Other Scripts ==============================
    
    public void Teleport(float x, float y) { _rigidbody2D.position = new Vector3(x, y); }

    public void UpdateColor(Color color)
    {
        if(!splashSFX.isPlaying){splashSFX.Play(0);}
        spriteRenderer.color = color;
        animationRender.color = color;
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

    public void SetStopMovement()
    {
        _stopMovement = false;
        _shapeIndex = 1;
    }

}
