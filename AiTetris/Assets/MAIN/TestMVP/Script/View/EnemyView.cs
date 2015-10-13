using System;
using UnityEngine;

public class EnemyView : MonoBehaviour, IEnemyView 
{
	public event EventHandler<EnemyClickedEventArgs> OnClicked = (sender, e) => {};
	public Vector3 Position { set { transform.position = value; } }

	private int _index;
	public int Index { get { return _index; } set { _index = value; } }

	void Update () 
	{
		if (Input.GetMouseButtonDown(0)) {
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay (ray.origin, ray.direction * 200, Color.yellow);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit) && hit.transform == transform) {
				var eventArgs = new EnemyClickedEventArgs();
				OnClicked(this, eventArgs);
			}
		}
	}
}
