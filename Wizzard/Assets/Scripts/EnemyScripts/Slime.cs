using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public List<Transform> transforms;
    public GameObject slimePrefab;

    private void OnDestroy()
    {
        // ToDo: maybe add knockback after spawning small slimes?
        Instantiate(slimePrefab, transform.position + new Vector3(0, 0.03f, 0), Quaternion.identity);
        Instantiate(slimePrefab, transform.position + new Vector3(-0.03f, 0, 0), Quaternion.identity);
        Instantiate(slimePrefab, transform.position + new Vector3(0, -0.03f, 0), Quaternion.identity);
    }
}
