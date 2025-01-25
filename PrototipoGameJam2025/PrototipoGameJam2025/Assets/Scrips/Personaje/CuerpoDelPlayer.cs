using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuerpoDelPlayer : MonoBehaviour
{
    [SerializeField] private PlayerController playerControllerContrario;
    [SerializeField] private GameObject GloboPlayerContrario;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BallonPlayer"))
        {
            Destroy(GloboPlayerContrario); //Destruye la burbuja contraria
            playerControllerContrario.DropFaster(); //Aumeneta la gravedad y masa del contrario
        }
    }
}
