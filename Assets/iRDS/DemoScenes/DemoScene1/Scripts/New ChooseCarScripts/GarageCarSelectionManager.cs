using UnityEngine;
using System.Collections;
using IRDS.ChampionshipSystem;



public class GarageCarSelectionManager : MonoBehaviour {
    public Transform carInstantiatePosition;
	public Vector3 carsPosition = new Vector3(-78f,309.5f,-240.95f);
	public Vector3 carsRotation = new Vector3(0,-90,0);
	/// <summary>
	/// The height of the instantiate.
	/// </summary>
	public float instantiateHeight = 0.1f;

	/// <summary>
	/// The temp cars.
	/// </summary>
	Object[] tempCars;

	/// <summary>
	/// The car selected.
	/// </summary>
	int carSelected = 0;
	
	/// <summary>
	/// The temp object.
	/// </summary>
	GameObject tempObj;
	
	/// <summary>
	/// The current car inputs.
	/// </summary>
	private IRDSCarControllInput currentCarInputs;

	void Start()
	{
        if (ChampionShipSystem.Instance == null)
        {
            UpdateCarListSingleRace();
            if (tempObj == null) CarSelection(carSelected);
        }
		
	}

	public void LoadSelectedTrack()
	{
		MenuManager.Instance.LoadSelectedTrack();
	}

	public void SetSelectedCar() 
	{
		IRDSLevelLoadVariables.Instance.firstPlayerSettings.selectedCar = tempCars[carSelected].name;
	}

	public void GoToMainMenuScene()
	{
		MenuManager.Instance.GoToMainMenuScene();
	}

	public void PreviousCar()
	{
		if (carSelected > 0)
			carSelected--;
		else carSelected = tempCars.Length-1;
		CarSelection(carSelected);
		SetSelectedCar();
	}
	
	public void NextCar()
	{
		if (carSelected < tempCars.Length-1)
			carSelected++;
		else carSelected = 0;
		CarSelection(carSelected);
		SetSelectedCar();
	}

	/// <summary>
	/// Update the current car to the new selected one
	/// </summary>
	/// <param name='i'>
	/// I, this is the index of the selected car.
	/// </param>
	/// <param name='rotation'>
	/// Rotation.
	/// </param>
	void CarSelection(int i)
	{
		if (tempObj !=null) DestroyImmediate(tempObj);
        if (carInstantiatePosition != null)
            tempObj = InstantiateObject.instantiateCar(tempObj, transform, tempCars[i], carInstantiatePosition.rotation, carInstantiatePosition.position, new Vector3(0, instantiateHeight, 0), RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ, 10);
        else
            tempObj = InstantiateObject.instantiateCar(tempObj, transform, tempCars[i], Quaternion.Euler(carsRotation), carsPosition, new Vector3(0, instantiateHeight, 0), RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ, 10);
        GameObject.Destroy(tempObj.GetComponent<IRDSNavigateTWaypoints>().CurrentWaypointTransform.gameObject);
		tempObj.GetComponent<IRDSCarControllerAI>().enabled = false;
		tempObj.GetComponent<IRDSNavigateTWaypoints>().enabled = false;
		tempObj.transform.parent = transform;

        if (carInstantiatePosition != null)
        {
            tempObj.transform.position = carInstantiatePosition.position + new Vector3(0, instantiateHeight, 0);
            tempObj.transform.rotation = carInstantiatePosition.rotation;
        }else
        {
            tempObj.transform.position = carsPosition+ new Vector3(0, instantiateHeight, 0);
            tempObj.transform.rotation = Quaternion.Euler(carsRotation);
        }

        tempObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		tempObj.GetComponent<Rigidbody>().inertiaTensor *= 10;
		
		ChangeCarColor(IRDSLevelLoadVariables.Instance.carAiColors[0]);
		IRDSLevelLoadVariables.Instance.firstPlayerSettings.selectedCarColor[0] = IRDSLevelLoadVariables.Instance.carAiColors[0];
		currentCarInputs = tempObj.GetComponent<IRDSCarControllInput>();
	}
	
	void ChangeCarColor (Color32 carColor)
	{
		MeshRenderer[] tempMaterial = tempObj.GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer mesh in tempMaterial)
			foreach (Material material in mesh.materials)
				foreach (Shader shader in IRDSLevelLoadVariables.Instance.carShader)
					if ((material.shader.name.ToString() == shader.name.ToString())) material.SetColor("_Color",carColor);
		//			if ((material.shader.name.ToString().Length> 8?material.shader.name.ToString().Substring(0,9): "no") == "Car Paint") material.SetColor("_Color",carColor);
	}
	
	public void SetControlInput(int selected)
	{
		IRDSLevelLoadVariables.Instance.firstPlayerSettings.controlAsigments.mobileControlSelected = selected;
	}

	/// <summary>
	/// Updates the car list.
	/// </summary>
	void UpdateCarListSingleRace()
	{
		Resources.UnloadUnusedAssets();
		ChampionShipSystem champSystem = ChampionShipSystem.Instance;
		IRDSLevelLoadVariables loadedSettings = IRDSLevelLoadVariables.Instance;
		//We have to make a copy of the list and arrays, to avoid erasing them if the list and arrays changes.
		if (champSystem != null){
			loadedSettings.preloadedCarsPath = new System.Collections.Generic.List<IRDSLevelLoadVariables.IRDSCarsPaths>(champSystem.defaultCarsPath);
			loadedSettings.carsFolders = champSystem.originalCarFolders.Clone() as string[];
			loadedSettings.CarsForRace = champSystem.originalCars.Clone() as GameObject[];
		}
		//Preload all the cars that by default are on the default resources folder Cars.
		tempCars = Resources.LoadAll("Cars",typeof(GameObject));
		if (loadedSettings.carsFolders.Length !=0)
		{
			foreach (string folder in loadedSettings.carsFolders)
			{
				Object[] tempcars1 = UnityEngine.Resources.LoadAll(folder,typeof(GameObject));
				int tempindex = tempCars.Length;
				System.Array.Resize<Object>(ref tempCars, tempCars.Length + tempcars1.Length);
				System.Array.Copy(tempcars1,0,tempCars,tempindex,tempcars1.Length);
			}
		}
		for (int i = 0;i < loadedSettings.preloadedCarsPath.Count;i++)
		{
			if (loadedSettings.preloadedCarsPath[i].enabledForPlayers){
				Object[] tempcars1 = UnityEngine.Resources.LoadAll(loadedSettings.preloadedCarsPath[i].folderName,typeof(GameObject));
				int tempindex = tempCars.Length;
				System.Array.Resize<Object>(ref tempCars, tempCars.Length + tempcars1.Length);
				System.Array.Copy(tempcars1,0,tempCars,tempindex,tempcars1.Length);
			}
		}
		foreach(GameObject car in loadedSettings.CarsForRace){
			System.Array.Resize<Object>(ref tempCars, tempCars.Length+1);
			tempCars[tempCars.Length-1] = car;
		}
	}
	
	
	/// <summary>
	/// Updates the car list for selected team.
	/// </summary>
	public void UpdateCarListForSelectedTeam()
	{
        Debug.Log("Updating team cars!-->" + MenuManager.Instance.teamSelectedByPlayer);
		ChampionShipSystem champSystem = ChampionShipSystem.Instance;
		
		tempCars = new Object[0];
		Resources.UnloadUnusedAssets();
		if (champSystem.championshipTeams[MenuManager.Instance.teamSelectedByPlayer].teamCars.carsArray.Length !=0)
		{
			int tempindex = tempCars.Length;
			System.Array.Resize<Object>(ref tempCars, tempCars.Length + champSystem.championshipTeams[MenuManager.Instance.teamSelectedByPlayer].teamCars.carsArray.Length);
			System.Array.Copy(champSystem.championshipTeams[MenuManager.Instance.teamSelectedByPlayer].teamCars.carsArray,0,tempCars,tempindex,champSystem.championshipTeams[MenuManager.Instance.teamSelectedByPlayer].teamCars.carsArray.Length);
		}
		foreach (IRDSLevelLoadVariables.IRDSCarsPaths car in champSystem.championshipTeams[MenuManager.Instance.teamSelectedByPlayer].teamCars.teamCars)
		{
			
			if (car.enabledForPlayers){
				foreach(string carName in car.preloadedCarsPath){
					Object tempcars1 = UnityEngine.Resources.Load(carName.Replace("\\","/"),typeof(GameObject));
					System.Array.Resize<Object>(ref tempCars, tempCars.Length+1);
					tempCars[tempCars.Length-1] = tempcars1;
				}
			}
		}
        carSelected = 0;
        CarSelection(carSelected);
	}

	void LateUpdate()
	{
		//Set the brake lights on.
		if (currentCarInputs != null) 
		{
			currentCarInputs.CarVisuals.brakeLight(1);
			currentCarInputs.setBrakeInput(1);
			currentCarInputs.Drivetrain.changeGearToTarget(1,0f);
			currentCarInputs.Drivetrain.SetGear(1);
			currentCarInputs.Drivetrain.gearWanted =1;
		}
	}

}
