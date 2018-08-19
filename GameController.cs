using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    
    public static GameController instance;
    private bool gameOver = false;
    
    void Awake()
    {
        //Make sure there is only one instance of the GameController class (Singleton)
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        //TODO move all game state to GameData
        GameData.LevelCount = Utils.GetLevelCount();
        GameProgressBar.instance.Init(GameData.LevelCount);
        UIManager.instance.ActivateLevel(GameData.CurrentLevel);
    }

    void Start()
    {
        
        this.Player.transform.position = new Vector3(0, 0, 0);
        

    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            return;
        }

        GameData.CurrentLevel += 1;
        UIManager.instance.ActivateLevel(GameData.CurrentLevel);
        SceneManager.LoadScene(currentIndex + 1);
    }
}

