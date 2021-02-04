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
    public int ma_container;     // Amount of points needed to complete Math Sector
    public enum ContainerType
    {
        SCIENCE,
        TECHNOLOGY,
        ENGINEERING,
        ART,
        MATH
    }
    public Container active_container;
    public ContainerType active_container_type;

    #endregion

    #region Methods
    void Start()
    {
        //Pickups
        pickup_count = 0;
        // active_container_type = ContainerType.SCIENCE;
        active_container.SelectContainer();

    }

    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            NextRegion();
        }
    }

    //Pickups
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
        switch (active_container_type)
        {
            case ContainerType.SCIENCE:
                if (pickup_count >= s_container) { return true; }
                else { return false; }
            case ContainerType.TECHNOLOGY:
                if (pickup_count >= t_container) { return true; }
                else { return false; }
            case ContainerType.ENGINEERING:
                if (pickup_count >= e_container) { return true; }
                else { return false; }
            case ContainerType.ART:
                if (pickup_count >= a_container) { return true; }
                else { return false; }
            case ContainerType.MATH:
                if (pickup_count >= ma_container) { return true; }
                else { return false; }
            default:
                return false;
        }

    }

    //Regions
    public void NextRegion()
    {
        if (active_container_type != ContainerType.MATH)
        {
            active_container_type++;
        }
        else
        {
            active_container_type = ContainerType.SCIENCE;
        }

        active_container.SelectContainer();
    }


    #endregion
}
