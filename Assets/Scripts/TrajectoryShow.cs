using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryShow : MonoBehaviour
{
    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // функция отрисовки "лазера" - наведения
    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[15];
        line.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;

            points[i] = origin + speed * time;
        }

        line.SetPositions(points);
    }
}
