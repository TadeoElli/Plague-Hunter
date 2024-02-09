using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    static int level = 0;


    //[SerializeField] Animator anim;
    //public AnimEvents events;
    const string defaultGameScene = "ForestLevel1";
    const string secondGameScene = "CrisTestingTownLevel";
    const string bossGameScene = "BossScene";
    const string restGameScene = "RestZone";
    const string EndGameScene = "VictoryScene";
    //const string EndGameScene = "TaioTownLevel";
    const string testingGameScene = "GerTesting";
    const string mainMenuScene = "MainMenuModificado";
    //const string mainMenuScene = "MainMenu";
    const string creditsScene = "Credits";
    const string controlsScene = "Controls";
    const string defeatScene = "Defeat";
    const string tutorialScene = "Tutorial";
    const string AudioMenuScene = "AudioMenu";


    List<string> levelsList = new List<string>();

    void Start()
    {
        levelsList.Add(mainMenuScene);
        levelsList.Add(tutorialScene);
        levelsList.Add(defaultGameScene);
        levelsList.Add(secondGameScene);
        levelsList.Add(restGameScene);
        levelsList.Add(bossGameScene);
        levelsList.Add(EndGameScene);
        //events.ADD_EVENT("fade", OnFadeComplete);
    }

    public void NewScene(string name)
    {
        //anim.SetTrigger("FadeOut");
        GameManager.isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }

    public void CloseGame() => Application.Quit();
    void OnFadeComplete() { }
    public void StartMenu()
    {
        level = 0;
        NewScene(mainMenuScene);
    }
    public void StartCredits() => NewScene(creditsScene);
    public void StartDefeat() => NewScene(defeatScene);
    public void StartControllers() => NewScene(controlsScene);
    public void StartAudioMenu() => NewScene(AudioMenuScene);
    public void StartGame()
    {
        /*
        Debug.Log("Starting Game");
        level = 1;
        //NewScene(defaultGameScene);
        NewScene(tutorialScene);
        */
        StartTutorial();
    }
    public void StartTestingGame()
    {
        level = 0;
        NewScene(testingGameScene);
    }
    public void StartTutorial()
    {
        level = 1;
        Debug.Log("Starting Tutorial");
        NewScene(tutorialScene);
    }
    public void RestartGame()
    {
        //Invoke("StartGame",10);
        StartGame();
        LimpiarInventarios();
    }
    private void LimpiarInventarios()
    {
        GameManager.GetEquipmentInventory().Clear();
        GameManager.GetHudEquipmentInventory().Clear();
        GameManager.GetInventory().Clear();
    }
    public void GoToNextLevel()
    {
        if (level == 0)
        {
            //level++;
            StartGame();
            //StartTutorial();
        }
        else //TODO: En vez de hacer un level++, podria chequear en base al index e ir a la siguiente escena levelsList[getindexfromactuallevel()+1]
        {
            level++;
            if (level > levelsList.Count - 1)
            {
                StartMenu();
            }
            else
            {
                Debug.Log($"Next level: {level} {levelsList[level]}");
                NewScene(levelsList[level]);
            }

        }
    }
    private void OnApplicationQuit()
    {
        LimpiarInventarios();
    }
}
