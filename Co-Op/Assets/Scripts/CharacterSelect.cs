using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterSelect : MonoBehaviour
{
    public GameObject charSelectPanel;

    public static CharacterSelect instance;

    public List<GameObject> characterList = new List<GameObject>();

    [SerializeField] GameObject chosenCharacter = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);

        charSelectPanel.SetActive(true);
    }

    private void ChooseCharacter(int n)
    {
        chosenCharacter = characterList[n];
    }

    public void ChooseSquare()
    {
        ChooseCharacter(0);
    }

    public void ChooseCircle()
    {
        ChooseCharacter(1);
    }

    public void StartGame()
    {
        if(chosenCharacter != null)
        {
            charSelectPanel.SetActive(false);
        }
    }

    public GameObject GetCharacter()
    {
        return chosenCharacter;
    }
}
