using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_gerenciador : MonoBehaviour
{
    [Header("gerenciador de gold")]

    [Space]

    public float gold;
    public int goldIni;
    public Text gText;

    [Header("gerenciador de upgrades")]

    [Space]

    public int upGold;
    public int upHPCasas;
    public int multGold;
    public int upDano;
    public int upResist;
    public int upHP;
    public float repairSpeed;

    [Header("gerenciador de links")]

    [Space]

    public scr_caminhao cLink;
    public scr_WinLoose wlLink;

    [Header("gerenciador de Fase")]

    [Space]

    public bool venceu;
    public bool perdeu;

    [Header("contadores")]

    public float tempoAtual;
    public int tempoFinal;
    public float tempoDaWave;
    public float[] tempoDaWaveIni;
    public float tempoDeIntervalo;
    public int tempoDeIntervaloIni;
    public int waveAtual;
    public int contaWaves;
    public bool partidaSurvivor;
    public int estado;
    public string nomeFase;

    public int contaCasa = 0;
    public int curou = 0;

    [Space]

    [Header("paineis")]

    public GameObject painelStart;
    public GameObject painelIntervalo;
    public Text contaTempoFaseText;
    public int minutos;
    public int segundos;

    public scr_paineis linkP;
    public bool dialogo;
    public GameObject dialogoObj;
    public bool startFade;

    public GameObject[] zumbis;

    public scr_soundMenager somLink;

    //bool pra ajudar a tocar som de orda
    public bool tocou;

    private void Awake()
    {
        linkP = GameObject.Find("HUD").GetComponent<scr_paineis>();
        cLink = GameObject.Find("caminhao").GetComponent<scr_caminhao>();
        wlLink = GameObject.Find("Vitoria/Derrota").GetComponent<scr_WinLoose>();

        somLink = GameObject.Find("SoundMenager").GetComponent<scr_soundMenager>();
    }

    // Use this for initialization
    void Start()
    {
        venceu = false;
        perdeu = false;

        Time.timeScale = 1;

        nomeFase = Application.loadedLevelName;

        estado = 0;
        contaWaves = tempoDaWaveIni.Length;


        upGold = 3;
        upHPCasas = 0;
        multGold = 1;
        upDano = 0;
        upResist = 0;
        upHP = 0;
        repairSpeed = 1;

        gold = goldIni;
        resetador();

        dialogo = false;

        if (startFade == false)
        {
            preGame();
        }

    }

    void Update()
    {
        zumbis = GameObject.FindGameObjectsWithTag("zumbi");

        if (linkP.painelPause.active)
        {
            AudioListener.volume = 0.2f;
        }
        else
        {
            AudioListener.volume = 1f;
        }

        controlaNiveis();
        if (painelStart.active == false)
        {
            contaTempo();
            gold += Time.deltaTime * upGold;
        }

        switch (nomeFase)
        {
            case "fase2":
                Missao2();
                break;

            case "fase3":
                Missao3();
                break;
        }

        switch (estado)
        {
            case 0:
                
                break;
            case 1:
                //start game
                startGame();
                break;
            case 2:
                //intervalo
             
                intervalo();
                break;
            case 3:
                //start wave
                startWaves();
                break;
        }

        if (dialogoObj.active)
        {
            linkP.pausado = true;
        }

        if (cLink.HP <= 0)
        {
            if (perdeu == false)
            {
                if(somLink.musicaLoose.isPlaying == false)
                {
                    somLink.musicaLoose.Play();
                }
                AudioListener.volume = 0.5f;
                wlLink.perdeu();
                perdeu = true;
            }
        }

      
        gText.text = ("$ " + Mathf.RoundToInt(gold));
    }

    public void preGame()
    {
        painelStart.SetActive(true);
        dialogo = true;
    }

    public void startGame()
    {
        somLink.radio_estatica.Stop();
        somLink.dialogo_fim.Play();
        linkP.pausado = false;
        linkP.cameraLink.GetComponent<scr_camera>().enabled = true;
        painelStart.SetActive(false);
        Time.timeScale = 1;
        waveAtual = -1;
        estado = 2;
    }

    public void controlaNiveis()
    {
        if (partidaSurvivor)
        {
            if (tempoAtual > tempoFinal)
            {
                vitoria();
            }
        }
    }

    public void vitoria()
    {
       if(venceu == false)
        {
            if (somLink.musicaWin.isPlaying == false)
            {
                somLink.musicaWin.Play();
            }
            AudioListener.volume = 0.5f;
            venceu = true;
            wlLink.ganhou();
        }
    }


    public void contaTempo()
    {
        tempoAtual += Time.deltaTime;
        minutos = Mathf.FloorToInt(tempoAtual / 60);
        segundos = Mathf.RoundToInt(tempoAtual - (minutos * 60));
        if (segundos < 10)
        {
            contaTempoFaseText.text = (minutos + ":0" + segundos);
        }
        else
        {
            contaTempoFaseText.text = (minutos + ":" + segundos);
        }

    }

    public void intervalo()
    {
        if (zumbis.Length == 0)
        {
            if (tempoDeIntervalo <= 0)
            {
                tocou = false;
                painelIntervalo.SetActive(false);
                if (waveAtual == contaWaves - 1)
                {
                    estado = 3;
                }
                else
                {
                    waveAtual++;
                    estado = 3;
                }
            }
            else
            {
                if(tocou == false)
                {
                    somLink.next_orda.Play();
                    tocou = true;
                }
               
                tempoDeIntervalo -= Time.deltaTime;
                painelIntervalo.SetActive(true);
            }
        }
    }

    public void startWaves()
    {
        tempoDaWave -= Time.deltaTime;
        if (tempoDaWave <= 0)
        {
            resetador();
            estado = 2;

        }
    }

    public void Missao2()
    {
        if (contaCasa == 2)
        {
            vitoria();
        }
    }

    public void Missao3()
    {
        if (curou == 2)
        {
            vitoria();
        }
    }

    public void resetador()
    {
        tempoDeIntervalo = tempoDeIntervaloIni;
        tempoDaWave = tempoDaWaveIni[waveAtual];
    }
}
