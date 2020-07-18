using UnityEngine;

// we need a list object so we use the collections libraries (specifically the generic lists)
using System.Collections;
using System.Collections.Generic;

// we need IO to read and write files and also to see if files exist (if they dont we dont bother reading from them)
using System.IO;

// ths module keeps track of the best times for a game 

// once we have saved at least one new time (score) a file 
// called "savedtimes.txt" will appear in the projectview in the unity editor

public class BestTimes : MonoBehaviour 
{
    // only save the top X (default is 10) scores in the file
	public int rememberAtMost = 10;
	
	// this is the name of the file
	private string bestTimesFileName = "Assets/savedtimes.txt";

	// we only want to load the file once as we keep the scores stored in the bestTimes variable
	private bool bestTimesLoaded = false;
	
	// we just store the time in seconds - the list is sorted by us
	private List<float> bestTimes = new List<float>();
		
	void Start ()
	{
		DontDestroyOnLoad (this);
		for (int i = 0; i < rememberAtMost; i++) {
			bestTimes.Add (0);
		}
		for (int i = 0; i < bestTimes.Count; i++) {
			string s = "" + i;
			PlayerPrefs.SetFloat (s, bestTimes [i]);
		}
	}	
	
	public void NewTime (float newTime)
	{
		// load current best times if not done already
		if (!bestTimesLoaded) {
			for (int i = 0; i < rememberAtMost; i++) {
				string s = "" + i;
				bestTimes.Add (PlayerPrefs.GetFloat (s));
			}
			bestTimesLoaded = true;
			int index = 0;
			for (; index < bestTimes.Count; index++) {
				if (newTime > bestTimes [index])
					break;
			}
			
			bestTimes.Insert (index, newTime);
			if (bestTimes.Count > rememberAtMost) {
				int numToRemove = bestTimes.Count - rememberAtMost;
				bestTimes.RemoveRange (rememberAtMost, numToRemove);
			}

			for (int i = 0; i < bestTimes.Count; i++) {
				string s = "" + i;
				PlayerPrefs.SetFloat (s, bestTimes [i]);
			}
		}
//			// see if the file exists - if not then dont bother trying to read it
//			if (File.Exists (bestTimesFileName))
//			{
//				// it does exist so quickly get a list of strings, where each string
//				// is one line of the text file, each line contains a float, and 
//				// each lines time is lower than the next
//				//
//				//  eg  3.5
//				//      4.6
//				//      10.2
//				string[] lines = File.ReadAllLines (bestTimesFileName);
//				foreach (string line in lines)
//				{
//					// convert each line from a string to a float value and store in our list
//					float t = float.Parse (line);
//					bestTimes.Add (t);
//				}
//			}
//			
//			// make sure we don't do this again
//			bestTimesLoaded = true;
//
//
//		}
//		
//		// find the correct place to add our new score
//
//		int index = 0;
//		for (; index < bestTimes.Count; index ++)
//		{
//			if (newTime > bestTimes [index])
//			{
//				// this is where we insert the new time
//				break;
//			}
//		}
//		// we stopped looking through the besttimes list when we found
//		// a time that was worse than the new time
//		bestTimes.Insert (index, newTime);
//		
//		// trim the list so we only store as many times as needed
//		// if for example our time was worse than all the existing
//		// times and we already had 10 top times, then our list
//		// will now contain 11 entries with the new one at the bottom
//		if (bestTimes.Count > rememberAtMost)
//		{
//			// removerange (indexoffirst, numbertoremove)
//			int numToRemove = bestTimes.Count - rememberAtMost;
//			bestTimes.RemoveRange (rememberAtMost, numToRemove);
//		}
//		
//		// save the updated list back to the file
//		// we do this by creating a list of strings where each string will
//		// be one line in the file (look back at where we read this file in)
//		// the number of lines is equal to the number of scores we have
//		string[] textToSave = new string [bestTimes.Count];
//		for (int i = 0; i < bestTimes.Count; i ++)
//		{
//			// convert the time from a float to a string
//			textToSave [i] = bestTimes[i].ToString ();
//		}
//		// save to the file
//		File.WriteAllLines(bestTimesFileName, textToSave);
		
		}

	// We don't need our own OnGUI function as ShowBestTimes
	// is called from an OnGUI in another component (in this case from GameLogic.cs)
	public void ShowBestTimes ()
	{
		if (bestTimesLoaded)
		{
			// Displays 10 high scores
			// Adds player if they are lower than top 10
			GUILayout.Label("High Scores");
			GUILayout.Label("1. " + bestTimes[0]);
			GUILayout.Label("2." + bestTimes[1]);
			GUILayout.Label("3."+ bestTimes[2]);
			GUILayout.Label("4."+ bestTimes[3]);
			GUILayout.Label("5."+ bestTimes[4]);
			GUILayout.Label("6."+ bestTimes[5]);
			GUILayout.Label("7."+ bestTimes[6]);
			GUILayout.Label("8."+ bestTimes[7]);
			GUILayout.Label("9."+ bestTimes[8]);
			GUILayout.Label("10."+ bestTimes[9]);
			
		}
	}			
}
