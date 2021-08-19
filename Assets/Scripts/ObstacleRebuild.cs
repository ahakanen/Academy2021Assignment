using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRebuild : MonoBehaviour
{
	public GameObject[] rebuildPieces;
	Vector3[] rebuildPositions;

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
		Debug.Log("REBUILDING OBSTACLE!");
		for (int i = 0; i / 2 < rebuildPieces.Length; i += 2)
		{
			int j = i / 2;
			rebuildPieces[j].transform.localPosition = rebuildPositions[i];
			rebuildPieces[j].transform.localEulerAngles = rebuildPositions[i + 1];
		}
	}
}
