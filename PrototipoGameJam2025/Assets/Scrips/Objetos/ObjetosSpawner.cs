using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetosSpawner : MonoBehaviour
{
    [Header("Prefabs y Configuración de Spawn")]
    // Cambiado de un único prefab a una lista de prefabs
    [SerializeField] private List<GameObject> objectsPrefabs;
    [SerializeField] private float tiempoEntreOleadas = 4f;      
    [SerializeField] private int objetosPorOleada = 3;          

    [Header("Área de Spawn")]
    [SerializeField] private float minX = -25f;
    [SerializeField] private float maxX = 25f;
    [SerializeField] private float ySpawn = 15f;

    [Header("Variación de Spawn")]
    [SerializeField] private float tiempoEntreObjetosDentroOleada = 0.1f;

    [Header("Retardo inicial")]
    [SerializeField] private float tiempoInicialDeEspera = 10f;

    private Coroutine spawnerCoroutine; 
    private Coroutine waveCoroutine;

    private void Start()
    {
        // Inicia la corutina para spawnear burbujas en oleadas
        spawnerCoroutine = StartCoroutine(SpawnBubblesInWaves());
    }

    private void OnDisable()
    {
        // Asegura que las corutinas se detengan si el objeto se desactiva
        if (spawnerCoroutine != null)
        {
            StopCoroutine(spawnerCoroutine);
        }

        if (waveCoroutine != null)
        {
            StopCoroutine(waveCoroutine);
        }
    }

    private IEnumerator SpawnBubblesInWaves()
    {
        while (true)
        {
            // Espera el tiempo inicial configurado antes de comenzar la generación de burbujas
            yield return new WaitForSeconds(tiempoInicialDeEspera);

            // Inicia una oleada de burbujas y guarda la referencia
            waveCoroutine = StartCoroutine(SpawnWave());

            // Espera a que la oleada termine antes de continuar
            yield return waveCoroutine;

            // Espera el tiempo definido antes de la siguiente oleada
            yield return new WaitForSeconds(tiempoEntreOleadas);
        }
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < objetosPorOleada; i++)
        {
            SpawnBubble();
            // Espera un breve tiempo antes de spawnear la siguiente burbuja dentro de la oleada
            yield return new WaitForSeconds(tiempoEntreObjetosDentroOleada);
        }
    }

    private void SpawnBubble()
    {
        // Verifica que la lista de prefabs no esté vacía
        if (objectsPrefabs == null || objectsPrefabs.Count == 0)
        {
            Debug.LogWarning("No se han asignado prefabs de burbujas en el BubbleSpawner.");
            return;
        }

        int randomIndex = Random.Range(0, objectsPrefabs.Count);
        GameObject selectedPrefab = objectsPrefabs[randomIndex];

        // Calcula una posición aleatoria en X dentro del rango especificado
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, ySpawn);

        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }

    public void DetenerSpawn()
    {
        if (spawnerCoroutine != null)
        {
            StopCoroutine(spawnerCoroutine);
            spawnerCoroutine = null;
        }

        if (waveCoroutine != null)
        {
            StopCoroutine(waveCoroutine);
            waveCoroutine = null;
        }
    }

    public void ReanudarSpawn()
    {
        // Solo reanudamos si no hay corutina principal corriendo actualmente
        if (spawnerCoroutine == null)
        {
            spawnerCoroutine = StartCoroutine(SpawnBubblesInWaves());
        }
    }
}
