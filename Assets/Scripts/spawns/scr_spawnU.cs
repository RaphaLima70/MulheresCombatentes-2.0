using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_spawnU : MonoBehaviour
{
    public scr_spawnU spawnU;
    public scr_paineis paineisLink;
    public Transform spawnPoint;
    public Collider colisao;

    public scr_gerenciador linkG;
    public GameObject[] unidades;
    public float tempoDelay;
    public float tempoDelayIni;
    public int idUnidade;

    public Image barraProgresso;
    public GameObject barraProgressoObj;
    public GameObject iconP;
    public GameObject iconM;
    public GameObject iconA;
    public GameObject iconPsi;

    public scr_path linkPath;
    public Transform[] pontoPosicoes;

    public scr_soundMenager somLink;

    private void Awake()
    {
        linkG = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
        paineisLink = GameObject.Find("HUD").GetComponent<scr_paineis>();
        tempoDelayIni = 5;
        somLink = GameObject.Find("SoundMenager").GetComponent<scr_soundMenager>();
    }

    private void Start()
    {
        pontoPosicoes = linkPath.caminhoU;
    }

    // Update is called once per frame
    void Update()
    {
        if (tempoDelay <= 0)
        {
            iconA.SetActive(false);
            iconP.SetActive(false);
            iconM.SetActive(false);
            iconPsi.SetActive(false);
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
        if (paineisLink.painelAtivo == 0 && paineisLink.pausado == false)
        {
            paineisLink.spawnULink = GetComponent<scr_spawnU>();
            paineisLink.painelAtivo = 6;
            paineisLink.ativaPainel();
        }
    }

    public void spawnUnidade()
    {
        StartCoroutine(spawnar());
    }

    IEnumerator spawnar()
    {
        tempoDelay = tempoDelayIni;
        paineisLink.fechaTudo();
        yield return new WaitForSeconds(tempoDelay);
        var clone = Instantiate(unidades[idUnidade], spawnPoint.transform.position, new Quaternion(0, 0, 0, 0));
        clone.GetComponent<scr_unidades>().pontoPosicoes = pontoPosicoes;
        somLink.unidade_recrutada.Play();
    }

    public void spawnPolicial()
    {
        if (linkG.gold >= 75)
        {
            tempoDelayIni = 10;
            iconP.SetActive(true);
            linkG.gold -= 75;
            idUnidade = 0;
            somLink.comprou.Play();
            spawnUnidade();
        }
        else
        {
            somLink.falta_grana.Play();
            paineisLink.fechaTudo();
            Debug.Log("você só tem: " + linkG.gold + " moedas");
        }
    }

    public void spawnMedica()
    {
        if (linkG.gold >= 40)
        {
            tempoDelayIni = 8;
            iconM.SetActive(true);
            linkG.gold -= 40;
            idUnidade = 1;
            somLink.comprou.Play();
            spawnUnidade();
        }
        else
        {
            somLink.falta_grana.Play();
            paineisLink.fechaTudo();
            Debug.Log("você só tem: " + linkG.gold + " moedas");
        }
    }

    public void spawnAssistente()
    {
        if (linkG.gold >= 25)
        {
            tempoDelayIni = 6;
            iconA.SetActive(true);
            linkG.gold -= 25;
            idUnidade = 2;
            somLink.comprou.Play();
            spawnUnidade();
        }
        else
        {
            somLink.falta_grana.Play();
            paineisLink.fechaTudo();
            Debug.Log("você só tem: " + linkG.gold + " moedas");
        }
    }

    public void spawnPsicologa()
    {
        if (linkG.gold >= 40)
        {
            tempoDelayIni = 8;
            iconPsi.SetActive(true);
            linkG.gold -= 40;
            idUnidade = 3;
            somLink.comprou.Play();
            spawnUnidade();
        }
        else
        {
            somLink.falta_grana.Play();
            paineisLink.fechaTudo();
            Debug.Log("você só tem: " + linkG.gold + " moedas");
        }
    }

}


