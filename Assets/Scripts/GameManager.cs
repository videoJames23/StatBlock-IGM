using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private PlayerController playerController;
    private Rigidbody2D enemyRb;
    private EnemyController enemyController;
    private StatBlockUI statBlockUI;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<PlayerController>();
        
        GameObject statBlockP = GameObject.FindGameObjectWithTag("StatBlockP");
        statBlockUI = statBlockP.GetComponent<StatBlockUI>();
        
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyRb =  enemy.GetComponent<Rigidbody2D>();
        enemyController = enemy.GetComponent<EnemyController>();
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        
        if (playerController.bInMenu)
        {
            enemyRb.constraints = RigidbodyConstraints2D.FreezeAll;
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            
        }
        else if (!playerController.bInMenu)
        {
            enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            playerRb.constraints = RigidbodyConstraints2D.None;
        }
        
        switch (statBlockUI.statsP[1]) // player speeds
        {
            case 0: playerController.fPlayerSpeed = 0; break;
            case 1: playerController.fPlayerSpeed = 3; break;
            case 2: playerController.fPlayerSpeed = 7; break;
            case 3: playerController.fPlayerSpeed = 10; break;
        }

        switch (statBlockUI.statsP[2]) //player jump heights
        {
            case 0: playerController.fPlayerJump = 0; break;
            case 1: playerController.fPlayerJump = 5; break;
            case 2: playerController.fPlayerJump = 7; break;
            case 3: playerController.fPlayerJump = 9; break;
        }
        switch (statBlockUI.statsE[1]) // enemy speeds
        {
            case 0: enemyController.fEnemySpeed = 0; break;
            case 1: enemyController.fEnemySpeed = 3 * enemyController.fEnemyDir; break;
            case 2: enemyController.fEnemySpeed = 7 * enemyController.fEnemyDir; break;
            case 3: enemyController.fEnemySpeed = 10 * enemyController.fEnemyDir; break;
        }

        switch (statBlockUI.statsE[2]) //enemy sizes
        {
            case 1: enemyController.fEnemySize = 1.5f; break;
            case 2: enemyController.fEnemySize = 3f; break;
            case 3: enemyController.fEnemySize = 4.5f; break;
        }
        playerController.iPlayerHealth = statBlockUI.statsP[0];
        statBlockUI.UpdateUI();
    }

    
}
