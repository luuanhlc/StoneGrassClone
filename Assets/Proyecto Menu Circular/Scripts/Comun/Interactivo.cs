
#region Librerias
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#endregion

namespace MoonAntonio.UI
{
	[AddComponentMenu("Moon Antonio/UI/Interactivo")]
	public class Interactivo : MonoBehaviour
	{
		#region Variables Publicas
		public Accion[] opciones;										// Opciones del menu circular
		public string titulo;                                           // Titulo de la interaccion
		#endregion

		#region Inicializadores
		
		private void Start()// Inicializador de Interactivo
		{
			if (titulo == "" || titulo == null) titulo = gameObject.name;
		}
		#endregion

		#region Metodos

		[Tooltip("Hold duration in seconds")]
		[Range(0.3f, 5f)] public float holdDuration = 0.5f;
		public UnityEvent onLongPress;

		private bool isPointerDown = false;
		private bool isLongPressed = false;
		private float elapsedTime = 0f;

		private Button button;

		public static Interactivo Ins;
		private void Awake()
		{
			Ins = this;
			button = GetComponent<Button>();
		}

		private void Update()
		{
			if (isPointerDown && !isLongPressed && !BuildingDrag.Ins._isDrag)
			{
				elapsedTime += Time.deltaTime;
				if (elapsedTime >= holdDuration)
				{
					isLongPressed = true;
					elapsedTime = 0f;
					if (button.interactable && !object.ReferenceEquals(onLongPress, null))
						onLongPress.Invoke();
				}
			}
		}

		public void OnMouseUp()
		{
			_tutPos = Vector2.zero;
			isPointerDown = false;
			isLongPressed = false;
			elapsedTime = 0f;
		}
		public void OnMouseDown()
        {
			isPointerDown = true;
        }
		public bool _isOn;
        public void Event()
        {
			_isOn = true;
			ManagerMenuCircular.instance.AbrirMenu(this, Vector2.zero);
		}
		public Vector2 _tutPos;
		public void EventTut()
        {
			_isOn = true;
			ManagerMenuCircular.instance.AbrirMenu(this, _tutPos);
		}
		#endregion
	}
}
