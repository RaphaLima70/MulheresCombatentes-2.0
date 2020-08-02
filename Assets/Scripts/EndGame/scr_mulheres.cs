using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_mulheres : MonoBehaviour {

    public Animator animacao;

    private void Awake()
    {
        animacao = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(Espera());
	}

    IEnumerator Espera()
    {
        animacao.enabled = false;
        yield return new WaitForSeconds(Random.Range(0,3.4f));
        animacao.enabled = true;
    }

}
