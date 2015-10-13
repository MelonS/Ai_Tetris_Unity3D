using UnityEngine;

public class EnemyController : IEnemyController 
{
	private readonly IEnemyModel _model;
	private readonly IEnemyView _view;

	public EnemyController(IEnemyModel model, IEnemyView view)
	{
		_model = model;
		_view = view;

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
}
