using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
   public void ChangeTextButton()
   {
      Text _text = GetComponent<Text>();
      if (_text.text == "Play")
      {
         GameManager.Instance.PostNotification(EVENT_TYPE.GAME_PLAY, this);
         _text.text = "Pause";
      }
      else
      {
         GameManager.Instance.PostNotification(EVENT_TYPE.GAME_PAUSE, this);
         _text.text = "Play";
      }
   }
}
