using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_soundMenager : MonoBehaviour
{
    [Header("sons interface")]

    [Space]

    public AudioSource comprou;//ok
    public AudioSource construcao_fim;//ok
    public AudioSource dialogo_fim;//ok
    public AudioSource radio_estatica;//ok
    public AudioSource dialogo_inicio;//ok
    public AudioSource falta_grana;//ok
    public AudioSource next_orda;//ok
    public AudioSource unidade_recrutada;//ok
    public AudioSource upgrade_completo;//ok
    public AudioSource click_botao;//ok

    public AudioSource musicaFase;
    public AudioSource musicaWin;
    public AudioSource musicaLoose;

    public void BotaoClick()
    {
        click_botao.Play();
    }

}
