using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    Dropdown ai_dropdown;
    Text ai_Desc;

    [SerializeField]
    GameObject[] panels;

    //Close the panel in param
    public void ClosePanel(int _i)
    {
        if(panels[_i])
        {
            panels[_i].SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    //Open the panel in param
    public void OpenPanel(int _i)
    {
        if (panels[_i])
        { 
            panels[_i].SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
