using UnityEngine ;
using UnityEngine.UI ;

public class SettingsMenuItem : MonoBehaviour {
   [HideInInspector] public CanvasGroup img ;
   [HideInInspector] public RectTransform rectTrans ;

   //SettingsMenu reference
   SettingsMenu settingsMenu ;

   //item button
   Button button ;

   //index of the item in the hierarchy
   int index ;

    public Image image;

    private bool _isToggle = true;

    void Awake () {
        img = GetComponent<CanvasGroup> () ;
        rectTrans = GetComponent<RectTransform> () ;

        settingsMenu = rectTrans.parent.GetComponent <SettingsMenu> () ;

        //-1 to ignore the main button
        index = rectTrans.GetSiblingIndex () - 1 ;

        //add click listener
        button = GetComponent<Button> () ;
        button.onClick.AddListener (OnItemClick) ;
        
        switch (index)
        {
            case 0:
                _isToggle = PlayerDataManager.GetMusicSetting();
                break;
            case 1:
                _isToggle = PlayerDataManager.GetSoundSetting();
                break;
            case 2:
                _isToggle = PlayerDataManager.GetVibraviton();
                break;
        }
        SetImage();
    }

   void OnItemClick () {
        _isToggle = !_isToggle;

        SetImage();
        //settingsMenu.OnItemClick (index) ;
   }

    void SetImage()
    {
        if (_isToggle)
            image.gameObject.SetActive(false);
        else
            image.gameObject.SetActive(true);
    }
   void OnDestroy () {
      //remove click listener to avoid memory leaks
      button.onClick.RemoveListener (OnItemClick) ;
   }
}
