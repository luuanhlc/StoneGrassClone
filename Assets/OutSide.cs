using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;
using MoonAntonio.UI;

    public class OutSide : MonoBehaviour, IPointerEnterHandler
	{
		public MenuCircular menu;

		public void OnPointerEnter(PointerEventData eventData)
		{
			menu.seleccionado = null;
		}
	}
