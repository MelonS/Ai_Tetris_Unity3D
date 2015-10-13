using System;
using UnityEngine;

public interface IEnemyView 
{
	event EventHandler<EnemyClickedEventArgs> OnClicked;
	Vector3 Position { set; }
	int Index { get; set; }
}
