using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWives : MonoBehaviour {
	[SerializeField] Sprite[] wifeList;
	[SerializeField] List<Wife_Pile> wife_Piles;
	ShuffleBag<Sprite> wifeBag;
	void Start(){
		List<Sprite> m_wifeList = new List<Sprite>();
		m_wifeList.AddRange(wifeList);
		m_wifeList.AddRange(wifeList);
		m_wifeList.AddRange(wifeList);

		wifeBag = new ShuffleBag<Sprite>(m_wifeList.ToArray());
	}

	public void Pick_My_Wife(){
		foreach(Wife_Pile lady in wife_Piles){
			lady.wife_select(wifeBag.Next());
		}
	}
}
