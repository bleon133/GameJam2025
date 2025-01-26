using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbPlayer;

    [SerializeField] private float newGravityScale = 5f; //Nueva gravedad
    [SerializeField] private float newMass = 2f; //Nueva masa


    private float tiempoTranscurrido = 0f;
    private float gravedadMaxima = 5f;

    private float valorRuptura = 20f;

    // Momento en el que sucederá el siguiente incremento.
    private float proximoIncremento = 22f;

    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;

        if (tiempoTranscurrido > valorRuptura)
        {
            if (tiempoTranscurrido >= proximoIncremento)
            {
                Debug.Log("Se cumplieron " + proximoIncremento + " segundos.");

                // Aumentamos la gravedad si no hemos llegado al máximo
                if (rbPlayer.gravityScale <= gravedadMaxima)
                {
                    rbPlayer.gravityScale += 0.1f;
                }

                // Fijamos el próximo incremento sumando 10 al anterior
                proximoIncremento += 2f;
            }
        }
    }

    // Para cuando el globo del player se destruye.
    public void DropFaster()
    {
        rbPlayer.mass = newMass;
        rbPlayer.gravityScale = newGravityScale;
    }
}