using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Highscore : MonoBehaviour
{
    #region Variables
    public static int highscore;
    
    #endregion

    #region Methods

    void Start()
    {
        highscore = 0;
    }

    #endregion
}
