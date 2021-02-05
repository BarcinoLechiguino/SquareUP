using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
#region Variables
private GameplayManager manager;
public Text figure_count;

#endregion

#region Methods
    void Start()
    {
        manager = FindObjectOfType<GameplayManager>();
    }

    void Update()
    {
        figure_count.text = manager.figure_amount.ToString();
    }
#endregion
}
