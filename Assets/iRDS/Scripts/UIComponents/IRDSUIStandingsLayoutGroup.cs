using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IRDS.UI{

	public class IRDSUIStandingsLayoutGroup : LayoutGroup
	{
		IRDSUIStandings standings;
		public float spacing;
//		protected DrivenRectTransformTracker m_Tracker;
		Vector2 size;
		void SetSize()
		{
			m_Tracker.Clear();
			if (rectChildren.Count == 0)return;
			if (standings != null){
				standings.StandingsInitialPos = new Vector2(padding.left - rectChildren[0].sizeDelta.x*((-rectChildren[0].pivot.x) ),
				                                            -padding.top - rectChildren[0].sizeDelta.y*(rectTransform.pivot.y-rectChildren[0].pivot.y ));
				standings.spacing = spacing;
				if (!Application.isPlaying)
				{
					for (int i =0;i < base.rectChildren.Count;i++)
					{
						m_Tracker.Add(this, base.rectChildren[i] as RectTransform, DrivenTransformProperties.AnchoredPosition);
						m_Tracker.Add(this, base.rectChildren[i] as RectTransform, DrivenTransformProperties.AnchorMax);
						m_Tracker.Add(this, base.rectChildren[i] as RectTransform, DrivenTransformProperties.AnchorMin);
						m_Tracker.Add(this, base.rectChildren[i] as RectTransform, DrivenTransformProperties.SizeDeltaX);

						base.rectChildren[i].anchoredPosition3D = new Vector2(standings.StandingsInitialPos.x,standings.StandingsInitialPos.y - (i * (base.rectChildren[i].rect.height+spacing))); 
						base.rectChildren[i].anchorMax = new Vector2(0,1);
						base.rectChildren[i].anchorMin = new Vector2(0,1);
						base.rectChildren[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, LayoutUtility.GetPreferredSize(base.rectChildren[i], (int)RectTransform.Axis.Horizontal));
					}
				}else{
					for (int i =0;i < base.rectChildren.Count;i++)
					{
						m_Tracker.Add(this, base.rectChildren[i] as RectTransform, DrivenTransformProperties.AnchorMax);
						m_Tracker.Add(this, base.rectChildren[i] as RectTransform, DrivenTransformProperties.AnchorMin);
						m_Tracker.Add(this, base.rectChildren[i] as RectTransform, DrivenTransformProperties.SizeDeltaX);
						base.rectChildren[i].anchorMax = new Vector2(0,1);
						base.rectChildren[i].anchorMin = new Vector2(0,1);
						base.rectChildren[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, LayoutUtility.GetPreferredSize(base.rectChildren[i], (int)RectTransform.Axis.Horizontal));
					}
				}
				 

			}


			size.x = rectChildren[0].sizeDelta.x + padding.left + padding.right;
			size.y = (rectChildren[0].sizeDelta.y ) * rectChildren.Count + spacing * (rectChildren.Count-1) + padding.top + padding.bottom;
			m_Tracker.Add(this, transform as RectTransform, DrivenTransformProperties.SizeDelta);
			m_Tracker.Add(this, transform as RectTransform, DrivenTransformProperties.PivotY);
			base.rectTransform.pivot = new Vector2(base.rectTransform.pivot.x,1);
			base.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,size.x);
			base.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,size.y);
			this.SetDirty();
		}
		
		public override float preferredHeight
		{
			get
			{
				return size.y;
			}
		}
		
		public override float preferredWidth
		{
			get
			{
				return size.x;
			}
		}
		
		protected override void OnEnable()
		{
			base.OnEnable();
			standings = GetComponent<IRDSUIStandings>();
			SetSize();
		}

		void GetStandingReference()
		{
			if (standings == null)
				standings = GetComponent<IRDSUIStandings>();
		}
		

		#if UNITY_EDITOR
		protected override void OnValidate()
		{
			//base.OnValidate();
			GetStandingReference();
			SetSize();
		}
		#endif
		
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			GetStandingReference();
			SetSize();
		}
		
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}
		
		protected override void OnDidApplyAnimationProperties()
		{
			this.OnDidApplyAnimationProperties();
			GetStandingReference();
			SetSize();
		}

		public override void CalculateLayoutInputHorizontal ()
		{
			base.CalculateLayoutInputHorizontal ();
			GetStandingReference();
			SetSize();
		}
		public override void CalculateLayoutInputVertical ()
		{
			GetStandingReference();
			SetSize();
		}

		public override void SetLayoutHorizontal ()
		{
			GetStandingReference();
			SetSize();
		}

		public override void SetLayoutVertical ()
		{
			GetStandingReference();
			SetSize();
		}
	}
}