using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_sons : MonoBehaviour {

    public AudioSource somBotao;
    public AudioSource selecFase;


	public void BotaoSom()
    {
        somBotao.Play();
    }

    public void SelecFase()
    {
        selecFase.Play();
    }


}
