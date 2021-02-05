using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameplayManager : MonoBehaviour
{
    #region Variables

    // Framerate Cap
    public float framerate_cap = 60;

    // Referents
    public bool region_completed = false;

    #region Pickups
    public int pickup_count;
    public int global_count;
    public int s_container;                     // Amount of points needed to complete Science Sector
    public int t_container;                     // Amount of points needed to complete Technology Sector
    public int e_container;                     // Amount of points needed to complete Engineering Sector
    public int a_container;                     // Amount of points needed to complete Art Sector
    public int ma_container;                    // Amount of points needed to complete Math Sector
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
    public DyingText add_text;

    #endregion

    #region Terrain
    public float variation;
    public float speed;
    public float max_speed;
    #endregion
    #region Sectors

    #endregion
    #region Figures
    public int figure_amount;

    #endregion

    #endregion

    #region Methods
    void Start()
    {
        //Pickups
        pickup_count = 0;
        global_count = 0;

        active_container.SelectContainer();

        Application.targetFrameRate = 61;
    }

    void Update()
    {
        //Debug
        if (Input.GetKeyDown("n"))
        {
            NextSector();
        }

        IncreaseSpeed();
    }

    public void SetFigureCount(int count)
    {
        figure_amount = count;
        if(CheckScore(Highscore.figure_highscore, figure_amount))
        {
           Highscore.figure_highscore = count;
        }
    }
    //Pickups
    public void IncreaseCount(int added_count = 1)
    {
        add_text.gameObject.SetActive(true);
        add_text.SetValue(added_count);
        AddCount(added_count);

        if(CheckScore(Highscore.highscore, global_count))
        {
           Highscore.highscore = global_count;
        }
        if (CheckCount())
        {
            pickup_count = 0;
            region_completed = true;
            //Function that makes the mentor appear
            //Place NextSector() inside of it
            NextSector();
        }
        else
        {
            active_container.AddValue();
        }
    }

    public void AddCount(int points)
    {
        pickup_count += points;
        global_count += points;
    }

    private bool CheckCount()
    {
        bool ret = false;

        switch (active_container_type)
        {
            case ContainerType.SCIENCE: { ret = (pickup_count >= s_container) ? true : false; } break;
            case ContainerType.TECHNOLOGY: { ret = (pickup_count >= t_container) ? true : false; } break;
            case ContainerType.ENGINEERING: { ret = (pickup_count >= e_container) ? true : false; } break;
            case ContainerType.ART: { ret = (pickup_count >= a_container) ? true : false; } break;
            case ContainerType.MATH: { ret = (pickup_count >= ma_container) ? true : false; } break;
            default: { ret = false; } break;
        }

        return ret;
    }

    // Terrains
    public void IncreaseSpeed()
    {
        if (speed <= max_speed)
        {
            speed += 0.02f * Time.deltaTime;
        }
    }

    // Sectors
    public void NextSector()
    {
        if (active_container_type != ContainerType.MATH)
        {
            active_container_type++;
            IncreaseSectorSpeed();
        }
        else
        {
            active_container_type = ContainerType.SCIENCE;
            ResetSectorSpeed();
        }

        active_container.SelectContainer();
    }

    public void ResetSectorSpeed()
    {
        max_speed = 6.0f;
    }
    public void IncreaseSectorSpeed()
    {
        max_speed += 1.0f;
    }

    public bool CheckScore(int highscore, int score)
    {
       return (score > highscore) ? true : false; 
    }
    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
