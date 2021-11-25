using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private ParticleSystem magnetParticle;
    [SerializeField] private ParticleSystem jump_particle; 
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    [SerializeField] private DynamicMagnet _magnet;

    [SerializeField] private string XInput;
    [SerializeField] private string YInput;
    [SerializeField] private string swapInput;
    private bool swapping = false;
    
    private float input_x;
    private float input_y;
    private float input_swap;
    private int jumps;
    private bool jumping = false;
    private float jump_timer;
    private bool grounded;
    private bool crouching = false;
    
    [SerializeField] private float ground_Drag;
    [SerializeField] private float speed;
    [SerializeField] private float jump_speed;
    [SerializeField] private float box_cast_size;
    [SerializeField] private LayerMask ground_layer_mask;
    [SerializeField] private float min_speed;
    [SerializeField] private float max_speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Ground_Check();
        rb.AddForce(transform.right * (input_x * speed), ForceMode2D.Impulse);
        ClampSpeed();
        SwapMagnet();
        ChangeAnimations();

        if (grounded)
        {
            if (input_y < 0) crouching = true;
            else
            {
                crouching = false;
                if (jumps > 0 && !jumping && input_y > 0)
                {
                    crouching = false;
                    rb.AddForce(transform.up * (input_y * jump_speed), ForceMode2D.Impulse);
                    jump_particle.Emit(10);
                    jumps--;
                    jumping = true;
                }
            }
        }
        else crouching = false;

        if (jumping)
        {
            jump_timer += Time.fixedDeltaTime;
            if (jump_timer >= 0.2f)
            {
                jumping = false;
            }
        }
        else if (grounded)
        {
            jump_timer = 0;
            jumps = 1;
        }
        
        ParticleSystem.MainModule ma = magnetParticle.main;
        if (crouching)
        {
            rb.mass = 100;
            rb.drag = 100;
            _magnet.pullForce = 50;
            ma.startColor = new Color(ma.startColor.color.r, ma.startColor.color.g, ma.startColor.color.b, 1f);
        }
        else
        {
            rb.mass = 1f;
            rb.drag = 0;
            _magnet.pullForce = 25;
            ma.startColor = new Color(ma.startColor.color.r, ma.startColor.color.g, ma.startColor.color.b, .25f);
        }
    }

    private void GetInput()
    {
        input_x = Input.GetAxisRaw(XInput);
        input_y = Input.GetAxisRaw(YInput);
        input_swap = Input.GetAxisRaw(swapInput);
    }
    
    private void Ground_Check()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, box_cast_size, ground_layer_mask);
        grounded = hit.collider != null;
    }

    private void SwapMagnet()
    {
        if (input_swap != 0)
        {
            if (!swapping)
            {
                _magnet.frequency =
                    _magnet.frequency == Frequency.North ? Frequency.South :
                    _magnet.frequency == Frequency.South ? Frequency.North : Frequency.Neutral;
                swapping = true;
            }
        }
        else
        {
            swapping = false;
        }
    }

    private void ClampSpeed()
    {
        if (input_x==0f)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, min_speed, max_speed)*ground_Drag,rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, min_speed, max_speed),rb.velocity.y);
        }
    }

    private void ChangeAnimations()
    {
        var localScale = transform.localScale;
        if (input_x > 0)
        {
            localScale = new Vector3(1, localScale.y, localScale.z);
            transform.localScale = localScale;
        } else if (input_x < 0)
        {
            localScale = new Vector3(-1, localScale.y, localScale.z);
            transform.localScale = localScale;
        }

        anim.SetBool("Air",!grounded);
        if (rb.velocity.x > 1 || rb.velocity.x < -1)
        {
            anim.SetBool("Walk",true);
        }
        else
        {
            anim.SetBool("Walk",false);
        }
    }
}
