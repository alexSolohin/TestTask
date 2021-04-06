using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMove : MonoBehaviour
{
	public float timeToMove;
	
	private SliderEvent slider;
	private TIME_EVENT timeEvent = TIME_EVENT.PAUSE;

	private float _time = 0;
	private float speed;
	private float _startTime;
	private Renderer _render;
	
	// dict of position cube at time
	private Dictionary<float, Vector3> positionAtTime;
	
	private void Start()
	{
		positionAtTime = new Dictionary<float, Vector3>();
		
		slider = GameObject.Find("Slider").GetComponent<SliderEvent>();
		
		GameManager.Instance.AddListener(EVENT_TYPE.GAME_PLAY, IsPlay);
		GameManager.Instance.AddListener(EVENT_TYPE.GAME_PAUSE, IsPause);
		
		_time = (float)Math.Round((double)slider.slider.value, 1);
		speed = 10 / (timeToMove - _time);
		_startTime = slider.slider.value;
		_render = GetComponent<Renderer>();
	}

	private void CheckTimeToMove()
	{
		if (_time >= timeToMove)
		{
			transform.localPosition = Vector3.zero;
			return;
		}
		if (_time <= _startTime)
		{
			transform.localPosition = new Vector3(0, 0, 10);
		}
	}
	private void MoveAtPause()
	{
		CheckTimeToMove();
		if (transform.localPosition.z >= 0)
		{
			if (positionAtTime.ContainsKey(_time))
			{
				transform.localPosition = positionAtTime[_time];
			}
		}
		else if (positionAtTime.ContainsKey(_time))
		{
			transform.localPosition = positionAtTime[_time];
		}	
	}
	
	private void MoveAtPlay()
	{
		CheckTimeToMove();
		if (transform.localPosition.z > 0)
		{
			transform.localPosition -= new Vector3(0, 0, speed * Time.deltaTime);
			if (!positionAtTime.ContainsKey(_time))
				positionAtTime.Add(_time, transform.localPosition);
		}
		else if (positionAtTime.ContainsKey(_time))
		{
			if (positionAtTime.ContainsKey(_time))
				transform.localPosition = positionAtTime[_time];
		}
	}
	
	private void FixedUpdate()
	{
		_time = (float)Math.Round((double)slider.slider.value, 1);
		switch (timeEvent)
		{
			case TIME_EVENT.PLAY:
				MoveAtPlay();
				break;
			case TIME_EVENT.PAUSE:
				MoveAtPause();
				break;
		}
		SetColor();
	}

	private void SetColor()
	{
		if (Vector3.Distance(transform.position, transform.parent.position) <= 0.02)
		{
			_render.material.SetColor("_Color", Color.red);
		}
		else
		{
			_render.material.SetColor("_Color", Color.gray);
		}
	}
	
	void IsPlay(EVENT_TYPE eventType, Component sender, object param = null)
	{
		timeEvent = TIME_EVENT.PLAY;
	}
    
	void IsPause(EVENT_TYPE eventType, Component sender, object param = null)
	{
		timeEvent = TIME_EVENT.PAUSE;
	}
}
