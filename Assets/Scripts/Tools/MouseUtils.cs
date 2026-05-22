using UnityEngine;

public static class MouseUtils
{
	private static Camera camera = Camera.main;
	
	public static Vector3 GetMousePositionInWorldSpace(float _zValue = 0f)
	{
		Plane _dragPlane = new(camera.transform.forward, new Vector3(0, 0, _zValue));
		Ray _ray = camera.ScreenPointToRay(Input.mousePosition);
		return _dragPlane.Raycast(_ray, out float _distance) ? _ray.GetPoint(_distance) : Vector3.zero;
	}
}
