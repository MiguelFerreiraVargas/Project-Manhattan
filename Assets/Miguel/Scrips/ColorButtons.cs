using UnityEngine;

public class ColorButton : MonoBehaviour
{
    public int buttonIndex; // 0 verde, 1 azul, 2 amarelo, 3 vermelho
    public ButtonPuzzle puzzle;

    void OnMouseDown()
    {
        puzzle.PlayerPress(buttonIndex);
    }
}