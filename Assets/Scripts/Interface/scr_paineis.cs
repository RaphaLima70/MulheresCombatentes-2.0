using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_paineis : MonoBehaviour
{
    [Header("controladores de painel")]

    public GameObject painelUniversidade;
    public GameObject paineiSpawnU;
    public GameObject paineiSpawnT;
    public GameObject painelPrefeitura;
    public GameObject painelDEAM;
    public GameObject painelEstrura;
    public GameObject painelPause;
    public GameObject fechador;
    public int painelAtivo;

    public GameObject confirmacaoC;
    public GameObject confirmacaoU;
    public GameObject confirmacaoP;
    public GameObject confirmacaoD;

    public GameObject cameraLink;


    public bool pausado;

    public scr_loading loadLink;

    [Space]

    [Header("painel estruturas")]

    public scr_estrutura eLink;

    [Space]

    [Header("painel prefeitura")]

    public scr_prefeitura prefeituraLink;
    public GameObject igualdade1Btn;
    public GameObject igualdade2Btn;
    public GameObject upVidaBtn;

    [Space]

    [Header("spawn torre")]

    public scr_gerenciador linkG;
    public scr_spawnT spawnTLink;

    [Space]

    [Header("spawn unidades")]

    public scr_spawnU spawnULink;

    [Space]

    [Header("painel pause")]

    public string nomeFase;

    public GameObject confirmReiniciar;
    public GameObject confirmSair;

    [Space]

    [Header("painel universidade")]

    public scr_universidade universidadeLink;
    public GameObject hpCasaBtn;
    public GameObject multGoldBtn;
    public GameObject repairSpeedBtn;

    [Space]

    [Header("painel delegacia")]

    public scr_DEAM delegaciaLink;
    public GameObject dano1Btn;
    public GameObject dano2Btn;
    public GameObject def1Btn;
    public GameObject def2Btn;

    private void Awake()
    {
        linkG = GameObject.Find("Gerenciador").GetComponent<scr_gerenciador>();
        cameraLink = GameObject.Find("Main Camera");
        loadLink = GameObject.Find("Loadings").GetComponent<scr_loading>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Time.timeScale != 1)
        {
            pausado = true;
        }
        if (pausado)
        {
            cameraLink.GetComponent<scr_camera>().enabled = false;
        }
    }

    public void ativaPainel()
    {
        switch (painelAtivo)
        {
            case 1:
                fechaQTudo();
                fechador.SetActive(true);
                painelEstrura.SetActive(true);
                break;
            case 2:
                fechaQTudo();
                fechador.SetActive(true);
                painelPrefeitura.SetActive(true);
                break;
            case 3:
                fechaQTudo();
                fechador.SetActive(true);
                painelDEAM.SetActive(true);
                break;
            case 4:
                fechaQTudo();
                fechador.SetActive(true);
                painelUniversidade.SetActive(true);
                break;
            case 5:
                fechaQTudo();
                fechador.SetActive(true);
                paineiSpawnT.SetActive(true);
                break;
            case 6:
                fechaQTudo();
                fechador.SetActive(true);
                paineiSpawnU.SetActive(true);
                break;
            case 7:


                break;

            default:
                break;
        }
    }

    public void fechaQTudo()
    {
        pausado = true;
        confirmacaoC.SetActive(false);
        confirmacaoD.SetActive(false);
        confirmacaoP.SetActive(false);
        confirmacaoU.SetActive(false);
        painelEstrura.SetActive(false);
        painelPrefeitura.SetActive(false);
        painelDEAM.SetActive(false);
        painelUniversidade.SetActive(false);
        paineiSpawnT.SetActive(false);
        paineiSpawnU.SetActive(false);
        fechador.SetActive(false);
        cameraLink.GetComponent<scr_camera>().enabled = false;

    }

    public void fechaTudo()
    {
        confirmacaoC.SetActive(false);
        confirmacaoD.SetActive(false);
        confirmacaoP.SetActive(false);
        confirmacaoU.SetActive(false);

        pausado = false;

        eLink = null;
        painelEstrura.SetActive(false);
        prefeituraLink = null;
        painelPrefeitura.SetActive(false);
        delegaciaLink = null;
        painelDEAM.SetActive(false);
        universidadeLink = null;
        painelUniversidade.SetActive(false);
        spawnTLink = null;
        paineiSpawnT.SetActive(false);
        spawnULink = null;
        paineiSpawnU.SetActive(false);
        fechador.SetActive(false);
        cameraLink.GetComponent<scr_camera>().enabled = true;
        painelAtivo = 0;
    }

    //------------------------------------------------------------------------------------------------------------------------//

    // Casa

    public void repairEstrutura()
    {
        eLink.repair();
    }

    public void confirmaC()
    {
        confirmacaoC.SetActive(true);
    }

    public void cancelaC()
    {
        confirmacaoC.SetActive(false);
    }

    public void destroiEstrutura()
    {
        eLink.destruicao();
        fechaTudo();
    }

    //------------------------------------------------------------------------------------------------------------------------//



    //------------------------------------------------------------------------------------------------------------------------//

    // Prefeitura

    public void upHP()
    {
        prefeituraLink.upVida();
    }

    public void upGrana1()
    {
        prefeituraLink.upGrana1();
    }

    public void upGrana2()
    {
        prefeituraLink.upGrana2();
    }

    public void reparaPrefeitura()
    {
        prefeituraLink.repara();
    }

    public void confirmaP()
    {
        confirmacaoP.SetActive(true);
    }

    public void cancelaP()
    {
        confirmacaoP.SetActive(false);
    }

    public void destroiPrefeitura()
    {
        prefeituraLink.destruicao();
        fechaTudo();
    }


    //------------------------------------------------------------------------------------------------------------------------//



    //------------------------------------------------------------------------------------------------------------------------//

    // Universidade

    public void upCasaHP()
    {
        universidadeLink.upCasaHP();
    }

    public void upMultGrana()
    {
        universidadeLink.upGrana();
    }

    public void upRepair()
    {
        universidadeLink.upRepair();
    }

    public void reparaUniversidade()
    {
        universidadeLink.repara();
    }

    public void confirmaU()
    {
        confirmacaoU.SetActive(true);
    }

    public void cancelaU()
    {
        confirmacaoU.SetActive(false);
    }

    public void destroiUniversidade()
    {
        universidadeLink.destruicao();
        fechaTudo();
    }


    //------------------------------------------------------------------------------------------------------------------------//


    //------------------------------------------------------------------------------------------------------------------------//

    // Delegacia

    public void upDano1()
    {
        delegaciaLink.upDano1();
    }

    public void upDano2()
    {
        delegaciaLink.upDano2();
    }

    public void upDef1()
    {
        delegaciaLink.upDef1();
    }

    public void upDef2()
    {
        delegaciaLink.upDef2();
    }

    public void reparaDelegacia()
    {
        delegaciaLink.repara();
    }

    public void confirmaD()
    {
        confirmacaoD.SetActive(true);
    }

    public void cancelaD()
    {
        confirmacaoD.SetActive(false);
    }

    public void destroiDelegacia()
    {
        delegaciaLink.destruicao();
        fechaTudo();
    }

    //------------------------------------------------------------------------------------------------------------------------//



    //------------------------------------------------------------------------------------------------------------------------//

    // Spawn Estruturas

    public void spawnCasa()
    {
        spawnTLink.spawnCasa(); 
    }

    public void spawnPrefeitura()
    {
        spawnTLink.spawnPrefeitura();
    }

    public void spawnDEAM()
    {
        spawnTLink.spawnDEAM();
    }

    public void spawnUniversidade()
    {
        spawnTLink.spawnUniversidade();
    }


    //------------------------------------------------------------------------------------------------------------------------//

    // Spawn Unidades

    public void spawnPolicial()
    {
        fechaQTudo();
        spawnULink.spawnPolicial();
    }

    public void spawnMedica()
    {
        fechaQTudo();
        spawnULink.spawnMedica();
    }

    public void spawnAssistente()
    {
        fechaQTudo();
        spawnULink.spawnAssistente();
    }

    public void spawnPsicologa()
    {
        fechaQTudo();
        spawnULink.spawnPsicologa();
    }


    //------------------------------------------------------------------------------------------------------------------------//

    //------------------------------------------------------------------------------------------------------------------------//

    // Painel Pause

    public void abrePause()
    {
        pausado = true;
        fechaQTudo();
        painelAtivo = 7;
        Time.timeScale = 0;
        painelPause.SetActive(true);
    }

    public void fechaPause()
    {
        cameraLink.GetComponent<scr_camera>().enabled = true;
        pausado = false;
        painelAtivo = 0;
        fechador.SetActive(false);
        Time.timeScale = 1;
        painelPause.SetActive(false);
    }

    public void ReiniciarConfirm()
    {
        confirmReiniciar.SetActive(true);
    }
    public void SairConfirm()
    {
        confirmSair.SetActive(true);
    }

    public void ReiniciarCancel()
    {
        confirmReiniciar.SetActive(false);
    }

    public void SairCancel()
    {
        confirmSair.SetActive(false);
    }

    public void reiniciaFase()
    {
        StartCoroutine(ReiniciaFaseE());
    }

    public void menuPrincipal()
    {
        StartCoroutine(MenuPrincipalE());
    }

    IEnumerator ReiniciaFaseE()
    {
        Time.timeScale = 1;
        loadLink.Loader();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(Application.loadedLevel);
    }

    IEnumerator MenuPrincipalE()
    {
        Time.timeScale = 1;
        loadLink.Loader();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync("menuPrincipal");
    }

    //------------------------------------------------------------------------------------------------------------------------//
}
