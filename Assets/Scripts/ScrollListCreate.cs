using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ScrollListCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField] 
    private GameObject content;

    private int _iterator;
    void Start()
    {
        GameManager.Instance.AddListener(EVENT_TYPE.CREATE_PANEL, CreatePanel);
    }

    private void CreatePanel(EVENT_TYPE eventType,
        Component sender,
        object param = null)
    {
        GameObject point = (GameObject) param;
        if (point.Equals(null))
            return;
        GameObject _panel = Instantiate(panel);
        _panel.transform.SetParent(content.transform, false);

        SetRandomColor(point);
        _panel.GetComponent<PanelBehavior>().SendMessage("SetPointDataToPanel", point);
    }

    private void SetRandomColor(GameObject point)
    {
        Color randomColor = Random.ColorHSV(0, 1);
        point.GetComponent<Renderer>().material.SetColor("_Color", randomColor);
    }
}
