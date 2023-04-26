using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager Instance;

    //The variable will be easly accessed from others scripts as it is public
    //but not from the inspector.
    [HideInInspector]
    public int sheepSaved;

    [HideInInspector]
    public int sheepDropped;

    public int sheepDroppedBeforeGameOver;
    public SheepSpawner sheepSpawner;

   
    void Awake()
    {
        Instance = this;
        sheepDropped = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("Title");
        }
    }

    public void SavedSheep(){
        sheepSaved ++;
        UIManager.Instance.UpdateSheepSaved();
    }

    public void DroppedSheep(){
        sheepDropped = sheepDropped + 1;
        UIManager.Instance.UpdateSheepDropped();
        if(sheepDropped == sheepDroppedBeforeGameOver){
            GameOver();
        }
    }

    private void GameOver(){
        sheepSpawner.canSpawn = false;
        sheepSpawner.DestroyAllSheep();
        UIManager.Instance.ShowGameOverWindow();

    }
}
