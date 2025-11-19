using System;
using System.Collections;
using System.Linq;
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
    private EnemyController enemyController;
    private float fBaseSpeed = 3;
    private float fBaseJump = 3;



    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        playerRb = GetComponent<Rigidbody2D>();
        cSpriteRenderer = GetComponent<SpriteRenderer>();
        
        GameObject statBlock = GameObject.FindGameObjectWithTag("StatBlock");
        statBlockUI = statBlock.GetComponent<StatBlockUI>();
        
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyController = enemy.GetComponent<EnemyController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (bIsTouchingStatBlock && Input.GetKeyDown(KeyCode.E))
        {
            bInMenu = !bInMenu;
        }
        
        if (bInMenu)
        {
            fPlayerSpeed = 0;
            fPlayerJump = 0;
            bIsGrounded = true;
            enemyController.fEnemySpeed = 0;
        }
        else
        {
            
            fPlayerSpeed = (statBlockUI.stats[1] * fBaseSpeed);
            fPlayerJump = (statBlockUI.stats[2] * fBaseJump);
            enemyController.fEnemySpeed = enemyController.fEnemyBaseSpeed;
        }
        playerRb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * fPlayerSpeed, playerRb.linearVelocity.y);
        
        
        if (Input.GetKeyDown("space") && bIsGrounded)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, fPlayerJump);
        }
        
        iPlayerHealth = statBlockUI.stats[0];
        
        

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
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
