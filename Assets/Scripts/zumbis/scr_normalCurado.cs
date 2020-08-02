using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scr_normalCurado : MonoBehaviour
{

    [SerializeField]
    private NavMeshAgent agent;

    Transform posicao;
    public Vector3 destino;
    public Transform caminhao;
    public float distancia;
    public bool go;

    // Use this for initialization
    void Start()
    {
        go = false;
        posicao = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        caminhao = GameObject.Find("caminhao").transform;
        StartCoroutine(delay());
    }

    // Update is called once per frame
    void Update()
    {
        if (go)
        {
            destino = caminhao.transform.position;
            agent.destination = destino;

            distancia = Vector3.Distance(posicao.position, destino);
            if (distancia < 3.5f)
            {
                Destroy(gameObject);
            }
        } 
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0f);
        go = true;
    }
}
