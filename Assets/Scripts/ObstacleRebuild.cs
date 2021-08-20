using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// there are definitely better ways to save and iterate for this, but should be just fine for a prototype.

public class ObstacleRebuild : MonoBehaviour
{
	[Header("Pieces that will be rebuilt")]
	public GameObject[] rebuildPieces;
	Vector3[] rebuildPositions;

	// Start is called before the first frame update
	void Start()
	{
		// save initial transforms for later rebuilds
		ObstacleLogic obstacleLogic = GetComponent<ObstacleLogic>();
		if (obstacleLogic && obstacleLogic.shouldRebuild)
		{
			rebuildPositions = new Vector3[rebuildPieces.Length * 2];
			for (int i = 0; i / 2 < rebuildPieces.Length; i += 2)
			{
				int j = i / 2;
				rebuildPositions[i] = rebuildPieces[j].transform.localPosition;
				rebuildPositions[i + 1] = rebuildPieces[j].transform.localEulerAngles;
			}
		}
	}

	public void Rebuild()
	{
		Debug.Log("Rebuilding Obstacles!");
		for (int i = 0; i / 2 < rebuildPieces.Length; i += 2)
		{
			int j = i / 2;
			rebuildPieces[j].transform.localPosition = rebuildPositions[i];
			rebuildPieces[j].transform.localEulerAngles = rebuildPositions[i + 1];
		}
	}
}
