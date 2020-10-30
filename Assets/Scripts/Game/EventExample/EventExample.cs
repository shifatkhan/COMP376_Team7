using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventExample : MonoBehaviour
{
    public GameEvent buttonClickedEvent;

    public void ButtonClicked()
    {
        buttonClickedEvent.Raise();
    }
}
