using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

enum TIME_EVENT
{
    PLAY,
    PAUSE,
    TIME_CHANGE,
}
public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPoint;
    
    private float speed = 4f;
    private Vector3 _endPosition;
    private Camera _camera;

    private TIME_EVENT timeEvent = TIME_EVENT.PLAY;
    
    private void Start()
    {
        GameManager.Instance.AddListener(EVENT_TYPE.GAME_PLAY, IsPlay);
        GameManager.Instance.AddListener(EVENT_TYPE.GAME_PAUSE, IsPause);
        _camera = Camera.main;
        _endPosition = transform.position + new Vector3(0, 0, 60 * speed);
    }

    void IsPlay(EVENT_TYPE eventType, Component sender, object param = null)
    {
        timeEvent = TIME_EVENT.PLAY;
    }
    
    void IsPause(EVENT_TYPE eventType, Component sender, object param = null)
    {
        timeEvent = TIME_EVENT.PAUSE;
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GameManager.Instance.PostNotification(EVENT_TYPE.CREATE_POINT, this, MousePointToWorld());
        }
        switch (timeEvent)
        {
            case TIME_EVENT.PLAY:
                if (transform.position.z <= _endPosition.z)
                    transform.position += new Vector3(0, 0, speed * Time.deltaTime);
                break;
            case TIME_EVENT.PAUSE:
                break;
        }
        
    }
    
    private Vector3 MousePointToWorld()
    {
        Vector3 point = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y,
                _camera.nearClipPlane));
        return point;
    }
}
