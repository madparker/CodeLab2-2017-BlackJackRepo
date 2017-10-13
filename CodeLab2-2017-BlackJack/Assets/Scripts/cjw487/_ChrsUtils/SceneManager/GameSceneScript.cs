using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneScript : Scene<TransitionData>
{

    [SerializeField] private Switcheroo[] tokens;
    [SerializeField] private bool playerWon;

    private Switcheroo lockedToken;

    private AudioClip _clip;

    internal override void OnEnter(TransitionData data)
    {
        playerWon = false;
        Services.EventManager.Register<KeyPressedEvent>(OnKeyPressed);
        Services.EventManager.Register<CheckIfSolvedEvent>(CheckIfSolved);
        Services.EventManager.Register<ResetTokenLockEvent>(ResetTokenLock);
    }

    internal override void OnExit()
    {
        Services.EventManager.Unregister<KeyPressedEvent>(OnKeyPressed);
        Services.EventManager.Unregister<CheckIfSolvedEvent>(CheckIfSolved);
        Services.EventManager.Unregister<ResetTokenLockEvent>(ResetTokenLock);
    }

    private void OnKeyPressed(KeyPressedEvent e)
    {
        //  Token lock is handled here
        //  If nothing is locked then a token can be locked
        //  If another token is locked, I (the new token) cannot be locked
        //  If I am locked if the player presses my direction, I become unlcoked

        if (e.key == KeyCode.UpArrow && (lockedToken == tokens[1] || lockedToken == null))
        {
            ToggleTokenLock(tokens[1]);     
        }
        if (e.key == KeyCode.DownArrow && (lockedToken == tokens[2] || lockedToken == null))
        {
            ToggleTokenLock(tokens[2]);
        }
        if (e.key == KeyCode.RightArrow && (lockedToken == tokens[3] || lockedToken == null))
        {
            ToggleTokenLock(tokens[3]);
        }
        if (e.key == KeyCode.LeftArrow && (lockedToken == tokens[4] || lockedToken == null))
        {
            ToggleTokenLock(tokens[4]);
        }

        if (e.key ==KeyCode.Space)
        {
            _clip = Resources.Load("Audio_cjw487/DogButton") as AudioClip;
            Services.GameManager.AudioSource.PlayOneShot(_clip);
        }
    }

    private void ToggleTokenLock(Switcheroo token)
    {
        token.changeThisRound = !token.changeThisRound;
        if (lockedToken == null)
        {
            _clip = Resources.Load("Audio_cjw487/Select") as AudioClip;
            Services.GameManager.AudioSource.PlayOneShot(_clip);
            lockedToken = token;
        }
        else
        {
            _clip = Resources.Load("Audio_cjw487/Deselect") as AudioClip;
            Services.GameManager.AudioSource.PlayOneShot(_clip);
            lockedToken = null;
        }

    }

    private void ResetTokenLock(ResetTokenLockEvent e)
    {
        lockedToken = null;
        for (int i = 0; i < tokens.Length; i++)
        {
            tokens[i].changeThisRound = true;
        }
    }

    private void CheckIfSolved(CheckIfSolvedEvent e)
    {
        for (int i = 0; i < tokens.Length; i++)
        {

            if (tokens[i].Shape == Switcheroo.ShapeType.CIRCLE)
            {
                playerWon = true;
            }
            else
            {
                playerWon = false;
                break;
            }
        }
    }
}
