using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float fuerzaHaciaArriba = 10f;
    [SerializeField] private float fuerzaRebote = 10f;
    private Rigidbody2D rb;

    public static List<Bubble> allBubbles = new List<Bubble>();

    // Variables para guardar el estado al pausar.
    private Vector2 savedVelocity;
    private float savedAngularVelocity;
    private float savedGravityScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Asegúrate de que el Rigidbody2D esté en modo Dynamic
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Agregamos esta burbuja a la lista estática al instanciarse
        allBubbles.Add(this);
    }

    private void Start()
    {
        // La burbuja se impulsa hacia arriba una vez al aparecer
        rb.AddForce(Vector2.up * fuerzaHaciaArriba, ForceMode2D.Impulse);
    }

    private void OnDestroy()
    {
        // La removemos de la lista cuando se destruye
        allBubbles.Remove(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Rebote manual usando la posición relativa 
            Vector2 direccion = collision.transform.position - transform.position;
            direccion.Normalize();
            collision.rigidbody.AddForce(direccion * fuerzaRebote, ForceMode2D.Impulse);

            // Destruir la burbuja en cuanto colisione con el jugador
            Destroy(gameObject);
        }
    }

    public void Pausar()
    {
        if (rb != null)
        {
            // Guardamos su estado actual
            savedVelocity = rb.velocity;
            savedAngularVelocity = rb.angularVelocity;
            savedGravityScale = rb.gravityScale;

            // Ajustamos para “congelar” el Rigidbody sin cambiarlo a Kinematic
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f;
        }
    }

    public void Reanudar()
    {
        if (rb != null)
        {
            // Restauramos su estado
            rb.gravityScale = savedGravityScale;
            rb.velocity = savedVelocity;
            rb.angularVelocity = savedAngularVelocity;
        }
    }
}