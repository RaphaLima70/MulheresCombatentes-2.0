using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_maia : MonoBehaviour
{
    public AudioSource apontando;
    public AudioSource atirando;


    [Header("Atributos")]
    public float atackSpeed;
    public float atackSpeedIni;
    public int dano;
    public Animator animacao;
    public bool atacando;

    [Header("Controladores")]
    //public int zumbiQTD;
    public int estado;
    public Transform mulher;
    float veloRota;
    public Vector3 distancia;
    public Transform posIni;

    [Header("Links")]
    public scr_hp zumbiHP;

    // Use this for initialization
    void Start()
    {

        posIni.rotation = transform.rotation;
        veloRota = 10;
        atacando = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (zumbiHP == null)
        {
            atacando = false;

            estado = 1;
        }

        if(zumbiHP!= null)
        {
            if (zumbiHP.HP > 0)
            {
                atacando = true;
                estado = 2;
            }
            else
            {
                atacando = false;
                zumbiHP = null;
                estado = 1;
            }
        }

        switch (estado)
        {
            case 1:
                idle();
                break;

            case 2:
                atacar();
                break;
        }

    }

    public void idle()
    {
        mulher.transform.rotation = posIni.rotation;
        animacao.SetInteger("estado", 1);
    }


    public void atacar()
    {
        if (atacando)
        {
            distancia = zumbiHP.gameObject.transform.position - mulher.transform.position;
            mulher.transform.rotation = (Quaternion.Slerp(mulher.transform.rotation, Quaternion.LookRotation(new Vector3(distancia.x, mulher.transform.rotation.x, distancia.z)), veloRota * Time.deltaTime));
            if (atackSpeed <= 0)
            {
                animacao.SetInteger("estado", 2);
                animacao.Play("Maia_Atira", 0, 0f);

                atirando.Play();
                apontando.Play(22050/2);

                zumbiHP.HP -= dano;
                atackSpeed = atackSpeedIni;
            }

            atackSpeed -= Time.deltaTime;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "zumbi")
        {
            if (zumbiHP == null)
            {
                zumbiHP = other.gameObject.GetComponent<scr_hp>();
            }
            else
            {
                if (zumbiHP.HP > 0)
                {
                    atacando = true;
                    estado = 2;
                }
                else
                {
                    atacando = false;
                    zumbiHP = null;
                    estado = 1;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "zumbi")
        {
            atacando = false;
            zumbiHP = null;
        }

        if (other.gameObject == null)
        {
            zumbiHP = null;
        }
    }
}
