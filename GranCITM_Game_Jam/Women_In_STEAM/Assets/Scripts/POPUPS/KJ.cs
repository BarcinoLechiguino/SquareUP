using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KJ : MonoBehaviour
{
    public GameObject Image;

    public void OpenImage()
    {
        if (Image != null)
        {
            Animator animator = Image.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("KJ_Open");
                animator.SetBool("KJ_Open", !isOpen);
            }
        }
    }
}