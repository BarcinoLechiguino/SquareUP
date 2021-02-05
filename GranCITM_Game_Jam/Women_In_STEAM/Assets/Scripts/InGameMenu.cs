using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject Image;

    public void OpenImage()
    {
        if (Image != null)
        {
            Animator animator = Image.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("Menu_Open");
                animator.SetBool("Menu_Open", !isOpen);
            }
        }
    }

    public void GoHomee()
    {
        SceneManager.LoadScene(0);
    }
}
