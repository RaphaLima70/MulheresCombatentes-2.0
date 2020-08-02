using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_prefeitura : MonoBehaviour
{
    scr_prefeitura prefeitura;

    public scr_gerenciador link;
    public float tempoDelay;
    public float tempoDelayIni;
    public int idUP;
    public Collider colisao;

    public Image barraProgresso;
    public GameObject barraProgressoObj;
    public GameObject iconIgualdade1;
    public GameObject iconIgualdade2;
    public GameObject iconUpVida;

    public scr_paineis paineisLink;
    public scr_estrutura estruturaLink;
    public scr_soundMenager somLink;

    void Awake()
    {
        prefeitura = GetComponent<scr_prefeitura>();
        estruturaLink = GetComponent<scr_estrutura>();
        link = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
        paineisLink = GameObject.Find("HUD").GetComponent<scr_paineis>();
        somLink = GameObject.Find("SoundMenager").GetComponent<scr_soundMenager>();
    }

    public void OnMouseUp()
    {
        if (paineisLink.painelAtivo == 0 && paineisLink.pausado == false)
        {
            paineisLink.prefeituraLink = prefeitura;
            paineisLink.painelAtivo = 2;
            paineisLink.ativaPainel();
        }
    }

    public void upGrana1()
    {
        if (link.gold >= 50)
        {
            link.gold -= 50;
            iconUpVida.SetActive(true);
            paineisLink.igualdade1Btn.SetActive(false);
            paineisLink.igualdade2Btn.SetActive(true);
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

    public void upGrana2()
    {
        if (link.gold >= 150)
        {
            link.gold -= 150;
            iconUpVida.SetActive(true);
            paineisLink.igualdade2Btn.SetActive(false);
            tempoDelayIni = 30;
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


    public void upVida()
    {
        if (link.gold >= 120)
        {
            link.gold -= 120;
            iconUpVida.SetActive(true);
            paineisLink.upVidaBtn.SetActive(false);
            tempoDelayIni = 60;
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

    void Update()
    {
        if (tempoDelay <= 0)
        {
            iconIgualdade1.SetActive(false);
            iconIgualdade2.SetActive(false);
            iconUpVida.SetActive(false);
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
        if (Application.loadedLevelName == "fase6")
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
                link.upGold++;
                break;
            case 2:
                link.upGold+=2;
                break;
            case 3:
                link.upHP = 20;
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
