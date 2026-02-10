using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickNoTempo : MonoBehaviour
{
    public Image imagem;

    // 🔒 FLAG GLOBAL: espaço pertence ao minigame
    public static bool espacoEmUso = false;

    // TEMPOS ALEATÓRIOS
    public Vector2 tempoAparecerRandom = new Vector2(0.5f, 2f);
    public Vector2 tempoVermelhoRandom = new Vector2(1f, 3f);
    public Vector2 tempoVerdeRandom = new Vector2(0.5f, 1.5f);

    // FIXOS (vitória / derrota)
    public int totalRodadas = 15;
    public int maxErros = 4;

    int rodadaAtual = 0;
    int errosAtuais = 0;

    bool podeClicar = false;
    bool rodadaAtiva = false;
    bool jogoAtivo = false;

    void Start()
    {
        imagem.gameObject.SetActive(false);
    }

    public void IniciarMiniGame()
    {
        if (!jogoAtivo)
            StartCoroutine(Jogo());
    }

    IEnumerator Jogo()
    {
        jogoAtivo = true;
        espacoEmUso = true; // 🔒 trava o espaço
        rodadaAtual = 0;
        errosAtuais = 0;

        while (rodadaAtual < totalRodadas && errosAtuais < maxErros)
        {
            rodadaAtual++;
            yield return StartCoroutine(Rodada());
        }

        FimDoJogo();
    }

    IEnumerator Rodada()
    {
        rodadaAtiva = true;
        podeClicar = false;

        yield return new WaitForSeconds(
            Random.Range(tempoAparecerRandom.x, tempoAparecerRandom.y)
        );

        imagem.gameObject.SetActive(true);
        imagem.color = Color.red;

        yield return new WaitForSeconds(
            Random.Range(tempoVermelhoRandom.x, tempoVermelhoRandom.y)
        );

        imagem.color = Color.green;
        podeClicar = true;

        float tempoVerde = Random.Range(
            tempoVerdeRandom.x, tempoVerdeRandom.y
        );

        float t = 0f;
        while (t < tempoVerde)
        {
            t += Time.deltaTime;
            yield return null;

            if (!rodadaAtiva)
                yield break;
        }

        RegistrarErro();
    }

    void Update()
    {
        // ❌ fora do minigame, não usa espaço
        if (!jogoAtivo || !rodadaAtiva)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (podeClicar)
                EncerrarRodada();
            else
                RegistrarErro();
        }
    }

    void RegistrarErro()
    {
        errosAtuais++;
        EncerrarRodada();
    }

    void EncerrarRodada()
    {
        rodadaAtiva = false;
        podeClicar = false;
        imagem.gameObject.SetActive(false);
    }

    void FimDoJogo()
    {
        jogoAtivo = false;
        espacoEmUso = false; // 🔓 libera o espaço

        if (errosAtuais >= maxErros)
            Debug.Log("DERROTA");
        else
            Debug.Log("VITORIA");
    }
}
