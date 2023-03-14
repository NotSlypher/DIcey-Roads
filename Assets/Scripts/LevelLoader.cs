using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator loaderAnim;
    public static LevelLoader instance;
    public float transitionTime = 1f;
        private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void loadNextLevel()
    {
        StartCoroutine(loadScene(SceneManager.GetActiveScene().buildIndex+1));
    }
    public void loadAnyLevel(int index)
    {
        StartCoroutine(loadScene(index));
        UIManager.Instance.spacePressed = false;
        UIManager.Instance.escapePressed = false;
        UIManager.Instance.instructionIndex = 0;
    }
    IEnumerator loadScene(int buildIndex)
    {
        loaderAnim.SetBool("LoadScene",true);

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(buildIndex);
        loaderAnim.SetBool("LoadScene", false);
        //loaderAnim.ResetTrigger("Start");
    }
}
