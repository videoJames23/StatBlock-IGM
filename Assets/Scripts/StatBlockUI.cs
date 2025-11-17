using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class StatBlockUI : MonoBehaviour
{
    public TextMeshProUGUI[] valueTexts;

   
    [FormerlySerializedAs("values")] public int[] stats = { 1, 2, 1};
    private int selectedIndex;
    public PlayerController playerController;
    public int iPointsLeft;
    public int iPointsTotal;


    
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // find player
        playerController = player.GetComponent<PlayerController>();
        iPointsTotal = 9;
        iPointsLeft = iPointsTotal - stats.Sum();
        UpdateUI();
    }

    void Update()
    {
        
        if (playerController.bInMenu)
        {


            // Move selection UP visually
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = stats.Length - 1;

                UpdateUI();
            }

            // Move selection DOWN visually
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedIndex++;
                if (selectedIndex >= stats.Length)
                    selectedIndex = 0;
                

                UpdateUI();
            }

            // Increase value
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                stats[selectedIndex]++;

                if (stats[selectedIndex] > 3)
                {
                    stats[selectedIndex] = 3;
                }

                UpdateUI();
            }

            // Decrease value
            if (Input.GetKeyDown(KeyCode.LeftArrow))
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

    void UpdateUI()
    {
        for (int i = 0; i < valueTexts.Length - 1; i++)
        {
            valueTexts[i].text = stats[i].ToString();
            valueTexts[3].text = iPointsLeft.ToString();

            valueTexts[i].color = (i == selectedIndex) ? Color.yellow : Color.white;
        }
    }
}