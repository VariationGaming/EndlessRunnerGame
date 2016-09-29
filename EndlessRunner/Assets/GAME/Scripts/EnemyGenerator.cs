using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {

	public float moveSpeed;

	public ObjectPooler EnemyPool;

	public void SpawnEnemies(Vector3 startPos){
	
		GameObject enemyGround = EnemyPool.GetPooledObject ();
		enemyGround.transform.position = startPos;
		enemyGround.SetActive (true);
	}
		

}
