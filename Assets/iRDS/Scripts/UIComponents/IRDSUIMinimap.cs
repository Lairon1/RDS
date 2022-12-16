using UnityEngine;
using System.Collections.Generic;

namespace IRDS.UI{

	public class IRDSUIMinimap : MonoBehaviour{
		
		/// <summary>
		/// The map target.
		/// </summary>
		public Transform mapTarget;
		
		/// <summary>
		/// The zoom level.
		/// </summary>
		public float zoomLevel = 8f;
		
		
		/// <summary>
		/// The map image.
		/// </summary>
		public RectTransform mapImage;
		
		/// <summary>
		/// The player icon.
		/// </summary>
		public RectTransform playerIcon;
		
		/// <summary>
		/// The opponent icon.
		/// </summary>
		public RectTransform opponentIcon;
		
		/// <summary>
		/// The map limits offset percentage.
		/// </summary>
		public float mapLimitsOffsetPercentage = 0.95f;
		
		/// <summary>
		/// The keep inside.
		/// </summary>
		public bool keepInside = true;
		
		/// <summary>
		/// The lock scale.
		/// </summary>
		public bool lockScale = false;
		
		/// <summary>
		/// The lock rotation.
		/// </summary>
		public bool lockRotation = false;
		
		/// <summary>
		/// The minimum scale.
		/// </summary>
		public float minScale = 5f;
		
		/// <summary>
		/// The current scale.
		/// </summary>
		public float currentScale = 7f;

		/// <summary>
		/// The type of the minimap.
		/// </summary>
		public MinimapType minimapType = MinimapType.Player_centered;

		/// <summary>
		/// The map rect.
		/// </summary>
		RectTransform mapRect;


		
		[HideInInspector]
		Blip[] blips;
		
		public bool BlipsNull {
			get {
				return (blips ==null);
			}
		}
		
		public int BlipsLength {
			get {
				return blips.Length;
			}
		}
		
		public enum MinimapType{
			Static,
			Line_Horizontal,
			Line_Vertical,
			Player_centered
		}
		
		
		Vector2 XRotation = Vector2.right;
		Vector2 YRotation = Vector2.up;
		
		/// <summary>
		/// The local car list.  this is a cache of the IRDSStatistics Car list.
		/// </summary>
		List<IRDSCarControllerAI> localCarList;

		void Start()
		{

			mapRect = GetComponent<RectTransform>();
			if (mapRect == null){
				this.enabled = false;
				Debug.LogError("No RectTrasform found on the GameObject, please use a UI Panel as" +
				               " the Minimap main object");
				return;
			}
			localCarList = IRDSStatistics.Instance.GetAllDriversList();
			InitializeBlips(localCarList, transform);


		}

		/// <summary>
		/// Initializes the blips.
		/// </summary>
		public void InitializeBlips(List<IRDSCarControllerAI> carlist, Transform parent)
		{
			if (blips != null) { //New block
				for ( int i = 1; i < blips.Length; ++i )
					GameObject.Destroy( blips[i].myRectTransform.gameObject );
			}

			GameObject newMinimap = new GameObject(name + "_Dummy Targets");
			blips = new Blip[carlist.Count+1];
			if (this.mapTarget == null)
			{
				this.mapTarget = new GameObject().transform;
				this.mapTarget.position = Vector3.zero;
				this.mapTarget.rotation = Quaternion.identity;
				this.mapTarget.name = "Dummy_Main_Minimap_Target";
				this.mapTarget.parent = newMinimap.transform;
			}
			for (int i  = 0; i < blips.Length;i++){
				blips[i] = new Blip();
				//				blips[i].map = this;
				
				if (i == 0)
				{
					if (minimapType != MinimapType.Static)
					{
						blips[i].target = this.mapTarget;
					}else
					{
						blips[i].target = this.mapTarget;
					}
					blips[i].myRectTransform = mapImage;
					blips[i].imMap = true;
				}else{
					blips[i].originalTargetTransform = carlist[i-1].transform;
					blips[i].target = new GameObject().transform;
					blips[i].target.name = "Dummy_Minimap_Target"+ i.ToString();
					blips[i].target.parent = newMinimap.transform;
					blips[i].carReference = carlist[i-1];
					if (carlist[i-1].IsPlayer){
						if (minimapType != MinimapType.Static)
						{
							this.mapTarget = blips[i].target;
						}
						blips[i].myRectTransform = (RectTransform)Instantiate( playerIcon);
						blips[i].myRectTransform.gameObject.SetActive(true); //New
					}else
					{
						blips[i].myRectTransform = (RectTransform)Instantiate( opponentIcon);
						blips[i].myRectTransform.gameObject.SetActive(true); //New
					}
					blips[i].myRectTransform.SetParent(mapRect.transform);
				}
				blips[i].myRectTransform.transform.localRotation = Quaternion.identity; //NEW
			}
			playerIcon.gameObject.SetActive(false);
			opponentIcon.gameObject.SetActive(false);
		}
		
		/// <summary>
		/// Updates the rotation.  This is called once per update cycle
		/// </summary>
		public void UpdateRotation()
		{
			XRotation = new Vector2(mapTarget.right.x, -mapTarget.right.z);
			YRotation = new Vector2(-mapTarget.forward.x, mapTarget.forward.z);
		}
		
		public Vector2 TransformPostion(Vector3 position, bool useRotation)
		{
			Vector3 offset = (position) - mapTarget.position;
			if (useRotation){
				Vector2 newPosition = offset.x*XRotation;
				newPosition +=offset.z *YRotation;
				newPosition *=zoomLevel;
				return newPosition;
			}else
			{
				Vector2 newPosition = new Vector2(offset.x,offset.z) * zoomLevel;
				return newPosition;
			}
			
		}
		
		public Vector3 TransformRotation(Vector3 rotation)
		{
			return new Vector3(0,0,mapTarget.eulerAngles.y-rotation.y);
		}
		
		public Vector2 KeepInside(Vector2 point)
		{
			point = Vector2.Max(point,mapRect.rect.min*mapLimitsOffsetPercentage);
			point = Vector2.Min(point,mapRect.rect.max*mapLimitsOffsetPercentage);
			return point;
		}
		
		
		
		public Vector2 UpdateTransformLineMinimapHorizontal(Vector3 position, IRDSCarControllerAI car)
		{
			return new Vector2(Mathf.Lerp(mapRect.rect.min.x,mapRect.rect.max.x, (float)car.GetLapCompletionPercentage()),position.z);
		}
		
		public Vector2  UpdateTransformLineMinimapVertical(Vector3 position, IRDSCarControllerAI car)
		{
			
			return new Vector2( position.x,Mathf.Lerp(mapRect.rect.min.y,mapRect.rect.max.y, (float)car.GetLapCompletionPercentage()));
			
			
		}
		
		
		public void UpdateBlip(int blipIndex)
		{
			
			//early exit if target is null
			if (blips[blipIndex].target ==null)return;
			
			Vector2 newPosition = Vector2.zero;
			bool isCurrentCar = IRDSStatistics.CurrentCar != null? blips[blipIndex].carReference == IRDSStatistics.CurrentCar:false;
			switch(minimapType)
			{
			case MinimapType.Line_Horizontal:
				if (blips[blipIndex].carReference != null)
					newPosition = UpdateTransformLineMinimapHorizontal(blips[blipIndex].target.position,blips[blipIndex].carReference);
				break;
				
			case MinimapType.Line_Vertical:
				if (blips[blipIndex].carReference != null)
					newPosition = UpdateTransformLineMinimapVertical(blips[blipIndex].target.position,blips[blipIndex].carReference);
				break;
				
			case MinimapType.Player_centered:
				blips[blipIndex].UpdateDummyTargetPosRot();
				newPosition = TransformPostion(blips[blipIndex].target.position, true);
				
				if (keepInside && !blips[blipIndex].imMap)
					newPosition = KeepInside(newPosition);
				
				if (!lockRotation || blips[blipIndex].imMap)
					blips[blipIndex].myRectTransform.localEulerAngles = TransformRotation(blips[blipIndex].target.eulerAngles);
				break;
				
			case MinimapType.Static:
				blips[blipIndex].UpdateDummyTargetPosRot();
				newPosition = TransformPostion(blips[blipIndex].target.position, false);
				break;
			}
			if (!lockScale )//&& !blips[blipIndex].imMap
			{
				float scale = blips[blipIndex].imMap?zoomLevel:Mathf.Max(isCurrentCar?currentScale:minScale,zoomLevel);
				blips[blipIndex].myRectTransform.localScale = new Vector3(scale,scale,1);
			}
			
			if (isCurrentCar)
			{
				if (blips[blipIndex].myRectTransform.GetSiblingIndex()!=blips[blipIndex].myRectTransform.parent.childCount-1)
					blips[blipIndex].myRectTransform.SetAsLastSibling();
			}
			
			blips[blipIndex].myRectTransform.localPosition = newPosition;
		}
		
		
		[System.Serializable]
		public class Blip{
			public Transform target;
			public bool imMap = false;
			public RectTransform myRectTransform;
			
			[HideInInspector]
			public IRDSCarControllerAI carReference;
			
			public Transform originalTargetTransform;
			public void UpdateDummyTargetPosRot()
			{
				if (originalTargetTransform == null)return;
				target.position = originalTargetTransform.position;
				target.eulerAngles = new Vector3(0, originalTargetTransform.eulerAngles.y,0);
			}
		}


		void LateUpdate()
		{
			if (blips != null)
			{
				UpdateRotation();
				for (int i = 0;i< blips.Length;i++)
				{
					UpdateBlip(i);
				}
			}
		}

	}
}