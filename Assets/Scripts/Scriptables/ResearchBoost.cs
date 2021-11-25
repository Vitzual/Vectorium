using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tech", menuName = "Building/Research")]
public class ResearchBoost : IdentifiableScriptableObject
{
    // Start is called before the first frame update
    public Sprite icon;
    public new string name;
    [TextArea] public string description;
    public int essenceRequirement;
    public int heatRequirement;
    public int powerRequirement;
    public int organization;
    public List<Cost> cost;
    public ResearchType type;
    public float amount;
    public Resource.CurrencyType currency;
}
