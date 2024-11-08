using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UI;
//code adapted from https://pastebin.com/jZ6hzMcJ
public enum GameMenu
{
    HUD,
    Main,
    Options,
    Credits,
    GameOver
}

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Menus;
    public GameMenu startingMenu;
    public GameMenu currentMenu;
    public Slider health;
    public TMP_Text ammo;
    public GameObject player;
    //public KeyCode nextKey;
    //public KeyCode[] menuKeys;
    private static UIManager _instance;
    private UIInfo ui_instance;
    private Camera cam;
    private SceneManager sceneManager;

    public static UIManager Instance
    {
        get
        { 
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        if(player != null) ui_instance = player.GetComponent<PlayerController>().uiInfo;
        sceneManager = GetComponentInChildren<SceneManager>();
        if (cam == null)
        {
            cam = Camera.main;
        }
        foreach (GameObject menu in Menus)
        {
            if (menu.GetComponent<MenuManager>() == null)
                Debug.LogError("No MenuManager found on " + menu.name);
            menu.GetComponent<MenuManager>().CloseMenu(); //make sure all menus start closed
        }
        OpenMenu(startingMenu); //open a starting menu if one is declared
    }

    // Update is called once per frame
    void Update()
    {
        if (health != null && player != null)
        {
            health.value = ui_instance.health;
            if(ui_instance.health <= 0)
            {
                sceneManager.LoadSceneAsync("MainMenu");
                Cursor.lockState = CursorLockMode.None;

            }
        }
        if (ammo != null && player != null)
        {
            ammo.text = ui_instance.ammo.ToString();
        }
    }

    //public void CheckMenuInput()
    //{

    //}

    private bool CloseMenu(GameMenu Menu)
    {
        Menus[(int)Menu].GetComponent<MenuManager>().CloseMenu();

        return true;
    }

    private bool OpenMenu(GameMenu Menu)
    {
        Debug.Log("OpenMenu: " + ((int)Menu).ToString());
        Menus[(int)Menu].GetComponent<MenuManager>().OpenMenu();

        return true;
    }

    public void GoToMenuFromButton(string menu)
    {
        switch(menu)
        {
            case "Main":
                GoToMenu(GameMenu.Main);
                break;
            case "Options":
                GoToMenu(GameMenu.Options);
                break;
            case "Credits":
                GoToMenu(GameMenu.Credits);
                break;
            default:
                Debug.LogWarning("Invalid menu: " + menu);
                break;
        }
    }

    public void GoToMenu(GameMenu Menu)
    {
        if (currentMenu == Menu)
        {
            Debug.LogWarning("Cannot move to " + Menu + ", currently on");
        }
        else
        {
            CloseMenu(currentMenu);
            currentMenu = Menu;
            OpenMenu(currentMenu);
        }
    }
}