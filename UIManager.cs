
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button RestartButton;
    public Button NextLevelButton;
    public static UIManager instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
       
        this.RestartButton.onClick.AddListener(GameController.instance.RestartLevel);
        this.NextLevelButton.onClick.AddListener(GameController.instance.NextLevel);
        this.NextLevelButton.interactable = false;
        
    }

   
    
    public void ActivateLevel(int level)
    {
        GameProgressBar.instance.ActivateLevel(level);
    }


}