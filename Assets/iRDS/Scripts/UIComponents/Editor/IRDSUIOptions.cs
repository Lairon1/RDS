using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using IRDS.UI;

[System.Serializable]
public class IRDSUIOptions : Editor
{

    private const string kUILayerName = "UI";
    private const float kWidth = 160f;
    private const float kThickHeight = 30f;
    private const float kThinHeight = 20f;
    private static string[] standingTitles = {
        "Pos",
        "Driver Name",
        "Car",
        "Laps",
        "Lap Time",
        "Best Time",
        "Total Time",
        "Points",
        "Total Points",
        "Team",
        "Points",
        "Total Points"};
    private static string[] standingItems = {
        "Position",
        "DriverName",
        "CarName",
        "Laps",
        "LapTime",
        "BestTime",
        "TotalTime",
        "DriverPoints",
        "DriverTotalPoints",
        "TeamName",
        "TeamPoints",
        "TeamTotalPoints"
    };
    private static string[] standingItemsDefaultText = {
        "1",
        "DriverName",
        "CarName",
        "1",
        "00:00:00",
        "00:00:00",
        "00:00:00",
        "1",
        "1",
        "Team",
        "1",
        "1"};
    private static float[] standingWidth = {
        50f,
        200f,
        150f,
        50f,
        100f,
        100f,
        100f,
        80,
        80,
        200,
        80,
        80
    };
    private static TextAnchor[] standingTextAligment = {
        TextAnchor.MiddleRight,
        TextAnchor.MiddleLeft,
        TextAnchor.MiddleLeft,
        TextAnchor.MiddleRight,
        TextAnchor.MiddleLeft,
        TextAnchor.MiddleLeft,
        TextAnchor.MiddleLeft,
        TextAnchor.MiddleRight,
        TextAnchor.MiddleRight,
        TextAnchor.MiddleLeft,
        TextAnchor.MiddleRight,
        TextAnchor.MiddleRight,
    };
    private const int standingsFontSize = 20;
    private static Vector2 s_ThickGUIElementSize = new Vector2(kWidth, kThickHeight);
    private static Vector2 s_ThinGUIElementSize = new Vector2(kWidth, kThinHeight);
    private static Vector2 s_ImageGUIElementSize = new Vector2(100f, 100f);
    private static Color s_PanelColor = new Color(1f, 1f, 1f, 0.392f);
    private static Color s_TextColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);

    [MenuItem("GameObject/UI/iRDS/Stats/Add All/With Labels", false, 0)]
    [MenuItem("GameObject/iRDS/UI/Stats/Add All/With Labels", false, 0)]
    static void AddAllUIWithLabel(MenuCommand menuCommand)
    {
        AddBestTimeLabel(menuCommand);
        AddLastLapTimeLabel(menuCommand);
        AddLapTimeLabel(menuCommand);
        AddTotalTimeLabel(menuCommand);
        AddLapCounterLabel(menuCommand);
        AddPositionLabel(menuCommand);
        AddMinimap(menuCommand);
        AddInitialCounterText(menuCommand);
        AddStandings(menuCommand);
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Add All/Simple", false, 0)]
    [MenuItem("GameObject/iRDS/UI/Stats/Add All/Simple", false, 0)]
    static void AddAllUISimple(MenuCommand menuCommand)
    {
        AddBestTimeSimple(menuCommand);
        AddLastLapTimeSimple(menuCommand);
        AddLapTimeSimple(menuCommand);
        AddTotalTimeSimple(menuCommand);
        AddLapCounterSimple(menuCommand);
        AddPositionSimple(menuCommand);
        AddMinimap(menuCommand);
        AddInitialCounterText(menuCommand);
        AddStandings(menuCommand);
    }



    [MenuItem("GameObject/UI/iRDS/Stats/Best Time/With Label", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Best Time/With Label", false, 100)]
    static void AddBestTimeLabel(MenuCommand menuCommand)
    {
        AddNewUILabelAndComponent<IRDSUIBestTime>(menuCommand, "BestTimeLabel", "Best", "00:00:00");
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Best Time/Simple", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Best Time/Simple", false, 100)]
    static void AddBestTimeSimple(MenuCommand menuCommand)
    {
        AddTextType<IRDSUIBestTime>(menuCommand, "BestTime", "00:00:00");
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Last Lap Time/With Label", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Last Lap Time/With Label", false, 100)]
    static void AddLastLapTimeLabel(MenuCommand menuCommand)
    {
        AddNewUILabelAndComponent<IRDSUILastLapTime>(menuCommand, "LastLapTimeLabel", "Last", "00:00:00");
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Last Lap Time/Simple", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Last Lap Time/Simple", false, 100)]
    static void AddLastLapTimeSimple(MenuCommand menuCommand)
    {
        AddTextType<IRDSUILastLapTime>(menuCommand, "LastLapTime", "00:00:00");
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Lap Time/With Label", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Lap Time/With Label", false, 100)]
    static void AddLapTimeLabel(MenuCommand menuCommand)
    {
        AddNewUILabelAndComponent<IRDSUILapTime>(menuCommand, "LapTimeLabel", "Lap Time", "00:00:00");
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Lap Time/Simple", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Lap Time/Simple", false, 100)]
    static void AddLapTimeSimple(MenuCommand menuCommand)
    {
        AddTextType<IRDSUILapTime>(menuCommand, "LapTimeLabel", "00:00:00");
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Total Time/With Label", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Total Time/With Label", false, 100)]
    static void AddTotalTimeLabel(MenuCommand menuCommand)
    {
        AddNewUILabelAndComponent<IRDSUITotalTime>(menuCommand, "TotalTimeLabel", "Time", "00:00:00");
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Total Time/Simple", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Total Time/Simple", false, 100)]
    static void AddTotalTimeSimple(MenuCommand menuCommand)
    {
        AddTextType<IRDSUITotalTime>(menuCommand, "TotalTimeLabel", "00:00:00");
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Lap counter/With Label", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Lap counter/With Label", false, 100)]
    static void AddLapCounterLabel(MenuCommand menuCommand)
    {
        AddNewUILabelAndComponent<IRDSUILap>(menuCommand, "LapCounterLabel", "Lap", "1/1");
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Lap counter/Simple", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Lap counter/Simple", false, 100)]
    static void AddLapCounterSimple(MenuCommand menuCommand)
    {
        AddTextType<IRDSUILap>(menuCommand, "LapCounterLabel", "1/1");
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Position/With Label", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Position/With Label", false, 100)]
    static void AddPositionLabel(MenuCommand menuCommand)
    {
        AddNewUILabelAndComponent<IRDSUIPosition>(menuCommand, "PositionLabel", "Pos", "1/1");
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Misc Times", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Misc Times", false, 100)]
    static void AddMiscLabel(MenuCommand menuCommand)
    {
        AddTextType<IRDSUIMiscTimes>(menuCommand, "Misc Times", "00:00:00");
    }


    [MenuItem("GameObject/UI/iRDS/Stats/Position/Simple", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Position/Simple", false, 100)]
    static void AddPositionSimple(MenuCommand menuCommand)
    {
        AddTextType<IRDSUIPosition>(menuCommand, "PositionLabel", "1/1");
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Minimap", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Minimap", false, 100)]
    static void AddMinimap(MenuCommand menuCommand)
    {
        GameObject minimapParent = CreateUIElementRoot("MinimapPanel", menuCommand, s_ThickGUIElementSize);
        Image minimapMainImage = AddNewUIImage(minimapParent, "MinimapMain");
        IRDSUIMinimap minimapMain = minimapMainImage.gameObject.AddComponent<IRDSUIMinimap>();
        minimapMain.gameObject.AddComponent<Mask>();

        AddNewUIImage(minimapParent, "MapBorderImage");

        Image map = AddNewUIImage(minimapMain.gameObject, "MapImage");
        Image playerIcon = AddNewUIImage(minimapMain.gameObject, "PlayerIcon");
        Image opponentIcon = AddNewUIImage(minimapMain.gameObject, "OpponentIcon");
        minimapMain.mapImage = map.rectTransform;
        minimapMain.playerIcon = playerIcon.rectTransform;
        minimapMain.opponentIcon = opponentIcon.rectTransform;


    }

    [MenuItem("GameObject/UI/iRDS/Stats/Standings", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Standings", false, 100)]
    static void AddStandings(MenuCommand menuCommand)
    {

        //Add the main panel and all its components to make a basic good looking standings board
        GameObject standingsParent = CreateUIElementRoot("StandingsPanel", menuCommand, s_ThickGUIElementSize);
        Image standingsPImage = standingsParent.AddComponent<Image>();
        standingsPImage.color = s_PanelColor;
        standingsParent.AddComponent<VerticalLayoutGroup>();
        ContentSizeFitter sizeFitter = standingsParent.AddComponent<ContentSizeFitter>();
        sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        //Add the Main Title panel and its children
        Image Titles = AddNewUIImage(standingsParent, "Titles");
        Titles.color = s_PanelColor;
        HorizontalLayoutGroup titlesGroup = Titles.gameObject.AddComponent<HorizontalLayoutGroup>();
        titlesGroup.spacing = 10f;
        for (int i = 0; i < standingTitles.Length; i++)
        {
            Text title = AddNewUIText(Titles.gameObject, standingTitles[i] + "_Label");
            title.text = standingTitles[i];
            title.alignment = standingTextAligment[i];
            title.fontSize = standingsFontSize;
            LayoutElement titleLayout = title.gameObject.AddComponent<LayoutElement>();
            titleLayout.preferredWidth = standingWidth[i];
        }


        //Add the standings manager child of the panel
        string name = "StandingsManager";
        Image StandingsManager = AddNewUIImage(standingsParent, name);
        StandingsManager.color = s_PanelColor;
        StandingsManager.gameObject.AddComponent<IRDSUIStandings>();

        //Add the Main Title panel and its children
        Image Item = AddNewUIImage(StandingsManager.gameObject, "Item");
        Item.color = s_PanelColor;
        HorizontalLayoutGroup itemGroup = Item.gameObject.AddComponent<HorizontalLayoutGroup>();
        itemGroup.spacing = 10f;
        Item.gameObject.AddComponent<ContentSizeFitter>();
        Item.rectTransform.sizeDelta = new Vector2(Item.rectTransform.sizeDelta.x, 35f);
        for (int i = 0; i < standingItems.Length; i++)
        {
            Text title = AddNewUIText(Item.gameObject, standingItems[i]);
            title.text = standingItemsDefaultText[i];
            title.alignment = standingTextAligment[i];
            title.fontSize = standingsFontSize;
            LayoutElement titleLayout = title.gameObject.AddComponent<LayoutElement>();
            titleLayout.preferredWidth = standingWidth[i];
        }

    }

    [MenuItem("GameObject/UI/iRDS/Stats/Initial Counter/Text", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Initial Counter/Text", false, 100)]
    static void AddInitialCounterText(MenuCommand menuCommand)
    {
        IRDSUIInitialCounter initialCounter = AddTextType<IRDSUIInitialCounter>(menuCommand, "Initial Counter Text");
        initialCounter.initialCounterType = IRDSUIInitialCounter.InitialCounterType.Text;
    }

    [MenuItem("GameObject/UI/iRDS/Stats/Initial Counter/Image Array", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Stats/Initial Counter/Image Array", false, 100)]
    static void AddInitialCounterImageArray(MenuCommand menuCommand)
    {
        IRDSUIInitialCounter initialCounter = AddImageType<IRDSUIInitialCounter>(menuCommand, "Initial Counter Image Array");
        initialCounter.initialCounterType = IRDSUIInitialCounter.InitialCounterType.Sprite_Array;
    }



    /*This is the section for adding all the UI for the car info
	 RPM, SPeedo, Turbo, Fuel, Tires and Gears*/
    [MenuItem("GameObject/UI/iRDS/Car Stats/Add All", false, 0)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/Add All", false, 0)]
    static void AddAllCarUI(MenuCommand menuCommand)
    {
        AddGears(menuCommand);
        AddSpeedoGauge(menuCommand);
        AddSpeedoText(menuCommand);
        AddSpeedoFilled(menuCommand);
        AddRPMGauge(menuCommand);
        AddRPMText(menuCommand);
        AddRPMFilled(menuCommand);
        AddTurboGauge(menuCommand);
        AddTurboText(menuCommand);
        AddTurboFilled(menuCommand);
        AddFuelGauge(menuCommand);
        AddFuelText(menuCommand);
        AddFuelFilled(menuCommand);
        AddTires(menuCommand);
    }


    [MenuItem("GameObject/UI/iRDS/Car Stats/Gears", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/Gears", false, 100)]
    static void AddGears(MenuCommand menuCommand)
    {
        AddTextType<IRDSUIGears>(menuCommand, "Gears");
    }

    [MenuItem("GameObject/UI/iRDS/Car Stats/Speedo/Gauge", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/Speedo/Gauge", false, 100)]
    static void AddSpeedoGauge(MenuCommand menuCommand)
    {
        IRDSUISpeedo Speedo = AddGaugeType<IRDSUISpeedo>(menuCommand, "SpeedoGauge");
        Speedo.uiType = IRDSUISpeedo.UIType.Gauge;
    }

    [MenuItem("GameObject/UI/iRDS/Car Stats/Speedo/Filled", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/Speedo/Filled", false, 100)]
    static void AddSpeedoFilled(MenuCommand menuCommand)
    {
        IRDSUISpeedo Speedo = AddFilledType<IRDSUISpeedo>(menuCommand, "SpeedoFilled");
        Speedo.uiType = IRDSUISpeedo.UIType.Filled;
    }

    [MenuItem("GameObject/UI/iRDS/Car Stats/Speedo/Text", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/Speedo/Text", false, 100)]
    static void AddSpeedoText(MenuCommand menuCommand)
    {
        IRDSUISpeedo Speedo = AddTextType<IRDSUISpeedo>(menuCommand, "SpeedoText");
        Speedo.uiType = IRDSUISpeedo.UIType.Text;
    }

    [MenuItem("GameObject/UI/iRDS/Car Stats/RPM/Gauge", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/RPM/Gauge", false, 100)]
    static void AddRPMGauge(MenuCommand menuCommand)
    {
        IRDSUIRPM RPM = AddGaugeType<IRDSUIRPM>(menuCommand, "RPMGauge");
        RPM.uiType = IRDSUIRPM.UIType.Gauge;
    }

    [MenuItem("GameObject/UI/iRDS/Car Stats/RPM/Filled", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/RPM/Filled", false, 100)]
    static void AddRPMFilled(MenuCommand menuCommand)
    {
        IRDSUIRPM RPM = AddFilledType<IRDSUIRPM>(menuCommand, "RPMFilled");
        RPM.uiType = IRDSUIRPM.UIType.Filled;
    }

    [MenuItem("GameObject/UI/iRDS/Car Stats/RPM/Text", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/RPM/Text", false, 100)]
    static void AddRPMText(MenuCommand menuCommand)
    {
        IRDSUIRPM RPM = AddTextType<IRDSUIRPM>(menuCommand, "RPMText");
        RPM.uiType = IRDSUIRPM.UIType.Text;
    }

    [MenuItem("GameObject/UI/iRDS/Car Stats/Turbo/Gauge", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/Turbo/Gauge", false, 100)]
    static void AddTurboGauge(MenuCommand menuCommand)
    {
        IRDSUITurbo Turbo = AddGaugeType<IRDSUITurbo>(menuCommand, "TurboGauge");
        Turbo.uiType = IRDSUITurbo.UIType.Gauge;
    }

    [MenuItem("GameObject/UI/iRDS/Car Stats/Turbo/Filled", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/Turbo/Filled", false, 100)]
    static void AddTurboFilled(MenuCommand menuCommand)
    {
        IRDSUITurbo Turbo = AddFilledType<IRDSUITurbo>(menuCommand, "TurboFilled");
        Turbo.uiType = IRDSUITurbo.UIType.Filled;
    }

    [MenuItem("GameObject/UI/iRDS/Car Stats/Turbo/Text", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/Turbo/Text", false, 100)]
    static void AddTurboText(MenuCommand menuCommand)
    {
        IRDSUITurbo Turbo = AddTextType<IRDSUITurbo>(menuCommand, "TurboText");
        Turbo.uiType = IRDSUITurbo.UIType.Text;
    }

    [MenuItem("GameObject/UI/iRDS/Car Stats/Fuel/Gauge", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/Fuel/Gauge", false, 100)]
    static void AddFuelGauge(MenuCommand menuCommand)
    {
        IRDSUIFuel fuel = AddGaugeType<IRDSUIFuel>(menuCommand, "FuelGauge");
        fuel.uiType = IRDSUIFuel.UIType.Gauge;
    }

    [MenuItem("GameObject/UI/iRDS/Car Stats/Fuel/Filled", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/Fuel/Filled", false, 100)]
    static void AddFuelFilled(MenuCommand menuCommand)
    {
        IRDSUIFuel fuel = AddFilledType<IRDSUIFuel>(menuCommand, "FuelFilled");
        fuel.uiType = IRDSUIFuel.UIType.Filled;
    }

    [MenuItem("GameObject/UI/iRDS/Car Stats/Fuel/Text", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/Fuel/Text", false, 100)]
    static void AddFuelText(MenuCommand menuCommand)
    {
        IRDSUIFuel fuel = AddTextType<IRDSUIFuel>(menuCommand, "FuelText");
        fuel.uiType = IRDSUIFuel.UIType.Text;
    }

    static T AddGaugeType<T>(MenuCommand menuCommand, string name) where T : Component
    {
        GameObject GaugeParent = CreateUIElementRoot(name, menuCommand, s_ThickGUIElementSize);
        GaugeParent.AddComponent<Image>();
        name += " Needle";
        GameObject child = new GameObject(name);

        //Register for Undo
        Undo.RegisterCreatedObjectUndo(child, "Create " + name);
        Undo.SetTransformParent(child.transform, GaugeParent.transform, "TireParent " + child.name);
        GameObjectUtility.SetParentAndAlign(child, GaugeParent);

        child.AddComponent<Image>();
        return child.AddComponent<T>();
    }

    static T AddFilledType<T>(MenuCommand menuCommand, string name) where T : Component
    {
        GameObject FilledParent = CreateUIElementRoot(name, menuCommand, s_ThickGUIElementSize);
        Image image = FilledParent.AddComponent<Image>();

        image.type = Image.Type.Filled;
        return FilledParent.AddComponent<T>();
    }


    static T AddImageType<T>(MenuCommand menuCommand, string name) where T : Component
    {
        GameObject ImageParent = CreateUIElementRoot(name, menuCommand, s_ImageGUIElementSize);
        //		Image image = ImageParent.AddComponent<Image>();
        return ImageParent.AddComponent<T>();
    }

    static T AddTextType<T>(MenuCommand menuCommand, string name) where T : Component
    {
        GameObject Text = CreateUIElementRoot(name, menuCommand, s_ThickGUIElementSize);
        Text text = Text.AddComponent<Text>();
        text.color = s_TextColor;
        return Text.AddComponent<T>();
    }

    static T AddTextType<T>(MenuCommand menuCommand, string name, string content) where T : Component
    {
        GameObject Text = CreateUIElementRoot(name, menuCommand, s_ThickGUIElementSize);
        Text text = Text.AddComponent<Text>();
        text.color = s_TextColor;
        text.text = content;
        return Text.AddComponent<T>();
    }


    static Image AddNewUIImage(GameObject parent, string name)
    {
        GameObject child = new GameObject(name);

        //Register for Undo
        Undo.RegisterCreatedObjectUndo(child, "Create " + name);
        Undo.SetTransformParent(child.transform, parent.transform, parent.name + " " + child.name);
        GameObjectUtility.SetParentAndAlign(child, parent);
        return child.AddComponent<Image>();
    }

    static Text AddNewUIText(GameObject parent, string name)
    {
        GameObject child = new GameObject(name);

        //Register for Undo
        Undo.RegisterCreatedObjectUndo(child, "Create " + name);
        Undo.SetTransformParent(child.transform, parent.transform, parent.name + " " + child.name);
        GameObjectUtility.SetParentAndAlign(child, parent);
        Text text = child.AddComponent<Text>();
        text.color = s_TextColor;
        return text;
    }

    static void AddNewUILabelAndComponent<T>(MenuCommand menuCommand, string name, string label, string content) where T : Component
    {
        GameObject textParent = CreateUIElementRoot(name.Replace("Label", ""), menuCommand, s_ThinGUIElementSize);
        textParent.AddComponent<HorizontalLayoutGroup>();

        Text childTextLabel = AddNewUIText(textParent, name);
        childTextLabel.color = s_TextColor;
        childTextLabel.text = label;

        Text childText = AddNewUIText(textParent, name.Replace("Label", ""));
        childText.rectTransform.sizeDelta = s_ThinGUIElementSize;
        childText.gameObject.AddComponent<T>();
        childText.text = content;
    }


    [MenuItem("GameObject/UI/iRDS/Car Stats/Tires", false, 100)]
    [MenuItem("GameObject/iRDS/UI/Car Stats/Tires", false, 100)]
    static void AddTires(MenuCommand menuCommand)
    {
        GameObject TireParent = CreateUIElementRoot("TiresUI", menuCommand, s_ThickGUIElementSize);
        IRDSUITires tiresComponent = TireParent.AddComponent<IRDSUITires>();
        tiresComponent.tireImage = new Image[4];
        GameObject[] tiresChildren = new GameObject[4];
        for (int i = 0; i < tiresChildren.Length; i++)
        {
            string wheelPos = "";
            switch (i)
            {
                case 0:
                    wheelPos = "FL";
                    break;
                case 1:
                    wheelPos = "FR";
                    break;
                case 2:
                    wheelPos = "RL";
                    break;
                case 3:
                    wheelPos = "RR";
                    break;
            }
            string name = "Tire_" + wheelPos;
            GameObject child = new GameObject(name);

            //Register for Undo
            Undo.RegisterCreatedObjectUndo(child, "Create " + name);
            Undo.SetTransformParent(child.transform, TireParent.transform, "TireParent " + child.name);
            GameObjectUtility.SetParentAndAlign(child, TireParent);

            tiresChildren[i] = child;
            tiresComponent.tireImage[i] = tiresChildren[i].AddComponent<Image>();
        }

    }

    private static GameObject CreateUIElementRoot(string name, MenuCommand menuCommand, Vector2 size)
    {
        GameObject parent = menuCommand.context as GameObject;
        if (parent == null || parent.GetComponentInParent<Canvas>() == null)
        {
            parent = GetOrCreateCanvasGameObject();
            if (parent.GetComponent<IRDSStatsGUI>())
            {
                IRDSStatsGUI statsUI = parent.GetComponent<IRDSStatsGUI>();
                statsUI.mainUICanvas = parent.GetComponent<Canvas>();
            }
        }
        GameObject child = new GameObject(name);

        Undo.RegisterCreatedObjectUndo(child, "Create " + name);
        Undo.SetTransformParent(child.transform, parent.transform, "Parent " + child.name);
        GameObjectUtility.SetParentAndAlign(child, parent);

        RectTransform rectTransform = child.AddComponent<RectTransform>();
        rectTransform.sizeDelta = size;
        if (parent != menuCommand.context) // not a context click, so center in sceneview
        {
            SetPositionVisibleinSceneView(parent.GetComponent<RectTransform>(), rectTransform);
        }
        Selection.activeGameObject = child;
        return child;
    }

    // Helper function that returns a Canvas GameObject; preferably a parent of the selection, or other existing Canvas.
    static public GameObject GetOrCreateCanvasGameObject()
    {
        GameObject selectedGo = Selection.activeGameObject;

        // Try to find a gameobject that is the selected GO or one if its parents.
        Canvas canvas = (selectedGo != null) ? selectedGo.GetComponentInParent<Canvas>() : null;
        if (canvas != null && canvas.gameObject.activeInHierarchy)
            return canvas.gameObject;

        // No canvas in selection or its parents? Then use just any canvas..
        canvas = Object.FindObjectOfType(typeof(Canvas)) as Canvas;
        if (canvas != null && canvas.gameObject.activeInHierarchy)
            return canvas.gameObject;

        // No canvas in the scene at all? Then create a new one.
        return CreateNewUI();
    }

    static public GameObject CreateNewUI()
    {
        // Root for the UI
        var root = new GameObject("Canvas");
        root.layer = LayerMask.NameToLayer(kUILayerName);
        Canvas canvas = root.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        root.AddComponent<CanvasScaler>();
        root.AddComponent<GraphicRaycaster>();
        if (root.GetComponent<IRDSStatsGUI>() == null)
            root.AddComponent<IRDSStatsGUI>();
        Undo.RegisterCreatedObjectUndo(root, "Create " + root.name);

        // if there is no event system add one...
        CreateEventSystem(false);
        return root;
    }

    public static void CreateEventSystem(MenuCommand menuCommand)
    {
        GameObject parent = menuCommand.context as GameObject;
        CreateEventSystem(true, parent);
    }

    private static void CreateEventSystem(bool select)
    {
        CreateEventSystem(select, null);
    }

    private static void CreateEventSystem(bool select, GameObject parent)
    {
        var esys = Object.FindObjectOfType<EventSystem>();
        if (esys == null)
        {
            var eventSystem = new GameObject("EventSystem");
            GameObjectUtility.SetParentAndAlign(eventSystem, parent);
            esys = eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
            Undo.RegisterCreatedObjectUndo(eventSystem, "Create " + eventSystem.name);
        }

        if (select && esys != null)
        {
            Selection.activeGameObject = esys.gameObject;
        }
    }

    private static void SetPositionVisibleinSceneView(RectTransform canvasRTransform, RectTransform itemTransform)
    {
        // Find the best scene view
        SceneView sceneView = SceneView.lastActiveSceneView;
        if (sceneView == null && SceneView.sceneViews.Count > 0)
            sceneView = SceneView.sceneViews[0] as SceneView;

        // Couldn't find a SceneView. Don't set position.
        if (sceneView == null || sceneView.camera == null)
            return;

        // Create world space Plane from canvas position.
        Vector2 localPlanePosition;
        Camera camera = sceneView.camera;
        Vector3 position = Vector3.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRTransform, new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2), camera, out localPlanePosition))
        {
            // Adjust for canvas pivot
            localPlanePosition.x = localPlanePosition.x + canvasRTransform.sizeDelta.x * canvasRTransform.pivot.x;
            localPlanePosition.y = localPlanePosition.y + canvasRTransform.sizeDelta.y * canvasRTransform.pivot.y;

            localPlanePosition.x = Mathf.Clamp(localPlanePosition.x, 0, canvasRTransform.sizeDelta.x);
            localPlanePosition.y = Mathf.Clamp(localPlanePosition.y, 0, canvasRTransform.sizeDelta.y);

            // Adjust for anchoring
            position.x = localPlanePosition.x - canvasRTransform.sizeDelta.x * itemTransform.anchorMin.x;
            position.y = localPlanePosition.y - canvasRTransform.sizeDelta.y * itemTransform.anchorMin.y;

            Vector3 minLocalPosition;
            minLocalPosition.x = canvasRTransform.sizeDelta.x * (0 - canvasRTransform.pivot.x) + itemTransform.sizeDelta.x * itemTransform.pivot.x;
            minLocalPosition.y = canvasRTransform.sizeDelta.y * (0 - canvasRTransform.pivot.y) + itemTransform.sizeDelta.y * itemTransform.pivot.y;

            Vector3 maxLocalPosition;
            maxLocalPosition.x = canvasRTransform.sizeDelta.x * (1 - canvasRTransform.pivot.x) - itemTransform.sizeDelta.x * itemTransform.pivot.x;
            maxLocalPosition.y = canvasRTransform.sizeDelta.y * (1 - canvasRTransform.pivot.y) - itemTransform.sizeDelta.y * itemTransform.pivot.y;

            position.x = Mathf.Clamp(position.x, minLocalPosition.x, maxLocalPosition.x);
            position.y = Mathf.Clamp(position.y, minLocalPosition.y, maxLocalPosition.y);
        }

        itemTransform.anchoredPosition = position;
        itemTransform.localRotation = Quaternion.identity;
        itemTransform.localScale = Vector3.one;
    }
}
