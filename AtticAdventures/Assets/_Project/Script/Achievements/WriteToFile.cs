using System;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace AtticAdventures
{
    public class WriteToFile : MonoBehaviour
    {
        public static WriteToFile Instance { get; private set; }

        private string _folderPath;
        private string _subFolderPath;
        private string _csvFilePath;

        private float _startTime;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _startTime = Time.time;

            string sessionID = AnalyticsManager.Instance.GetSessionId().ToString();
            _folderPath = Path.Combine(Application.persistentDataPath, sessionID);
            _subFolderPath = Path.Combine(_folderPath, "AchievementTimeStamps");
            _csvFilePath = Path.Combine(_subFolderPath, $"{sessionID}_timeStamps.csv");

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
            if (!Directory.Exists(_subFolderPath))
            {
                Directory.CreateDirectory(_subFolderPath);
            }
        }

        public void WriteAchievementWithTimestamp(string text)
        {
            float timeSinceStart = Time.time - _startTime;
            int intSeconds = (int)timeSinceStart;
            string line = $"{text},{intSeconds}";

            try
            {
                using (StreamWriter sw = new StreamWriter(_csvFilePath, true))
                {
                    sw.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Fehler beim Schreiben in CSV: {ex.Message}");
            }
        }
    }
}
