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
}
