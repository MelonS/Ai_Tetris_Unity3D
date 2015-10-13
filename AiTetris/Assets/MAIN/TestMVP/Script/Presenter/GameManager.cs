using UnityEngine;
using System.Collections;

public class GameManager : MELONS.GameObjectSingleton<GameManager> {

	private IEnemyModel _model;
	private IEnemyView _view;

	private GameManager() 
	{
		name = "GameManager";
	}

	public void Init()
	{
		var modelFactory = new EnemyModelFactory();
		_model = modelFactory.Model;
		
		_model.Position = new Vector3(0, 0, 0);
		
		var viewFactory = new EnemyViewFactory();
		_view = viewFactory.View;

		AddEventHandler();
	}

	private void AddEventHandler() 
	{
		_view.OnClicked += HandleClicked;
		_model.OnPositionChanged += HandlePositionChanged;
		
		SyncPosition();
	}
	
	private void HandleClicked(object sender, EnemyClickedEventArgs e)
	{
		_model.Position += new Vector3(1, 0, 0); // click => x + 1
	}
	
	private void HandlePositionChanged(object sender, EnemyPositionChangedEventArgs e)
	{
		SyncPosition();
	}
	
	private void SyncPosition()
	{
		_view.Position = _model.Position;
	}
	
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
}
