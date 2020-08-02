using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_spawnT : MonoBehaviour {

    public AudioSource spawnTorre;

    public scr_spawnT spawnT;
    public scr_paineis paineisLink;
    public GameObject ponto;
    public GameObject[] torres;
    public Collider colisao;

    public scr_gerenciador linkG;
    public scr_spawnT spawnTLink;
    public float tempoDelay;
    public float tempoDelayIni;
    public int idTorre;


    public Image barraProgresso;
    public GameObject barraProgressoObj;
    public GameObject iconC;
    public GameObject iconP;
    public GameObject iconD;
    public GameObject iconU;

    public scr_soundMenager somLink;

    private void Awake()
    {
        linkG = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
        paineisLink = GameObject.Find("HUD").GetComponent<scr_paineis>();
        spawnT = GetComponent<scr_spawnT>();
        colisao = GetComponent<Collider>();
        somLink = GameObject.Find("SoundMenager").GetComponent<scr_soundMenager>();
    }

    // Update is called once per frame
    void Update () {

        if (tempoDelay <= 0)
        {
            iconC.SetActive(false);
            iconP.SetActive(false);
            iconD.SetActive(false);
            iconU.SetActive(false);
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

    public void OnMouseUp()
    {
        if (paineisLink.pausado == false && paineisLink.pausado == false)
        {
            paineisLink.spawnTLink = spawnT;
            paineisLink.painelAtivo = 5;
            paineisLink.ativaPainel();
        }
      
    }

    public void spawnEstruturas()
    {
        StartCoroutine(spawnar());
    }

    IEnumerator spawnar()
    {
        tempoDelay = tempoDelayIni;
        paineisLink.fechaTudo();
        yield return new WaitForSeconds(tempoDelay);
        var clone = Instantiate(torres[idTorre], ponto.transform.position, ponto.transform.rotation);
        clone.GetComponent<scr_estrutura>().spawnado = true;
        if (clone.GetComponent<scr_estrutura>().spawnado)
        {
            clone.GetComponent<scr_estrutura>().spawnTLink = spawnT;
        }
        somLink.construcao_fim.Play();
        spawnTorre.Play();
        yield return new WaitForSeconds(1.5f);
        if (Application.loadedLevelName == "fase2")
        {
            if (idTorre == 0 || idTorre == 1)
            {
                linkG.contaCasa++;
            }
        }
    }

    public void spawnCasa()
    {
        if (linkG.gold >= 150)
        {
            tempoDelayIni = 30;
            iconC.SetActive(true);
            linkG.gold -= 150;
            idTorre = Random.Range(0, 2);
            Debug.Log(idTorre);
            somLink.comprou.Play();
            spawnEstruturas();
        }
        else
        {
            somLink.falta_grana.Play();
            Debug.Log("você só tem: " + linkG.gold + " moedas");
        }
    }

    public void spawnPrefeitura()
    {
        if (linkG.gold >= 300)
        {
            tempoDelayIni = 45;
            iconP.SetActive(true);
            linkG.gold -= 300;
            idTorre = 2;
            somLink.comprou.Play();
            spawnEstruturas();
        }
        else
        {
            somLink.falta_grana.Play();
            Debug.Log("você só tem: " + linkG.gold + " moedas");
        }
    }

    public void spawnDEAM()
    {
        iconD.SetActive(true);
        if (linkG.gold >= 300)
        {
            tempoDelayIni = 60;
            linkG.gold -= 300;
            idTorre = 3;
            somLink.comprou.Play();
            spawnEstruturas();
        }
        else
        {
            somLink.falta_grana.Play();
            Debug.Log("você só tem: " + linkG.gold + " moedas");
        }
    }

    public void spawnUniversidade()
    {
        iconU.SetActive(true);
        if (linkG.gold >= 250)
        {
            tempoDelayIni = 60;
            linkG.gold -= 250;
            idTorre = 4;
            somLink.comprou.Play();
            spawnEstruturas();
        }
        else
        {
            somLink.falta_grana.Play();
            Debug.Log("você só tem: " + linkG.gold + " moedas");
        }
    }
}
