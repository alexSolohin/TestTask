using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderEvent : MonoBehaviour, IPointerDownHandler
{
    public Slider slider;
    
    private Vector3 _startCameraCoord;
    private bool _pause;
    
    private void Start()
    {
        _pause = false;
        GameManager.Instance.AddListener(EVENT_TYPE.GAME_PAUSE, IsPauseGame);
        GameManager.Instance.AddListener(EVENT_TYPE.GAME_PLAY, IsPauseGame);
        _startCameraCoord = Camera.main.transform.position;
    }

    private void Update()
    {
        if (!_pause)
        {
            float dist = Camera.main.transform.position.z - _startCameraCoord.z;
            slider.value = dist / 4f;
            GameManager.Instance.PostNotification(EVENT_TYPE.TIME, this, slider.value);
        }
    }
    
    public void ChangeTime(float time)
    {
        float dist = time * 4;
        Vector3 positionCam = Camera.main.transform.position;
        Camera.main.transform.position = _startCameraCoord + new Vector3(0, 0, dist);
    }

    private void IsPauseGame(EVENT_TYPE eventType, Component sender, object Param = null)
    {
        if (eventType == EVENT_TYPE.GAME_PAUSE)
        {
            _pause = true;
        }
        else
        {
            _pause = false;
        }
        
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.PostNotification(EVENT_TYPE.TIME_CHANGE, this, slider.value);
    }
}
