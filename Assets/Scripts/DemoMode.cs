using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMode : MonoBehaviour
{
    public GameObject[] HideDemo;
    // Start is called before the first frame update
 

    public void OpenDebug (string A)
    {
        if (A == "ctinfo01")
        {
            for (int i = 0; i < HideDemo.Length; i++)
            {
                HideDemo[i].SetActive(true);
            }
        }
    }

    public void CloseDebug(string A)
    {
        if (A == "ctinfo02")
        {
            for (int i = 0; i < HideDemo.Length; i++)
            {
                HideDemo[i].SetActive(false);
            }
        }
    }
}
