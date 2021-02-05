using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MK : MonoBehaviour
{
    public GameObject Image;

    public void OpenImage()
    {
        if (Image != null)
        {
            Animator animator = Image.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("MK_Open");
                animator.SetBool("MK_Open", !isOpen);
            }
        }
    }
}
