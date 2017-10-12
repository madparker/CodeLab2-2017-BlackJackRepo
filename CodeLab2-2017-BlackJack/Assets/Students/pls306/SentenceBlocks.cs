using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentenceBlocks : MonoBehaviour {

	public static ShuffleBag<Block> deck;

	public class Block{

		public enum Part{
			SUBJECT,
			VERB,
			PREDICATE
//			A 12th century treatise describes a bath filled with tongues.
		}

		public string[] subjects = { 
			"A stray electron", 
			"A disillusioned Supreme Court Justice",
			"A portly gym trainer",
			"A brutal nanny",
			"A meticulous gravity sculptor",
			"A forgotten bronze statue",
			"A disgruntled dreamcatcher",
			"A lazy cobbler",
			"A morose necromancer",
			"An extraterrestrial hive mind",
			"A sentient AI",
			"A 90-year-old drag queen"		
		};

		public string[] verbs = {
			"navigates",
			"ushers in",
			"prevents",
			"destroys",
			"questions",
			"circumvents",
			"blasts",
			"protects",
			"remembers",
			"sells",
			"insults",
			"converses with"
		};

		public string[] descriptions = {
			"a Toyota minivan filled with blue limbs.",
			"a lumbering giant beset by existential distress.",
			"a termite-infested Trojan Horse",
			"a gathering of Mozart acolytes.",
			"a mass of dustmites under the dining table.",
			"an airplane that prefers to be underground.",
			"a labyrinthine office space on Madison Avenue.",
			"a ten-storey-tall stack of Toblerone",
			"an unsolved murder case in Quezon City.",
			"an empty concert hall north of the Great Wall",
			"a dictator whose advisor is an old librarian.",
			"a dive bar that serves Moonshine distilled in the 1920s."
		}; 
	}

}
