using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RussianRouletteDealerTurn : RussianRouletteTurn {

    private RussianRouletteTurn player;

	// Use this for initialization
	void Start () {
        revolver = GameObject.Find("Revolver").GetComponent<Revolver>();
        _manager = GameObject.Find("RussianRouletteManager").GetComponent<RussianRouletteManager>();
        player = GameObject.Find("Player").GetComponent<RussianRouletteTurn>();
        Setup();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void UpdateState()
    {
        //If there was no state change we don't need to check switch statement
        if (currentState == previousState)
        {
            return;
        }

        switch (currentState)
        {
            case ActionState.Dead:
                break;

            case ActionState.DroppedOut:
                break;

            case ActionState.LoadingRevolver:
                roundsRemaining = roundsPerTurn;
                int amountToLoad = Random.Range((int)0,(int)3);
                
                for(int i = 0; i < amountToLoad; i++)
                {
                    LoadGun();
                }

                if (revolver.GetCount() < revolver.chambers)
                {
                    for(int j = revolver.GetCount(); j < revolver.chambers; j++)
                    {
                        revolver.AddBlanksToRevolver();
                    }
                }

                Invoke("Pass" ,2f);
                break;

            case ActionState.ShootingRevolver:
                PullTrigger();
                break;

            case ActionState.Waiting:
                break;
        }
    }

    public override void Pass()
    {
        this.SetState(ActionState.Waiting);
        player.SetState(ActionState.ShootingRevolver);
    }

    public override void PullTrigger()
    {
        Revolver.Bullet bullet = revolver.ShootRevolver();

        if (bullet.bulletRound == Revolver.Bullet.Round.MAGNUM)
        {
            //Dealer Shot Themself
            _manager.DealerDead();

        }
        else
        {
            Invoke("Pass", 1);
        }
    }

    protected override void Setup()
    {
        currentState = ActionState.Waiting;
        previousState = ActionState.Waiting;
    }

    protected override void UpdateUI()
    {
        playerStatus.text = "Dealer:";
    }
}
