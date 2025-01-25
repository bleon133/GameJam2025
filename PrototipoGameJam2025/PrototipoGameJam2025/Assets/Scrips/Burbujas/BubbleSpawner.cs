using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [Header("Prefabs y Configuración de Spawn")]
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float tiempoEntreOleadas = 4f; // Tiempo entre cada oleada de burbujas
    [SerializeField] private int burbujasPorOleada = 3; // Cantidad de burbujas por oleada

    [Header("Área de Spawn")]
    [SerializeField] private float minX = -25f;
    [SerializeField] private float maxX = 25f;
    [SerializeField] private float ySpawn = -15f; // Altura desde donde nacen las burbujas

    [Header("Variación de Spawn")]
    [SerializeField] private float tiempoEntreBurbujasDentroOleada = 0.1f; // Tiempo entre burbujas dentro de una oleada

    private Coroutine spawnerCoroutine;

    private void Start()
    {
        // Inicia la corutina para spawnear burbujas en oleadas
        spawnerCoroutine = StartCoroutine(SpawnBubblesInWaves());
    }

    private void OnDisable()
    {
        // Asegura que la corutina se detenga si el objeto se desactiva
        if (spawnerCoroutine != null)
        {
            StopCoroutine(spawnerCoroutine);
        }
    }

    private IEnumerator SpawnBubblesInWaves()
    {
        while (true)
        {
            // Inicia una oleada de burbujas
            StartCoroutine(SpawnWave());
            // Espera el tiempo definido antes de la siguiente oleada
            yield return new WaitForSeconds(tiempoEntreOleadas);
        }
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < burbujasPorOleada; i++)
        {
            SpawnBubble();
            // Espera un breve tiempo antes de spawnear la siguiente burbuja dentro de la oleada
            yield return new WaitForSeconds(tiempoEntreBurbujasDentroOleada);
        }
    }

    private void SpawnBubble()
    {
        // Calcula una posición aleatoria en X dentro del rango especificado
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, ySpawn);

        // Instancia la burbuja en la posición calculada
        Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Limpiador"))
        {
            Destroy(other.gameObject);
        }
    }

}
