
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System.IO;
using System.Collections;
using UnityEditor;
using levelConfiguration;


public class level_Manager : MonoBehaviour
{
   
    [Header("Configuration")]
    [SerializeField] List<levelconfig> Levelconfigs;
    [SerializeField] List<GameObject> Textobj;

    int currentLvl = 0;


    [Header("UI")]
    [SerializeField] GameObject correctPanel;
    [SerializeField] GameObject wrongPanel;
    [SerializeField] SpriteRenderer bg;
    [SerializeField] GameObject thanksPanel;
    [SerializeField] GameObject dropSlot;
    private void OnEnable()
    {
        Actions.answercheck += CheckAnswer;
    }
    private void OnDisable()
    {
        Actions.answercheck -= CheckAnswer;
    }

    private void Start()
    {
        Levelconfigs.Clear(); //clear the list

        string folderPath = "Assets/Levels/";
        // If there is no such Directory 
        if (!Directory.Exists(folderPath))
        {
            Debug.LogWarning("The directory does not exist: " + folderPath);
            return;
        }

        //The *.asset pattern tells Directory.GetFiles to find all files with the .asset extension in the specified folder
        string[] assetPaths = Directory.GetFiles(folderPath, "*.asset");

        foreach (string assetPath in assetPaths)
        {
            levelconfig levelConfig = AssetDatabase.LoadAssetAtPath<levelconfig>(assetPath);
            if (levelConfig != null)
            {
                //add to the list of levelCongis
                Levelconfigs.Add(levelConfig);
            }
        }

      

    }
    //load level
    public void LoadLevel(int levelIndex)
    {


        if (levelIndex >= Levelconfigs.Count)
        {
            thanksPanel.SetActive(true);
            return;
        }
        if (Levelconfigs[levelIndex] == null)
            return;
        
        dropSlot.SetActive(true);
        var currentlevelConfig = Levelconfigs[levelIndex];
        //set bg sprite
        if (currentlevelConfig.backgroudSprite != null)
        {
            bg.sprite = Levelconfigs[levelIndex].backgroudSprite;
        }

        // shuffle the word
        string[] suffledWOrds = ShuffleWords(currentlevelConfig.words);
        for (int i = 0; i < currentlevelConfig.noOfwords; i++)
        {
         
            

            //set the text in the button 
            Textobj[i].gameObject.SetActive(true);
            Textobj[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = suffledWOrds[i];


        }


    }

    private string[] ShuffleWords(List<string> words)
    {
        string[] shuffledWords = new string[words.Count];
        words.CopyTo(shuffledWords, 0);//copy the words in the array

        for (int i = 0; i < shuffledWords.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, shuffledWords.Length);
            // Swap the current element with the randomly chosen element
            string temp = shuffledWords[i];
            shuffledWords[i] = shuffledWords[randomIndex];
            shuffledWords[randomIndex] = temp;
        }

        return shuffledWords;
    }

    //check if the selected ans correct or not
    public void CheckAnswer(string selectedWOrd)
    {
        if (Levelconfigs[currentLvl] == null || currentLvl >= Levelconfigs.Count)
            return;
       
            
            //ans is correct
            if (Levelconfigs[currentLvl].correctans == selectedWOrd)
            {
          
                StartCoroutine(PlayNextLevelAnim());
            }
            //ans is wrong
            else
            {
                StartCoroutine(worngAnsAnim());
            }
    }
    

    //
    IEnumerator PlayNextLevelAnim()
    {

        correctPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        correctPanel.SetActive(false);
        currentLvl += 1;
        LoadLevel(currentLvl);
        Debug.Log("correct ans");
    }

    IEnumerator worngAnsAnim()
    {

        wrongPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        wrongPanel.SetActive(false);
        LoadLevel(currentLvl);
        Debug.Log("wrong ans");
    }

   
}
