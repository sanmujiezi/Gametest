using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public InputManager InputManager;
    public LineMove lineMove;

    private void OnEnable()
    {
        lineMove.OnSubmit += Submit;
    }

    private void OnDisable()
    {
        lineMove.OnSubmit -= Submit;
    }

    public void Submit()
    {
        var answer = lineMove.GetAnswer();
        var result = InputManager.GetResult();
        if (answer.Equals(result))
        {
            Debug.Log("》》》》》》》成功");
        }
    }
}
