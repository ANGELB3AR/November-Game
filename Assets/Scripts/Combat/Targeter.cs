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
        targetGroup.AddMember(CurrentTarget.transform, 1, 2);
    }

    public void CycleTargetRight()
    {
        if (currentIndex == sortedTargets.Count - 1) { return; }
        targetGroup.RemoveMember(CurrentTarget.transform);
        currentIndex++;
        CurrentTarget = sortedTargets[currentIndex];
        targetGroup.AddMember(CurrentTarget.transform, 1, 2);
    }
}
