using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineEvents : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkJSON;
    [SerializeField]
    private Animator cutsceneBars;
    [SerializeField]
    private GameObject player;

    public bool inCutscene;
    public List<PlayableAsset> playables;
    private PlayableDirector director;
    private PlayableAsset myPlayable;
    

    private static TimelineEvents instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        director = GetComponent<PlayableDirector>();
        if (DataPersistenceManager.instance.isNewGame)
        {
            PlayCutscene(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static TimelineEvents GetInstance()
    {
        return instance;
    }

    public void PlayDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }

    public void PlayCutscene(int index)
    {
        myPlayable = playables[index];
        director.Play(myPlayable);
    }

    public void LoadCutsceneBars()
    {
        inCutscene = true;
        cutsceneBars.SetBool("isOpen", true);
    }

    public void CloseCutsceneBars()
    {
        cutsceneBars.SetBool("isOpen", false);
        
    }

    public void ExitTimeline()
    {
        director.Stop();
        player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y, 0);
    }
}
