using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuerpoDelPlayer : MonoBehaviour
{
    [SerializeField] private PlayerController playerControllerPrincipal;
    [SerializeField] private PlayerController playerControllerContrario;
    [SerializeField] private GameObject GloboPlayerContrario;
    [SerializeField] private GameObject PanelReinicio;
    [SerializeField] private GameObject ImagenPlayerOtherWin;
    [SerializeField] private GameObject ImagenPlayerMeOver;
    [SerializeField] private BubbleSpawner spawner1;
    [SerializeField] private BubbleSpawner spawner2;
    [SerializeField] private TimeScaleManager timescaleManager;
    [SerializeField] private CuerpoDelPlayer playerContrario;
    [SerializeField] AudioSource musicaFondo;
    [SerializeField] private AudioClip sonidoFinPartida;


    private Animator animator;
    [SerializeField] private string bubbleFallingAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BallonPlayer"))
        {
            playerControllerContrario.DropFaster(); //Aumeneta la gravedad y masa del contrario
            GloboPlayerContrario.GetComponent<Balloon>().AnimacionExplosion();
            playerContrario.AnimacionFalling();
        }
        else if (other.gameObject.CompareTag("ZonaMuerte"))
        {
            timescaleManager.PauseGame();
            PanelReinicio.SetActive(true);
            ImagenPlayerOtherWin.SetActive(true);
            ImagenPlayerMeOver.SetActive(true);
            musicaFondo.Pause();
            if (sonidoFinPartida != null)
            {
                AudioSource.PlayClipAtPoint(sonidoFinPartida, transform.position);
            }
        }
    }

    public void AnimacionFalling()
    {
        if (animator != null)
        {
            animator.Play(bubbleFallingAnimation);
        }
    }
}