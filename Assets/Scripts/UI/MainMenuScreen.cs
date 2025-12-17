using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuScreen : MonoBehaviour
    {
        public GameObject levelSelectUI;
        public GameObject creditsUI;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        
            Button buttonStart = root.Q<Button>("start__button");
            Button buttonLevelSelect = root.Q<Button>("level__select__button");
            Button buttonCredits = root.Q<Button>("credits__button");

            buttonStart.clicked += () => SceneManager.LoadScene(1);
            buttonLevelSelect.clicked += () => Destroy(gameObject);
            buttonLevelSelect.clicked += () => Instantiate(levelSelectUI);
            buttonCredits.clicked += () => Destroy(gameObject);
            buttonCredits.clicked += () => Instantiate(creditsUI);
        }

    }
}
