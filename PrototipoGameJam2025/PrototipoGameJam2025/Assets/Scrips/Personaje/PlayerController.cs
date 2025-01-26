using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbPlayer;

    [Header("Par�metros de ca�da")]
    [SerializeField] private float newGravityScale = 5f; //Nueva gravedad
    [SerializeField] private float newMass = 2f; //Nueva masa


    [Header("Tiempos y l�gica")]
    private float tiempoTranscurrido = 0f;
    private float tiempoSinMovimiento = 10f;   // Primeros 10 segundos inm�vil
    private float gravedadMaxima = 5f;

    private float valorRuptura = 30f;
    private float proximoIncremento = 40f;

    private bool puedeMoverse = false;

    private void Start()
    {
        // Al inicio, desactivamos la f�sica para que NO se mueva ni tenga gravedad
        rbPlayer.gravityScale = 0f;
        rbPlayer.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;

        // Pasados 20 segundos, permitimos movimiento y reactivamos la f�sica
        if (!puedeMoverse && tiempoTranscurrido >= tiempoSinMovimiento)
        {
            puedeMoverse = true;

            // Liberamos las constraints para que pueda moverse, pero evitamos que rote en Z
            rbPlayer.constraints = RigidbodyConstraints2D.FreezeRotation;

            // Ajusta la gravedad inicial que desees para tu objeto
            rbPlayer.gravityScale = 0.5f;
        }

        if (puedeMoverse)
        {
            if (tiempoTranscurrido > valorRuptura)
            {
                if (tiempoTranscurrido >= proximoIncremento)
                {
                    Debug.Log("Se cumplieron " + proximoIncremento + " segundos.");

                    // Aumentamos la gravedad si no hemos llegado al m�ximo
                    if (rbPlayer.gravityScale <= gravedadMaxima)
                    {
                        rbPlayer.gravityScale += 0.1f;
                    }

                    // Fijamos el pr�ximo incremento sumando 10 al anterior
                    proximoIncremento += 10f;
                }
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