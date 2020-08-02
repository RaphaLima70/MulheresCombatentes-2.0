using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_WinLoose : MonoBehaviour {

    public GameObject painelWin;
    public GameObject painelLoose;
    public string proxFase;
    public scr_paineis paineisLink;
    public int faseAtual;

    public scr_loading loadLink;

    private void Awake()
    {
        loadLink = GameObject.Find("Loadings").GetComponent<scr_loading>();
    }

    public void perdeu()
    {
        Time.timeScale = 0;
        paineisLink.fechaTudo();
        painelLoose.SetActive(true);
    }

    public void ganhou()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        
        PlayerPrefs.SetInt("levelAtual", faseAtual+1);
        paineisLink.fechaTudo();
        painelWin.SetActive(true);
    }

    public void passaFase()
    {
        StartCoroutine(PassaFaseE());
    }

    public void repeteFase()
    {
        StartCoroutine(RepeteFaseE());
    }

    IEnumerator PassaFaseE()
    {
        Time.timeScale = 1;
        loadLink.Loader();
        yield return new WaitForSeconds(1);
        Application.LoadLevel(proxFase);
    }

    IEnumerator RepeteFaseE()
    {
        Time.timeScale = 1;
        loadLink.Loader();
        yield return new WaitForSeconds(1);
        Application.LoadLevel(Application.loadedLevel);
    }
}
