using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scr_zumbiAmoroso : MonoBehaviour
{
    [Header("Atributos")]
    //atributos
    public int estado;
    public int drop;
    [SerializeField]
    private float fireRate;
    public float atackSpeedIni;
    public int danoFis;
    public int danoPsi;
    public int danoPatri;
    public bool atacando;
    public int danoAD;
    public Animator animacao;

    public bool causouDano;

    [Space]

    [Header("Movimentação")]
    // movimeta inimigo
    public Transform[] pontoPosicoes;
    public Transform inimigo;
    [SerializeField]
    private NavMeshAgent agent;
    private NavMeshObstacle agentO;
    private Vector3 destino;
    public Vector3 distanciaR;
    public float distancia;
    public float distanciaMin;
    public float distanciaMinBase;
    float veloRota;
    public int contaWay;
    public float speed;


    [Space]

    [Header("Links")]
    //links
    public scr_gerenciador gLink;
    public scr_unidades mulher;
    public scr_pathL linkL;
    public scr_hp hplink;
    public GameObject alvo;
    public scr_zumbiLouco zlouco;
    public scr_caminhao baseLink;

    public bool atacou;
    public bool atacandoMulher;
    public bool atacandoBase;
    public GameObject curado;
    public GameObject fumaca;


    public AudioSource[] curado_som;
    public AudioSource[] andando_som;
    public AudioSource[] atacando_som;

    public bool tocou;
    public bool fumacou;
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

    private void Awake()
    {
        gLink = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
        hplink = GetComponent<scr_hp>();
        agentO = GetComponent<NavMeshObstacle>();
        agentO.enabled = false;
        agent = GetComponent<NavMeshAgent>();
        destino = agent.destination;
    }

    void Start()
    {
        pontoPosicoes = linkL.pontoPosicoes;
        causouDano = false;
        fireRate = atackSpeedIni;
        contaWay = 0;
        veloRota = 10;
        estado = 1;
        agent.speed = speed;
    }

    void Update()
    {
        if (hplink.HP <= 0)
        {
            estado = 3;
        }

        if(estado!= 2)
        {
            atacando_som[0].Stop();
        }

        if (alvo == null && hplink.HP > 0)
        {
            atacando = false;
            agentO.enabled = false;
            agent.enabled = true;
            agent.Resume();

            estado = 1;
        }

        switch (estado)
        {
            case 1:
                andando();
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

        if (zlouco == null)
        {
            danoAD = 0;
        }
    }

    public void andando()
    {
        if (!andando_som[0].isPlaying && !andando_som[1].isPlaying && !andando_som[2].isPlaying)
        {
            andando_som[Random.Range(0, 3)].Play();
        }

        destino = pontoPosicoes[contaWay].transform.position;
        agent.destination = destino;
        animacao.SetInteger("estado", 1);
        if (Vector3.Distance(inimigo.transform.position, pontoPosicoes[contaWay].transform.position) < 2.5f)
        {
            contaWay++;
        }
    }

    public void atacar()
    {
        if (atacando)
        {

            if (agent.enabled)
            {
                destino = alvo.transform.position;
                agent.destination = destino;
            }
            causandoDano();
            Debug.DrawLine(inimigo.transform.position, alvo.transform.position);
            distancia = Vector3.Distance(inimigo.transform.position, alvo.transform.position);
            if (atacandoMulher)
            {
                if (distancia < distanciaMin)
                {

                    agent.enabled = false;
                    agentO.enabled = true;

                    distanciaR = alvo.transform.position - inimigo.transform.position;
                    inimigo.transform.rotation = Quaternion.Slerp(inimigo.transform.rotation, Quaternion.LookRotation(new Vector3(distanciaR.x, inimigo.transform.rotation.x, distanciaR.z)), veloRota * Time.deltaTime);

                    if (!atacando_som[0].isPlaying)
                    {
                        atacando_som[0].Play();
                    }

                    if (fireRate > 1.0004)
                    {
                        fireRate -= Time.deltaTime;
                    }

                    //começa a animação ataque em looping


                    animacao.SetInteger("estado", 2);
                    mulher.abracada = true;
                    // quando o firerate chegar a 1
                    if (fireRate <= 1.0004f)
                    {
                        //causar dano na mulher selecionada
                        if (mulher != null && atacou == false)
                        {
                            mulher.HP -= danoFis + danoAD;
                            atacou = true;
                        }

                        fireRate -= Time.deltaTime;

                        if (fireRate <= 0)
                        {
                            fireRate = atackSpeedIni;
                            atacou = false;
                        }
                    }
                }
                else
                {
                    animacao.SetInteger("estado", 1);
                    agent.enabled = true;
                    destino = alvo.transform.position;
                    agent.destination = destino;
                }
            }

            if (atacandoBase)
            {
                if (distancia < distanciaMinBase)
                {
                    agent.enabled = false;
                    agentO.enabled = true;
                    distanciaR = alvo.transform.position - inimigo.transform.position;
                    inimigo.transform.rotation = Quaternion.Slerp(inimigo.transform.rotation, Quaternion.LookRotation(new Vector3(distanciaR.x, inimigo.transform.rotation.x, distanciaR.z)), veloRota * Time.deltaTime);

                    if (fireRate > 1.0004)
                    {
                        fireRate -= Time.deltaTime;
                    }

                    animacao.SetInteger("estado", 4);
                    if (fireRate <= 1.0004f)
                    {
                        if (baseLink != null && atacou == false)
                        {
                            atacando_som[Random.Range(1, 3)].Play();
                            baseLink.HP -= danoPatri;
                            atacou = true;
                        }

                        fireRate -= Time.deltaTime;

                        if (fireRate <= 0)
                        {

                            fireRate = atackSpeedIni;
                            atacou = false;
                        }
                    }
                }
                else
                {
                    animacao.SetInteger("estado", 1);
                    agent.enabled = true;
                    destino = alvo.transform.position;
                    agent.destination = destino;
                }
            }

        }
    }

    public void morrendo()
    {
        if (agent.enabled)
        {
            agent.enabled = false;
            if (agentO == false)
            {
                agentO.enabled = true;
            }
        }

        if (mulher != null)
        {
            if (mulher.abracada)
            {
                mulher.abracada = false;
            }

            mulher.zumbiQTD--;
            mulher.zumbiHP = null;
            mulher = null;
        }

        if (tocou == false)
        {
            curado_som[Random.Range(0, 7)].Play();
            tocou = true;
        }

        gameObject.tag = "Untagged";
        animacao.SetInteger("estado", 3);
        StartCoroutine(SpawnarCurado());
    }

    IEnumerator SpawnarCurado()
    {
        yield return new WaitForSeconds(1);
        if (fumacou == false)
        {
            Instantiate(fumaca, transform.position, transform.rotation);
            fumacou = true;
        }

        yield return new WaitForSeconds(1);
        Curar();
        DestroyImmediate(gameObject);
    }

    void Curar()
    {
        {
            gLink.gold += drop;

            Instantiate(curado, transform.position, transform.rotation);
        }
    }

    public void causandoDano()
    {
        if (causouDano == false)
        {
            if (mulher != null && alvo == mulher.gameObject)
            {
                causouDano = true;
                mulher.zumbiQTD++;
            }

            causouDano = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "mulher")
        {
            if (mulher == null)
            {
                mulher = other.gameObject.GetComponent<scr_unidades>();

                if (mulher.fechou == false && mulher.abracada == false && mulher.HP > 0)
                {
                    atacando = true;
                    atacandoMulher = true;
                    estado = 2;
                    alvo = mulher.gameObject;
                }
            }
            else
            {
                if (mulher.HP <= 0 )
                {
                    causouDano = false;
                    mulher = null;
                    alvo = null;
                }
            }
        }

        else if (other.gameObject.tag == "base")
        {
            if (baseLink == null && mulher == null)
            {
                atacando = true;
                baseLink = other.gameObject.GetComponent<scr_caminhao>();
                atacandoBase = true;
                alvo = baseLink.gameObject;
                estado = 2;
            }
        }

        if (other.gameObject.GetComponent<scr_zumbiLouco>())
        {
            zlouco = other.gameObject.GetComponent<scr_zumbiLouco>();
            if (zlouco.atacando)
            {
                danoAD = 2;
            }
            else
            {
                danoAD = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "mulher")
        {
            atacando = false;
            mulher = null;
            alvo = null;
        }

        if (other.gameObject.GetComponent<scr_zumbiLouco>())
        {
            zlouco = null;
        }
    }
}
