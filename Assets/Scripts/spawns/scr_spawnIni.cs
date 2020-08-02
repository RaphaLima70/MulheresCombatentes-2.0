using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_spawnIni : MonoBehaviour
{

    public float spawnRate;

    public float[] spawnRateIni;

    public int spawnRateAtual;

    public GameObject[] ordas0;
    public GameObject[] ordas1;
    public GameObject[] ordas2;
    public GameObject[] ordas3;

    public GameObject[] ordaAtual;

    public Vector3 spawnPosition;

    public scr_gerenciador gLink;

    public scr_path linkPath;
    public Transform[] pontoPosicoes;

    private void Awake()
    {

        gLink = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
    }

    private void Start()
    {
        pontoPosicoes = linkPath.caminho;
    }

    void Update()
    {
        spawnRateAtual = gLink.waveAtual;
        if (gLink.estado == 3)
        {
            if (gLink.tempoDeIntervalo <= 0)
            {
                if (gLink.tempoDaWave > 5)
                {
                    if (spawnRate <= 0)
                    {
                        Spawn();
                    }
                    else
                    {
                        spawnRate -= Time.deltaTime;
                    }
                }
                else
                {
                    spawnRate = spawnRateIni[spawnRateAtual];
                }
            }

        }
        Ordas();
    }

    public void Ordas()
    {
        switch (gLink.waveAtual)
        {
            case 0:
                ordaAtual = ordas0;
                break;

            case 1:
                ordaAtual = ordas1;
                break;

            case 2:
                ordaAtual = ordas2;
                break;

            case 3:
                ordaAtual = ordas3;
                break;

            default:
                if (gLink.waveAtual > 1)
                {
                    ordaAtual = ordas3;
                }
                else
                {
                    ordaAtual = ordas0;
                }
               
                break;
        }
    }

    public void Spawn()
    {
        var clone = Instantiate(ordaAtual[Mathf.RoundToInt(Random.Range(0, ordaAtual.Length))], transform.position, transform.rotation);
        spawnRate = spawnRateIni[spawnRateAtual];
        clone.GetComponent<scr_pathL>().pontoPosicoes = pontoPosicoes;
    }

}
