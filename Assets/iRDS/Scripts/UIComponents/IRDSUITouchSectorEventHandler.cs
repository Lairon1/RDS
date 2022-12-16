using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class IRDSUITouchSectorEventHandler : MonoBehaviour,IPointerDownHandler,
IPointerUpHandler,IDragHandler 
{
	[Serializable]
	public class TriggerEvent : UnityEvent<PointerEventData>
	{}
		
	[Serializable]
	public class Entry
	{
		public EventTriggerType eventID = EventTriggerType.PointerClick;
		public TriggerEvent callback = new TriggerEvent();
	}
		
	public List<Entry> delegates;

	protected IRDSUITouchSectorEventHandler()
	{}
		
	private void Execute(EventTriggerType id, PointerEventData eventData)
	{
		if (delegates != null)
		{
			for (int i = 0, imax = delegates.Count; i < imax; ++i)
			{
				var ent = delegates[i];
				if (ent.eventID == id && ent.callback != null)
					ent.callback.Invoke(eventData);
			}
		}
	}
		
	public virtual void OnDrag(PointerEventData eventData)
	{
		Execute(EventTriggerType.Drag, eventData);
	}
		
	public virtual void OnPointerDown(PointerEventData eventData)
	{
		Execute(EventTriggerType.PointerDown, eventData);
	}

	public virtual void OnPointerUp(PointerEventData eventData)
	{
		Execute(EventTriggerType.PointerUp, eventData);
	}

}
