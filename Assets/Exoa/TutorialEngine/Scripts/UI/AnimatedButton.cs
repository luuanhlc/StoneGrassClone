using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AnimatedButton : UIBehaviour, IPointerDownHandler, IPointerClickHandler
{
    #region Declare
    [Serializable]
	public class ButtonClickedEvent : UnityEvent { }

	public bool interactable = true;
	public bool useClickInsteadDown;

	[SerializeField]
	private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

	private Animator m_animator;
	public AudioClip clickSound;

	private int crrStep = 0;

	public float time = .5f;

    #endregion
    override protected void Start()
	{
		base.Start();
		m_animator = GetComponent<Animator>();
		StartCoroutine(delayFristFeedBacks(time));
	}

	private IEnumerator delayFristFeedBacks(float time)
    {
		yield return Yielders.Get(time);
		if (FeedBackTutorialList.Ins != null)
		FeedBackTutorialList.Ins.actionFeedBacks(0);

	}

	public ButtonClickedEvent onClick
	{
		get { return m_OnClick; }
		set { m_OnClick = value; }
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if (useClickInsteadDown)
			return;

		if (eventData.button != PointerEventData.InputButton.Left || !interactable)
			return;

		Press();
	}

	private void Press()
	{
		if (!IsActive())
			return;
		crrStep++;
		if(FeedBackTutorialList.Ins != null)
			FeedBackTutorialList.Ins.actionFeedBacks(crrStep);
		m_animator.SetTrigger("Pressed");
		Invoke("InvokeOnClickAction", 0.1f);
	}

	private void InvokeOnClickAction()
	{
		m_OnClick.Invoke();
		if (clickSound != null)
		{
			AudioSource source = GetComponent<AudioSource>();
			if (source == null) source = gameObject.AddComponent<AudioSource>();
			source.clip = clickSound;
			source.Play();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!useClickInsteadDown)
			return;

		if (eventData.button != PointerEventData.InputButton.Left || !interactable)
			return;

		Press();
	}
}

