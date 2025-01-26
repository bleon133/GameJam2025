using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    public List<GameObject> layers; // Lista de capas.
    public Camera mainCamera; // C�mara principal.
    public float teleportThreshold = 68f; // Umbral en Y para teletransportar las capas.
    public float teleportTargetY = 16f; // Nueva posici�n en Y para la teletransportaci�n.
    public float delayTeleport = 25f;

    void Update()
    {
        // Recorremos cada capa para verificar su posici�n.
        foreach (GameObject layer in layers)
        {
            if (layer == null) continue; // Ignorar capas nulas.

            // Imprimimos la posici�n actual para depuraci�n.
            //Debug.Log($"Capa: {layer.name}, Posici�n Y actual: {layer.transform.position.y}");

            // Si la capa supera el umbral, la teletransportamos.
            if (layer.transform.position.y >= teleportThreshold - delayTeleport)
            {
                //Debug.Log($"Teletransportando {layer.name} desde {layer.transform.position.y} a {teleportTargetY}");

                layer.transform.position = new Vector3(
                    layer.transform.position.x,
                    teleportTargetY - delayTeleport,
                    layer.transform.position.z
                );
            }
        }
    }
}