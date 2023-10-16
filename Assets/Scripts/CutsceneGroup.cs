using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneGroup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCutsceneBool()
    {
        TimelineEvents.GetInstance().inCutscene = false;
    }

    public void InCutsceneBool()
    {
        TimelineEvents.GetInstance().inCutscene=true;
    }
}
