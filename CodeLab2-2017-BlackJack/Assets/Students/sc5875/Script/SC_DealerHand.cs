using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DealerHand : DealerHand {
	protected override bool DealStay(int handVal){
		return handVal >= 17;
	}
}
