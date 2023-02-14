using UnityEngine;
using Exoa.TutorialEngine;
using System.Collections;

    public class runTutorial : MonoBehaviour
    {
    public static runTutorial Ins;
    public GameObject Uicanvas;
    public GameObject tutorialPerfab;
    IsLandBuildTut _isLandTut;
    private void Awake()
    {
        Ins = this;
    }

    public void run()
    {
        StartCoroutine(_runCountDown(1.5f));
    }

    IEnumerator _runCountDown(float time)
    {
        yield return Yielders.Get(time);
        Debug.Log("Running");
        tut = Instantiate(tutorialPerfab, Uicanvas.transform);
        tut.transform.SetSiblingIndex(2);
        _isLandTut = tut.GetComponent<IsLandBuildTut>();
        _isLandTut.SwipeTut();
    }
    public GameObject tut;
    public void TutorialIsOver()
    {
        
    }
    }
