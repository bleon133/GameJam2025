using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float scrollSpeed; // Velocidad de desplazamiento de la capa.
    public float layerHeight; // Altura de la capa (se calcula en base al sprite).
    private Vector3 startPosition; // Posici�n inicial para el bucle.

    void Start()
    {
        // Guarda la posici�n inicial y calcula la altura de la capa.
        startPosition = transform.position;
        layerHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        // Mueve la capa hacia abajo.
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        // Si la capa se mueve fuera del �rea visible, reinicia su posici�n.
        if (transform.position.y <= startPosition.y - layerHeight)
        {
            transform.position = new Vector3(
                transform.position.x,
                startPosition.y - layerHeight,
                transform.position.z
            );
        }
    }
}