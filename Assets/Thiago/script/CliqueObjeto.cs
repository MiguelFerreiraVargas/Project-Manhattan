using UnityEngine;

public class CliqueObjeto : MonoBehaviour
{
    public ClickNoTempo miniGame;

    void OnMouseDown()
    {
        if (miniGame != null)
        {
            miniGame.IniciarMiniGame();
        }
    }
}
