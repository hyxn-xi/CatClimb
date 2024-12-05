using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                          // LoadScece을 사용하는 데 필요

public class ClearDirector : MonoBehaviour
{
    
    void Update()
    {
        if ( Input.GetMouseButtonDown(0) )
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
