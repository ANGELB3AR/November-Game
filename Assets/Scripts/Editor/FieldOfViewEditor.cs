using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView FOV = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(FOV.transform.position, Vector3.up, Vector3.forward, 360, FOV.GetRadius());

        Vector3 viewAngle01 = DirectionFromAngle(FOV.transform.eulerAngles.y, -FOV.GetAngle() / 2);
        Vector3 viewAngle02 = DirectionFromAngle(FOV.transform.eulerAngles.y, FOV.GetAngle() / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(FOV.transform.position, FOV.transform.position + viewAngle01 * FOV.GetRadius());
        Handles.DrawLine(FOV.transform.position, FOV.transform.position + viewAngle02 * FOV.GetRadius());

        if (FOV.CanSeeTarget())
        {
            Handles.color = Color.green;
            Handles.DrawLine(FOV.transform.position, FOV.GetPlayer().transform.position);
        }
    }

    Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
