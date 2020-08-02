using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_escolheFase : MonoBehaviour {

    public scr_loading link;
    public int faseEscolhida;

    private void Awake()
    {
        link = GameObject.Find("Loadings").GetComponent<scr_loading>();
    }

    public void Fase1()
    {
        faseEscolhida = 1;
        StartCoroutine(MudaFase());
    }
    public void Fase2()
    {
        faseEscolhida = 2;
        StartCoroutine(MudaFase());
    }
    public void Fase3()
    {
        faseEscolhida = 3;
        StartCoroutine(MudaFase());
    }
    public void Fase4()
    {
        faseEscolhida = 4;
        StartCoroutine(MudaFase());
    }
    public void Fase5()
    {
        faseEscolhida = 5;
        StartCoroutine(MudaFase());
    }
    public void Fase6()
    {
        faseEscolhida = 6;
        StartCoroutine(MudaFase());
    }
    public void Fase7()
    {
        faseEscolhida = 7;
        StartCoroutine(MudaFase());
    }
    public void Fase8()
    {
        faseEscolhida = 8;
        StartCoroutine(MudaFase());
    }
    public void Fase9()
    {
        faseEscolhida = 9;
        StartCoroutine(MudaFase());
    }

    public void EndGame()
    {
        link.Loader();
        SceneManager.LoadSceneAsync("EndGame");
    }

    IEnumerator MudaFase()
    {
        link.Loader();
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("fase" + faseEscolhida);
    }
}
