using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject pauseMenuObj;
    [SerializeField] GameObject inventoryMenuObj;

    void Start()
    {
        //TODO: no se pueden buscar gameObjects desactivados por Tag
        //Instance.pauseMenuObj = GameObject.FindGameObjectWithTag(Tags.PauseMenu).gameObject; 
        //Instance.inventoryMenuObj = GameObject.FindGameObjectWithTag(Tags.inventory).gameObject;
        ToggleInventory();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.TogglePauseGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.GoToNextLevel();
        }
    }

    public static void TogglePauseGame()
    {
        GameManager.isPaused = !GameManager.isPaused;

        if (GameManager.isPaused == true)
        {
            Time.timeScale = 0;
            Instance.pauseMenuObj.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Instance.pauseMenuObj.SetActive(false);
        }
    }


    public static bool ToggleInventory()
    {
        if (Instance.inventoryMenuObj.activeSelf)
        {
            Instance.inventoryMenuObj.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Instance.inventoryMenuObj.SetActive(true);
            Time.timeScale = 0.3f;
        }
        return Instance.inventoryMenuObj.activeSelf;
    }

}
