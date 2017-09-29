using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerHandExtended : DealerHand {

	// Use this for initialization
	protected override bool DealStay(int handVal){
		return handVal >= 17;
	}

}
