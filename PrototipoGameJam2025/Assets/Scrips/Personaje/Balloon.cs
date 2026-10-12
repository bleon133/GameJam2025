using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private Animator animator;

    // Nombre de la animación que deseas reproducir
    [SerializeField] private string bubbleExplosionAnimation = "BubbleExplosion";

    [SerializeField] private AudioClip sonidoColision;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimacionExplosion()
    {
        if (animator != null && sonidoColision != null)
        {
            AudioSource.PlayClipAtPoint(sonidoColision, transform.position);
            animator.Play(bubbleExplosionAnimation);
        }
    }

}
