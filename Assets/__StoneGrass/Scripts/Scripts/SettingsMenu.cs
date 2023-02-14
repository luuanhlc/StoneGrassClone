using UnityEngine ;
using UnityEngine.UI ;
using DG.Tweening ;

public class SettingsMenu : MonoBehaviour {
   [Header ("space between menu items")]
   [SerializeField] Vector2 spacing ;

   [Space]
   [Header ("Main button rotation")]
   [SerializeField] float rotationDuration ;
   [SerializeField] Ease rotationEase ;

   [Space]
   [Header ("Animation")]
   [SerializeField] float expandDuration ;
   [SerializeField] float collapseDuration ;
   [SerializeField] Ease expandEase ;
   [SerializeField] Ease collapseEase ;

   [Space]
   [Header ("Fading")]
   [SerializeField] float expandFadeDuration ;
   [SerializeField] float collapseFadeDuration ;

   Button mainButton ;
   SettingsMenuItem[] menuItems ;

    public Sprite openImg;

    public Sprite offImg;

   //is menu opened or not
   public bool isExpanded = true;

   Vector2 mainButtonPosition ;
   int itemsCount ;

    public static SettingsMenu Ins;

    private void Awake()
    {
        Ins = this;
    }

    void Start () {
      //add all the items to the menuItems array
      itemsCount = transform.childCount - 1 ;
      menuItems = new SettingsMenuItem[itemsCount] ;
      for (int i = 0; i < itemsCount; i++) {
         // +1 to ignore the main button
         menuItems [ i ] = transform.GetChild (i + 1).GetComponent <SettingsMenuItem> () ;
      }

      mainButton = transform.GetChild (0).GetComponent <Button> () ;
      mainButton.onClick.AddListener (ToggleMenu) ;
      //SetAsLastSibling () to make sure that the main button will be always at the top layer
      mainButton.transform.SetAsLastSibling () ;

      mainButtonPosition = mainButton.GetComponent <RectTransform> ().anchoredPosition ;

      //set all menu items position to mainButtonPosition
      ResetPositions () ;
   }

    void ResetPositions () {
        for (int i = 0; i < itemsCount; i++) 
        {
            menuItems [ i ].rectTrans.anchoredPosition = mainButtonPosition ;
            menuItems[i].img.DOFade(0f, collapseFadeDuration).SetUpdate(true);
        }
    }

    public void ToggleMenu () {
        isExpanded = !isExpanded ;

        if (isExpanded) {
            //menu opened
            UiController.Ins._UISetting.Open();
            mainButton.GetComponent<Image>().sprite = openImg;

            for (int i = 0; i < itemsCount; i++) 
            {
                menuItems [ i ].rectTrans.DOAnchorPos (mainButtonPosition + spacing * (i + 1), expandDuration).SetDelay(.1f).SetEase (expandEase).SetUpdate(true) ;
                //Fade to alpha=1 starting from alpha=0 immediately
                menuItems [ i ].img.DOFade (1f, expandFadeDuration).SetDelay(.1f).From (0f).SetUpdate(true) ;
            }
        } 
        else 
        {
            mainButton.GetComponent<Image>().sprite = offImg;

            UISetting.Ins._Close(false);
            //menu closed
            for (int i = 0; i < itemsCount; i++) {
            menuItems [ i ].rectTrans.DOAnchorPos (mainButtonPosition, collapseDuration).SetEase (collapseEase).SetUpdate(true) ;
            //Fade to alpha=0
            menuItems [ i ].img.DOFade (0f, collapseFadeDuration).SetUpdate(true) ;
        }
    }

        //rotate main button arround Z axis by 180 degree starting from 0
    /*mainButton.transform
			.DORotate (Vector3.forward * 180, rotationDuration)
			.From (Vector3.zero)
			.SetEase (rotationEase).SetUpdate(true) ;*/
   }

   public void OnItemClick (int index) {
      //here you can add you logic 
      switch (index) {
         case 0: 
				//first button
            break ;
         case 1: 
				//second button
            break ;
         case 2: 
				//third button
            break ;
      }
   }

   void OnDestroy () {
      //remove click listener to avoid memory leaks
      mainButton.onClick.RemoveListener (ToggleMenu) ;
   }
}
