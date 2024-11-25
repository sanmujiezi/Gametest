using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public Button redBtn;
    public Button greenBtn;
    public Button blueBtn;
    private string result = "";
    void Start()
    {
        redBtn.onClick.AddListener(OnClickRedBtn);
        greenBtn.onClick.AddListener(OnClickGreenBtn);
        blueBtn.onClick.AddListener(OnClickBlueBtn);
    }

    private void OnClickBlueBtn()
    {
        result += "蓝";
        Debug.Log($"点击了:<color=#E539D4>蓝色</color>按钮");
    }

    private void OnClickGreenBtn()
    {
        result += "绿";
        Debug.Log($"点击了:<color=#E539D4>绿色</color>按钮");
    }

    private void OnClickRedBtn()
    {
        result += "红";
        Debug.Log($"点击了:<color=#E539D4>红色</color>按钮");
    }

    public string GetResult()
    {
        string tempResult =  new string(result) ;
        result = "";
        return tempResult;
    }
}
