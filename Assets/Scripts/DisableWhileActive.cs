using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhileActive : MonoBehaviour
{
    
    public GameObject disableObject;
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            disableObject.SetActive(false);
        }
    }
}
