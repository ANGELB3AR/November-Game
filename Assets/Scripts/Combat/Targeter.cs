using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] CinemachineTargetGroup targetGroup;

    List<Target> targets = new List<Target>();
    List<Target> sortedTargets;
    Camera mainCamera;

    public Target CurrentTarget { get; private set; }

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        RemoveTarget(target);
    }

    void RemoveTarget(Target target)
    {
        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }

    public bool SelectCenterTarget()
    {
        if (targets.Count == 0) { return false; }

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach (Target target in targets)
        {
            Vector2 viewPosition = mainCamera.WorldToViewportPoint(target.transform.position);

            if (!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }

            Vector2 toCenter = viewPosition - new Vector2(0.5f, 0.5f);
            if (toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }

        if (closestTarget == null) { return false; }

        CurrentTarget = closestTarget;
        targetGroup.AddMember(CurrentTarget.transform, 1, 2);
        return true;
    }

    public void SortTargets()
    {
        sortedTargets = targets.OrderBy(gameObject =>
        {
            Vector3 targetDirection = gameObject.transform.position - Camera.main.transform.position;
            var cameraForward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
            var targetDir = new Vector2(targetDirection.x, targetDirection.z);
            float angle = Vector2.Angle(cameraForward, targetDir);
            return angle;
        }).ToList();
    }
}
