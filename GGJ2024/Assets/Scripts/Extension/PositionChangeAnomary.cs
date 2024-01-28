using Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class PositionChangeAnomary : AbstractEvent
{
    [SerializeField] private List<GameObject> ChangePeople;
    [SerializeField] private Vector3 PlusPos;
    [SerializeField] private Quaternion Rotation;
    [SerializeField] private Vector3 PlusScale;
    private List<Quaternion> ReRotation = new List<Quaternion>();
    
    public override void Play()
    {
      foreach(GameObject Human in ChangePeople)
        {
            ReRotation.Add(Human.transform.rotation);
            Human.transform.localPosition += PlusPos;
            Human.transform.rotation = Rotation;
            Human.transform.localScale += PlusScale;
        }

    }

    public override void Rewind()
    {
     for(int i =0; i < ChangePeople.Count; i++)
        {
            ChangePeople[i].transform.localPosition -= PlusPos;
            ChangePeople[i].transform.rotation = ReRotation[i];
            ChangePeople[i].transform.localScale -= PlusScale;
        }
    }
}
