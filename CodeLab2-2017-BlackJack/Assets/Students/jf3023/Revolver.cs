using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour {

    public int chambers = 6;
    public int remaining;

    public class Bullet
    {
        public enum Round
        {
            BLANK,
            MAGNUM

        };

        public Round bulletRound;

        public Bullet(Round bulletRound)
        {
            this.bulletRound = bulletRound;
        }
    }

    public static ShuffleBag<Bullet> revolver;

    private void Awake()
    {
        revolver = new ShuffleBag<Bullet>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
    public virtual void AddBulletsToRevolver()
    {
        if(revolver.Count < chambers)
        {
            revolver.Add(new Bullet(Bullet.Round.MAGNUM));
            remaining++;
        }
    }

    public virtual void AddBlanksToRevolver()
    {
        if (revolver.Count < chambers)
        {
            revolver.Add(new Bullet(Bullet.Round.BLANK));
            remaining++;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public virtual Bullet ShootRevolver()
    {
        Bullet nextBullet = revolver.Next();
        remaining--;
        return nextBullet;
    }

    public int GetCount()
    {
        return revolver.Count;
    }
}
