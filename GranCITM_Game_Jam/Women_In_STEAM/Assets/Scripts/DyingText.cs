using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DyingText : MonoBehaviour
{
    #region Variables

    private float starting_time = 2.0f;
    private float current_time;
    private Text m_Text;
    private GameplayManager manager;
    private int value;

    #endregion

    #region Methods
    void Start()
    {
         manager = FindObjectOfType<GameplayManager>();
        m_Text = GetComponent<Text>();
    }
    void OnEnable()
    {
        current_time = starting_time;
    }

    void Update()
    {
        if (current_time > 0.0f)
        {
            current_time -= 1 * Time.deltaTime;
            DrawText(manager.active_container.active_color, value);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void DrawText(Color color, int value)
    {
        m_Text.text = "+" + value.ToString();
        m_Text.color = color;
    }

    public void SetValue(int added_value)
    {
        value = added_value;
    }

    #endregion
}
