using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateDebugger : MonoBehaviour
{
	public int targetrate = 20;
	void Awake()
	{
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = targetrate;
	}
	}
