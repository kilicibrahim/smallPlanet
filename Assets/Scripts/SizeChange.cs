using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChange : MonoBehaviour
{
    public Vector3 sizeChange = new Vector3 (0.001f, 0.001f, 0.001f);
    public float size = 3;
    public float sizeChanger = 0.001f;

    private void FixedUpdate() {
    transform.localScale  += sizeChange;
    size += sizeChanger;
    }
}
