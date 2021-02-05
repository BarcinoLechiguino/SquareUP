using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD : MonoBehaviour
{
    public GameObject Image;

    public void OpenImage()
    {
        if (Image != null)
        {
            Animator animator = Image.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("AD_Open");
                animator.SetBool("AD_Open", !isOpen);
            }
        }
    }
}
