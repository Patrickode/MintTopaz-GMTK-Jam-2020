using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private GameObject currentMenu = null;

    private void OnEnable() { GetCurrentMenu(); }

    public void LoadScene(int indexToLoad) { SceneManager.LoadScene(indexToLoad); }

    public void QuitGame() { Application.Quit(); }

    public void MenuSwitch(GameObject destination)
    {
        if (destination && currentMenu)
        {
            destination.SetActive(true);
            currentMenu.SetActive(false);
            currentMenu = destination;
        }
    }

    /// <summary>
    /// Goes through <paramref name="transformToCheck"/>'s children to find the first active menu, 
    /// and asssigns it to currentMenu. Will check nested children as well. Only follows through if currentMenu 
    /// is null.
    /// </summary>
    /// <param name="transformToCheck">The transform to check for active children tagged "Menu."
    /// Defaults to this object's transform.</param>
    /// <returns>Whether currentMenu was assigned.</returns>
    private bool GetCurrentMenu(Transform transformToCheck = null)
    {
        //We don't need to assign currentMenu if it's already assigned.
        if (!currentMenu)
        {
            //If transformToCheck is null, use this object's transform instead. Otherwise,
            //just use transformToCheck.
            transformToCheck = transformToCheck ? transformToCheck : transform;

            foreach (Transform child in transformToCheck)
            {
                //If this child is inactive, move on; we don't have to check its children either,
                //because they'll also be inactive.
                if (child.gameObject.activeInHierarchy)
                {
                    if (child.CompareTag("Menu"))
                    {
                        currentMenu = child.gameObject;
                        return true;
                    }
                    //If child is active but isn't a menu, it might still have a menu inside it.
                    //Check it's children and return true if an active menu is found within.
                    else if (GetCurrentMenu(child)) { return true; }
                }
            }
        }

        //If we got this far, there are no active children that are menus in transformToCheck.
        return false;
    }
}
