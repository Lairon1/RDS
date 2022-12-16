using System.IO;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class IRDSEditorValidator
{
    static IRDSEditorValidator()
    {
#if UNITY_2018_3_OR_NEWER
        string[] files = Directory.GetFiles(Application.dataPath, "iRDSEditorTool.unity2018", SearchOption.AllDirectories);

        if (files.Length > 0)
        {
            string[] currentDll = Directory.GetFiles(Application.dataPath, "iRDSEditorTool.dll", SearchOption.AllDirectories);

            if (currentDll.Length > 0)
            {
                File.Move(currentDll[0], string.Format("{0}{1}", currentDll[0],"BK"));
            }

            File.Move(files[0], files[0].Replace("unity2018", "dll"));
            Debug.Log(files[0]);
        }
#endif
    }
}
