using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_estrutura : MonoBehaviour
{
    public AudioSource morre;

    public float HPini;
    public float HP;
    public int zumbiQTD;
    public bool fechou;
    public bool reparando;

    public Transform ponto;
    public scr_gerenciador link;
    public scr_paineis paineisLink;
    public Image barraHp;

    public GameObject iconRepair;

    public scr_mulherC mulher;

    public float scale;

    public bool casa;

    public scr_spawnT spawnTLink;
    public bool spawnado;

    void Awake()
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        link = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
        paineisLink = GameObject.Find("HUD").GetComponent<scr_paineis>();
        scale = 0;

    }

    // Use this for initialization
    void Start()
    {

        zumbiQTD = 0;
        fechou = false;
        HPini += link.upHPCasas;
        HP = HPini;
        StartCoroutine(delay());
    }
    IEnumerator cresce()
    {
        yield return new WaitForSeconds(0);
        scale += Time.deltaTime * 4;
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        if (casa)
        {
            link.upGold++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        barraHp.fillAmount = HP / HPini;

        gameObject.transform.localScale = new Vector3(scale, scale, scale);

        if (spawnado)
        {
            if (HP > 0)
            {
                spawnTLink.enabled = false;
            }
        }
      

        if (reparando)
        {
            iconRepair.SetActive(true);
        }
        else
        {
            iconRepair.SetActive(false);
        }

        if (scale < 1)
        {
            StartCoroutine(cresce());
        }

        if (scale > 1)
        {
            scale = 1;
        }
    

        if (HP <= 0)
        {
            spawnTLink.enabled = true;
            destruicao();
        }

        if (zumbiQTD == 3)
        {
            fechou = true;
        }

        if (reparando)
        {
            if (HP < HPini && link.gold > 0)
            {
                Mathf.RoundToInt(HP += 3 * Time.deltaTime * link.repairSpeed);
                Mathf.RoundToInt(link.gold -= 1 * Time.deltaTime);
            }
            else
            {
                reparando = false;
            }
        }
    }

    public void repair()
    {
        reparando = true;
    }

    public void OnMouseUp()
    {
        if (casa)
        {
            if (paineisLink.pausado == false && paineisLink.pausado == false)
            {
                paineisLink.eLink = GetComponent<scr_estrutura>();
                paineisLink.painelAtivo = 1;
                paineisLink.ativaPainel();
            }
        }
    }

    public void destruicao()
    {
      
        morre.Play();
        gameObject.tag = "Untagged";
        Destroy(gameObject, 1);
    }
}
