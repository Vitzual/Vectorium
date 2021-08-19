﻿using UnityEngine;

public class ResearchLab : DefaultBuilding
{
    private void Start()
    {
        Research.LabsAvailable += 1;
        GameObject.Find("Research").GetComponent<Research>().UpdateAvailable();
    }
}