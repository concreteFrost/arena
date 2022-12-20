using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Creator/Character Creator")]
public class CharacterCreatorSO : ScriptableObject
{
    public List<GameObject> characters;
    public List<Material> maleMaterials = new List<Material>();
    public List<Material> femaleMaterials = new List<Material>();
    public List<GameObject> additionalItems = new List<GameObject>();
    public List<GameObject> starterWeapon = new List<GameObject>();

    public void MaterialReset()
    {
       
            foreach (Transform t in characters[0].transform)
            {
                if (t.GetComponent<SkinnedMeshRenderer>() != null)
                {
                    t.GetComponent<SkinnedMeshRenderer>().material = maleMaterials[0];
                }
            }

        foreach (Transform t in characters[1].transform)
        {
            if (t.GetComponent<SkinnedMeshRenderer>() != null)
            {
                t.GetComponent<SkinnedMeshRenderer>().material = femaleMaterials[0];
            }
        }

    }
}
