using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Highscore : MonoBehaviour
{
    #region Variables
    public static int figure_highscore;
    public static int highscore;
    
    #endregion

    #region Methods

    void Start()
    {
        figure_highscore = 0;
        highscore = 0;
    }

    #endregion
}
