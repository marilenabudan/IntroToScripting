using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance; //reference to itself to be accessible from other scripts

    public GameObject you_win;

    [HideInInspector] // it will not be shown in the editor - but accessible from other scripts 
    public int sheepSaved; // amount of sheeps saved

    [HideInInspector]
    public int sheepDropped; // amount of sheeps dropped 

    public int sheepDroppedBeforeGameOver; // amount of sheeps that CAN be dropped
    public SheepSpawner sheepSpawner; // reference to SheepSpawner 

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        you_win.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartScene();
        }
    }

    /* update counter of sheeps saved */
    public void SavedSheep()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();  // Update UI manager
        if (sheepSaved >= 30)
        {
            Debug.Log("end");
            you_win.SetActive(true);
            sheepSpawner.canSpawn = false;  // do not spawn more sheeps 
            sheepSpawner.DestroyAllSheep(); // delete all sheeps 
            Invoke("StartScene", 2f); ;
        }
    }

    /* end game */
    private void GameOver()
    {
        sheepSpawner.canSpawn = false; // do not spawn more sheeps 
        sheepSpawner.DestroyAllSheep(); // delete all sheeps
        UIManager.Instance.ShowGameOverWindow(); // show game over window 
    }

    /* update the counter of dropped sheeps and check if it reached maximum*/
    public void DroppedSheep()
    {
        sheepDropped++;
        UIManager.Instance.UpdateSheepDropped();

        if (sheepDropped == sheepDroppedBeforeGameOver)
        {
            GameOver();
        }
    }

    public void StartScene()
    {
        SceneManager.LoadScene("Title");
    }
}