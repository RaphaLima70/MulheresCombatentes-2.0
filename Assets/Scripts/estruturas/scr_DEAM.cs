using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_DEAM : MonoBehaviour {

    scr_DEAM delegacia;

    public scr_gerenciador link;
    public float tempoDelay;
    public float tempoDelayIni;
    public int idUP;
    public Collider colisao;

    public Image barraProgresso;
    public GameObject barraProgressoObj;
    public GameObject iconDano1;
    public GameObject iconDano2;
    public GameObject iconDef1;
    public GameObject iconDef2;



    public scr_paineis paineisLink;
    public scr_estrutura estruturaLink;
    public scr_soundMenager somLink;

    void Awake()
    {
        delegacia = GetComponent<scr_DEAM>();
        link = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
        paineisLink = GameObject.Find("HUD").GetComponent<scr_paineis>();
        somLink = GameObject.Find("SoundMenager").GetComponent<scr_soundMenager>();
    }

    public void OnMouseUp()
    {

        if (paineisLink.painelAtivo == 0 && paineisLink.pausado == false)
        {
            paineisLink.delegaciaLink = delegacia;
            paineisLink.painelAtivo = 3;
            paineisLink.ativaPainel();
        }
    }

    public void upDano1()
    {
        if (link.gold >= 75)
        {
            link.gold -= 75;
            iconDano1.SetActive(true);
            paineisLink.dano1Btn.SetActive(false);
            paineisLink.dano2Btn.SetActive(true);
            tempoDelayIni = 30;
            idUP = 1;
            somLink.comprou.Play();
            FazUp();
        }
        else
        {
            somLink.falta_grana.Play();
            paineisLink.fechaTudo();
            Debug.Log("você só tem: " + link.gold + " moedas");
        }

    }

    public void upDano2()
    {
        if (link.gold >= 150)
        {
            link.gold -= 150;
            iconDano2.SetActive(true);
            paineisLink.dano2Btn.SetActive(false);
            tempoDelayIni = 60;
            idUP = 2;
            somLink.comprou.Play();
            FazUp();
        }
        else
        {
            somLink.falta_grana.Play();
            paineisLink.fechaTudo();
            Debug.Log("você só tem: " + link.gold + " moedas");
        }

    }

    public void upDef1()
    {
        if (link.gold >= 50)
        {
            link.gold -= 50;
            iconDef1.SetActive(true);
            paineisLink.def1Btn.SetActive(false);
            paineisLink.def2Btn.SetActive(true);
            tempoDelayIni = 30;
            idUP = 3;
            somLink.comprou.Play();
            FazUp();
        }
        else
        {
            somLink.falta_grana.Play();
            paineisLink.fechaTudo();
            Debug.Log("você só tem: " + link.gold + " moedas");
        }

    }

    public void upDef2()
    {
        if (link.gold >= 125)
        {
            link.gold -= 125;
            iconDef2.SetActive(true);
            paineisLink.def2Btn.SetActive(false);
            tempoDelayIni = 60;
            idUP = 4;
            somLink.falta_grana.Play();
            FazUp();
        }
        else
        {
            somLink.falta_grana.Play();
            paineisLink.fechaTudo();
            Debug.Log("você só tem: " + link.gold + " moedas");
        }

    }

    void Update()
    {

        if (tempoDelay <= 0)
        {
            iconDano1.SetActive(false);
            iconDano2.SetActive(false);
            iconDef1.SetActive(false);
            iconDef2.SetActive(false);
            barraProgressoObj.SetActive(false);
            tempoDelay = 0;
            colisao.enabled = true;
        }
        else
        {
            barraProgressoObj.SetActive(true);
            colisao.enabled = false;

            tempoDelay -= Time.deltaTime;
        }

        barraProgresso.fillAmount = tempoDelay / tempoDelayIni;

        }


    public void FazUp()
    {
        StartCoroutine(Up());
    }

    IEnumerator Up()
    {
        tempoDelay = tempoDelayIni;
        paineisLink.fechaTudo();
        yield return new WaitForSeconds(tempoDelay);
        EscolheUP();
        somLink.upgrade_completo.Play();
        if (Application.loadedLevelName == "fase8")
        {
            paineisLink.linkG.vitoria();
        }
        colisao.enabled = true;
    }

    public void EscolheUP()
    {
        switch (idUP)
        {
            case 1:
                link.upDano = 1;
                break;
            case 2:
                link.upDano = 2;
                break;
            case 3:
                link.upResist = 1;
                break;
            case 4:
                link.upResist = 2;
                break;
        }
    }

    public void repara()
    {
        estruturaLink.repair();
    }

    public void destruicao()
    {
        Destroy(gameObject);
    }
}
