using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public EnemySpawner spawner;

	public string enemyName = "";

	public Vector3 spawnerPosition;
	public Vector3 tempVector;
	public bool spawned = false;

	public float attackRange;
	public int attackDamage;
	public int maxHealth;
	public int maxMoves;
	public int moves;

	// Use this for initialization
	void Start () {

		tempVector = transform.position;
		tempVector [1] = tempVector [1] - 0.5f;
		spawnerPosition = tempVector;
		print ("Started spawner");
	}
	
	// Update is called once per frame
	void Update () {
		if (spawned == false) {
			print ("Sspawnend == false");
			GameManager.instance.spawnEnemy(spawnerPosition, attackRange, attackDamage, maxHealth, maxMoves, moves, enemyName);
			spawned = true;
		}

		if (spawned == true) {
			Destroy(this.gameObject);
		}
	}
}
