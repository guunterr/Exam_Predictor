using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Subject { English, Maths, Science, Latin, Spanish, Geography, History, Art, French, Mandarin, Computing }
public enum Parameter { Sleep, Study, Tuition, SleepRange }

public class AppManager : MonoBehaviour {

    public static List<float> ParameterValues = new List<float> { 8, 3, 1, 2 };
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
