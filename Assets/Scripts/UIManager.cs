using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        // Use the SceneManager to load the WalkingScene
        SceneManager.LoadScene("PacMan");
        DontDestroyOnLoad(gameObject);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject hud = GameObject.Find("HUD");
        if (hud != null)
        {
            DontDestroyOnLoad(hud);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
