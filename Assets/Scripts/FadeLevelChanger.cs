using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeLevelChanger : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;
    public LevelLoader levelLoader;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("fadeOut");
    }

    public void OnFadeComplete()
    {
        levelLoader.LoadLevel(levelToLoad);
    }
}
