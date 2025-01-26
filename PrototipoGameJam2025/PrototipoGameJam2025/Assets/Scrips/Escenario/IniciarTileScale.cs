using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarTileScale : MonoBehaviour
{
    private float fixedDeltaTime;

    void Awake()
    {
        Time.timeScale = 1.0f;
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }
}
