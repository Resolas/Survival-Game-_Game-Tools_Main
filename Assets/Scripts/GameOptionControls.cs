using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptionControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject menu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeInHierarchy != true)
            {
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
            }

        }


        menuFunctions();

    }

    void menuFunctions()
    {

        if (menu.activeInHierarchy == true)
        {

            Time.timeScale = 0;


        }
        else
        {
            Time.timeScale = 1;
        }

    }

    public void menuResume()
    {

        menu.SetActive(false);

    }

    public void menuQuit()
    {
        Application.Quit();
    }

}
