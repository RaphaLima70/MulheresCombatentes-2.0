using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_universidade : MonoBehaviour
{

    scr_universidade universidade;

    public scr_gerenciador link;
    public int idUP;
    public Collider colisao;
    public float tempoDelay;
    public float tempoDelayIni;

    public Image barraProgresso;
    public GameObject barraProgressoObj;
    public GameObject iconMultGold;
    public GameObject iconHpCasas;
    public GameObject iconRepairSpeed;

    public scr_paineis paineisLink;
    public scr_estrutura estruturaLink;
    public scr_soundMenager somLink;

    void Awake()
    {
        universidade = GetComponent<scr_universidade>();
        link = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
        paineisLink = GameObject.Find("HUD").GetComponent<scr_paineis>();
        somLink = GameObject.Find("SoundMenager").GetComponent<scr_soundMenager>();
    }

    public void OnMouseUp()
    {
        if (paineisLink.painelAtivo == 0 && paineisLink.pausado == false)
        {
            paineisLink.universidadeLink = universidade;
            paineisLink.painelAtivo = 4;
            paineisLink.ativaPainel();
        }
    }

    public void upCasaHP()
    {
        if (link.gold >= 120)
        {
            link.gold -= 120;
            iconHpCasas.SetActive(true);
            paineisLink.hpCasaBtn.SetActive(false);
            tempoDelayIni = 60;
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

    public void upGrana()
    {
        if (link.gold >= 100)
        {
            link.gold -= 100;
            iconMultGold.SetActive(true);
            paineisLink.multGoldBtn.SetActive(false);
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

    public void upRepair()
    {
        if(link.gold>= 120)
        {
            link.gold -= 120;
            iconRepairSpeed.SetActive(true);
            paineisLink.repairSpeedBtn.SetActive(false);
            tempoDelayIni = 60;
            somLink.comprou.Play();
            idUP = 3;
            FazUp();
        }
        else
        {
            somLink.falta_grana.Play();
            paineisLink.fechaTudo();
            Debug.Log("você só tem: " + link.gold + " moedas");
        }
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
        if (Application.loadedLevelName == "fase5")
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
                link.upHPCasas = 50;
                break;
            case 2:
                link.multGold = 2;
                break;
            case 3:
                link.repairSpeed = 2;
                break;
        }
    }

    void Update()
    {
        if (tempoDelay <= 0)
        {
            iconHpCasas.SetActive(false);
            iconMultGold.SetActive(false);
            iconRepairSpeed.SetActive(false);
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


    public void repara()
    {
        estruturaLink.repair();
    }

    public void destruicao()
    {
        Destroy(gameObject);
    }

}




