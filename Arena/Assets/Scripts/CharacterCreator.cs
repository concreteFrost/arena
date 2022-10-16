using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class CharacterCreator : MonoBehaviour
{
    public List<GameObject> characterList = new List<GameObject>();
    public List<GameObject> itemsList = new List<GameObject>();
    public List<Material> maleMaterialList = new List<Material>();
    public List<Material> femaleMaterialList = new List<Material>();
    
    public static GameObject character;
    public GameObject item;
  
    public TMP_Dropdown[] dropdowns;
    public TMP_Dropdown hatsDropdown;
    public Button[] genderChoiseButtons;
    public TMP_InputField nameInput;

    public TMP_Text[] playerStatsUI;
    public TMP_Dropdown starterWeaponDropdown;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject ch = Instantiate(characterList[0], new Vector3(0, 0, 0), Quaternion.identity);
        starterWeaponDropdown.value = PlayerPrefs.GetInt("Weapon On Start");
        character = ch;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

        if (Input.GetMouseButton(1))
        {
            var rotX = Input.GetAxis("Mouse X") * Time.deltaTime * 1000;

            character.transform.Rotate(Vector3.up * rotX);
        }

    }


    private void OnEnable()
    {
        ChangeClothes();
        ChangeItems();
        ChangeWeapon();
    }

     void UpdateUI()
    {
        var stats = character.GetComponent<PlayerStats>();

        playerStatsUI[1].text = "Health: " + stats.health.ToString();
        playerStatsUI[2].text = "Stamina: " + stats.stamina.ToString();
        playerStatsUI[3].text = "Speed: " + stats.speed.ToString();
        playerStatsUI[4].text = "Total Price: " + stats.price.ToString();
    }

    public void SubmitName()
    {
        if (nameInput.text.Length > 0)
        {
            playerStatsUI[0].text = "Name: " + nameInput.text;
            character.GetComponent<PlayerStats>().name = nameInput.text;
        }
    }

    void ChangeItems()
    {

        hatsDropdown.onValueChanged.AddListener((x) =>
        {
            if(item != null)
                Destroy(item.gameObject);

            var hatPosition = GameObject.FindGameObjectWithTag("hatPlacer");
            GameObject i = Instantiate(itemsList[hatsDropdown.value], hatPosition.transform.position,hatPosition.transform.root.localRotation);
            i.transform.parent = hatPosition.transform;
            item = i;

            var itemType = item.GetComponent<ItemStats>().boosterType;
            var plStats = character.GetComponent<PlayerStats>();
            
            plStats.ResetParams();
            switch (itemType)
            {
                case BoosterType.health:
                    
                    plStats.health += 20;
                    break;
                case BoosterType.speed:
                    
                    plStats.speed += 2;    
                    break;
                case BoosterType.stamina:
                    
                    plStats.stamina += 20;         
                    break;
            }
            plStats.price = plStats.minPrice;
            plStats.price += i.GetComponent<ItemStats>().i_price;
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
        character.GetComponent<PlayerStats>().name = nameInput.text;


    }

    public void ChangeWeapon()
    {
        starterWeaponDropdown.onValueChanged.AddListener((x) =>
        {
            PlayerPrefs.SetInt("Weapon On Start", starterWeaponDropdown.value);
            Debug.Log(PlayerPrefs.GetInt("Weapon On Start"));
        });
       
    }


    public void CreateCharacter()
    {
        Debug.Log("Done");
        DontDestroyOnLoad(character);
        LoadPlayer.player = character;
        Application.LoadLevel(1);
        
    }
}
