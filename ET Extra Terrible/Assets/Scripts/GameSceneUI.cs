using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    private Button myButton = null;
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() => {
            Debug.Log("Click Main Menu");
            Loader.Load(Loader.Scene.TitleCard);
        });
    }
}
