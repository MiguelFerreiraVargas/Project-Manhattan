using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;      // Velocidade de movimento do personagem
    private Animator animator;    // Referência ao Animator

    void Start()
    {
        animator = GetComponent<Animator>();  // Pega o Animator no mesmo GameObject
    }

    void Update()
    {
        // Pega o input vertical (W/S, seta cima/baixo)
        float move = Input.GetAxis("Vertical");

        // Define se está andando baseado em uma deadzone (> 0.1)
        bool isWalking = Mathf.Abs(move) > 0.1f;

        // Atualiza o parâmetro no Animator
        animator.SetBool("isWalking", isWalking);

        // Se estiver andando, move o personagem pra frente
        if (isWalking)
        {
            transform.Translate(Vector3.forward * move * speed * Time.deltaTime);
        }
    }
}
