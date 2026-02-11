using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    public List<int> sequence = new List<int>();
    public int playerIndex = 0;

    public GameObject[] buttons; // 0 = verde, 1 = azul, 2 = amarelo, 3 = vermelho
    public float lightTime = 0.5f;
    public float delayBetween = 0.3f;

    public bool canPress = false;

    public CameraShake cameraShake;

    void Start()
    {
        AddNewColor();
        StartCoroutine(ShowSequence());
    }

    void AddNewColor()
    {
        int random = Random.Range(0, 4);
        sequence.Add(random);
    }

    IEnumerator ShowSequence()
    {
        canPress = false;
        yield return new WaitForSeconds(1f);

        foreach (int i in sequence)
        {
            LightButton(i);
            yield return new WaitForSeconds(lightTime);
            UnlightButton(i);
            yield return new WaitForSeconds(delayBetween);
        }

        canPress = true;
        playerIndex = 0;
    }

    public void PlayerPress(int buttonIndex)
    {
        if (!canPress) return;

        if (buttonIndex == sequence[playerIndex])
        {
            playerIndex++;

            if (playerIndex >= sequence.Count)
            {
                AddNewColor();
                StartCoroutine(ShowSequence());
            }
        }
        else
        {
            Debug.Log("ERRO!");
            Coroutine coroutine1 = StartCoroutine(cameraShake.Shake(0.3f, 0.2f));
            Coroutine coroutine = coroutine1;
            ResetPuzzle();
        }
    }

    void ResetPuzzle()
    {
        sequence.Clear();
        AddNewColor();
        StartCoroutine(ShowSequence());
    }

    void LightButton(int index)
    {
        Renderer r = buttons[index].GetComponent<Renderer>();
        r.material.EnableKeyword("_EMISSION");
        r.material.SetColor("_EmissionColor", r.material.color * 2f);
    }

    void UnlightButton(int index)
    {
        Renderer r = buttons[index].GetComponent<Renderer>();

        r.material.SetColor("_EmissionColor", Color.black);

        if (index == 0) r.material.color = Color.green;
        if (index == 1) r.material.color = Color.blue;
        if (index == 2) r.material.color = Color.yellow;
        if (index == 3) r.material.color = Color.red;
    }
}


