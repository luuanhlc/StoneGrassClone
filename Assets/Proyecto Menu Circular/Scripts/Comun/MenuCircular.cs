#region Librerias
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
#endregion

namespace MoonAntonio.UI
{
	[AddComponentMenu("Moon Antonio/UI/MenuCircular")]
	public class MenuCircular : MonoBehaviour 
	{
		#region Variables Publicas
		/// <summary>
		/// <para>Prefab del boton del menu circular.</para>
		/// </summary>
		public BtnMenuCircular btnPrefab;						// Prefab del boton del menu circular
		/// <summary>
		/// <para>Boton seleccionado actualmente.</para>
		/// </summary>
		public UnityEvent seleccionado;                    // Boton seleccionado actualmente
		/// <summary>
		/// <para>Texto de titulo.</para>
		/// </summary>
		public Text label;                                      // Texto de titulo
        #endregion

        #region Actualizadores
        private void Awake()
        {
			seleccionado = null;
        }

        private void Update()// Actualizador de MenuCircular
		{
			if (Input.GetMouseButtonUp(0))
			{
                if (seleccionado != null)
                {
					seleccionado.Invoke();
                }
				Interactivo.Ins._isOn = false;
				Destroy(this.gameObject);
			}
		}
		public void TurnOFF()
        {
			Interactivo.Ins._isOn = false;
			Destroy(this.gameObject);
		}

		private IEnumerator AnimacionBtns(Interactivo interac)// Inicia la animacion de los botones
		{
			for (int n = 0; n < interac.opciones.Length; n++)
			{
				BtnMenuCircular newBtn = Instantiate(btnPrefab) as BtnMenuCircular;

				newBtn.transform.SetParent(transform, false);

				float theta = (2 * Mathf.PI / interac.opciones.Length) * n;
				float xPos = Mathf.Sin(theta);
				float yPos = Mathf.Cos(theta);

				newBtn.transform.localPosition = new Vector3(xPos, yPos, 0.0f) * 100.0f;

				newBtn.circulo.color = interac.opciones[n].color;
				newBtn.icono.sprite = interac.opciones[n].sprite;
				newBtn.titulo = interac.opciones[n].titulo;
				newBtn._event = interac.opciones[n]._event;
				newBtn.menu = this;
				newBtn.AnimacionON();

				yield return new WaitForSeconds(0.06f);
			}
		}
		#endregion

		#region Metodos
		public void AbrirBotones(Interactivo interac)// Inicializador de MenuCircular
		{
			// Iniciar animacion
			StartCoroutine(AnimacionBtns(interac));
		}
		#endregion
	}
}
