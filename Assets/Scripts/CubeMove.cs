using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMove : MonoBehaviour
{
    public float timeToMove;
    
    private TIME_EVENT timeEvent = TIME_EVENT.PAUSE;
    private Renderer _render;


    public ArrayList positionArray;
    private CubeAtTime _cubeAtTime;
    private float _time = 0;

    private void Start()
    {
        positionArray = new ArrayList();
        _cubeAtTime = new CubeAtTime();
        
        _cubeAtTime.startPosition = transform.position;
        
        _render= GetComponent<Renderer>();
        _cubeAtTime.Start(_time);
        
        //Listeners
        GameManager.Instance.AddListener(EVENT_TYPE.GAME_PAUSE, IsPause);
        GameManager.Instance.AddListener(EVENT_TYPE.GAME_PLAY, IsPlay);
        GameManager.Instance.AddListener(EVENT_TYPE.TIME_CHANGE, TimeChange);
        GameManager.Instance.AddListener(EVENT_TYPE.TIME, TimeAtSlider);
    }

    private void FixedUpdate()
    {
        if (timeEvent == TIME_EVENT.TIME_CHANGE)
        {
           MoveWhenTimeChange();
        }
        else if (timeEvent == TIME_EVENT.PLAY)
        {
           MoveWhenPlay();
        }
    }

    private void TimeLessStart()
    {
        if (_time <= _cubeAtTime.startTime)
        {
            transform.localPosition = new Vector3(0, 0, 10f);
            _cubeAtTime.timeAtStartCube = 0;
            _cubeAtTime.startPosition = transform.position;
            _render.material.SetColor("_Color", Color.gray);
        }
    }
    private void MoveWhenTimeChange()
    {
        if (_time >= _cubeAtTime.startTime + timeToMove)
        {
            transform.position = transform.parent.position;
        }
        TimeLessStart();
        if (_cubeAtTime.startTime <= _time && _time <= _cubeAtTime.startTime + timeToMove)
        {
            float speed = 10 / timeToMove;
            if (transform.localPosition.z <= 10f)
            {
                if (positionArray.Count >= 1)
                {
                    transform.position = (Vector3) positionArray[positionArray.Count - 1];
                    positionArray.RemoveAt(positionArray.Count - 1);
                }
            }
        }
    }
    
    private void MoveWhenPlay()
    {
        _cubeAtTime.timeAtStartCube += Time.deltaTime / timeToMove;
        _cubeAtTime.position = Vector3.Lerp(_cubeAtTime.startPosition,
            transform.parent.position,
            _cubeAtTime.timeAtStartCube);
        transform.position = _cubeAtTime.position;
        positionArray.Add(transform.position);
        CheckDistance();
    }
    
    private void TimeAtSlider(EVENT_TYPE eventType, 
        Component sender, object param = null)
    {
        _time = (float) param;
    }
    
    private void CheckDistance()
    {
        if (Vector3.Distance(transform.position, transform.parent.position) <= 0.02
            && _render.material.color != Color.red)
        {
            _cubeAtTime.endTime = _time;
            _render.material.SetColor("_Color", Color.red);
        }
    }
    
    private void TimeChange(EVENT_TYPE eventType, 
        Component sender, object param = null)
    {
        timeEvent = TIME_EVENT.TIME_CHANGE;
        _cubeAtTime.valueTime = (float) param;
    }
    
    private void IsPlay(EVENT_TYPE eventType, Component sender, object param = null)
    {
        timeEvent = TIME_EVENT.PLAY;
    }
    
    private void IsPause(EVENT_TYPE eventType, Component sender, object param = null)
    {
        timeEvent = TIME_EVENT.PAUSE;
    }
}

/// <summary>
/// Class with time var
/// </summary>
public class CubeAtTime
{
    public float valueTime;
    public float timeAtStartCube;
    public float startTime;
    public float endTime;
    public Vector3 position;
    public Vector3 startPosition;
    
    public void Start(float time)
    {
        startTime = GameObject.Find("Slider").GetComponent<SliderEvent>().slider.value;
    }
    
}
