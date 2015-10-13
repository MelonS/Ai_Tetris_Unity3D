using System;
using UnityEngine;

public interface IEnemyModel 
{
	event EventHandler<EnemyPositionChangedEventArgs> OnPositionChanged;
	Vector3 Position { get; set; }
	int Index { get; set; }
}
