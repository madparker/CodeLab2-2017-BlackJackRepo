using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RussianRouletteTurn : MonoBehaviour {

    public Text playerStatus;
    public int roundsPerTurn = 2;
    protected int roundsRemaining;

    protected RussianRouletteManager _manager;
    protected Revolver revolver;

    public enum ActionState
    {
        LoadingRevolver,
        Waiting,
        ShootingRevolver,
        DroppedOut,
        Dead
    };

    public ActionState currentState;
    public ActionState previousState;

    private RussianRouletteDealerTurn dealer;

	// Use this for initialization
	void Start () {
        revolver = GameObject.Find("Revolver").GetComponent<Revolver>();
        _manager = GameObject.Find("RussianRouletteManager").GetComponent<RussianRouletteManager>();
        dealer = GameObject.Find("Dealer").GetComponent<RussianRouletteDealerTurn>();
        currentState = ActionState.Waiting;
        previousState = ActionState.Waiting;
        Setup();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateUI();
	}


    public virtual void DropOut()
    {
        //drop out
        //no longer can shoot keeps points if dealer drops 
        //but if dealer shoots and wins steals your points
        _manager.DropOut();
    }


    public virtual void LoadGun()
    {
        if (roundsRemaining > 0)
        {
            revolver.AddBulletsToRevolver();
            roundsRemaining--;
        }
    }


    protected virtual void UpdateState()
    {
        //If there was no state change we don't need to check switch statement
        if(currentState == previousState)
        {
            return;
        }

        switch (currentState)
        {
            case ActionState.Dead:
                _manager.HidePlayerButtons();
                break;

            case ActionState.DroppedOut:
                _manager.HidePlayerButtons();
                break;

            case ActionState.LoadingRevolver:
                roundsRemaining = roundsPerTurn;
                _manager.HidePlayerButtons();
                _manager.ShowSetupButtons();
                break;

            case ActionState.ShootingRevolver:
                _manager.HidePlayerButtons();
                _manager.ShowDecisionButtons();
                break;

            case ActionState.Waiting:
                _manager.HidePlayerButtons();
                break;
        }
    }

    public virtual void Pass()
    {
        if(currentState == ActionState.LoadingRevolver)
        {
            this.SetState(ActionState.Waiting);
            dealer.SetState(ActionState.LoadingRevolver);
           
        }
        else
        {
            this.SetState(ActionState.Waiting);
            dealer.SetState(ActionState.ShootingRevolver);
            
        }
    }

    public virtual void PullTrigger()
    {
        Revolver.Bullet bullet = revolver.ShootRevolver();

        if (bullet.bulletRound == Revolver.Bullet.Round.MAGNUM)
        {
            //You Lose
            _manager.PlayerDead();

        }
        else
        {
            Pass();
        }
    }

    public virtual void SetState(ActionState state)
    {
        this.previousState = this.currentState;
        this.currentState = state;
        UpdateState();
    }

    protected virtual void Setup()
    {
        this.SetState(ActionState.LoadingRevolver);
    }

    protected virtual void UpdateUI()
    {
        playerStatus.text = "Player: " + roundsRemaining + " rounds remaining";
    }
}
