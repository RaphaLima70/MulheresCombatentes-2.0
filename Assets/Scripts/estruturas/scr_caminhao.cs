using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_caminhao : MonoBehaviour {


    public float HPini;
    public float HP;

    public Image barraHp;

    public Transform ponto;

    public scr_gerenciador link;

    void Awake()
    {
        link = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
    }

    void Start()
    {
        HP = HPini + link.upHPCasas;
    }

    void Update()
    {
        barraHp.fillAmount = HP / HPini;
    }

    public void repair()
    {
        if (link.gold > 0)
        {
            Mathf.RoundToInt(HP + 3 * Time.deltaTime * link.repairSpeed);
            Mathf.RoundToInt(link.gold - 1 * Time.deltaTime);
        }
    }
}
