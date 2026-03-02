using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void SairDoJogo()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); // Fecha o jogo na Build
#endif
    }
}