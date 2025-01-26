using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float fuerzaHaciaArriba = 10f;
    [SerializeField] private float fuerzaRebote = 10f;
    private Rigidbody2D rb;

    public static List<Bubble> allBubbles = new List<Bubble>();

    [SerializeField] private AudioClip sonidoColision;

    private Animator animator;

    [SerializeField] private string bubbleExplosionAnimation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // La burbuja se impulsa hacia arriba una vez al aparecer
        rb.AddForce(Vector2.up * fuerzaHaciaArriba, ForceMode2D.Impulse);
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Rebote manual usando la posición relativa 
            Vector2 direccion = collision.transform.position - transform.position;
            direccion.Normalize();
            collision.rigidbody.AddForce(direccion * fuerzaRebote, ForceMode2D.Impulse);

            if (sonidoColision != null)
            {
                AudioSource.PlayClipAtPoint(sonidoColision, transform.position);
            }

            if (animator != null)
            {
                animator.Play(bubbleExplosionAnimation);
                StartCoroutine(DestroyAfterAnimation());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Esperar un frame para asegurarse de que el Animator ha cambiado de estado
        yield return null;

        // Obtener información del estado actual del Animator en la capa 0
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        float animationDuration = stateInfo.length;

        if (stateInfo.loop)
        {
            yield return new WaitForSeconds(animationDuration);
        }
        else
        {
            yield return new WaitForSeconds(animationDuration);
        }

        // Destruir el objeto
        Destroy(gameObject);
    }
}
