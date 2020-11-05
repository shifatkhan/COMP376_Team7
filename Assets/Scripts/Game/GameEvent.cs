using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://unity.com/how-to/architect-game-code-scriptable-objects#code-example-gameeventlistener
/// </summary>
[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
	private List<GameEventListener> listeners =
		new List<GameEventListener>();

	public void Raise()
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised();
	}

	public void RegisterListener(GameEventListener listener)
	{ listeners.Add(listener); }

	/// <summary>
	/// Unregister a listener. Used for when a GameObject is destroyed, disabled, etc.
	/// </summary>
	/// <param name="listener"></param>
	public void UnregisterListener(GameEventListener listener)
	{ listeners.Remove(listener); }
}
