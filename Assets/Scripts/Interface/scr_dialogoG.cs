using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_dialogoG : MonoBehaviour {

    private Queue<string> sentencas;
    public Animator animacao;
    public Text dialogoTexto;
    public scr_gerenciador gerenciador;
    public int[] idPersonagem;
    public int contaId;
    public GameObject botao;

    public GameObject imgMaia;
    public GameObject imgMedica;
    public GameObject imgDCasa;
    public GameObject imgPolicial;
    public GameObject imgAssist;
    public GameObject imgPsi;

    public bool endGame;
    public scr_loading loadLink;
    public scr_palco linkPalco;
    public scr_soundMenager somLink;


    //bool pra ajudar a tocar o radio_estatica
    public bool tocou;

    private void Awake()
    {
        if (endGame == false)
        {
            gerenciador = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
            somLink = GameObject.Find("SoundMenager").GetComponent<scr_soundMenager>();
        }
        else
        {
            loadLink = GameObject.Find("Loadings").GetComponent<scr_loading>();
            linkPalco = GameObject.Find("Palco").GetComponent<scr_palco>();
        }       
    }

    void Start () {
        sentencas = new Queue<string>();
        botao.SetActive(false);
        idPersonagem = GetComponent<scr_dialogoG>().idPersonagem;
        contaId = -1;
    }

    public void personagens()
    {
        if(contaId +1< idPersonagem.Length)
        {
            contaId += 1;
        }
        
        limpa();
        switch (idPersonagem[contaId])
        {
            case 1:
                imgMaia.SetActive(true);
                break;
            case 2:
                imgMedica.SetActive(true);
                break;
            case 3:
                imgDCasa.SetActive(true);
                break;
            case 4:
                imgPolicial.SetActive(true);
                break;
            case 5:
                imgAssist.SetActive(true);
                break;
            case 6:
                imgPsi.SetActive(true);
                break;
            default:
                Debug.Log("acabou dialogo");
                break;
        }
    }

    public void limpa()
    {
                imgMaia.SetActive(false);
                imgMedica.SetActive(false);
                imgDCasa.SetActive(false);
                imgPolicial.SetActive(false);
                imgAssist.SetActive(false);
                imgPsi.SetActive(false);
    }

    public void ComecaDialogo(scr_dialogo dialogo)
    {
        animacao.SetBool("aberto", true);    
        
        sentencas.Clear();
        
        foreach (string sentenca in dialogo.sentencas)
        {
            sentencas.Enqueue(sentenca);
        }
        MostrarProxSentenca();
    }

    IEnumerator EscreveSentenca(string sentenca)
    {
        dialogoTexto.text = "";
        foreach (char letras in sentenca.ToCharArray())
        {
            dialogoTexto.text += letras;
            yield return null;
        }
        botao.SetActive(true);
    }

    private void Update()
    {
        if (Application.loadedLevelName != "EndGame")
        {
            if (somLink.dialogo_inicio.isPlaying == false && somLink.dialogo_fim.isPlaying == false)
            {
                if (tocou == false)
                {
                    tocou = true;
                    somLink.radio_estatica.Play();
                }
            }
        }
        
    }

    public void MostrarProxSentenca()
    {
        if (sentencas.Count == 0)
        {
            AcabaDialogo();
            return;
        }
        string sentenca = sentencas.Dequeue();
        StartCoroutine(EscreveSentenca(sentenca));
        if (Application.loadedLevelName != "EndGame")
        {
            somLink.dialogo_inicio.Play();
        }
        botao.SetActive(false);
        personagens();    
    }

    IEnumerator AcabaI()
    {
        animacao.SetBool("aberto", false);
        if (Application.loadedLevelName != "EndGame")
        {
            somLink.dialogo_fim.Play();
            somLink.radio_estatica.Stop();
        }

        if (endGame == false)
        {
            gerenciador.startGame();
        }
        else
        {
            botao.SetActive(false);
            linkPalco.FechaCortina();
            yield return new WaitForSeconds(2);
            loadLink.Loader();
            SceneManager.LoadSceneAsync("menuPrincipal");

        }
    }

    public void AcabaDialogo()
    {
        StartCoroutine(AcabaI());
    }
}
