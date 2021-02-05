using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    #region Variables

    private GameplayManager manager;
    float rotation_speed = 200.0f;
    float speed_y = 5.0f;
    float height = 0.2f;

    #endregion

    #region Methods
    void Start()
    {
        manager = FindObjectOfType<GameplayManager>();
        ActivateChild();
    }
    void Update()
    {
        TransformAnimation();
    }
    void TransformAnimation()
    {
        Vector3 pos = gameObject.transform.position;
        transform.Rotate(Vector3.up * Time.deltaTime * rotation_speed);
        // float newY = Mathf.Sin(Time.time * speed_y);
        // transform.position = new Vector3(pos.x, newY, pos.z) * height;
    }
    void ActivateChild()
    {
        Debug.Log((int)manager.active_container_type);
        transform.GetChild((int)manager.active_container_type).gameObject.SetActive(true);
        
    }
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
