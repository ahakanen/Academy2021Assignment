using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PlayerColor
{
	blue,
	red,
	green,
	yellow,
}

public class GameController : MonoBehaviour
{
	[Header("Physics")]
	public float gravity = 0.5f;
	public float jumpHeight = 150f;
	float jumpVelocity;
	Vector2 velocity = Vector2.zero;
	Transform player;
	[Header("Starting Color")]
	public PlayerColor playerColor = PlayerColor.blue;
	int score = 0;
	int bestScore = 0;
	int scoreMultiplier = 1;
	[Header("Score")]
	public int maxScoreMultiplier = 16;
	public float scoreMultiplierTimerDuration;
	float scoreMultiplierTimer;
	[Header("Dependencies")]
	public Transform objectControllerTransform;
	public ObjectController objectController;
	public TextMeshPro scoreText;
	public TextMeshPro scoreMultiplierText;
	public TextMeshPro bestScoreText;
	bool isDead = false;
	public GameObject playerPickupEffect;
	public GameObject playerDeathEffect;
	public SpriteRenderer spriteRenderer;
	public AudioSource deathSound;
	public AudioSource pickupSoundStar;
	public AudioSource pickupSoundColorSwapper;
	Vector2 despawnPosPlayer = new Vector2(-20f, 0);

	// Start is called before the first frame update
	void Start()
	{
		player = GetComponent<Transform>();
		// derive the initial jump velocity from the desired jump height
		jumpVelocity = Mathf.Sqrt(2 * jumpHeight * gravity);
		// init player color
		UpdatePlayerColor();
	}

	// update physics with fixed update to make sure gravity is consistent through screen sizes and devices
    void FixedUpdate()
	{
		// apply gravity to player by lowering gravity's worth of velocity
		if (isDead == false)
		{
			velocity.y -= gravity;
		}
		// move player based on current velocity
		Vector2 tmp = player.position;
		tmp.y += velocity.y * Time.fixedDeltaTime;
		player.position = tmp;
		if (player.position.y > 0) // if above middle of screen
		{
			// move background down
			tmp = objectControllerTransform.position;
			tmp.y -= player.position.y;
			objectControllerTransform.position = tmp;
			// send distance traveled to check if should spawn objects or obstacles
			objectController.ObjectSpawn(player.position.y);
			objectController.ObstacleSpawn(player.position.y);
			// move player down
			tmp = player.position;
			tmp.y = 0;
			player.position = tmp;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0)) // on left mouse button
		{
			if (isDead == false) // if not dead, jump up
			{
				velocity.y = jumpVelocity;
			}
			else // if dead, reset game
			{
				ResetGame();
			}
		}
		UpdateScoreMultiplier();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Collision with " + other);
		if (other.tag == "Obstacle")
		{
			if (other.name == "BottomKillBox") // kill player on collision regardless of color if falling off the screen
			{
				PlayerDeath();
			}
			else // if obstacle color doesn't match with player color, kill player on collision
			{
				SpriteRenderer obstacleRenderer = other.GetComponent<SpriteRenderer>();
				if (obstacleRenderer && obstacleRenderer.color != spriteRenderer.color)
				{
					PlayerDeath();
				}
			}
		}
		else if (other.tag == "Pickup")
		{
			// update score and despawn pickup
			score += scoreMultiplier;
			scoreText.text = "Score: " + score.ToString();
			scoreMultiplier += 1;
			scoreMultiplierTimer = 0;
			if (scoreMultiplier > maxScoreMultiplier) // limit score multiplier to maximum score multiplier
			{
				scoreMultiplier = maxScoreMultiplier;
			}
			scoreMultiplierText.text = "x" + scoreMultiplier.ToString();
			Instantiate(playerPickupEffect, transform.position, transform.rotation);
			objectController.ObjectDespawn(other.gameObject);
			if (other.name == "ColorSwitcher")
			{
				int i = Random.Range(0, 3);
				while (i == (int)playerColor) // get a new random color if color is what it was before until a new color is found
				{
					i = Random.Range(0, 3);
				}
				// change player color
				playerColor = (PlayerColor)i;
				UpdatePlayerColor();
				pickupSoundColorSwapper.Play(0);
			}
			else // if object wasn't a color switcher, it must be a star
			{
				pickupSoundStar.Play(0);
			}
		}
	}

	// if player doesn't reset the timer by gaining score, he will lose his multiplier
	void UpdateScoreMultiplier()
	{
		scoreMultiplierTimer += Time.deltaTime;
		if (scoreMultiplierTimer > scoreMultiplierTimerDuration)
		{
			scoreMultiplier = 1;
			scoreMultiplierTimer = 0;
			scoreMultiplierText.text = "x" + scoreMultiplier.ToString();
		}
	}

	void UpdatePlayerColor()
	{
		if (playerColor == PlayerColor.blue)
		{
			spriteRenderer.color = Color.blue;
			scoreText.color = Color.blue;
			scoreMultiplierText.color = Color.blue;
			bestScoreText.color = Color.blue;
		}
		else if (playerColor == PlayerColor.green)
		{
			spriteRenderer.color = Color.green;
			scoreText.color = Color.green;
			scoreMultiplierText.color = Color.green;
			bestScoreText.color = Color.green;
		}
		else if (playerColor == PlayerColor.red)
		{
			spriteRenderer.color = Color.red;
			scoreText.color = Color.red;
			scoreMultiplierText.color = Color.red;
			bestScoreText.color = Color.red;
		}
		else if (playerColor == PlayerColor.yellow)
		{
			spriteRenderer.color = Color.yellow;
			scoreText.color = Color.yellow;
			scoreMultiplierText.color = Color.yellow;
			bestScoreText.color = Color.yellow;
		}
	}

	void PlayerDeath()
	{
		deathSound.Play(0);
		isDead = true;
		velocity = Vector2.zero;
		Instantiate(playerDeathEffect, transform.position, transform.rotation);
		player.position = despawnPosPlayer;
		if (score > bestScore) // save score if it was the best so far
		{
			bestScore = score;
			bestScoreText.text = "Best: " + bestScore.ToString();
		}
	}

	void ResetGame()
	{
		velocity = Vector2.zero;
		score = 0;
		scoreText.text = "Score: " + score.ToString();
		scoreMultiplier = 1;
		scoreMultiplierText.text = "x" + scoreMultiplier.ToString();
		isDead = false;
		objectController.ObjectsReset();
		player.position = Vector2.zero;
	}
}
