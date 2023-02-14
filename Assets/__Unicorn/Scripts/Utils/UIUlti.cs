using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class UIUlti
{
    public static bool IsPointerOverUIObject()
    {
        //check mouse
        if(EventSystem.current.IsPointerOverGameObject())
            return true;
             
        //check touch
        if(Input.touchCount > 0 ){
            if(EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return true;
        }

        return false;
    }
}