using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassGen : MonoBehaviour
{
    public GameObject theTarget;
    private int xPos;
    private int yPos;
    private int zPos;
    public int objectToGen;
    private int objectNumber;
    private GameObject clone;

    void Start()
    {
        StartCoroutine(GenObject());
    }

    IEnumerator GenObject()
    {
        while(objectNumber < 10)
        {
            objectToGen = Random.Range(1, 2);
            xPos = Random.Range(-10, 11);
            zPos = Random.Range(-10, 11);
            yPos = Random.Range(1, 11);

            if (objectToGen == 1)
            {
                clone = Instantiate(theTarget, new Vector3(xPos, yPos, zPos), Quaternion.identity);
                clone.AddComponent<RotateTarget>();
            }
            yield return new WaitForSeconds(0.5f);
            objectNumber += 1;
            Destroy(clone, 10.0f);
            objectNumber -= 1;
        }
    }
}



