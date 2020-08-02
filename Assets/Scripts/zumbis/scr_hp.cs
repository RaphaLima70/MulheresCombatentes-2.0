using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_hp : MonoBehaviour {

    public float HP;
    public float HPini;
    public Image barraHp;

    public scr_gerenciador link;

    void Awake()
    {
        link = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
    }

    // Use this for initialization
    void Start () {
        HP = HPini;
	}
	
	// Update is called once per frame
	void Update () {

        barraHp.fillAmount = HP / HPini;
    }
}
