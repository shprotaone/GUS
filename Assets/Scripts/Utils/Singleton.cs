using UnityEngine;

/// <summary>
/// Синглтоны считаются антипаттерном. Но в небольших проектах можно использовать.
/// </summary>
public class Singleton<T> : MonoBehaviour where T:Component
{
	private static T _instance;
	public static T Instance
	{
		get 
		{ 
			if(_instance == null)
			{
				Init();
			}
			return _instance; 
		}
		
		private set 
		{ 
			_instance = value; 
		}
	}
	
	private static void Init()
	{
		_instance = FindObjectOfType<T>();
		if(_instance == null)
		{
			Debug.LogError($"{typeof(T).ToString()} не существует в текущем контексте!");
		}
	}
}
