using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCreator : MonoBehaviour
{
    public List<GameObject> characterList = new List<GameObject>();
    public List<GameObject> itemsList = new List<GameObject>();
    public GameObject character;
    public GameObject item;
    public List<Material> maleMaterialList = new List<Material>();
    public List<Material> femaleMaterialList = new List<Material>();
    public TMP_Dropdown[] dropdowns;
    public TMP_Dropdown hatsDropdown;
    public Button[] genderChoiseButtons;
    // Start is called before the first frame update
    void Start()
    {
        GameObject ch = Instantiate(characterList[0], new Vector3(0, 0, 0), Quaternion.identity);
        character = ch;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCharacterOnStage();
            
    }


    private void OnEnable()
    {
        ChangeClothes();
        ChangeItems();
    }

    void ChangeItems()
    {
        hatsDropdown.onValueChanged.AddListener((x) =>
        {
            if(item != null)
                Destroy(item.gameObject);

            var hatPosition = GameObject.FindGameObjectWithTag("hatPlacer");
            GameObject i = Instantiate(itemsList[hatsDropdown.value], hatPosition.transform.position, Quaternion.LookRotation(Vector3.forward,hatPosition.transform.position));
            i.transform.parent = hatPosition.transform;
            item = i;
        });
       
    }

    void ChangeClothes()
    {
        foreach (var d in dropdowns)
        {
            d.onValueChanged.AddListener((i) =>
            {
                string part = "";
                switch (d.name)
                {
                    case "top_dropdown":
                        part = "Tops";
                        break;
                    case "bottom_dropdown":
                        part = "Bottoms";
                        break;
                    case "shoes_dropdown":
                        part = "Shoes";
                        break;
                }

                var gender = character.GetComponent<PlayerStats>().gender;
                var newMaterial = character.transform.Find(part).GetComponent<SkinnedMeshRenderer>();
                switch (gender)
                {
                    case Gender.male:
                        {
                            
                            newMaterial.material = maleMaterialList[d.value];
                        }
                        break;
                    case Gender.female:
                        {
                            
                            newMaterial.material = femaleMaterialList[d.value];
                        }
                        break;
                }
                
            });
        }
    }

    public void ChangeGender(string g)
    {
        Destroy(character);
        GameObject ch;
        switch (g)
        {
            case "male":
                ch = Instantiate(characterList[0], new Vector3(0, 0, 0), Quaternion.identity);
                character = ch;
                break;
            case "female":
                ch = Instantiate(characterList[1], new Vector3(0, 0, 0), Quaternion.identity);
                character = ch;
                break;
        }

        foreach(var d in dropdowns)
        {
            d.value = 0;
        }

        hatsDropdown.value = 0;

        

    }

    void RotateCharacterOnStage()
    {
        var mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * 1500f;

        if (Input.GetMouseButton(1))
            character.transform.Rotate(0, mouseX, 0);

    }

}
