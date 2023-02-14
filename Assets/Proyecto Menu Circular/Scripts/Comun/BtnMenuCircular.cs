#region Librerias
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;
#endregion

namespace MoonAntonio.UI
{
	[AddComponentMenu("Moon Antonio/UI/BtnMenuCircular")]
	public class BtnMenuCircular : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler
	{
		#region Variables Publicas
		public Image circulo;								
		public Image icono;									
		public string titulo;                               
		[HideInInspector] public UnityEvent _event;

		public MenuCircular menu;                           
		public float velAnimacion = 0.8f;					
		#endregion

		#region Variables Privadas
		private Color colorBase;                            
		#endregion

		#region Actualizadores
		private IEnumerator AnimacionBotonesIn()
		{
			transform.localScale = Vector3.zero;
			float timer = 0.0f;

			while (timer < (1 / velAnimacion))
			{
				timer += Time.deltaTime;
				transform.localScale = Vector3.one * timer * velAnimacion;
				yield return null;
			}
			transform.localScale = Vector3.one;
		}
		#endregion

		#region Metodos
		public void AnimacionON()
		{
			StartCoroutine(AnimacionBotonesIn());
		}
		#endregion

		#region Eventos

		private bool _isOn;
		public void OnPointerEnter(PointerEventData eventData)
		{
			menu.seleccionado = this._event;
			colorBase = circulo.color;
			circulo.color = Color.white;
		}

        public void OnPointerExit(PointerEventData eventData)
		{
			//menu.seleccionado = null;
			circulo.color = colorBase;
		}
		#endregion
	}
}
