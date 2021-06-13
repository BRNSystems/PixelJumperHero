using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlockLighter : MonoBehaviour
{
    float distance;
    RaycastHit2D[] hits;
    public Blinky blinky;
    public Transform lighty;
    public int i = 0;
    public Vector3 lightpos;
    public GameObject[] allChildren;
    // Start is called before the first frame update
    void Start()
    {
        allChildren = new GameObject[transform.childCount];
        //Find all child obj and store to that array
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }
        foreach (GameObject child1 in allChildren)
        {
            foreach (GameObject child2 in allChildren)
            {
                Debug.DrawLine(child1.transform.position, child2.transform.position, Color.blue, 3f);
            }
        }
    }

    // Update is called once per frame
   public void LightThem()
    {
        foreach (GameObject child in allChildren)
        {
            lightpos = new Vector3(lighty.transform.position.x, lighty.transform.position.y, lighty.transform.position.z);
            Debug.DrawLine(lightpos, child.transform.position, Color.red, 5f);
            hits = Physics2D.RaycastAll(lightpos, child.transform.position, 2f);
            foreach (RaycastHit2D hit in hits)
            {
                distance = hit.distance;
                if (hit.transform.tag.Equals("Level"))
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
