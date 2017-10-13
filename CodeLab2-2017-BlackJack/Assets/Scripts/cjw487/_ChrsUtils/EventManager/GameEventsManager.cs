using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	GameEventsManager: The program pattern for Event Management							*/
/*																						*/
/*		Functions:																		*/
/*			public:																		*/
/*				void Register<T>(GameEvent.Handler handler) where T : GameEvent 		*/
/*				void Unregister<T>(GameEvent.Handler handler) where T : GameEvent 		*/
/*				void Fire(GameEvent e) 													*/
/*																						*/
/*			private:																	*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class GameEventsManager 
{

    public delegate void GameEventDelegate<T>(T e) where T : GameEvent; //  Delegate for GameEvents
    private delegate void GameEventDelegate(GameEvent e);

    static private GameEventsManager instance;          //  Instance of GameEventsManager
    static public GameEventsManager Instance 
    { 
        get 
        {
            if (instance == null) 
            {
                instance = new GameEventsManager();
            }
            return instance;
        }
    }
    
    //  Dictionary of all GameEvents
    private Dictionary<Type, GameEventDelegate> delegates = new Dictionary<Type, GameEventDelegate>();
    private Dictionary<System.Delegate, GameEventDelegate> delegateLookup = new Dictionary<System.Delegate, GameEventDelegate>();

    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Register<T>: Registers script for a GameEvent          								*/
    /*				T : a GameEvent															*/
    /*			param:																		*/
    /*				GameEvent.Handler handler - Handler for the GameEvent       			*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    public void Register<T>(GameEventDelegate<T> del) where T : GameEvent 
    {
        if (delegateLookup.ContainsKey(del))
        {
            return;
        }

        GameEventDelegate internalDelegate = (e) => del((T)e);
        delegateLookup[del] = internalDelegate;

        GameEventDelegate tempDel;
        if (delegates.TryGetValue(typeof(T), out tempDel))
        {
            delegates[typeof(T)] = tempDel + internalDelegate;
        }
        else
        {
            delegates[typeof(T)] = internalDelegate;
        }
    }
    
    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Unregister<T>: Unregisters script for a GameEvent          							*/
    /*				T : a GameEvent															*/
    /*			param:																		*/
    /*				GameEvent.Handler handler - Handler for the GameEvent       			*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    public void Unregister<T>(GameEventDelegate<T> del) where T : GameEvent 
    {
        GameEventDelegate internalDelegate;
        if (delegateLookup.TryGetValue(del, out internalDelegate))
        {
            GameEventDelegate tempDel;
            if (delegates.TryGetValue(typeof(T), out tempDel))
            {
                tempDel -= internalDelegate;
                if (tempDel == null)
                {
                    delegates.Remove(typeof(T));
                }
                else
                {
                    delegates[typeof(T)] = tempDel;
                }
            }
            delegateLookup.Remove(del);
        }
    }
    
    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Fire: Fires the event          								                        */
    /*			param:																		*/
    /*				GameEvent e - The current GameEvent                          			*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    public void Fire(GameEvent e) 
    {
        GameEventDelegate del;
        if (delegates.TryGetValue(e.GetType(), out del))
        {
            del.Invoke(e);
        }
    }
 }

