using System;
using UnityEngine;

namespace Tetris
{
	public class PieceView : MonoBehaviour, IPieceView 
	{
		public event EventHandler<PieceClickedEventArgs> OnClicked = (sender, e) => {};
		public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
		
		private int _id;
		public int ID { get { return _id; } set { _id = value; } }
		
		void Update () 
		{
			if (Input.GetMouseButtonDown(0)) {
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Debug.DrawRay (ray.origin, ray.direction * 200, Color.yellow);
				
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit) && hit.transform == transform) {
					var eventArgs = new PieceClickedEventArgs();
					OnClicked(this, eventArgs);
				}
			}
		}
	}
}
