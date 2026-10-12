using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpiadorObjects : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Objeto"))
        {
            Destroy(other.gameObject);
        }
    }
}
