using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWhiteboard : MonoBehaviour
{
    public GameObject whiteboardPrefab;

    // Keeps track of current slide, so that a new whiteboard can spawn at the next


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWhiteboard());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnWhiteboard()
    {
        yield return new WaitForSeconds(3f);
        GameObject newWhiteboard = Instantiate(whiteboardPrefab, new Vector3(1.747f, 1, 1), Quaternion.Euler(0, 45, 0));
        
    }
}
