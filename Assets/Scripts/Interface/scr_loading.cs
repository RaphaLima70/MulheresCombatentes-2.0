using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_loading : MonoBehaviour
{
    public GameObject[] loading;
    public GameObject roda;
    public int randomificador;

    public int velocidade;

    private void Update()
    {
        roda.transform.Rotate(new Vector3(0,0,-1) * Time.deltaTime * velocidade);
    }

    public void Loader()
    {
        randomificador = Mathf.RoundToInt(Random.RandomRange(0, loading.Length));
        roda.SetActive(true);
        loading[randomificador].SetActive(true);
    }
}
