using UnityEngine;

public class TestColourAndPosition : MonoBehaviour
{
	void Start ()
	{
		for (int i = 0; i < 10; i++)
		{
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);   
			cube.transform.position = new Vector3(i * 2.0f, 0, 0);
			cube.GetComponent<Renderer>().material.color =  new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		}
	}

	void Update ()
	{
	}
}