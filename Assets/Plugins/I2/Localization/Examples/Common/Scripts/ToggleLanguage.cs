using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	public class ToggleLanguage : MonoBehaviour 
	{
		void Start () 
		{
			Invoke("Test", 3);
		}

		void Test()
		{
			//--  to move into the next language ----

				List<string> languages = LocalizationManager.GetAllLanguages();
				int Index = languages.IndexOf(LocalizationManager.CurrentLanguage);
				if (Index<0)
					Index = 0;
				else
					Index = (Index+1) % languages.Count;

			//-- Call this function again in 3 seconds

				Invoke("Test", 3);
		}
	}
}