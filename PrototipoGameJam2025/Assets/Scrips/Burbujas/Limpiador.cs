using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpiadorObjetos : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            Destroy(other.gameObject);
        }
    }
}
