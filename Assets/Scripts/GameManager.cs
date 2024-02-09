using System.Diagnostics.Tracing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tags
{
    public const string Player = "Player";
    public const string effect = "Effect";
    public const string iceCage = "Ice Cage";
    public const string RightHand = "RightHand";
    public const string MainCamera = "MainCamera";
    public const string LeftHand = "LeftHand";
    public const string hud = "HUD";
    public const string PotionPreview = "PotionPreview";
    public const string inventory = "Inventory";
    public const string PauseMenu = "PauseMenu";
    public const string InventoryImageSlot = "InventoryImageSlot";
    public const string InventorySelection = "InventorySelection";
}

public class GameManager : Singleton<GameManager>
{
    //public static GameManager Instance;

    static public bool isPaused = false;
    //public static Tags tags;

    HUDEquipment hud;
    Camera mainCamera;
    PlayerController player;
    ChangeScene changeScene;
    PotionPreview potionPreview;
    AudioManager audioManager;

    //Inventory
    PlayerInventory playerInventory;
    [SerializeField] InventoryObject inventory;
    [SerializeField] InventoryObject equipmentInventory;
    [SerializeField] InventoryObject hudEquipmentInventory;
    [SerializeField] ItemsDatabaseObject itemsDB;

    protected override void Awake()
    {
        base.Awake();
        changeScene = GetComponent<ChangeScene>();
        mainCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
        playerInventory = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerInventory>();
        hud = GameObject.FindGameObjectWithTag(Tags.hud).GetComponentInChildren<HUDEquipment>();
        potionPreview = GameObject.FindGameObjectWithTag(Tags.PotionPreview).GetComponent<PotionPreview>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    public static HUDEquipment GetHUD() => Instance.hud;
    public static Camera GetMainCamera() => Instance.mainCamera;
    public static PlayerController GetPlayer() => Instance.player;
    public static InventoryObject GetInventory() => Instance.inventory;
    public static PlayerInventory GetPlayerInventory() => Instance.playerInventory;
    public static InventoryObject GetEquipmentInventory() => Instance.equipmentInventory;
    public static InventoryObject GetHudEquipmentInventory() => Instance.hudEquipmentInventory;
    public static PotionPreview GetPotionPreview() => Instance.potionPreview;
    public static ItemsDatabaseObject GetItemsDB() => Instance.itemsDB;
    public static void EndScene() => Instance.changeScene.StartDefeat();
    public static AudioManager GetAudioManager() => Instance.audioManager;

    public static void GoToNextLevel() => Instance.changeScene.GoToNextLevel();
}


public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    public static T Instance => _instance;

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}