using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuerpoDelPlayer : MonoBehaviour
{
    [SerializeField] private PlayerController playerControllerPrincipal;
    [SerializeField] private PlayerController playerControllerContrario;
    [SerializeField] private GameObject GloboPlayerContrario;
    [SerializeField] private GameObject PanelReinicio;
    [SerializeField] private BubbleSpawner spawner1;
    [SerializeField] private BubbleSpawner spawner2;
    [SerializeField] private 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BallonPlayer"))
        {
            Destroy(GloboPlayerContrario); //Destruye la burbuja contraria
            playerControllerContrario.DropFaster(); //Aumeneta la gravedad y masa del contrario
        }else if (other.gameObject.CompareTag("ZonaMuerte"))
        {
            playerControllerPrincipal.PausarMovimiento();   
            playerControllerContrario.PausarMovimiento();
            spawner1.DetenerSpawn();
            spawner1.PausarTodasLasBurbujas();
            spawner2.DetenerSpawn();
            spawner2.PausarTodasLasBurbujas();
            PanelReinicio.SetActive(true);
        }
    }
}
