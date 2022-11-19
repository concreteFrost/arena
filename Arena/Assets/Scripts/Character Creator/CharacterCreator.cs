using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class CharacterCreator : MonoBehaviour
{
    public List<GameObject> characterList = new List<GameObject>();
    public CharacterCreatorSO characterSettings;
    public PlayerStatsSO pl_statsSo;
    
    public GameObject character;

    public TMP_Dropdown[] dropdowns;
    public TMP_Dropdown hatsDropdown;
    public Button[] genderChoiseButtons;
    public TMP_InputField nameInput;

    public TMP_Text[] playerStatsUI;
    public TMP_Dropdown starterWeaponDropdown;
 
    // Start is called before the first frame update
    void Start()
    {
        pl_statsSo.ResetParams();
        characterSettings.MaterialReset();
        character = Instantiate(pl_statsSo.character, Vector3.zero, Quaternion.identity);

        pl_statsSo.starterWeapon = characterSettings.starterWeapon[starterWeaponDropdown.value];
        pl_statsSo.additionalItem = characterSettings.additionalItems[hatsDropdown.value];

        UpdateUI();
    }


    private void OnEnable()
    {
        ChangeClothes();
        ChangeItems();
        ChangeWeapon();
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            var rotX = Input.GetAxis("Mouse X") * Time.deltaTime * 1000;

            character.transform.Rotate(Vector3.up * rotX);
        }

    }

     void UpdateUI()
    {
       

        playerStatsUI[1].text = "Health: " + pl_statsSo.health.ToString();
        playerStatsUI[2].text = "Stamina: " + pl_statsSo.stamina.ToString();
        playerStatsUI[3].text = "Speed: " + pl_statsSo.speed.ToString();
        playerStatsUI[4].text = "Total Price: " + pl_statsSo.price.ToString();

        
    }

    public void SubmitName()
    {
        if (nameInput.text.Length > 0)
        {
            playerStatsUI[0].text = "Name: " + nameInput.text;
            pl_statsSo.name = nameInput.text;
        }
        UpdateUI();
    }

    void ChangeItems()
    {

        hatsDropdown.onValueChanged.AddListener((x) =>
        {
       
            var hatPosition = GameObject.FindGameObjectWithTag("hatPlacer");

            if(hatPosition.transform.childCount > 0)
            Destroy(hatPosition.transform.GetChild(0).gameObject);

            GameObject i = Instantiate(characterSettings.additionalItems[hatsDropdown.value], hatPosition.transform.position,hatPosition.transform.root.localRotation);
            pl_statsSo.additionalItem = characterSettings.additionalItems[hatsDropdown.value];
            i.transform.parent = hatPosition.transform;

            var itemType = i.GetComponent<ItemStats>().boosterType;
        
            switch (itemType)
            {
                case BoosterType.health:

                    pl_statsSo.health += 20;
                    break;
                case BoosterType.speed:

                    pl_statsSo.speed += 2;    
                    break;
                case BoosterType.stamina:

                    pl_statsSo.stamina += 20;         
                    break;
            }
            pl_statsSo.price = pl_statsSo.minPrice;
            pl_statsSo.price += i.GetComponent<ItemStats>().price;
            UpdateUI();
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

                AssignClothes(part, i);
                
                UpdateUI();

            });
        }
    }

    void AssignClothes(string part, int i)
    {
        var newMaterial = character.transform.Find(part).GetComponent<SkinnedMeshRenderer>();
        var newMaterialForSO = pl_statsSo.character.transform.Find(part).GetComponent<SkinnedMeshRenderer>();

        switch (pl_statsSo.gender)
        {
            case Gender.male:
                {

                    newMaterial.material = characterSettings.maleMaterials[i];
                    newMaterialForSO.material = characterSettings.maleMaterials[i];

                }
                break;
            case Gender.female:
                {

                    newMaterial.material = characterSettings.femaleMaterials[i];
                    newMaterialForSO.material = characterSettings.femaleMaterials[i];
                }
                break;
        }
    }

    public void ChangeGender(string g)
    {
        Destroy(character.gameObject);
       
        switch (g)
        {
            case "male": 
                character = Instantiate(characterSettings.characters[0], Vector3.zero, Quaternion.identity);
                pl_statsSo.character = characterSettings.characters[0];
                pl_statsSo.gender = Gender.male;
                break;
            case "female":     
                character = Instantiate(characterSettings.characters[1], Vector3.zero, Quaternion.identity);
                pl_statsSo.character = characterSettings.characters[1];
                pl_statsSo.gender = Gender.female;
                break;
        }

        foreach(var d in dropdowns)
        {
            d.value = 0;
        }

        hatsDropdown.value = 0;


    }

    public void ChangeWeapon()
    {
        starterWeaponDropdown.onValueChanged.AddListener((x) =>
        {
            pl_statsSo.starterWeapon = characterSettings.starterWeapon[starterWeaponDropdown.value];
        });
       
    }


    public void CreateCharacter()
    {
        Application.LoadLevel(1);
        
    }
}
