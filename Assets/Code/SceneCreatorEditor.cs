using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SceneCreator))]
public class SceneCreatorEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		SceneCreator myScript = (SceneCreator)target;
		if (GUILayout.Button ("Instantiate Tree"))
			myScript.TreePlacement ();

		if (GUILayout.Button ("Remove Trees"))
			myScript.RemoveTrees ();
	}
}
