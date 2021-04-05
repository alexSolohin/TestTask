using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePoints : MonoBehaviour
{
    [SerializeField] private GameObject prefabPoint;
    
    private int _iterator = 0;
    
    void Start()
    {
        GameManager.Instance.AddListener(EVENT_TYPE.CREATE_POINT, CreatePoint);
    }

    private void CreatePoint(EVENT_TYPE eventType, Component sender,
        object param = null)
    {
        Vector3 position = (Vector3)param;
        position += new Vector3(0, 0, 2f);
        
        if (position.y < 0.1f && position.x < 0.08f)
        {
            GameObject point = Instantiate(prefabPoint, position, Quaternion.identity);
            point.name = "point_" + _iterator;
            GameManager.Instance.PostNotification(EVENT_TYPE.CREATE_PANEL, this, point);
            _iterator++;
        }
    }


}
