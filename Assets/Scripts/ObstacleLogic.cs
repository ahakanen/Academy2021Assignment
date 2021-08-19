using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{
	public float rotationSpeed = 25f;
	public bool shouldOffset;
	int rotationInvert = 1;
	public SpriteRenderer firstQuarter;
	public SpriteRenderer secondQuarter;
	public SpriteRenderer thirdQuarter;
	public SpriteRenderer fourthQuarter;
	public bool shouldRebuild = false;
	public ObstacleRebuild rebuild;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (transform.position.x <= 10f) // only rotate while spawned
		{
			transform.Rotate(Vector3.forward * rotationSpeed * rotationInvert * Time.deltaTime);
		}
    }

	public void OnSpawn()
	{
		// set random colors on spawn
		int i = Random.Range(0, 3);
		SetColor(firstQuarter, i);
		int j = Random.Range(0, 3);
		while (j == i)
		{
			j = Random.Range(0, 3);
		}
		SetColor(secondQuarter, j);
		int k = Random.Range(0, 3);
		while (k == i || k == j)
		{
			k = Random.Range(0, 3);
		}
		SetColor(thirdQuarter, k);
		for (int m = 0; m < 4; m++)
		{
			if (m != i && m != j && m != k)
			{
				SetColor(fourthQuarter, m);
			}
		}
		// set offset if necessary, random between left or right
		if (shouldOffset == true)
		{
			Debug.Log("OFFSETTING OBSTACLE!");
			Vector3 tmp = transform.position;
			if (Random.value >= 0.5f)
			{
				tmp.x += 2f;
				transform.position = tmp;
				rotationInvert = -1;
			}
			else
			{
				tmp.x -= 2f;
				transform.position = tmp;
				rotationInvert = 1;
			}
		}
		if (shouldRebuild == true)
		{
			rebuild.Rebuild();
		}
	}

	void SetColor(SpriteRenderer quarter, int i)
	{
		if (i == 0)
		{
			quarter.color = Color.blue;
		}
		else if (i == 1)
		{
			quarter.color = Color.red;
		}
		else if (i == 2)
		{
			quarter.color = Color.green;
		}
		else if (i == 3)
		{
			quarter.color = Color.yellow;
		}
	}
}
