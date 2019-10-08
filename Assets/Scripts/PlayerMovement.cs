using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	public bool EditMode = false;
	int Health = 5;
	float EnginePower = 1;
	Rigidbody2D rb;
	int NumParts = 0;
	int Energy = 0;
	float FireRate = 3;
	float Cooldown;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		//Debug.Log(float.Parse(Util.Parse("Enemy_ShootAtPlayer")));
		UpdateShip();
		gameObject.SendMessage("DestroyGhosts");
	}

	// Update is called once per frame
	void Update()
	{
		if(!EditMode) {
			Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 lookAt = mouseScreenPosition;
			float AngleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);
			float AngleDeg = (180 / Mathf.PI) * (AngleRad + Mathf.PI * 3/2);
			Quaternion Target = Quaternion.Euler(0, 0, AngleDeg);

			transform.rotation = Quaternion.Lerp(transform.rotation, Target, Time.deltaTime * (EnginePower + 2) / ((float)NumParts/8 + 1));

            rb.velocity = transform.up * Input.GetAxis("Vertical") * (EnginePower + 1) / ((float)NumParts/8 + 1);

		}

		Debug.DrawLine(transform.position, transform.position + transform.up);

		if(Input.GetKeyUp("q")) {
			EditMode = !EditMode;
			if(EditMode) {
				gameObject.SendMessage("CreateAnchors");
			} else {
				gameObject.SendMessage("DestroyGhosts");
			}
			foreach (Transform Child in transform) {
				if(EditMode) {
					Child.SendMessage("CreateAnchors", null, SendMessageOptions.DontRequireReceiver);
				} else {
					Child.SendMessage("DestroyGhosts", null, SendMessageOptions.DontRequireReceiver);
				}
			}
		}

		if(Health <= 0) {
			SceneManager.LoadSceneAsync("GameOver");
		}

		if((Input.GetKey("space") || Input.GetMouseButton(0)) && !EditMode && Time.time > Cooldown) {
			Cooldown = Time.time + FireRate;
			Instantiate(Resources.Load("Sounds/Shoot"));
			foreach (Transform Child in transform) {
				Child.SendMessage("Fire", null, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	void UpdateShip() {
		NumParts = 0;
		EnginePower = 0;
		Energy = 0;
		int Trophies = 0;
		gameObject.SendMessage("DestroyGhosts");
		if(EditMode)
		gameObject.SendMessage("CreateAnchors");
		gameObject.SendMessage("ChangeArt");
		foreach (Transform Child in transform) {
			if(Child.tag != "Ghosts"){
				NumParts++;
			}
			Engine engine = Child.GetComponent<Engine>();
			if(engine != null) {
				EnginePower += engine.EnginePower;
			}
			PowerCell powerCell = Child.GetComponent<PowerCell>();
			if(powerCell != null) {
				Energy++;
			}
			Trophy trophy = Child.GetComponent<Trophy>();
			if(trophy != null) {
				Trophies++;
			}
			Child.SendMessage("DestroyGhosts", null, SendMessageOptions.DontRequireReceiver);
			if(EditMode)
			Child.SendMessage("CreateAnchors", null, SendMessageOptions.DontRequireReceiver);
			Child.SendMessage("ChangeArt", null, SendMessageOptions.DontRequireReceiver);
		}
		FireRate = (float)(3.0f / (Energy + 1));
		GameObject Cam = GameObject.Find("Main Camera");
		if(NumParts > 3) {
			Cam.SendMessage("ChangeMusic", new int[] {1, 100});
		} else {
			Cam.SendMessage("ChangeMusic", new int[] {1, 0});
		}
		if(NumParts > 6) {
			Cam.SendMessage("ChangeMusic", new int[] {2, 100});
		} else {
			Cam.SendMessage("ChangeMusic", new int[] {2, 0});
		}

		if(Trophies > 0) {
			Cam.SendMessage("ChangeMusic", new int[] {4, 100});
		}
		if(Trophies > 1) {
			Cam.SendMessage("ChangeMusic", new int[] {5, 100});
		}
		if(Trophies == 3) {
			SceneManager.LoadSceneAsync("YouWin");
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.layer == 10) {
			Health--;
			Destroy(col.gameObject);
			Instantiate(Resources.Load("Sounds/Hurt"));
		}
	}
}
