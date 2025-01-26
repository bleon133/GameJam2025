using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionVsInicial : MonoBehaviour
{
    private float tiempoTranscurrido = 0f;
    private float timeAnimacion1 = 5f;
    private float timeAnimacion2 = 10f;
    [SerializeField] private GameObject panelVS;
    [SerializeField] private GameObject burbujaPersonajeA;
    private Animator animatorBurbujaA;
    [SerializeField] private string stringAnimacionA;
    [SerializeField] private GameObject burbujaPersonajeB;
    private Animator animatorBurbujaB;
    [SerializeField] private string stringAnimacionB;
    private Boolean flag =  false;

    [SerializeField] private AudioSource fondoMusica;
    [SerializeField] private AudioClip peleasonido;

    private float fixedDeltaTime;

    private void Start()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        animatorBurbujaA = burbujaPersonajeA.GetComponent<Animator>();
        animatorBurbujaB = burbujaPersonajeB.GetComponent<Animator>();
        
    }

    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;
        if (tiempoTranscurrido > timeAnimacion1 && !flag)
        {
            animatorBurbujaA.Play(stringAnimacionA);
            animatorBurbujaB.Play(stringAnimacionB);

            AudioSource.PlayClipAtPoint(peleasonido, transform.position);

            flag = true;

            float delay = timeAnimacion2 - timeAnimacion1;
            Invoke("soundMusic", delay);
        }
    }

    void soundMusic()
    {
        if (panelVS != null)
        {
            Destroy(panelVS);
        }

        if (fondoMusica != null && !fondoMusica.isPlaying)
        {
            fondoMusica.Play();
        }
    }


}
