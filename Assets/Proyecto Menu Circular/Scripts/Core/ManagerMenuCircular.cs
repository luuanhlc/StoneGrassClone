//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// ManagerMenuCircular.cs (13/06/2017)											\\
// Autor: Antonio Mateo (Moon Antonio) 	antoniomt.moon@gmail.com				\\
// Descripcion:		Manager del menu circular									\\
// Fecha Mod:		16/06/2017													\\
// Ultima Mod:		Immplementacion de titulo									\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections;
#endregion

namespace MoonAntonio.UI
{
	/// <summary>
	/// <para>Manager del menu circular	</para>
	/// </summary>
	[AddComponentMenu("Moon Antonio/UI/ManagerMenuCircular")]
	public class ManagerMenuCircular : MonoBehaviour 
	{
		#region Instancia
		/// <summary>
		/// <para>Instancia de <see cref="ManagerMenuCircular"/>.</para>
		/// </summary>
		public static ManagerMenuCircular instance;				// Instancia de ManagerMenuCircular
		#endregion

		#region Variables Publicas
		/// <summary>
		/// <para>Prefab del menu circular.</para>
		/// </summary>
		public MenuCircular menu;								// Prefab del menu circular
		#endregion

		#region Inicializadores
		/// <summary>
		/// <para>Inicializador de <see cref="ManagerMenuCircular"/>.</para>
		/// </summary>
		private void Awake()// Inicializador de ManagerMenuCircular
		{
			// Singleton
			instance = this;
		}
		#endregion

		#region API
		/// <summary>
		/// <para>Abre el menu circular.</para>
		/// </summary>
		/// <param name="interac">Datos de la interaccion</param>
		MenuCircular newMenu;
		public void AbrirMenu(Interactivo interac, Vector2 _tranform)// Abre el menu circular.
		{
			// Instancia el menu
			newMenu = Instantiate(menu) as MenuCircular;

			// Fija su posicion y su padre
			newMenu.transform.SetParent(this.transform, false);
			
			newMenu.label.text = interac.titulo.ToUpper(); // Convertimos en mayusculas

			// Mostrar botones del menu
			newMenu.AbrirBotones(interac);
			if (_tranform == Vector2.zero)
				newMenu.transform.position = Input.mousePosition;
			else
			{
				newMenu.transform.position = _tranform;
				Interactivo.Ins._isOn = false;
				StartCoroutine(turnOff());
			}
		}
		#endregion

		IEnumerator turnOff()
        {
			yield return Yielders.Get(.9f);
			IsLandBuildTut.Ins.PressAndHold.gameObject.SetActive(false);
			IsLandBuildTut.Ins.DragTut();
			newMenu.TurnOFF();
        }
	}
}
