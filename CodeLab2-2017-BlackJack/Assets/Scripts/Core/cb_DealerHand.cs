using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cb_DealerHand : DealerHand {

	//TODO - fix it?
    protected override bool DealStay(int handValue) {
        return handValue >= 17;
    }


}
