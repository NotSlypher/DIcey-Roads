using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScript : MonoBehaviour
{
    public float time;

    bool TimeStart;
    private void Update()
    {
        if (time >= 1.5f)
        {
            if (SceneManager.GetActiveScene().buildIndex == 13)
            {
                LevelLoader.instance.loadAnyLevel(1);
                time = 0;
            }
            else
            {    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                LevelLoader.instance.loadNextLevel();
                time = 0;
            }
        }
        if (TimeStart)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        TimeStart = true;
    }
    private void OnTriggerExit(Collider other)
    {
        TimeStart = false;
    }
}
