using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    public List<GameObject> layers; // Lista de capas.
    public Camera mainCamera; // Cámara principal.
    public float teleportThreshold = 68f; // Umbral en Y para teletransportar las capas.
    public float teleportTargetY = 16f; // Nueva posición en Y para la teletransportación.
    public float delayTeleport = 25f;

    void Update()
    {
        // Recorremos cada capa para verificar su posición.
        foreach (GameObject layer in layers)
        {
            if (layer == null) continue; // Ignorar capas nulas.

            // Imprimimos la posición actual para depuración.
            //Debug.Log($"Capa: {layer.name}, Posición Y actual: {layer.transform.position.y}");

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