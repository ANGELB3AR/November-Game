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
    int currentIndex;

    const float targetCameraWeight = 1f;
    const float targetCameraRadius = 2f;

    public Target CurrentTarget { get; private set; }

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        if (targets.Contains(target)) { return; }

        targets.Add(target);
        target.OnDisabled += RemoveTarget;
        SortTargets();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        RemoveTarget(target);
        SortTargets();
    }

    void RemoveTarget(Target target)
    {
        if (CurrentTarget == target)
        {
            targetGroup.RemoveMember(CurrentTarget.transform);
        }

        targets.Remove(target);
        target.OnDisabled -= RemoveTarget;

        if (targets.Count == 0)
        {
            Cancel();
        }
    }

    public bool SelectClosestTarget()
    {
        if (targets.Count == 0) { return false; }

        if (CurrentTarget != null)
        {
            targetGroup.RemoveMember(CurrentTarget.transform);
        }

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
        FindCurrentIndex(CurrentTarget);
        return true;
    }

    void SortTargets()
    {
        sortedTargets = targets.OrderBy(gameObject =>
        {
            Vector3 position = mainCamera.WorldToViewportPoint(gameObject.transform.position);
            return position.x;
        }).ToList();
    }

    void FindCurrentIndex(Target currentTarget)
    {
        currentIndex = sortedTargets.IndexOf(currentTarget);
    }

    public void CycleTargetLeft()
    {
        if (currentIndex == 0) { return; }
        targetGroup.RemoveMember(CurrentTarget.transform);
        currentIndex--;
        CurrentTarget = sortedTargets[currentIndex];
        targetGroup.AddMember(CurrentTarget.transform, targetCameraWeight, targetCameraRadius);
    }

    public void CycleTargetRight()
    {
        if (currentIndex == sortedTargets.Count - 1) { return; }
        targetGroup.RemoveMember(CurrentTarget.transform);
        currentIndex++;
        CurrentTarget = sortedTargets[currentIndex];
        targetGroup.AddMember(CurrentTarget.transform, targetCameraWeight, targetCameraRadius);
    }

    public void Cancel()
    {
        if (CurrentTarget != null)
        {
            targetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
            sortedTargets.Clear();
        }
    }
}
