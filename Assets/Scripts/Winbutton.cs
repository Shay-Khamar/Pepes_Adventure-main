using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winbutton : MonoBehaviour
{
    [SerializeField] Button winButton;
    // Start is called before the first frame update
    void Start()
    {
        winButton.onClick.AddListener(LoadMainMenu);
        
    }

    private void LoadMainMenu()
    {
        ScenesManager.Instance.LoadMainMenu();
    }
}
