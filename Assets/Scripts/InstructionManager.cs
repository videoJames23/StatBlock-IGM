using System.Collections;
using TMPro;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    private GameObject controls;
    private bool inControlsArea;

    private GameObject health;
    private bool inHealthArea;

    private GameObject jump;
    private bool inJumpArea;

    private GameObject speed;
    private bool inSpeedArea;

    private GameObject player;

    private Rigidbody2D playerRb;
    private int fadeTime;
    
    private TMP_Text controlsText;
    private TMP_Text healthText;
    private TMP_Text jumpText;
    private TMP_Text speedText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controls = GameObject.FindGameObjectWithTag("ControlsText");
        health = GameObject.FindGameObjectWithTag("HealthText");
        jump = GameObject.FindGameObjectWithTag("JumpText");
        speed = GameObject.FindGameObjectWithTag("SpeedText");
        
        controlsText = controls.GetComponent<TMP_Text>();
        healthText = health.GetComponent<TMP_Text>();
        jumpText = jump.GetComponent<TMP_Text>();
        speedText = speed.GetComponent<TMP_Text>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void StartFadeIn(string area)
    {
        StartCoroutine(FadeIn(area));
    }
    
    public void StartFadeOut(string area)
    {
        string a = area;
        StartCoroutine(FadeOut(a));
    }

    

    IEnumerator FadeIn(string area)
    {
        switch (area)
        {
            case "Controls":
                for (float f = 0; f < 255; f += 5)
                {
                    controlsText.color = new Color(255, 255, 255, f/255);
                    yield return new WaitForSeconds(5/255);

                }
                
                break;
            
            case "Health":
                
                for (float f = 0; f < 255; f += 5)
                {
                    healthText.color = new Color(255, 255, 255, f/255);
                    yield return new WaitForSeconds(5/255);
                }
                break;
            
            case "Jump":
                
                break; 
            
            case "Speed":
                
                break;
        }
    }
    IEnumerator FadeOut(string area)
    {
        switch (area)
        {
            case "Controls":
                for (float f = 255; f < 0; f -= 5)
                {
                    controlsText.color = new Color(255, 255, 255, f/255);
                    yield return new WaitForSeconds(5/255);

                }
                
                break;
            
            case "Health":
                
                for (float f = 255; f < 0; f -= 5)
                {
                    healthText.color = new Color(255, 255, 255, f/255);
                    yield return new WaitForSeconds(5/255);

                }
                break;
            
            case "Jump":
                
                break; 
            
            case "Speed":
                
                break;
        }
    }

    public void FadeOut(string area)
    {
    //     switch (area)
    //     {
    //         case "Controls":
    //             controls.GetComponent<Animator>().SetTrigger("FadeOut");
    //             break;
    //         
    //         case "Health":
    //             health.GetComponent<Animator>().SetTrigger("FadeOut");
    //             break;
    //         
    //         case "Jump":
    //             jump.GetComponent<Animator>().SetTrigger("FadeOut");
    //             break; 
    //         
    //         case "Speed":
    //             speed.GetComponent<Animator>().SetTrigger("FadeOut");
    //             break;
    //     }
    }
}
