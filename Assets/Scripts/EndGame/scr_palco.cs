using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_palco : MonoBehaviour {

    public Animator animacao;

	// Use this for initialization
	void Start () {
        StartCoroutine(AbreCortina());
	}

    IEnumerator AbreCortina()
    {
        animacao.SetInteger("estado", 1);
        yield return new WaitForSeconds(3.1f);
        animacao.SetInteger("estado", 2);
    }

    public void FechaCortina()
    {
        animacao.SetInteger("estado", 3);
    }
}
