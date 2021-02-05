using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
    public int GiveReward()
    {
        switch (obstacle_level)
        {
            case Level.EASY:
                return 1;
            case Level.INTERMEDIATE:
                return 3;
            case Level.HARD:
                return 5;
            case Level.EXTREME:
                return 8;
            default:
                return 0;
        }
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            manager.IncreaseCount(GiveReward());
            gameObject.SetActive(false);
        }
    }
    #endregion
}
