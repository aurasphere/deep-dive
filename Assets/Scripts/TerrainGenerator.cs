using UnityEngine;
using System.Collections.Generic;
using AssemblyCSharp;

public class TerrainGenerator : MonoBehaviour {

	public int poolSize = 1000;

	private int tunnelPendence = 0;

	public int tunnelPendenceCoefficiency;

	private GameObject[] pool;

	public GameObject ring;

	private Vector3 currentPosition;

	private int poolPointer = 0;

	public int segmentDimension;

	private SegmentType currentSegmentType;

	private Commands playerCommands;

	public GameObject player;

	public Material test;

	// Initializes the terrain generation creating a tunnel composed by ringBuffer rings and adding them to the pool
	void Start () {
		// Initializes fields.
		pool = new GameObject[poolSize];
		currentPosition = Vector3.zero;
		currentSegmentType = new SegmentType ();
		playerCommands = (Commands) player.GetComponent (typeof(Commands));

		// Creates poolSize rings (along the Z-axis) and instantiates them and adds them to the pool.
		while(currentPosition.z < poolSize){
			GameObject ringObj = GameObject.Instantiate(ring) as GameObject;          
			ringObj.transform.position = currentPosition;
			ringObj.GetComponentInChildren<Transform>().Find("default").GetComponent<Renderer>().material.SetColor("_Color", new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f)));
			// Adds the ring to the pool.
			pool[(int)currentPosition.z] = ringObj;
			updateCurrentPosition ();
		}
	}

	// Each frame, I check the player position and I decide wheter to continue generating the tunnel or not according to his position.
	void Update () {
		if (pool [poolPointer] != null) {
			// If the player is ringDespawnDistance blocks away from the last ring, I put the last ring in the first position.
	//		if (player.transform.position.z > pool [poolPointer].transform.position.z + poolSize/50) {
			if (player.transform.position.z > pool [poolPointer].transform.position.z + 3) {				
				pool [poolPointer].transform.position = currentPosition;
				updateCurrentPosition ();
				poolPointer++;
				// If I reach the last element of the pool I start over.
				if (poolPointer == poolSize)
					poolPointer = 0;
		
				// Calculates a random segment and increments the player speed by one.
				if (currentPosition.z % segmentDimension == 0) {
					currentSegmentType.randomize ();
					// player.GetComponent<ConstantForce>().force.Scale(new Vector3(0f, 0f,5f));
					playerCommands.forwardSpeed += 1;
				}
			}	
		}
	}

	// Updates Y position of the current ring.
	void recalculateCurrentY(){
		if (currentSegmentType.slopeType == 1) {
			// Generates a slope.
			currentPosition.y -= Random.Range (0, 2);
		}else {
		// Generates a straight-like segment.
		// Every ring there's tunnelPendence 100/tunnelPendence probability the tunnel goes down a block on the Y-axis.
			if (Random.Range (0, tunnelPendence) == 0) {
				currentPosition.y--;
				tunnelPendence = Random.Range (0, tunnelPendenceCoefficiency);
			}
		}
	}

	// Updates X position of the current ring.
	void recalculateCurrentX(){
		// Checks the current segment and generates it according to the type.
		switch (currentSegmentType.bentType) {
		default:
			// Generates a straight-like segment.
			int random = Random.Range (0, 3);
			switch (random) {
			case 0:
				// Goes left one block.
				currentPosition.x++;
				break;
			case 1:
				// Goes right one block
				currentPosition.x--;
				break;
			case 2:
				// Goes straight one block.
				break;
			}
			break;
		case 1:
			// Generates a left bend.
			currentPosition.x += Random.Range (0, 2);
			break;
		case 2:
			// Generates a right bend.
			currentPosition.x -= Random.Range (0, 2);
			break;
		}
	}

	// Updates the position of the current ring.
	void updateCurrentPosition(){
		recalculateCurrentX ();
		recalculateCurrentY ();
		currentPosition.z++;
	}
}
