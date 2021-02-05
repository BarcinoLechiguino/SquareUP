using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FK : MonoBehaviour
{
    public GameObject Image;

    public void OpenImage()
    {
        if (Image != null)
        {
            Animator animator = Image.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("FK_Open");
                animator.SetBool("FK_Open", !isOpen);
            }
        }
    }
}
