using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    #region Variables

    // Referents
    public bool region_completed = false;

    // Pickups
    private int pickup_count;
    public int s_container;     // Amount of points needed to complete Science Sector
    public int t_container;     // Amount of points needed to complete Technology Sector
    public int e_container;     // Amount of points needed to complete Engineering Sector
    public int a_container;     // Amount of points needed to complete Art Sector
    public int m_container;     // Amount of points needed to complete Math Sector
    private enum Container
    {
        SCIENCE,
        TECHNOLOGY,
        ENGINEERING,
        ART,
        MATH
    }
    private Container active_container;


    #endregion

    #region Methods
    void Start()
    {
        //Pickups
        pickup_count = 0;
        active_container = Container.SCIENCE;
    }

    public void IncreaseCount()
    {
        pickup_count++;

        if (CheckCount())
        {
            pickup_count = 0;
            region_completed = true;
        }
    }

    private bool CheckCount()
    {
        switch (active_container)
        {
            case Container.SCIENCE:
                if (pickup_count >= s_container) { return true; }
                else { return false; }
            case Container.TECHNOLOGY:
                if (pickup_count >= t_container) { return true; }
                else { return false; }
            case Container.ENGINEERING:
                if (pickup_count >= e_container) { return true; }
                else { return false; }
            case Container.ART:
                if (pickup_count >= a_container) { return true; }
                else { return false; }
            case Container.MATH:
                if (pickup_count >= m_container) { return true; }
                else { return false; }
            default:
                return false;
        }

    }

    public void NextRegion()
    {
        active_container++;
    } 
    #endregion
}
