using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RF : MonoBehaviour
{
    public GameObject Image;

    public void OpenImage()
    {
        if(Image != null)
        {
            Animator animator = Image.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("RF_Open");
                animator.SetBool("RF_Open", !isOpen);
            }
        }
    }
}
