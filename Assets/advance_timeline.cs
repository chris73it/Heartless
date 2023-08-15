using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class advance_timeline : MonoBehaviour
{
    PlayableDirector timeline;
 
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }
 
    public void PlayTimeline() {
        timeline.Play();
    }

    public void PauseTimeline() {
        timeline.Pause();
    }
    
}
