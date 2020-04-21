using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text sheepSavedText;
    public Text sheepDroppedText;
    public GameObject gameOverWindow;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
    }

    /* update sheeps saved */
    public void UpdateSheepSaved() 
    {
        sheepSavedText.text = GameStateManager.Instance.sheepSaved.ToString();
    }

    /* update sheeps dropped */
    public void UpdateSheepDropped() 
    {
        sheepDroppedText.text = GameStateManager.Instance.sheepDropped.ToString();
    }

    /* game over window */
    public void ShowGameOverWindow()
    {
        gameOverWindow.SetActive(true);
    }
}
