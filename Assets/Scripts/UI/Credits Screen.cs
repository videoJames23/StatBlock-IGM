using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CreditsScreen : MonoBehaviour
{
    public GameObject mainMenuUI;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        
        Button buttonBack = root.Q<Button>("back__button");
        
        buttonBack.clicked += () => Destroy(gameObject);
        buttonBack.clicked += () => Instantiate(mainMenuUI);
        
    }
}
