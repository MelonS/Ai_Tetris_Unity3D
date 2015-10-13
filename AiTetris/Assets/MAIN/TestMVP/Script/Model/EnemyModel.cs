using System;
using UnityEngine;

public class EnemyModel : IEnemyModel 
{
	private Vector3 _position;
	public event EventHandler<EnemyPositionChangedEventArgs> OnPositionChanged = (sender, e) => {};

	public Vector3 Position
	{
		get { return _position; }
		set {
			if (_position != value) {
				_position = value;

				var eventArgs = new EnemyPositionChangedEventArgs();
				OnPositionChanged(this, eventArgs);
			}
		}
	}

	private int _index;
	public int Index
	{
		get { return _index; }
		set { _index = value; }
	}
}
