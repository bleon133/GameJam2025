using UnityEngine;

public class PruebaGravedad : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    private float tiempoTranscurrido = 0f;
    private float gravedadMaxima = 5f;

    private float valorRuptura = 15f;

    // Momento en el que suceder� el siguiente incremento.
    private float proximoIncremento = 17f;

    void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;

        if(tiempoTranscurrido > valorRuptura)
        {
            if (tiempoTranscurrido >= proximoIncremento)
            {
                //Debug.Log("Se cumplieron " + proximoIncremento + " segundos.");

                // Aumentamos la gravedad si no hemos llegado al m�ximo
                if (Rigidbody2D.gravityScale <= gravedadMaxima)
                {
                    Rigidbody2D.gravityScale += 0.1f;
                }

                // Fijamos el pr�ximo incremento sumando 10 al anterior
                proximoIncremento += 2f;
            }
        }
    }
}
