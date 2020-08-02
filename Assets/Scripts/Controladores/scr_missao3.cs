using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_missao3 : MonoBehaviour {

    public scr_mulherC link;
    public scr_gerenciador gLink;
    bool curar;

    private void Awake()
    {
        link = GetComponent<scr_mulherC>();
        gLink = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
    }

    // Use this for initialization
    void Start () {

        StartCoroutine(zeraHP());
	}

    IEnumerator zeraHP()
    {
        yield return new WaitForSeconds(0.5f);
        link.HP -= link.HPini;
    }

    private void Update()
    {
        if (curar == false)
        {
            if (link.curou)
            {
                curar = true;
                gLink.curou++;    
            }
        }
    }
}
