using UnityEngine;
using System.Collections.Generic;

public class GameManager : MELONS.GameObjectSingleton<GameManager> {

	private const int CUBE_COUNT = 10;
	private const int RAN = 15;
	
	private List<IEnemyModel> _modelList = new List<IEnemyModel>();
	private List<IEnemyView> _viewList = new List<IEnemyView>();

	private GameManager() 
	{
		name = "GameManager";
	}

	public void Init()
	{
		for (int i = 0; i < CUBE_COUNT; ++i) {
			var modelFactory = new EnemyModelFactory();
			var model = modelFactory.Model; 
			model.Position = new Vector3(Random.Range(-RAN,RAN), Random.Range(-RAN,RAN), 0);
			model.Index = i;
			_modelList.Add(model);

			var viewFactory = new EnemyViewFactory();
			var view = viewFactory.View;
			view.Index = i;
			_viewList.Add(view);
		}

		AddEventHandler();
	}

	private void AddEventHandler() 
	{
		for (int i = 0; i < CUBE_COUNT; ++i) {
			var model = _modelList[i];
			model.OnPositionChanged += HandlePositionChanged;
		}

		for (int i = 0; i < CUBE_COUNT; ++i) {
			var view = _viewList[i];
			view.OnClicked += HandleClicked;
		}
		
		SyncPosition();
	}
	
	private void HandlePositionChanged(object sender, EnemyPositionChangedEventArgs e)
	{
		SyncPosition();
	}
	
	private void HandleClicked(object sender, EnemyClickedEventArgs e)
	{
		IEnemyView view = sender as IEnemyView;
		if (view != null) {
			int index = view.Index;
			_modelList[index].Position += new Vector3(1, 0, 0); // click => x + 1
		}
	}
	
	private void SyncPosition()
	{
		for (int i = 0; i < CUBE_COUNT; ++i) {
			_viewList[i].Position = _modelList[i].Position;
		}
	}
}
