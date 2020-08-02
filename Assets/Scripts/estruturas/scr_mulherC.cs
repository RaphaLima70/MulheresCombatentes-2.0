using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_mulherC : MonoBehaviour
{
    public AudioSource atirando;
    public AudioSource apontando;
    public AudioSource morre;

    //bool pra ajudar a tocar morre_som
    public bool tocou;

    [Header("Atributos")]
    public float atackSpeed;
    public float atackSpeedIni;
    public int dano;
    public int def;
    public float HP;
    public float HPini;
    public float psi;
    public float psiIni;
    public Animator animacao;

    [Header("Controladores")]
    //public int zumbiQTD;
    public bool fechou;
    public bool abracada;
    public bool atacando;
    public int estado;
    public int zumbiQTD;
    public Transform mulher;
    public Transform posIni;
    float veloRota;

    [Header("Links")]
    public scr_hp zumbiHP;
    public scr_estrutura estruturaLink;

    public Image barraHp;
    public Image barraPsi;

    public GameObject medica;
    public GameObject psicologa;
    public float delayCura;
    public float delayCuraIni;
    public bool curadoRecentemente;

    public GameObject iconeMed;
    public GameObject iconePsi;

    public bool curou;

    public scr_gerenciador linkG;

    private void Awake()
    {
        linkG = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
    }

    // Use this for initialization
    void Start()
    {
        posIni.rotation = transform.rotation;
        psi = psiIni;
        HP = HPini;
        veloRota = 10;
        atacando = false;
        zumbiQTD = 0;
        fechou = false;
        abracada = false;
        def = def + linkG.upResist;
    }

    // Update is called once per frame
    void Update()
    {
        if(estado != 3)
        {
            tocou = false;
        }

        barraHp.fillAmount = HP / HPini;
        barraPsi.fillAmount = psi / psiIni;

        if (zumbiQTD <= 0)
        {
            zumbiQTD = 0;
        }

        if (zumbiQTD >= 3)
        {
            fechou = true;
        }
        else
        {
            fechou = false;
        }

        if (HP <= 0 || psi <= 0)
        {
            estado = 3;
        }

        if (zumbiHP != null)
        {
            if (zumbiHP.HP <= 0)
            {
                zumbiHP = null;
            }
        }

        if (delayCura <= 0)
        {
            curadoRecentemente = false;
        }
        else
        {
            delayCura -= Time.deltaTime;
            curadoRecentemente = true;
        }

        if (zumbiHP == null && HP > 0 && psi > 0)
        {
            atacando = false;
            estado = 1;
        }

        switch (estado)
        {
            case 1:
                idle();
                break;

            case 2:
                atacar();
                break;

            case 3:
                morrendo();
                break;

            default:
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
            mulher.transform.rotation = Quaternion.Slerp(mulher.transform.rotation, Quaternion.LookRotation(-(mulher.transform.position - zumbiHP.gameObject.transform.position)), veloRota * Time.deltaTime);
            if (atackSpeed <= 0)
            {
                animacao.SetInteger("estado", 2);
                animacao.Play("Mulher_Atira", 0, 0f);

                atirando.Play();
                apontando.Play(22050);

                zumbiHP.HP -= dano;
                atackSpeed = atackSpeedIni;
            }

            atackSpeed -= Time.deltaTime;

        }
    }

    public void morrendo()
    {
        if (!tocou)
        {
            tocou = true;
            morre.Play();
        }
        gameObject.tag = "mulherCDown";
        animacao.SetInteger("estado", 3);
        if (HP > 0 && psi >0)
        {
          
            animacao.SetInteger("estado", 1);
            estado = 1;
        }
    }

    public void CurandoHP()
    {
        StartCoroutine(ICurandoHP());
    }
    
    IEnumerator ICurandoHP()
    {
        curadoRecentemente = true;
        Destroy(medica.gameObject);
        iconeMed.SetActive(true);
        yield return new WaitForSeconds(5);
        iconeMed.SetActive(false);
        if (Application.loadedLevelName == "fase3")
        {
            curou = true;
        }
        HP = HPini;
        delayCura = delayCuraIni;
        gameObject.tag = "mulherC";
        if (animacao.CompareTag("mulherC")) 
        {
            animacao.Play("Mulher_Levanta");
        }
    }

    public void CurandoPsi()
    {
        StartCoroutine(ICurandoPsi());
    }

    IEnumerator ICurandoPsi()
    {
        curadoRecentemente = true;
        Destroy(medica.gameObject);
        iconePsi.SetActive(true);
        yield return new WaitForSeconds(5);
        iconePsi.SetActive(false);
        psi = psiIni;
        delayCura = delayCuraIni;
    }

    private void OnTriggerStay(Collider other)
    {
        if (HP > 0)
        {
            if (other.gameObject.tag == "zumbi")
            {
                if (zumbiHP == null)
                {
                    zumbiHP = other.gameObject.GetComponent<scr_hp>();
                    atacando = true;
                    estado = 2;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "zumbi")
        {
            zumbiQTD--;
            atacando = false;
            zumbiHP = null;
        }

        if (other.gameObject == null)
        {
            zumbiHP = null;
        }
    }
}
