using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    #region Enumerators

    public enum Region
    {
        SCIENCE,
        TECHNOLOGY,
        ENGINEERING,
        ART,
        MATH
    }

    #endregion

    #region Variables

    GameplayManager manager;

    #endregion

    #region Methods
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            manager.IncreaseCount();
            Destroy(gameObject);
        }
    }
    #endregion
}
