using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CreditsScreen : MonoBehaviour
{
    public GameObject mainMenuUI;
    private AudioSource downSource;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        
        Button buttonBack = root.Q<Button>("back__button");
        
        GameObject downAudio = GameObject.Find("Down");
        if (downAudio != null)
        {
            downSource = downAudio.GetComponent<AudioSource>();
        }
        
        buttonBack.clicked += () => Destroy(gameObject);
        buttonBack.clicked += () => Instantiate(mainMenuUI);
        buttonBack.clicked += () => downSource.Play();
        
    }
}
