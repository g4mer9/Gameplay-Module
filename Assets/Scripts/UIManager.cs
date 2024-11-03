using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//code adapted from https://pastebin.com/jZ6hzMcJ
public enum GameMenu
{
    HUD,
    Main,
    Options,
    Credits,
    Pause
}

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Menus;
    public GameMenu startingMenu;
    public GameMenu currentMenu;
    //public KeyCode nextKey;
    //public KeyCode[] menuKeys;
    private static UIManager _instance;

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
        foreach (GameObject menu in Menus)
        {
            if (menu.GetComponent<MenuManager>() == null)
                Debug.LogError("No MenuManager found on " + menu.name);
            menu.GetComponent<MenuManager>().CloseMenu(); //make sure all menus start closed
        }
        OpenMenu(startingMenu); //open a starting menu if one is declared
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    CheckMenuInput();
    //}

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
            case "Pause":
                GoToMenu(GameMenu.Pause);
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