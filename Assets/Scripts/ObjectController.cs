using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
	public float objectSpawnFrequency;
	public float obstacleSpawnFrequency;
	float objectSpawner = 0;
	float obstacleSpawner = 0;
	public GameObject[] objects;
	public GameObject[] obstacles;
	public Vector2 spawnPos = new Vector3(0, 10f);
	public Vector2 despawnPos = new Vector3(20f, 10f);
	bool allowColorSwitcher = true;

	// Start is called before the first frame update
	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
		ObjectCheckForDespawn();
    }

	// spawn object every objectSpawnFrequency of movement by moving one of the possible objects to spawnPos

	public void ObjectSpawn(float movement)
	{
		objectSpawner += movement;
		if (objectSpawner < objectSpawnFrequency)
			return;
		objectSpawner = 0;
		int i = Random.Range(0, objects.Length);
		if (objects[i].transform.position.x > 0) // if object is moved to the right, 
		{
			if (allowColorSwitcher == false && objects[i].name == "ColorSwitcher") // prevents spawning a color switcher twice in a row with a bool that is toggled 
			{
				return;
			}
			else
			{
				allowColorSwitcher = true;
			}
			objects[i].transform.position = spawnPos;
			Debug.Log("object spawned " + objects[i]);
		}
	}

	public void ObstacleSpawn(float movement)
	{
		obstacleSpawner += movement;
		if (obstacleSpawner < obstacleSpawnFrequency)
			return;
		obstacleSpawner = 0;
		int i = Random.Range(0, obstacles.Length);
		if (obstacles[i].transform.position.x > 0)
		{
			obstacles[i].transform.position = spawnPos;
			obstacles[i].GetComponent<ObstacleLogic>().OnSpawn();
			Debug.Log("obstacle spawned " + obstacles[i]);
		}
	}

	void ObjectCheckForDespawn()
	{
		for (int i = 0; i < objects.Length; i++)
		{
			if (objects[i].transform.position.y < -20f)
			{
				objects[i].transform.position = despawnPos;
				Debug.Log("object despawned " + objects[i]);
			}
		}
		for (int i = 0; i < obstacles.Length; i++)
		{
			if (obstacles[i].transform.position.y < -20f)
			{
				obstacles[i].transform.position = despawnPos;
				Debug.Log("object despawned " + obstacles[i]);
			}
		}
	}

	public void ObjectDespawn(GameObject obj)
	{
		obj.transform.position = despawnPos;
		Debug.Log("object despawned " + obj);
	}

	public void ObjectsReset()
	{
		for (int i = 0; i < objects.Length; i++)
		{
			objects[i].transform.position = despawnPos;
		}
		for (int i = 0; i < obstacles.Length; i++)
		{
			obstacles[i].transform.position = despawnPos;
		}
	}
}
