using UnityEngine;
using Exoa.TutorialEngine;
using System;

public class TutorialDemoScript : MonoBehaviour
{
    public static TutorialDemoScript Ins;

    private void Awake()
    {
        Ins = this;
    }
    public void KhoiTao()
    {
        TutorialLoader.instance.Load("BuildBrigdeTutorial");
        TutorialEvents.OnTutorialComplete += TutorialIsOver;
    }

    private void TutorialIsOver()
    {
        print("Tutorial is over");
    }
}
