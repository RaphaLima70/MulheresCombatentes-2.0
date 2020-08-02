using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_dialogoGatilho : MonoBehaviour
{

    public scr_dialogo dialogo;
    public scr_gerenciador gerenciador;

    public bool endGame;
    public bool gatilho;

    private void Awake()
    {
        if (endGame == false)
        {
            gerenciador = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
        }

    }

    private void Start()
    {
        gatilho = true;
    }

    private void Update()
    {
        if (endGame == false)
        {
            if (gerenciador.dialogo)
            {
                GatilhoDialogo();
                gerenciador.dialogo = false;
            }
        }
        else
        {
            if (gatilho)
            {
                GatilhoDialogo();
                gatilho = false;
            }
           
        }
    }

    public void GatilhoDialogo()
    {
        FindObjectOfType<scr_dialogoG>().ComecaDialogo(dialogo);
    }

}
