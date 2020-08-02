using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_movCamera : MonoBehaviour
{
    public GameObject player;
    public Transform alvo;
    public Transform[] ways;

    public float veloRota;
    public float velocidade;
    public float distancia;
    public int contaWay;

    public Transform[] caminhoJogarI;
    public Transform[] caminhoJogarV;
    public Transform[] caminhoLabI;
    public Transform[] caminhoLabV;
    public Transform[] caminhoSobreI;
    public Transform[] caminhoSobreV;

    public Transform jogar;
    public Transform lab;
    public Transform sobre;
    public Transform startAlvo;

    public scr_path caminhoJogar;
    public scr_path caminhoLab;
    public scr_path caminhoSobre;

    public bool andando;
    public int caminho;

    public GameObject voltarBtn;

    public GameObject painelMain;
    public GameObject painelJogar;
    public GameObject painelLab;
    public GameObject painelSobre;

    public GameObject painelQuit;

    public GameObject iconIni;
    public GameObject iconJogar;
    public GameObject iconLab;
    public GameObject iconSobre;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;

        andando = false;
        TrocaMenu();
        caminho = 2;

        caminhoJogarI = caminhoJogar.caminho;
        caminhoJogarV = caminhoJogar.caminhoU;

        caminhoLabI = caminhoLab.caminho;
        caminhoLabV = caminhoLab.caminhoU;

        caminhoSobreI = caminhoSobre.caminho;
        caminhoSobreV = caminhoSobre.caminhoU;

    }

    // Update is called once per frame
    void Update()
    {
        if (andando)
        {
            Anda();
            TrocaMenu();
        }
        else
        {
            var rot = Quaternion.LookRotation(alvo.transform.position - player.transform.position);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, veloRota * Time.deltaTime);

            var angulo = Quaternion.Angle(player.transform.rotation, rot);

            if (angulo <= 0.00000001f)
            {
                if (caminho == 1)
                {
                    voltarBtn.SetActive(true);
                    iconJogar.SetActive(true);
                    painelJogar.SetActive(true);
                }
                if (caminho == 3)
                {
                    voltarBtn.SetActive(true);
                    iconLab.SetActive(true);
                    painelLab.SetActive(true);
                }
                if (caminho == 5)
                {
                    voltarBtn.SetActive(true);
                    iconSobre.SetActive(true);
                    painelSobre.SetActive(true);
                }
                if (caminho == 2 || caminho == 4 || caminho == 6)
                {
                    voltarBtn.SetActive(false);
                    iconIni.SetActive(true);
                    painelMain.SetActive(true);
                }
            }
        }
    }

    public void TrocaMenu()
    {
        voltarBtn.SetActive(false);
        iconIni.SetActive(false);
        iconJogar.SetActive(false);
        iconLab.SetActive(false);
        iconSobre.SetActive(false);
        painelMain.SetActive(false);
        painelJogar.SetActive(false);
        painelLab.SetActive(false);
        painelSobre.SetActive(false);
    }

    public void Anda()
    {
        switch (caminho)
        {
            case 1:
                ways = caminhoJogarI;
                break;

            case 2:
                ways = caminhoJogarV;
                break;

            case 3:
                ways = caminhoLabI;
                break;

            case 4:
                ways = caminhoLabV;
                break;

            case 5:
                ways = caminhoSobreI;
                break;

            case 6:
                ways = caminhoSobreV;
                break;

        }

        distancia = Vector3.Distance(player.transform.position, ways[contaWay].position);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(ways[contaWay].position - player.transform.position), veloRota * Time.deltaTime);
        Debug.DrawLine(ways[contaWay].position, player.transform.position, Color.green);
        player.transform.position += player.transform.forward * velocidade * Time.deltaTime;
        if (distancia < 0.5f)
        {
            contaWay++;
        }

        if (contaWay >= ways.Length)
        {
            Parou();

        }
    }


    public void Parou()
    {
        andando = false;

    }

    public void JogarVai()
    {
        contaWay = 0;
        alvo = jogar;
        caminho = 1;
        andando = true;
    }

    public void JogarVolta()
    {
        contaWay = 0;
        caminho = 2;
        andando = true;
    }

    public void LabVai()
    {
        contaWay = 0;
        alvo = lab;
        caminho = 3;
        andando = true;
    }

    public void LabVolta()
    {
        contaWay = 0;
        caminho = 4;
        andando = true;
    }

    public void SobreVai()
    {
        contaWay = 0;
        alvo = sobre;
        caminho = 5;
        andando = true;
    }

    public void SobreVolta()
    {
        contaWay = 0;
        caminho = 6;
        andando = true;
    }


    public void Voltar()
    {
        if (caminho == 1)
        {
            JogarVolta();
        }
        if (caminho == 3)
        {
            LabVolta();
        }
        if (caminho == 5)
        {
            SobreVolta();
        }
        alvo = startAlvo;
    }

    public void AbreQuit()
    {
        painelQuit.SetActive(true);
    }

    public void CancelaQuit()
    {
        painelQuit.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
