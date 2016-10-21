using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneCreator : MonoBehaviour {

	[SerializeField] GameObject tree;
	[SerializeField] Transform parent;
	List<GameObject> trees = new List<GameObject>();

	public Vector2 treeZoneX;
	public Vector2 treeZoneZ;
	public int numTreeX;
	public int numTreeZ;

	public void TreePlacement() {
		float treeSepX = (treeZoneX.y - treeZoneX.x)/numTreeX;
		float treeSepZ = (treeZoneZ.y - treeZoneZ.x) / numTreeZ;

		for (int i = 0; i <= numTreeX; i++) {
			for (int j = 0; j <= numTreeZ; j++) {
				GameObject t = Instantiate(tree, Vector3.zero, Quaternion.identity) as GameObject;
				t.transform.SetParent (parent);
				t.transform.localEulerAngles = new Vector3(0f, Random.Range (0, 360), 0f);
				float scale = Random.Range (0.7f, 1f);
				t.transform.localScale = new Vector3 (scale, scale, scale);
				t.transform.localPosition = new Vector3 (treeZoneX.x + i * treeSepX, 2.3f*scale, treeZoneZ.x + j * treeSepZ);
				trees.Add (t);
			}
		}
	}

	public void RemoveTrees() {
		float childCount = parent.childCount;

		for (int i = 0; i < childCount; i++) {
			DestroyImmediate (parent.GetChild (0).gameObject);

		}

		trees.Clear ();
	}
}
