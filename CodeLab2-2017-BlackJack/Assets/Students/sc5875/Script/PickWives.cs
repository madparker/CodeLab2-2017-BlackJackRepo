using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWives : MonoBehaviour {
	[SerializeField] Sprite[] wifeList;
	[SerializeField] List<Wife_Pile> wife_Piles;
	[SerializeField] List<string> wife_Phrases;
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
	public string Get_String(Sprite m_sprite){
		return wife_Phrases[Get_Index(m_sprite)];
	}
	protected int Get_Index(Sprite m_sprite){
		for(int i = 0; i<wifeList.Length; i++){
			if(wifeList[i] == m_sprite){
				return i;
			}
		}
		return 0;
	}
}
