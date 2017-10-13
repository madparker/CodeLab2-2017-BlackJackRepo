using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InputManager
{
    static private KeyCode[] validKeyCodes;

    public InputManager()
    {
        if (validKeyCodes != null) return;
        validKeyCodes = (KeyCode[])System.Enum.GetValues(typeof(KeyCode));
    }

    private KeyCode FetchKey()
    {
        for (int i = 0; i < validKeyCodes.Length; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                return (KeyCode)i;
            }
        }

        return KeyCode.None;
    }

    public void Update()
    {
        Services.EventManager.Fire(new KeyPressedEvent(FetchKey()));
    }
}
