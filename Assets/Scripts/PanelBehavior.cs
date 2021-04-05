using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelBehavior : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI namePoint;
    [SerializeField] private GameObject inputPosPanel;
    [SerializeField] private GameObject inputRotPanel;
    [SerializeField] private GameObject cube;
    
    private GameObject _pointer;
    private GameObject _cube;
    
    /// <summary>
    /// set data in ui 
    /// </summary>
    /// <param name="point"></param>
    public void SetPointDataToPanel(GameObject point)
    {
        _pointer = point;
        namePoint.color = _pointer.GetComponent<Renderer>().material.color;
        namePoint.text = _pointer.name;

        for (int i = 0; i < 3; i++)
        {
            inputPosPanel.transform.GetChild(i).GetComponent<TMP_InputField>().text = _pointer.transform.position[i].ToString("0.00");
        }

        for (int i = 0; i < 3; i++)
        {
            inputRotPanel.transform.GetChild(i).GetComponent<TMP_InputField>().text =
                _pointer.transform.rotation[i].ToString();
        }
    }

    #region SetPositionAndRotation
    public void SetXPos(string _input)
    {
        float ptr;
        if (float.TryParse(_input, out ptr))
        {
            float input = Mathf.Clamp(float.Parse(_input), -5, 5); 
            _pointer.transform.position = new Vector3(input,
                _pointer.transform.position.y,
                _pointer.transform.position.z);
        }
    }
    
    public void SetYPos(string _input)
    {
        float ptr;
        if (float.TryParse(_input, out ptr))
        {
            float input = Mathf.Clamp(float.Parse(_input), -5, 5); 
            _pointer.transform.position = new Vector3(_pointer.transform.position.x,
                input,
                _pointer.transform.position.z);
        }
    }
    
    public void SetZPos(string _input)
    {
        float ptr;
        if (float.TryParse(_input, out ptr))
        {
            float input = Mathf.Clamp(float.Parse(_input), -5, 500);
            _pointer.transform.position = new Vector3(_pointer.transform.position.x,
                _pointer.transform.position.y,
                input);
        }
    }
    
    public void SetXRot(string _input)
    {
        float ptr;
        if (float.TryParse(_input, out ptr))
        {
            float input = Mathf.Clamp(float.Parse(_input), -360, 360);
            _pointer.transform.Rotate(input, 0, 0);
        }
    }
    
    public void SetYRot(string _input)
    {
        float ptr;
        if (float.TryParse(_input, out ptr))
        {
            float input = Mathf.Clamp(float.Parse(_input), -360, 360);
            _pointer.transform.Rotate(0, input, 0);
        }
    }
    
    public void SetZRot(string _input)
    {
        float ptr;
        if (float.TryParse(_input, out ptr))
        {
            float input = Mathf.Clamp(float.Parse(_input), -360, 360);
            _pointer.transform.Rotate(0, 0, input);
        }
    }
    
    #endregion
    
    /// <summary>
    /// set timer for cube
    /// </summary>
    /// <param name="_input"></param>
    public void SetTimeToMoveCube(string _input)
    {
        float ptr;
        if (float.TryParse(_input, out ptr))
        {
            float input = Mathf.Clamp(ptr, 0, 100);
            InstantiateCube(input);
        }
    }

    /// <summary>
    /// Instantiate Cube
    /// </summary>
    /// <param name="input"></param>
    private void InstantiateCube(float input)
    {
        if (_cube == null)
        {
            _cube = Instantiate(cube);
            _cube.transform.SetParent(_pointer.transform, false);
            _cube.transform.localPosition = new Vector3(0, 0, 10f);
            GameManager.Instance.PostNotification(EVENT_TYPE.CREATE_CUBE, this, _cube);
        }
        _cube.GetComponent<CubeMove>().timeToMove = input;
    }
}
