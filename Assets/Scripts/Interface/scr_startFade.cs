using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_startFade : MonoBehaviour
{

    public Image imagem;
    public Image imagem2;
    public Color c;

    public float tempo = 2f;
    public bool comecou;

    public scr_gerenciador link;
    public GameObject cameraLink;


    private void Awake()
    {
        link = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
        cameraLink = GameObject.Find("Main Camera");
    }
    // Use this for initialization
    void Start()
    {
        link.linkP.pausado = true;

        comecou = false;
        imagem = GetComponent<Image>();

        c = imagem.color;
        imagem.color = c;

        c.a = 0;
        cameraLink.GetComponent<scr_camera>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        imagem.color = c;
        imagem2.color = c;
        if (comecou == false)
        {
            c.a += 0.5f * Time.deltaTime;
            tempo -= Time.deltaTime;
            if (tempo <= 0)
            {
                comecou = true;
            }
        }
        else
        {
            c.a -= 0.5f * Time.deltaTime;
            if (c.a <= 0)
            {
                link.preGame();
                Destroy(gameObject);
            }
        }
    }
}
