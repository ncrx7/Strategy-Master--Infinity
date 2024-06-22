using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField] public LineRenderer _lineRenderer;

    [SerializeField] 
    [Range(3, 100)]
    private int _lineSegmentCount = 100;

    [SerializeField] 
    private float timeOfTheFlight = 5;

    public static DrawTrajectory Instance;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(this);
        
    }

    public void SetLineRendererObject(GameObject childObject)
    {
        _lineRenderer = childObject.GetComponent<LineRenderer>();
    }

    public void ShowTrajectoryLine(Vector3 StartPoint, Vector3 StartVelocity) //placing the lines we found
    {
        float timeStep = timeOfTheFlight / _lineSegmentCount;
        Vector3[] LineRendererPoints = CalculateTrajectoryLine(StartPoint, StartVelocity, timeStep);

        _lineRenderer.positionCount = _lineSegmentCount;
        _lineRenderer.SetPositions(LineRendererPoints);
    }

    private Vector3[] CalculateTrajectoryLine(Vector3 StartPoint, Vector3 StartVelocity, float TimeStep) //calculating line renderer lines
    {
        Vector3[] LineRendererPoints = new Vector3[_lineSegmentCount];
        LineRendererPoints[0] = StartPoint;

        for(int i = 1; i < _lineSegmentCount; i++)
        {
            float timeOffset = TimeStep * i;

            Vector3 progressBeforeGravity = StartVelocity * timeOffset;
            Vector3 gravityOffset = Vector3.up * - 0.5f * Physics.gravity.y * timeOffset * timeOffset;
            Vector3 newPosition = StartPoint + progressBeforeGravity - gravityOffset;
            LineRendererPoints[i] = newPosition;
        }

        return LineRendererPoints;
    }

}
