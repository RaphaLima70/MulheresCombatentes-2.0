using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_levelSelect : MonoBehaviour
{
    public Button[] botoes;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < botoes.Length; i++)
        {
            int levelAtual = PlayerPrefs.GetInt("levelAtual", 1);

            if (i + 1 > levelAtual)
            {
                botoes[i].interactable = false;
            }
        }
    }

    public void LiberaFases()
    {
        PlayerPrefs.SetInt("levelAtual", 9);
        Application.LoadLevel(Application.loadedLevelName);
    }
}
