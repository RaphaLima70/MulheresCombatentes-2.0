using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_splashScreen : MonoBehaviour
{

    public Image imagem;
    public Image imagem2;

    public Color c;
    public Color c2;

    public float n1;
    public float n2;
    public float n3;

    bool chamou = false;

    public scr_loading loadLink;

    // Use this for initialization
    void Start()
    {

        imagem = GetComponent<Image>();

        c = imagem.color;
        imagem.color = c;

        c2 = imagem2.color;
        c2.a = 0;
        imagem2.color = c2;

        n1 = 3;
        n2 = 1;
        n3 = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (n1 > 0)
        {
            n1 -= Time.deltaTime;
        }

        if (n1 <= 0)
        {
            c.a -= Time.deltaTime;
            imagem.color = c;
        }

        if (n2 > 0 && c.a <= 0)
        {
            n2 -= Time.deltaTime;
        }

        if (n2 <= 0)
        {
            c2.a += Time.deltaTime;
            imagem2.color = c2;
        }
        if (n3 > 0 && c2.a >= 1)
        {
            n3 -= Time.deltaTime;
        }

        if (n2 <= 0)
        {
            if(chamou ==false )
            {
                SceneManager.LoadSceneAsync("menuPrincipal");
                loadLink.Loader();
                chamou = true; 
            }
        }
    }
}
