using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRb;
    
    [FormerlySerializedAs("fSpeed")] [Header("Stats")]
    public int iPlayerHealth;
    public float fPlayerSpeed;
    public float fPlayerJump;
   
    
    private bool bIsGrounded;
    private float fIFramesDuration = 2;
    private int iNumberOfFlashes = 5;
    private SpriteRenderer cSpriteRenderer;
    public bool bInMenu;
    public bool bIsTouchingStatBlock;
    private StatBlockUI statBlockUI;
    private float fBaseSpeed = 3;
    private float fBaseJump = 3;



    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        cSpriteRenderer = GetComponent<SpriteRenderer>();

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject statBlock = GameObject.FindGameObjectWithTag("StatBlock"); // find player
        statBlockUI = statBlock.GetComponent<StatBlockUI>();

    }

    // Update is called once per frame
    void Update()
    {
        //menu pausing
        if (bInMenu)
        {
            fPlayerSpeed = 0;
            fPlayerJump = 0;
            bIsGrounded = true;
        }
        else
        {
            fPlayerSpeed = statBlockUI.stats[1] * 2 + fBaseSpeed;
            fPlayerJump = statBlockUI.stats[2] * 2 + fBaseJump;
        }
        playerRb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * fPlayerSpeed, playerRb.linearVelocity.y);
        
        // jump
        if (Input.GetKeyDown("space") && bIsGrounded)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, fPlayerJump);
        }
        
        if (bIsTouchingStatBlock && Input.GetKeyDown(KeyCode.E))
        {
            bInMenu = !bInMenu;
        }

        iPlayerHealth = statBlockUI.stats[0];
        
        

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Console.WriteLine("Level Complete!");
        }
    }
   
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            bIsGrounded = true;
        }
        else if (other.gameObject.CompareTag("StatBlock"))
        {
            bIsTouchingStatBlock = true;
        }
        
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            bIsGrounded = false;
        }
        else if (other.gameObject.CompareTag("StatBlock"))
        {
            bIsTouchingStatBlock = false;
        }
    }
    
    
    // Damage/I-Frames
    public void TakeDamage(int damage)
        {
            iPlayerHealth -= damage;
            
            // I-frames
            if (iPlayerHealth > 0)
            {
                StartCoroutine(Invulnerability());
            }
            
            else if (iPlayerHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        
        // flashes
        for (int i = 0; i < iNumberOfFlashes; i++)
        {
            cSpriteRenderer.color = new Color(0, 0.25f, 1, 0.5f);
            yield return new WaitForSeconds(fIFramesDuration/iNumberOfFlashes);
            cSpriteRenderer.color = Color.blue;
            yield return new WaitForSeconds(fIFramesDuration/iNumberOfFlashes);
        }
        
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

}
