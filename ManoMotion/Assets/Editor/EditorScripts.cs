using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using System.IO;

public class EditorScripts : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	[PostProcessBuild (999)]
	public static void OnPostProcessBuild (BuildTarget buildTarget, string path)
	{
#if UNITY_IOS
		if (buildTarget == BuildTarget.iOS) {
			string projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

                  PBXProject pbxProject = new PBXProject ();
            

            pbxProject.ReadFromFile (projectPath);

			string target = pbxProject.TargetGuidByName ("Unity-iPhone");            
			pbxProject.SetBuildProperty (target, "ENABLE_BITCODE", "NO");

			pbxProject.WriteToFile (projectPath);

		}
#endif
    }
}