using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NukeWarning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
        getText = textObject.GetComponent<Text>();
    }

    public EnemyScoutAI[] scouts;
    public bool warning = false;
    public GameObject textObject;
    private Text getText;
    private Image myImage;
    private bool alpha;
    private float currentAlpha;
    //private string warningMessage;
    // Update is called once per frame
    void Update()
    {
        DetectAllScoutsLOS();
        WarningFunction();
    }

    void DetectAllScoutsLOS()
    {
        scouts = FindObjectsOfType<EnemyScoutAI>();
        warning = false;
        for (int i = 0; i < scouts.Length; i++)
        {
            //warning = false;
            if (scouts[i].isVisible == true)
            {
                getText.text = "A Scout Spotted You!";
                warning = true;
            }
        }

        GameObject nuke = GameObject.FindGameObjectWithTag("Nuke");
        if (nuke != null)
        {
            warning = true;
            getText.text = "Nuke Incoming!";
        }

    }

    void WarningFunction()
    {
        if (warning == true)
        {
            if (alpha == true)
            {
                currentAlpha += 2 * Time.deltaTime;
                if (currentAlpha >= 1)
                {
                    alpha = false;
                }
            }
            else
            {
                currentAlpha -= 2 * Time.deltaTime;
                if (currentAlpha <= 0)
                {
                    alpha = true;
                }
            }



            myImage.color = new Color(1, 0, 0, currentAlpha);
            getText.color = new Color(1, 0, 0, currentAlpha);
        }
        else
        {
            myImage.color = new Color(1, 0, 0, 0.1f);
            getText.color = new Color(1, 0, 0, 0);
        }
    }
}
