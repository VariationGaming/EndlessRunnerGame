using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

	public GameObject thePlatform;
	public Transform generationPoint;

	private int distanceBetween;
	private bool occupied;

	public int minDistance;
	public int maxDistance;

	private int platformWidth;
	private int platformSelector;
	private int[] platformWidths;

	public ObjectPooler[] theObjectPools;

	private int minHeight;
	private int maxHeight;
	public Transform maxHeightPoint;
	public int maxHeightChange;
	private int heightChange;

	private CoinGenerator theCoinGenerator;
	private EnemyGenerator theEnemyGenerator;

	public float randomCoinThreshold;
	public float randomEnemyThreshold;

	void Start () {
		platformWidths = new int[theObjectPools.Length];
		for (int i = 0; i < theObjectPools.Length; i++) {
			platformWidths [i] = (int)theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
		}
		minHeight = (int)transform.position.y;
		maxHeight = (int)maxHeightPoint.position.y;
		theCoinGenerator = FindObjectOfType<CoinGenerator> ();
		theEnemyGenerator = FindObjectOfType<EnemyGenerator> ();
		occupied = false;
	}

	void Update () {

		distanceBetween = Random.Range (minDistance, maxDistance);

		if (transform.position.x < generationPoint.position.x){
			
			platformSelector = Random.Range (0, theObjectPools.Length);
			heightChange = (int)transform.position.y + Random.Range (-maxHeightChange, maxHeightChange);

			if (heightChange > maxHeight) {
				heightChange = maxHeight;
			}
			else if (heightChange < minHeight) {
				heightChange = minHeight;
			}

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector]/2) + distanceBetween,
			heightChange, transform.position.z);
	
			GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true);

			if (Random.Range (0f, 100f) < randomCoinThreshold) {
				theCoinGenerator.SpawnCoins (new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z));
			}
			else if(Random.Range(0f,100f) < randomEnemyThreshold) {
				theEnemyGenerator.SpawnEnemies(new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z));	
			}

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector]/2),
			transform.position.y, transform.position.z);
		}
	}
}
