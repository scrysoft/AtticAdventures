using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SurveyManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        [HideInInspector]
        public int ID;
        [TextArea]
        public string QuestionText;
        public CategoryType Category;
        [HideInInspector]
        public int Answer;
    }

    public enum CategoryType
    {
        Explorer,
        Achiever,
        Killer,
        Socializer
    }

    public GameObject QuestionPrefab;
    public Transform QuestionsContainer;
    public Button SubmitButton;
    public List<Question> Questions = new List<Question>();

    void OnEnable()
    {
        for (int i = 0; i < Questions.Count; i++)
        {
            Questions[i].ID = i + 1;
        }

        foreach (var question in Questions)
        {
            CreateQuestion(question);
        }

        SubmitButton.onClick.AddListener(SaveResultsToCsv);
    }

    void CreateQuestion(Question question)
    {
        GameObject questionObject = Instantiate(QuestionPrefab, QuestionsContainer);
        TextMeshProUGUI questionText = questionObject.transform.Find("QuestionText").GetComponent<TextMeshProUGUI>();
        Slider questionSlider = questionObject.transform.Find("AnswerSlider").GetComponentInChildren<Slider>();

        questionText.text = question.QuestionText;
        questionSlider.minValue = 0;
        questionSlider.maxValue = 3;
        questionSlider.wholeNumbers = true;
        questionSlider.onValueChanged.AddListener(value => question.Answer = (int)value);
    }

    void SaveResultsToCsv()
    {
        string sessionId = AnalyticsManager.Instance.GetSessionId().ToString();

        string folderName = $"{sessionId}";
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string heatmapFolderPath = Path.Combine(folderPath, "HeatmapTrackingData");

        if (!Directory.Exists(heatmapFolderPath))
        {
            Directory.CreateDirectory(heatmapFolderPath);
        }

        AnalyticsManager.Instance.SetSaveDirectory(heatmapFolderPath);

        string filePath = Path.Combine(folderPath, $"{sessionId}.csv");

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("ID,Frage,Kategorie,Antwort");

            foreach (var question in Questions)
            {
                string line = $"{question.ID},\"{question.QuestionText}\",{question.Category},{question.Answer}";
                writer.WriteLine(line);
            }
        }
    }

}
