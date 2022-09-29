using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCardUI : MonoBehaviour
{
    private Button myButton = null;
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() => {
            Debug.Log("Click Enter game");
            Loader.Load(Loader.Scene.MotherShip);
        });
    }
}
