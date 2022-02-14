using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    public float pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            print(hit.collider.name);
            hit.collider.GetComponent<Transform>();
            hit.transform.localPosition = new Vector3(hit.transform.localPosition.x, pos, hit.transform.localPosition.z);
        }
    }
}
