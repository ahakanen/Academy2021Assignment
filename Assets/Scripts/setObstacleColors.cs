using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setObstacleColors : MonoBehaviour
{
	public SpriteRenderer blue;
	public SpriteRenderer red;
	public SpriteRenderer green;
	public SpriteRenderer yellow;
    // Start is called before the first frame update
    void Start()
    {
		blue.color = Color.blue;
		red.color = Color.red;
		green.color = Color.green;
		yellow.color = Color.yellow;
    }
}
