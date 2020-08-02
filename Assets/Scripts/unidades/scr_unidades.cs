using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class scr_unidades : MonoBehaviour
{
    public AudioSource apontando;
    public AudioSource atirando;
    public AudioSource morrendo;
    //bool pra ajudar a tocar o morrendo_som;
    public bool tocou;

    [Header("Atributos")]
    public float HPini;
    public float HP;
    public float atackSpeed;
    public float atackSpeedIni;
    public int dano;
    public int def;

    [Space]

    [Header("Controladores")]
    public bool fechou;
    public bool abracada;
    public bool atacando;
    public bool andando;
    public int estado;
    public int zumbiQTD;
    public Animator animacao;


    public bool curando;
    public bool medica;
    public bool policial;
    public bool assistente;
    public bool psicologa;

    [Space]

    [Header("Movimentação")]
    public Transform[] pontoPosicoes;
    public Transform mulher;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private NavMeshObstacle agentO;
    private Vector3 destino;
    public float distancia;
    public float distanciaMin;
    public float distanciaMax;
    float veloRota;
    public int contaWay;

    [Space]

    [Header("Links")]
    public scr_hp zumbiHP;
    public scr_gerenciador gLink;
    public scr_mulherC mulherCLink;

    [Space]

    public Image barraHp;
    public float scale;

    [Space]

    [Header("Animações")]

    public string animaMulherMorre;
    public string animaMulherAtira;
    public string animaMulherAbracada;
    public string animaMulherAnda;


    public Transform caminhao;
    public bool voltaBase;

    private void Awake()
    {
        gLink = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
        agentO = GetComponent<NavMeshObstacle>();
        agentO.enabled = false;
        agent = GetComponent<NavMeshAgent>();
        destino = agent.destination;

        caminhao = GameObject.Find("caminhao").transform;
    }

    // Use this for initialization
    void Start()
    {
        veloRota = 10;
        atacando = false;
        zumbiQTD = 0;
        fechou = false;
        abracada = false;
        dano = dano + gLink.upDano;
        HP = HPini + gLink.upHP;
        andando = true;
        def = def + gLink.upResist;

        scale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);

        if (scale <= 0)
        {
            scale = 0;
        }
        barraHp.fillAmount = HP / HPini;

        if(HP> 0 && gLink.tempoDeIntervalo <=3 && gLink.tempoDeIntervalo > 1)
        {
          
            estado = 6;
            voltaBase = true;
        }

        if (andando)
        {
            agent.enabled = true;
            agentO.enabled = false;
            agent.Resume();
            agent.destination = destino;
        }
        else
        {
            agent.enabled = false;
            agentO.enabled = true;
        }

        if (abracada)
        {
            estado = 4;
        }

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

        if (HP <= 0)
        {
            estado = 3;
        }

        if (zumbiHP == null && HP > 0 && curando == false && voltaBase ==false)
        {
            andando = true;
            atacando = false;
            estado = 1;
        }

        if (atacando == false)
        {
            atackSpeed = 0;
        }


        if (zumbiHP != null)
        {
            if (zumbiHP.HP <= 0)
            {
                atacando = false;
                estado = 1;
                zumbiHP = null;
            }
        }


        switch (estado)
        {
            case 1:
                Andando();
                break;

            case 2:
                Atacar();
                break;

            case 3:
                Morrendo();
                break;

            case 4:
                Agarrada();
                break;
            case 5:
                if (medica)
                {
                    MedicaCura();
                }
                else if (psicologa)
                {
                    PsiCura();
                }
                break;
            case 6:
                VoltaBase();
                break;
            default:
                break;
        }
    }

    public void Andando()
    {
        andando = true;

        animacao.SetInteger("estado", 1);
        animacao.Play(animaMulherAnda);
        destino = pontoPosicoes[contaWay].transform.position;


        if (Vector3.Distance(mulher.transform.position, pontoPosicoes[contaWay].transform.position) < 2)
        {
            contaWay++;
        }

        if (contaWay + 1 > pontoPosicoes.Length)
        {
            Destroy(gameObject);
        }
    }

    public void Atacar()
    {
        if (atacando)
        {
            andando = false;
            distancia = Vector3.Distance(mulher.transform.position, zumbiHP.transform.position);
            mulher.transform.rotation = Quaternion.Slerp(mulher.transform.rotation, Quaternion.LookRotation(-(mulher.transform.position - zumbiHP.gameObject.transform.position)), veloRota * Time.deltaTime);
            Debug.DrawLine(mulher.transform.position, zumbiHP.transform.position);

            if (atackSpeed <= 0)
            {
                animacao.SetInteger("estado", 2);
                animacao.Play(animaMulherAtira, 0, 0f);
                if (policial)
                {
                    atirando.Play();
                    atirando.PlayDelayed(0.125f);
                    apontando.PlayDelayed(0.25f);
                    apontando.PlayDelayed(0.5f);
                }
                else
                {
                    atirando.Play();
                    apontando.PlayDelayed(0.25f);
                }
                zumbiHP.HP -= dano;
                atackSpeed = atackSpeedIni;
            }
            else
            {
                atackSpeed -= Time.deltaTime;
            }
        }
    }

    public void Agarrada()
    {
        andando = false;
        animacao.Play(animaMulherAbracada);
    }


    public void Morrendo()
    {
        if (tocou == false)
        {
            tocou = true;
            morrendo.Play();

        }

        StartCoroutine(diminui());
        gameObject.tag = "Untagged";
        andando = false;
        //animacao.Play(animaMulherMorre);
        Destroy(gameObject, 2);
    }

    IEnumerator diminui()
    {
        animacao.Play(animaMulherMorre);
        yield return new WaitForSeconds(1);
        scale -= Time.deltaTime;
    }

    public void VoltaBase()
    {
        distancia = Vector3.Distance(mulher.position, caminhao.position);
        animacao.SetInteger("estado", 1);
        destino = caminhao.transform.position;
        agent.destination = destino;
 

        if (distancia < 3.5f)
        {
            Destroy(gameObject);
        }
    }

    public void MedicaCura()
    {
        if (curando)
        {
            if (agent.enabled)
            {
                destino = mulherCLink.transform.position;
                agent.destination = destino;
            }
            Debug.DrawLine(mulher.transform.position, mulherCLink.transform.position);
            distancia = Vector3.Distance(mulher.transform.position, mulherCLink.transform.position);
            if (distancia < distanciaMin)
            {
                mulher.transform.rotation = Quaternion.Slerp(mulher.transform.rotation, Quaternion.LookRotation(-(mulher.transform.position - mulherCLink.gameObject.transform.position)), veloRota * Time.deltaTime);
                mulherCLink.CurandoHP();
            }
        }
    }

    public void PsiCura()
    {
        if (curando)
        {
            if (agent.enabled)
            {
                destino = mulherCLink.transform.position;
                agent.destination = destino;
            }
            Debug.DrawLine(mulher.transform.position, mulherCLink.transform.position);
            distancia = Vector3.Distance(mulher.transform.position, mulherCLink.transform.position);
            if (distancia < distanciaMin)
            {
                mulher.transform.rotation = Quaternion.Slerp(mulher.transform.rotation, Quaternion.LookRotation(-(mulher.transform.position - mulherCLink.gameObject.transform.position)), veloRota * Time.deltaTime);
                mulherCLink.CurandoPsi();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (medica)
        {
            if (other.gameObject.tag == "mulherCDown")
            {
                if (mulherCLink == null)
                {
                    mulherCLink = other.gameObject.GetComponent<scr_mulherC>();
                }
                else
                {
                    distancia = Vector3.Distance(mulher.transform.position, mulherCLink.transform.position);
                    if (mulherCLink.HP < mulherCLink.HPini && mulherCLink.curadoRecentemente == false)
                    {
                        if (distancia < 8)
                        {
                            mulherCLink.medica = mulher.gameObject;
                            curando = true;
                            estado = 5;
                        }
                        else
                        {
                            curando = false;
                            estado = 1;
                        }
                    }
                    else
                    {
                        mulherCLink = null;
                        curando = false;
                        estado = 1;
                    }
                }
            }
        }

        if (psicologa)
        {
            if (other.gameObject.tag == "mulherCDown")
            {
                if (mulherCLink == null)
                {
                    mulherCLink = other.gameObject.GetComponent<scr_mulherC>();
                }
                else
                {
                    distancia = Vector3.Distance(mulher.transform.position, mulherCLink.transform.position);
                    if (mulherCLink.psi < mulherCLink.psiIni && mulherCLink.curadoRecentemente == false)
                    {
                        if (distancia < 8)
                        {
                            mulherCLink.medica = mulher.gameObject;
                            curando = true;
                            estado = 5;
                        }
                        else
                        {
                            curando = false;
                            estado = 1;
                        }
                    }
                    else
                    {
                        mulherCLink = null;
                        curando = false;
                        estado = 1;
                    }
                }
            }
        }

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
    }
}