using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    #region Variables

    public enum Level
    {
        EASY = 0,
        INTERMEDIATE,
        HARD,
        EXTREME
    }

    public Level obstacle_level;
    private GameplayManager manager;
    public Sprite[] obstacle_sprites;
    private SpriteRenderer sprite_renderer;
    public Text figure_count;
    public Text figure_needed;
    public int[] figures_needed = { 1, 3, 5, 8 };

    #endregion

    #region Methods

    void Start()
    {
        manager = FindObjectOfType<GameplayManager>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        obstacle_level = GenerateDifficulty();
        InitializeObstacle();
    }

    public Level GenerateDifficulty()
    {
        float r = Random.Range(0.0f, 1.0f);

        if (r < 0.05f) // 5%
        {
            return Level.EXTREME;
        }
        else if (r < 0.2f) // 15%
        {
            return Level.HARD;
        }
        else if (r < 0.5f) // 30%
        {
            return Level.INTERMEDIATE;
        }
        else    // 50%
        {
            return Level.EASY;
        }
    }

    public void InitializeObstacle()
    {
        sprite_renderer.sprite = obstacle_sprites[(int)obstacle_level];
        figure_count.text = manager.figure_amount.ToString();
        figure_needed.text = figures_needed[(int)obstacle_level].ToString();
    }
    public int GiveReward()
    {
        return figures_needed[(int)obstacle_level];
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (CheckCount())
            {
                manager.IncreaseCount(GiveReward());
                gameObject.SetActive(false);
            }
        }
    }

    public bool CheckCount()
    {
        bool ret;
        ret = (manager.figure_amount >= figures_needed[(int)obstacle_level]) ? true : false;
        return ret;
    }
    #endregion
}
