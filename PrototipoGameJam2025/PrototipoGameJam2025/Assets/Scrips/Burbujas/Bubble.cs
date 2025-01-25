using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float fuerzaHaciaArriba = 10f;
    [SerializeField] private float fuerzaRebote = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // La burbuja se impulsa hacia arriba una vez al aparecer
        rb.AddForce(Vector2.up * fuerzaHaciaArriba, ForceMode2D.Impulse);
    }

    // M�todo de colisi�n
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el objeto que colisiona es el Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Opci�n A: Usar la normal del primer punto de contacto (para rebote "f�sico")
            // ---------------------------------------------------------
            // Vector2 normalDeColision = collision.GetContact(0).normal;
            // // Direcci�n de rebote es la contraria a la normal
            // Vector2 direccionRebote = -normalDeColision.normalized;
            // collision.rigidbody.AddForce(direccionRebote * fuerzaRebote, ForceMode2D.Impulse);

            // Opci�n B: Rebote manual usando la posici�n relativa (del centro de la burbuja al jugador)
            // ---------------------------------------------------------
            Vector2 direccion = collision.transform.position - transform.position;
            direccion.Normalize();
            collision.rigidbody.AddForce(direccion * fuerzaRebote, ForceMode2D.Impulse);

            // Destruir la burbuja en cuanto colisione con el jugador
            Destroy(gameObject);
        }
    }
}
