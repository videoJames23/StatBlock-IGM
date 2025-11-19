using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRb;
    private Rigidbody2D enemyRb;
    
    [FormerlySerializedAs("fSpeed")] [Header("Stats")]
    public int iPlayerHealth;
    public float fPlayerSpeed;
    public float fPlayerJump;
    
    
    
    public bool bIsTouchingStatBlock;
    public bool bInMenu;
    private bool bIsGrounded;
    
    private float fIFramesDuration = 2;
    private int iNumberOfFlashes = 5;
    
    private StatBlockUI statBlockUI;
    private EnemyController enemyController;
    private SpriteRenderer cSpriteRenderer;
    


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        playerRb = GetComponent<Rigidbody2D>();
        cSpriteRenderer = GetComponent<SpriteRenderer>();
        
        GameObject statBlock = GameObject.FindGameObjectWithTag("StatBlock");
        statBlockUI = statBlock.GetComponent<StatBlockUI>();
        
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyController = enemy.GetComponent<EnemyController>();
        enemyRb =  enemy.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (bIsTouchingStatBlock && Input.GetKeyDown(KeyCode.E))
        {
            bInMenu = !bInMenu;
            statBlockUI.UpdateUI();
        }
        
        if (bInMenu)
        {
            
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            
            bIsTouchingStatBlock = true;
        }
        else if (!bInMenu)
        {
            playerRb.constraints = RigidbodyConstraints2D.None;
            
        }

        switch (statBlockUI.stats[1]) // speeds
            {
                case 0: fPlayerSpeed = 0; break;
                case 1: fPlayerSpeed = 3; break;
                case 2: fPlayerSpeed = 7; break;
                case 3: fPlayerSpeed = 10; break;
            }

            switch (statBlockUI.stats[2]) //jump heights
            {
                case 0: fPlayerJump = 0; break;
                case 1: fPlayerJump = 5; break;
                case 2: fPlayerJump = 7; break;
                case 3: fPlayerJump = 9; break;
            }
            
    
        
        playerRb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * fPlayerSpeed, playerRb.linearVelocity.y);
        
        
        if (Input.GetKeyDown("space") && bIsGrounded)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, fPlayerJump);
        }
        
        iPlayerHealth = statBlockUI.stats[0];
        
        

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {  
            Debug.Log("Level Complete!");
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            bIsGrounded = true;
        }
        
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StatBlock"))
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
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StatBlock"))
        {
            bIsTouchingStatBlock = false;
        }
    }
    
    
    // Damage/I-Frames
    public void TakeDamage(int damage)
        {
            iPlayerHealth -= damage;
            statBlockUI.stats[0]--;
            statBlockUI.iPointsTotal--;
            statBlockUI.iPointsLeft = statBlockUI.iPointsTotal - statBlockUI.stats.Sum();
            statBlockUI.UpdateUI();
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
