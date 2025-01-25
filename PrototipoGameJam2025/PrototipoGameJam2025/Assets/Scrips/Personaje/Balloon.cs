using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private Animator animator;

    // Nombre de la animación que deseas reproducir
    [SerializeField] private string bubbleExplosionAnimation = "BubbleExplosion";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimacionExplosion()
    {
        if (animator != null)
        {
            animator.Play(bubbleExplosionAnimation);
        }
    }

}
