using System.Linq;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class StatBlockUI : MonoBehaviour
{
    public TextMeshProUGUI[] valueTexts;

   
    [FormerlySerializedAs("values")] public int[] stats = {1, 2, 1};
    private int selectedIndex;
    public PlayerController playerController;
    public int iPointsTotal;
    public int iPointsLeft;
    public bool bNoPoints;


    
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        iPointsTotal = 9;
        iPointsLeft = iPointsTotal - stats.Sum();
        UpdateUI();
    }

    void Update()
    {
        if (iPointsLeft == 0)
        {
            bNoPoints = true;
        }
        if (playerController.bInMenu)
        { 
            

            // select stat
            if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = stats.Length - 1;

                UpdateUI();
            }

            
            if (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.S))
            {
                selectedIndex++;
                if (selectedIndex >= stats.Length)
                    selectedIndex = 0;
                

                UpdateUI();
            }

            // change value
            if ((Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D)) && iPointsLeft > 0)
            {
                stats[selectedIndex]++;

                if (stats[selectedIndex] > 3)
                {
                    stats[selectedIndex] = 3;
                }

                UpdateUI();
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A))
            {
                stats[selectedIndex]--;

                if (stats[0] < 1)
                {
                    stats[0] = 1;
                }

                else if (stats[1] < 0)
                {
                    stats[1] = 0;
                }

                else if (stats[2] < 0)
                {
                    stats[2] = 0;
                }

                UpdateUI();
            }

            iPointsLeft = iPointsTotal - stats.Sum();
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < valueTexts.Length - 1; i++)
        {
            valueTexts[i].text = stats[i].ToString();
            valueTexts[3].text = iPointsLeft.ToString();

            if (playerController.bInMenu)
            {
                valueTexts[i].color = (i == selectedIndex) ? Color.green : Color.white;
            }
            else
            {
                valueTexts[i].color = Color.white;
            }
        }
    }
}