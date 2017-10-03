using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_DealerHand : DealerHand {

	// fix to let dealer stay when 17 or higher

	protected override bool DealStay(int handVal){
		if (handVal >= 17) {
			return true;
		} else {
			return false;
		}
	}
}
