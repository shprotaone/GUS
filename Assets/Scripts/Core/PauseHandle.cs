using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandle
{
   public event Action<bool> Pause;

   private List<IPaused> _pauseObj;
   
    public PauseHandle() {
        _pauseObj= new List<IPaused>();
    }

    public void SetPause(bool paused)
    {
        foreach (var obj in _pauseObj)
        {
            obj.SetPaused(paused);
        }
    }

    public void AddToPausedList(IPaused obj)
    {
        _pauseObj.Add(obj);
    }

    public void RemoveFromPausedList(IPaused obj)
    {
        _pauseObj.Remove(obj);
    }
}
