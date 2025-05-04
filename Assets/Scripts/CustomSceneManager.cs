using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager pauseMenu;
    
    private void Awake()
    {
        if (pauseMenu == null)
        {
            pauseMenu = this;
        }
        else if (pauseMenu != this)
        {
            Destroy(gameObject);
            return; 
        }
        
        DontDestroyOnLoad(gameObject);
    }
    
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            LoadScene(0);
        }
    }
    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}