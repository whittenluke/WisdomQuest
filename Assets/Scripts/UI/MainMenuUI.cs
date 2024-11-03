using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button quitButton;
    
    private void Start()
    {
        continueButton.interactable = SaveManager.Instance.HasSaveFile();
        
        continueButton.onClick.AddListener(ContinueGame);
        newGameButton.onClick.AddListener(StartNewGame);
        quitButton.onClick.AddListener(QuitGame);
    }
    
    private void ContinueGame()
    {
        SaveManager.Instance.LoadGame();
        SceneManager.LoadScene("LibraryOfLight");
    }
    
    private void StartNewGame()
    {
        SaveManager.Instance.DeleteSaveFile();
        SceneManager.LoadScene("LibraryOfLight");
    }
    
    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}