using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyThisOnLoading : MonoBehaviour
{    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
