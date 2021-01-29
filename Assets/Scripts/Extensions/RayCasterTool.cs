using UnityEngine;
using UnityEngine.EventSystems;
public static class RayCasterTool
{
    /// <summary>
    /// Raycast from the position of the mouse to the world
    /// </summary>
    /// <param name="hit"></param>
    /// <returns> Returns whether it hit something </returns>
    public static bool DoRaycastFromMouse(out RaycastHit hit)
    {
        Vector3 mousePosition = Input.mousePosition + new Vector3(0, 0, Camera.main.nearClipPlane);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 cameraPerspective = mouseWorldPosition - Camera.main.transform.position;

        bool isHitting = DoRaycastFromPosition(mouseWorldPosition, cameraPerspective, out hit);

        if (EventSystem.current)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return false;
        }

        return isHitting;
    }

    public static bool DoRaycastFromPosition(Vector3 startPosition, Vector3 direction, out RaycastHit hit)
    {
        return Physics.Raycast(startPosition, direction, out hit, Mathf.Infinity);
    }
}
