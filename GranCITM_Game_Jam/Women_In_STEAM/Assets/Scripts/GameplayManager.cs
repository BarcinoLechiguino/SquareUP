using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameplayManager : MonoBehaviour
{
    #region Variables
    //Particles
    public List<GameObject> Particle_containers;
    // Framerate Cap
    public float framerate_cap = 60;

    // Referents
    public bool region_completed = false;

    // Difficulty Mode
    public bool hard_mode = false;

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
    private TerrainGenerator generator;
    public float variation;
    public float delay_obstacles;
    public bool obstacles = true;
    public float speed;
    public float max_speed;
    #endregion
    #region Sectors    
    public GameObject sector_title;
    private Text sector_text;
    public SpriteRenderer flat_background;
    public bool waitingForMentor;
    public float mentor_delay = 3.0f;
    public float mentor_timer;

    public GameObject popup;
    public Image popup_image;
    public Sprite[] heads;

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
        sector_text = sector_title.GetComponent<Text>();
        generator = gameObject.GetComponent<TerrainGenerator>();

        SetSectorMessage();
        SetBackgroundColor();
        Application.targetFrameRate = 61;
    }

    void Update()
    {
        //Debug
        if (Input.GetKeyDown("n"))
        {
            InitiateMentor();
        }

        if (Input.GetKeyDown("p"))
        {
            Time.timeScale = (Time.timeScale == 1.0f) ? 0.0f : 1.0f;
        }

        if (obstacles)
        {
            obstacles = DelayObstacles();
        }

        if (waitingForMentor)
        {
            if (mentor_timer < 0.0f)
            {
                generator.SpawnMentor();
                waitingForMentor = false;
            }
            else
            {
                mentor_timer -= 1 * Time.deltaTime;
            }
        }

        if(popup.transform.position.y < -657)
        {
            popup.SetActive(false);
        }

        UnableSectorMessage();
        IncreaseSpeed();
    }

    public void SetFigureCount(int count)
    {
        figure_amount = count;
        if (CheckScore(Highscore.figure_highscore, figure_amount))
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

        if (CheckScore(Highscore.highscore, global_count))
        {
            Highscore.highscore = global_count;
        }
        if (CheckCount())
        {
            pickup_count = 0;
            region_completed = true;
            InitiateMentor();
           
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
            case ContainerType.SCIENCE: { ret = (pickup_count >= s_container) ? true : false; Particle_containers[0].SetActive(true); } break;
            case ContainerType.TECHNOLOGY: { ret = (pickup_count >= t_container) ? true : false; Particle_containers[1].SetActive(true); Particle_containers[0].SetActive(false); } break;
            case ContainerType.ENGINEERING: { ret = (pickup_count >= e_container) ? true : false; Particle_containers[2].SetActive(true); Particle_containers[1].SetActive(false); } break;
            case ContainerType.ART: { ret = (pickup_count >= a_container) ? true : false; Particle_containers[3].SetActive(true); Particle_containers[2].SetActive(false); } break;
            case ContainerType.MATH: { ret = (pickup_count >= ma_container) ? true : false; Particle_containers[4].SetActive(true); Particle_containers[3].SetActive(false); } break;
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

    public bool DelayObstacles()
    {
        if (delay_obstacles > 0.0f)
        {
            delay_obstacles -= 1 * Time.deltaTime;
            return true;
        }
        else
        {

            return false;
        }
    }

    // Sectors
    public void SetSectorMessage()
    {
        sector_title.SetActive(true);
        sector_text.text = active_container_type.ToString();
        Color color;
        color = active_container.active_color;
        sector_text.color = color;

    }

    public void SetSectorPopUp()
    {
        popup.SetActive(true);
        popup_image.sprite = heads[(int)active_container_type];
    }

    public void UnableSectorMessage()
    {
        if (sector_text.color.a <= 0.0f)
        {
            sector_title.SetActive(false);
        }
    }

    public void SetBackgroundColor()
    {
        Color new_color = new Color(active_container.active_color.r, active_container.active_color.g, active_container.active_color.b, 0.5f);
        flat_background.color = new_color;

    }
    public void NextSector()
    {
        SetSectorPopUp();

        if (active_container_type != ContainerType.MATH)
        {
            active_container_type++;
            //IncreaseSectorSpeed();
        }
        else
        {
            active_container_type = ContainerType.SCIENCE;
            //ResetSectorSpeed();
        }

        if (active_container_type == ContainerType.ENGINEERING)
        {
            hard_mode = true;
        }

        IncreaseSectorSpeed();
        active_container.SelectContainer();
        SetSectorMessage();
        SetBackgroundColor();
    }

    public void ResetSectorSpeed()
    {
        max_speed = 6.0f;
    }
    public void IncreaseSectorSpeed()
    {
        max_speed += 1.0f;
    }

    public void InitiateMentor()
    {
        waitingForMentor = true;
        mentor_timer = mentor_delay;
    }
    // Game loop
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
