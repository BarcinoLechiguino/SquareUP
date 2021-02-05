using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour
{
    #region Variables
    public Sprite[] container_sprites;
    public Sprite[] container_masks;
    private GameplayManager manager;
    private Slider slider;
    public Image container_shape;
    public Image container_mask;
    public Image container_fill;
    public Color active_color;

    #endregion

    #region Methods
    void Awake()
    {
        manager = FindObjectOfType<GameplayManager>();
        slider = GetComponent<Slider>();
    }

    public void AddValue()
    {
        slider.value++;
    }
    public void SetValue(int value)
    {
        slider.value = value;
    }
    public void SetMinValue(int min_value)
    {
        slider.minValue = min_value;
    }
    public void SetMaxValue(int max_value)
    {
        slider.maxValue = max_value;
    }

    public void ResetSlider()
    {
        slider.value = 0;
    }

    public void SelectContainer()
    {
        switch (manager.active_container_type)
        {
            case GameplayManager.ContainerType.SCIENCE:
                container_shape.sprite = container_sprites[0];
                container_mask.sprite = container_masks[0];
                Color s_color = new Color(0.0f, 0.5f, 0.7f, 1.0f);  // blue
                active_color = s_color;
                container_fill.color = s_color;
                SetMaxValue(manager.s_container);
                ResetSlider();
                //container_shape.SetNativeSize();
                //container_mask.SetNativeSize();
                break;
            case GameplayManager.ContainerType.TECHNOLOGY:
                container_shape.sprite = container_sprites[1];
                container_mask.sprite = container_masks[1];
                Color t_color = new Color(0.0f, 0.7f, 0.3f, 1.0f);  // green
                active_color = t_color;
                container_fill.color = t_color;
                SetMaxValue(manager.t_container);
                ResetSlider();
                //container_shape.SetNativeSize();
                //container_mask.SetNativeSize();
                break;

            case GameplayManager.ContainerType.ENGINEERING:
                container_shape.sprite = container_sprites[2];
                container_mask.sprite = container_masks[2];
                Color e_color = new Color(0.6f, 0.0f, 0.4f, 1.0f);  // pink/purple
                active_color = e_color;
                container_fill.color = e_color;
                SetMaxValue(manager.e_container);
                ResetSlider();
                //container_shape.SetNativeSize();
                //container_mask.SetNativeSize();
                break;

            case GameplayManager.ContainerType.ART:
                container_shape.sprite = container_sprites[3];
                container_mask.sprite = container_masks[3];
                Color a_color = new Color(0.9f, 0.8f, 0.0f, 1.0f);  // yellow
                active_color = a_color;
                container_fill.color = a_color;
                SetMaxValue(manager.a_container);
                ResetSlider();
                //container_shape.SetNativeSize();
                //container_mask.SetNativeSize();
                break;

            case GameplayManager.ContainerType.MATH:
                container_shape.sprite = container_sprites[4];
                container_mask.sprite = container_masks[4];
                Color ma_color = new Color(1.0f, 0.5f, 1.0f, 1.0f);  // pink
                active_color = ma_color;
                container_fill.color = ma_color;
                SetMaxValue(manager.ma_container);
                ResetSlider();
                //container_shape.SetNativeSize();
                //container_mask.SetNativeSize();
                break;

            default:
                break;
        }
    }
    #endregion
}
