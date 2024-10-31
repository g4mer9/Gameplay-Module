using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code adapted from https://pastebin.com/JPguqDmy

// To make a Menu: Create an inherited class of MenuManager that has special overrides for anything needed
// Example:
/* public class InventoryManager: MenuManager
 * 
 *  protected override void InnerAwake(){
 *   // do the things that are unique to this menu for awake
 *   }
 *  
 *  protected override void InnerUpdate(){
 *      //do update things unique to this menu
 *  }
 * 
 */

public class MenuManager : MonoBehaviour
{
    protected bool isActive = false;
    //protected bool isDirty = false;
    protected Canvas myCanvas;

    private void Awake()
    {
        if (myCanvas == null)
            myCanvas = GetComponent<Canvas>();
        //InnerAwake();
    }

    //protected virtual void InnerAwake()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        //if (isDirty)
        //{
        //    isDirty = false;
        //    RefreshMenu();
        //}

        //InnerUpdate();
    }

    //protected virtual void InnerUpdate()
    //{

    //}

    //public virtual void RefreshMenu()
    //{
    //    //do anything you need to 'reset' the menu to its expected state
    //    isDirty = false;
    //}

    public void CloseMenu()
    {
        // Any code that needs to run when a menu closes
        myCanvas.enabled = false;
    }

    public void OpenMenu()
    {
        // Any code that needs to run when a menu first opens
        myCanvas.enabled = true;
    }
}