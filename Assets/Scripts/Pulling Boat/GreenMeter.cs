using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMeter : MonoBehaviour
{
    [SerializeField] private float UpperBoundX;
    [SerializeField] private float LowerBoundX;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector2(Random.Range(LowerBoundX, UpperBoundX), 0);
    }

    public void Shuffle()
    {
        transform.localPosition = new Vector2(Random.Range(LowerBoundX, UpperBoundX), 0);
    }
}
