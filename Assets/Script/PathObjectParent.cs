using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObjectParent : MonoBehaviour
{

    public PathPoint[] commonPathPoint;
    public PathPoint[] BluePathPoint;
    public PathPoint[] BlackPathPoint;
    public PathPoint[] OrangePathPoint;
    public PathPoint[] YellowPathPoint;
    public PathPoint[] BasePoints;

    [Header("Scale and Position Difference")]

    public float[] scale;
    public float[] positionDiff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
