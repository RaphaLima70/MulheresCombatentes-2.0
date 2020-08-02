using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_lab : MonoBehaviour {

    public GameObject[] cards;
    public int selec;

    public GameObject vai;
    public GameObject volta;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(selec == cards.Length -1)
        {
            vai.SetActive(false);
        }
        else
        {
            vai.SetActive(true);
        }

        if (selec == 0)
        {
            volta.SetActive(false);
        }
        else
        {
            volta.SetActive(true);
        }

        
	}

    public void Limpa()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].SetActive(false);
        }
    }

    public void Passa()
    {
        Limpa();
        selec++;
        cards[selec].SetActive(true);
    }

    public void Volta()
    {
        Limpa();
        selec--;
        cards[selec].SetActive(true);
    }
}
