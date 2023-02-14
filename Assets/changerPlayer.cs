using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class changerPlayer : MonoBehaviour
    {
    public List<GameObject> character;

    public static changerPlayer Ins;
    private void Awake()
    {
        Ins = this;
        ChangerCharacter(PlayerDataManager.GetPlayerCurrent());
    }

    public void ChangerCharacter(int index)
    {
        for(int i = 0; i < character.Count; i++)
        {
            if (i == index)
                character[i].SetActive(true);
            else
                character[i].SetActive(false);
        }
    }
}

