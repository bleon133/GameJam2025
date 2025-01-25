using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    private float fixedDeltaTime;

    void Awake()
    {
        Time.timeScale = 1.0f;
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;

        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;

        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }
}
