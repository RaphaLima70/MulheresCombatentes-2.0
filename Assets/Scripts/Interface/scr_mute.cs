using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_mute : MonoBehaviour {

    public GameObject on;
    public GameObject off;      

    // se playerPrefs "som" = 1 liga
    // se playerPrefs "som" = 2 desliga

    private void Awake()
    {
        if (PlayerPrefs.GetInt("som") == 0)
        {
            PlayerPrefs.SetInt("som", 1);
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("som") == 1)
        {
            off.SetActive(false);
            on.SetActive(true);
            AudioListener.pause = false;
        }
        if(PlayerPrefs.GetInt("som") == 2)
        {
            on.SetActive(false);
            off.SetActive(true);
            AudioListener.pause = true;
        }
    }

    public void Mute()
    {
        if (PlayerPrefs.GetInt("som") == 1)
        {
            PlayerPrefs.SetInt("som", 2);
        }
        else if (PlayerPrefs.GetInt("som") == 2)
        {
            PlayerPrefs.SetInt("som", 1);
        }
    }
}
