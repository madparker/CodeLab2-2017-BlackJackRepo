using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	GameEvent: Abstract class for Game Events for the GameEventsManager				    */
/*																						*/
/*--------------------------------------------------------------------------------------*/
public abstract class GameEvent 
{
    public delegate void Handler(GameEvent e);      //  Delegate for GameEvents
}


public class KeyPressedEvent : GameEvent
{
    public readonly KeyCode key;
    public KeyPressedEvent(KeyCode _key)
    {
        key = _key;
    }
}

public class Reset : GameEvent { }