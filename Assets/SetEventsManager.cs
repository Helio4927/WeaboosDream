using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEventsManager : MonoBehaviour {

    public Animator [] animators;

     void Start()
    {
        foreach (var temp in animators)
        {
            AnimationClip[] animations = temp.runtimeAnimatorController.animationClips;

            AnimationEvent animStartEvent = new AnimationEvent();
            AnimationEvent animCompletedEvent = new AnimationEvent();

            foreach (var anim in animations)
            {

                if (!anim.isLooping)
                {
                    //Debug.Log("<color=greed>Registrado Anim CLip: </color>"+anim.name);
                    //Debug.Log("<color=greed>Anim CLip Length: </color>" + anim.length);
                    //Debug.Log("<color=greed>Anim CLip FrMEratw: </color>" + anim.frameRate);

                    //animation start event
                    animStartEvent.time = 0;
                    animStartEvent.stringParameter = anim.name;
                    animStartEvent.functionName = "OnAnimationStart";
                    //anim.AddEvent(animStartEvent);

                    //animation completed event
                    animCompletedEvent.time = anim.length - 0.1f;//se agrega esta resta porque la animation genera un frame adicional
                    animCompletedEvent.stringParameter = anim.name;
                    animCompletedEvent.functionName = "OnAnimationCompleted";
                    anim.AddEvent(animCompletedEvent);
                }
            }
        }
    }
}
