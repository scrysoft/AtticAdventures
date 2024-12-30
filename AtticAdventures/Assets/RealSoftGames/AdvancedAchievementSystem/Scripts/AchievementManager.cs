///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:48
///-----------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using static RealSoftGames.RTween;

namespace RealSoftGames.AdvancedAchievementSystem
{
    public class AchievementManager : MonoBehaviour
    {
        #region Variables

        private static AchievementManager instance;

        public static Action Oninitialized;

        private bool initialized;
        public static int totalAchievementPoints;

        [SerializeField, ReadOnly] private string savePath;
        private bool completingAchievements = false;

        [SerializeField, ReadOnly] private int currentAchievementPoints;
        [SerializeField, ReadOnly] private AdvancedAchievementSystem db;
        private ToggleGroup togglegroup;

        [Tooltip("Save file name with extension type"), SerializeField]
        private string fileName = "save.data";

        [SerializeField] private TMP_Text achievementCompletionText;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private TMP_InputField searchField;
        [SerializeField] private GameObject categoriesContent;
        [SerializeField] private GameObject achievementContent;
        [SerializeField] private GameObject achievementComplete;
        [SerializeField] private CanvasGroup achievementCompleteAlphaGroup;
        [SerializeField] private ReferenceMap referenceMap;
        [SerializeField] private AdvancedAchievementSystem databaseToLoad;
        [SerializeField] private List<CategoryBtn> categories;
        [SerializeField] private SerializableDictionary<AchievementList, Achievement> achievements;
        [SerializeField] private List<BaseAchievement> completedAchievements;

        public KeyCode InventoryInteractKey = KeyCode.Tab;
        [SerializeField] float animationTime = 0.1f;


        [SerializeField] private float displayTime = 5f;
        [SerializeField] private float fadeInTime = 1f;
        [SerializeField] private float fadeOutTime = 1f;

        public UnityEvent OnExpand;
        public UnityEvent OnClose;

        #endregion Variables

        #region Properties

        public TMP_InputField SearchField { get => searchField; set => searchField = value; }

        public ToggleGroup ToggleGroup { get => togglegroup; set => togglegroup = value; }

        public AdvancedAchievementSystem Database { get => db; set => db = value; }

        public AdvancedAchievementSystem DatabaseToLoad { get => databaseToLoad; set => databaseToLoad = value; }

        public List<BaseAchievement> CompletedAchievements { get => completedAchievements; set => completedAchievements = value; }

        public int CurrentAchievementPoints { get => currentAchievementPoints; set => currentAchievementPoints = value; }

        public GameObject AchievementComplete { get => achievementComplete; set => achievementComplete = value; }

        public GameObject CategoriesContent { get => categoriesContent; set => categoriesContent = value; }

        public GameObject AchievementContent { get => achievementContent; set => achievementContent = value; }
        public CanvasGroup AchievementCompleteAlphaGroup { get => achievementCompleteAlphaGroup; set => achievementCompleteAlphaGroup = value; }
        public AudioSource AudioSource { get => audioSource; }

        public SerializableDictionary<AchievementList, Achievement> Achievements { get => achievements; set => achievements = value; }
        public List<CategoryBtn> Categories { get => categories; set => categories = value; }
        public ReferenceMap Map { get => referenceMap; set => referenceMap = value; }
        public float DisplayTime { get => displayTime; set => displayTime = value; }
        public float FadeInTime { get => fadeInTime; set => fadeInTime = value; }
        public float FadeOutTime { get => fadeOutTime; set => fadeOutTime = value; }
        public static AchievementManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<AchievementManager>();
                    instance.Initialize();
                }
                return instance;
            }
        }

        #endregion Properties

        #region Methods

        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(InventoryInteractKey))
                ToggleAchievementSystem();
        }

        public virtual void ToggleAchievementSystem()
        {
            var background = Map.GetComponentFromReferenceMapComponent<Transform>(MapType.AchievementSystemBackground);

            if (background.IsExpanded())
                background.Close(animationTime, OpenDirection.Center, () => { OnClose?.Invoke(); });
            else
                background.Expand(animationTime, OpenDirection.Center, () => { OnExpand?.Invoke(); });
        }

        protected virtual void Initialize()
        {
            if (instance != null && instance != this)
                Destroy(gameObject);
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            if (initialized)
                return;

            if (referenceMap == null)
                referenceMap = GetComponent<ReferenceMap>();

            if (categoriesContent == null)
                categoriesContent = referenceMap.GetComponentFromReferenceMapComponent<Transform>(MapType.AchievementSystemCategoriesContent).gameObject;
            if (achievementContent == null)
                achievementContent = referenceMap.GetComponentFromReferenceMapComponent<Transform>(MapType.AchievementSystemAchievementContent).gameObject;

            ToggleGroup = categoriesContent.GetComponent<ToggleGroup>();

            if (achievementComplete == null)
                achievementComplete = referenceMap.GetComponentFromReferenceMapComponent<Transform>(MapType.AchievementComplete).gameObject;

            if (SearchField == null)
                searchField = referenceMap.GetType<TMP_InputField>(MapType.AchievementSearchFieldInputField);

            DontDestroyOnLoad(gameObject);

            if (Application.isEditor)
                savePath = Constants.EDITOR_SAVE_PATH;
            else
                savePath = Constants.APPLICATION_SAVE_PATH;

            CreateDirectory();
            SetupAchievementManager();
            initialized = true;
        }

        protected virtual void SetupAchievementManager()
        {
            Database = Instantiate(databaseToLoad) as AdvancedAchievementSystem;
            GenerateFields();

            CalculateAchievementPoints();
            SetupAchievementsUI();
            Oninitialized?.Invoke();
        }

        protected virtual void GenerateFields()
        {
            databaseToLoad.Categories.CopyTo(Database.Categories.ToArray());
            databaseToLoad.Achievements.CopyTo(Database.Achievements.ToArray());
            Database.Display = databaseToLoad.Display;
        }

        public virtual void SetupAchievementsUI()
        {
            for (int i = 0; i < Database.Achievements.Count; i++)
            {
                Achievement achievement = GetAchievement(Database.Achievements[i].Achievement);

                if (Database.Display == AdvancedAchievementSystem.DisplayType.Value)
                    achievement.Map.Maps.Find(c => c.MapType == MapType.AchievementProgressText).GetValue<TMP_Text>().text = Database.Achievements[i].CurrentValue + "/" + Database.Achievements[i].NeededValue;
                else
                    achievement.Map.Maps.Find(c => c.MapType == MapType.AchievementProgressText).GetValue<TMP_Text>().text = $"{((Database.Achievements[i].CurrentValue * 100) / Database.Achievements[i].NeededValue).ToString()}%";

                Slider progressSlider = achievement.Map.Maps.Find(c => c.MapType == MapType.AchievementProgressSlider).GetValue<Slider>();
                progressSlider.maxValue = Database.Achievements[i].NeededValue;
                progressSlider.value = Database.Achievements[i].CurrentValue;

                UpdateAchievementCompleteOverlay(Database.Achievements[i]);
            }
        }

        #region Achievement Update Methods

        public virtual void UpdateAchievementsByKey(AchievementKey key, Action<AchievementList> OnUpdateCallback = null, Action<AchievementList> OnAchievementCompleteCallback = null)
        {
            foreach (var achievement in Database.Achievements)
                if (achievement.Keys.Contains(key))
                    UpdateAchievement(achievement, OnUpdateCallback, OnAchievementCompleteCallback);
        }

        public virtual void UpdateAchievementsByKeys(AchievementKey[] keys, Action<AchievementList> OnUpdateCallback = null, Action<AchievementList> OnAchievementCompleteCallback = null)
        {
            foreach (var achievement in Database.Achievements)
            {
                if (achievement.Keys.Intersect(keys).Any())
                {
                    UpdateAchievement(achievement, OnUpdateCallback, OnAchievementCompleteCallback);
                }
            }
        }

        public virtual void UpdateAchievement(AchievementList achievement, Action OnUpdateCallback = null, Action OnAchievementCompleteCallback = null)
        {
            var _Achievement = Database.Achievements.Find(i => i.Achievement == achievement);
            Achievement achievementObject = GetAchievement(achievement);

            if (_Achievement == null || achievementObject == null)
            {
                Debug.LogError($"{achievement} Achievement is null!");
                return;
            }

            if (!_Achievement.Complete)
            {
                if (_Achievement.RequiresCompletedAchievement)
                {
                    var requiredAchievement = Database.Achievements.Find(i => i.Achievement == _Achievement.RequiresCompletedAchievementID);
                    if (requiredAchievement != null)
                        if (requiredAchievement.Complete)
                            UpdateValues(_Achievement);
                }
                else
                    UpdateValues(_Achievement);

                void UpdateValues(BaseAchievement _Achievement)
                {
                    _Achievement.CurrentValue++;
                    UpdateAchievementUI(_Achievement);

                    OnUpdateCallback?.Invoke();
                    _Achievement.OnUpdate?.Invoke();

                    if (_Achievement.CurrentValue >= _Achievement.NeededValue)
                    {
                        _Achievement.Complete = true;
                        UpdateAchievementCompleteOverlay(_Achievement);
                        CalculateAchievementPoints();
                        completedAchievements.Add(_Achievement);

                        if (!completingAchievements)
                            StartCoroutine(AchievementCompleted());

                        if (_Achievement.HiddenAchievement)
                            achievementObject.gameObject.SetActive(true);

                        _Achievement.CompleteTimeDate = DateTime.Now.ToString(Database.DateTimeFormat);

                        OnAchievementCompleteCallback?.Invoke();
                        _Achievement.OnComplete?.Invoke();
                    }
                }
            }
        }

        public virtual void UpdateAchievement(BaseAchievement achievement, Action<AchievementList> OnUpdateCallback = null, Action<AchievementList> OnAchievementCompleteCallback = null)
        {
            if (!achievement.Complete)
            {
                if (achievement.RequiresCompletedAchievement)
                {
                    var requiredAchievement = Database.Achievements.Find(i => i.Achievement == achievement.RequiresCompletedAchievementID);
                    if (requiredAchievement != null)
                        if (requiredAchievement.Complete)
                            UpdateValues(achievement);
                }
                else
                    UpdateValues(achievement);

                void UpdateValues(BaseAchievement achievement)
                {
                    achievement.CurrentValue++;
                    UpdateAchievementUI(achievement);

                    OnUpdateCallback?.Invoke(achievement.Achievement);
                    achievement.OnUpdate?.Invoke();

                    if (achievement.CurrentValue >= achievement.NeededValue)
                    {
                        achievement.Complete = true;
                        UpdateAchievementCompleteOverlay(achievement);
                        CalculateAchievementPoints();
                        completedAchievements.Add(achievement);

                        if (!completingAchievements)
                            StartCoroutine(AchievementCompleted());

                        if (achievement.HiddenAchievement)
                            GetAchievement(achievement.Achievement).gameObject.SetActive(true);

                        achievement.CompleteTimeDate = DateTime.Now.ToString(Database.DateTimeFormat);

                        OnAchievementCompleteCallback?.Invoke(achievement.Achievement);
                        achievement.OnComplete?.Invoke();
                        achievement.AchievementReward?.Execute();
                    }
                }
            }
        }

        public virtual void UpdateAchievementUI(BaseAchievement achievement)
        {
            Achievement ach = GetAchievement(achievement.Achievement);

            if (Database.Display == AdvancedAchievementSystem.DisplayType.Value)
                ach.Map.Maps.Find(i => i.MapType == MapType.AchievementProgressText).GetValue<TMP_Text>().text = achievement.CurrentValue + "/" + achievement.NeededValue;
            else
                ach.Map.Maps.Find(i => i.MapType == MapType.AchievementProgressText).GetValue<TMP_Text>().text = $"{((achievement.CurrentValue * 100) / achievement.NeededValue).ToString()}%";

            var slider = ach.Map.Maps.Find(i => i.MapType == MapType.AchievementProgressSlider).GetValue<Slider>();
            slider.maxValue = achievement.NeededValue;
            slider.value = achievement.CurrentValue;
        }

        public virtual void UpdateAchievementCompleteOverlay(BaseAchievement achievement)
        {
            Achievement ach = GetAchievement(achievement.Achievement);

            var reference = ach.Map.Maps.Find(i => i.MapType == MapType.AchievementCompleteOverlay);

            if (reference != null)
            {
                var img = reference.GetValue<Image>();
                if (img.sprite != null)
                    foreach (var category in Database.Categories)
                        foreach (var subCategory in category.subCategories)
                            if (ach._Category == subCategory.Category)
                            {
                                Color color = subCategory.Color;
                                color.a = subCategory.CompleteAlpha;
                                img.color = IsAchievementComplete(achievement) ? color : new Color(0f, 0f, 0f, 0f);
                                return;
                            }
            }
        }

        #endregion Achievement Update Methods

        #region Achievement Points Calculation

        public virtual void CalculateAchievementPoints()
        {
            totalAchievementPoints = 0;
            currentAchievementPoints = 0;

            foreach (var achievement in Database.Achievements)
            {
                totalAchievementPoints += achievement.AchievementPoints;

                if (achievement.Complete)
                    currentAchievementPoints += achievement.AchievementPoints;
            }

            achievementCompletionText.text = Constants.ACHIEVEMENT_COMPLETION_TEXT + currentAchievementPoints + "/" + totalAchievementPoints;
        }

        #endregion Achievement Points Calculation

        #region Achievement Completion Coroutine

        public virtual IEnumerator AchievementCompleted()
        {
            completingAchievements = true;
            while (completedAchievements.Count > 0)
            {
                referenceMap.Maps.Find(i => i.MapType == MapType.AchievementCompleteDescription).GetValue<TMP_Text>().text = completedAchievements[completedAchievements.Count - 1].AchievementName;
                referenceMap.Maps.Find(i => i.MapType == MapType.AchievementCompleteIcon).GetValue<Image>().sprite = completedAchievements[completedAchievements.Count - 1].Image;
                referenceMap.Maps.Find(i => i.MapType == MapType.AchievementCompleteAchievementPointsText).GetValue<TMP_Text>().text = Constants.ACHIEVEMENT_POINTS_TEXT + completedAchievements[completedAchievements.Count - 1].AchievementPoints.ToString();
                completedAchievements.RemoveAt(completedAchievements.Count - 1);

                if (Database.OnCompleteAudio != null)
                    AudioSource.PlayOneShot(Database.OnCompleteAudio);

                for (float t = 0.0f; t < 1.0f; t += Time.unscaledDeltaTime / fadeInTime)
                {
                    achievementCompleteAlphaGroup.alpha = Mathf.Lerp(achievementCompleteAlphaGroup.alpha, 1, t);
                    yield return null;
                }

                yield return new WaitForSeconds(displayTime);

                for (float t = 0.0f; t < 1.0f; t += Time.unscaledDeltaTime / fadeOutTime)
                {
                    achievementCompleteAlphaGroup.alpha = Mathf.Lerp(achievementCompleteAlphaGroup.alpha, 0, t);
                    yield return null;
                }
            }
            completingAchievements = false;
        }

        #endregion Achievement Completion Coroutine

        #region Achievement Search Methods

        public virtual void SearchAchievements()
        {
            List<CategoryList> categories = new List<CategoryList>();
            if (togglegroup.ActiveToggles().Count() > 0)
                categories = togglegroup.ActiveToggles().FirstOrDefault().gameObject.GetComponent<CategoryBtn>().Categories;

            if (searchField.text != string.Empty)
            {
                foreach (var baseAchievement in Database.Achievements)
                {
                    Achievement achievement = GetAchievement(baseAchievement.Achievement);

                    bool category = false;

                    foreach (var cat in categories)
                    {
                        if (achievement._Category == cat)
                        {
                            category = true;
                            break;
                        }
                    }

                    if (Database.Menu == AdvancedAchievementSystem.DropDownMenu.All)
                    {
                        if (achievement.SearchString.Contains(searchField.text, StringComparison.OrdinalIgnoreCase) && category ||
                            achievement.SearchString.Contains(searchField.text, StringComparison.OrdinalIgnoreCase) && categories[0] == CategoryList.All)
                        {
                            if (IsAchievementHidden(baseAchievement))
                            {
                                if (IsAchievementComplete(baseAchievement))
                                    achievement.gameObject.SetActive(true);
                                else
                                    achievement.gameObject.SetActive(false);
                            }
                            else
                                achievement.gameObject.SetActive(true);
                        }
                        else
                            achievement.gameObject.SetActive(false);
                    }
                    else if (Database.Menu == AdvancedAchievementSystem.DropDownMenu.Incomplete)
                    {
                        if (!baseAchievement.Complete)
                        {
                            if (achievement.SearchString.Contains(searchField.text, StringComparison.OrdinalIgnoreCase) && category && !IsAchievementHidden(baseAchievement) ||
                                achievement.SearchString.Contains(searchField.text, StringComparison.OrdinalIgnoreCase) && categories[0] == CategoryList.All && !IsAchievementHidden(baseAchievement))
                                achievement.gameObject.SetActive(true);
                            else
                                achievement.gameObject.SetActive(false);
                        }
                        else
                            achievement.gameObject.SetActive(false);
                    }
                    else if (Database.Menu == AdvancedAchievementSystem.DropDownMenu.Completed)
                    {
                        if (baseAchievement.Complete)
                        {
                            if (achievement.SearchString.Contains(searchField.text, StringComparison.OrdinalIgnoreCase) && category ||
                                achievement.SearchString.Contains(searchField.text, StringComparison.OrdinalIgnoreCase) && categories[0] == CategoryList.All)
                                achievement.gameObject.SetActive(true);
                            else
                                achievement.gameObject.SetActive(false);
                        }
                        else
                            achievement.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                SearchCategories(categories);
            }
        }

        public virtual void SearchCategories(List<CategoryList> categories)
        {
            if (categories.Count > 0)
                if (categories[0] == CategoryList.All)
                {
                    foreach (var baseAchievement in Database.Achievements)
                    {
                        Achievement achievement = GetAchievement(baseAchievement.Achievement);
                        if (Database.Menu == AdvancedAchievementSystem.DropDownMenu.All)
                        {
                            if (!IsAchievementHidden(baseAchievement))
                                achievement.gameObject.SetActive(true);
                            else if (IsAchievementHidden(baseAchievement) && IsAchievementComplete(baseAchievement))
                                achievement.gameObject.SetActive(true);
                            else
                                achievement.gameObject.SetActive(false);
                        }
                        else if (Database.Menu == AdvancedAchievementSystem.DropDownMenu.Incomplete)
                        {
                            if (!baseAchievement.Complete && !IsAchievementHidden(baseAchievement))
                                achievement.gameObject.SetActive(true);
                            else
                                achievement.gameObject.SetActive(false);
                        }
                        else if (Database.Menu == AdvancedAchievementSystem.DropDownMenu.Completed)
                        {
                            if (baseAchievement.Complete)
                                achievement.gameObject.SetActive(true);
                            else
                                achievement.gameObject.SetActive(false);
                        }
                    }
                    return;
                }

            foreach (var baseAchievement in Database.Achievements)
            {
                Achievement achievement = GetAchievement(baseAchievement.Achievement);
                bool contains = false;

                foreach (var cat in categories)
                {
                    if (achievement._Category == cat)
                    {
                        contains = true;
                        break;
                    }
                }

                if (contains)
                {
                    if (Database.Menu == AdvancedAchievementSystem.DropDownMenu.All)
                    {
                        if (IsAchievementHidden(baseAchievement) && !IsAchievementComplete(baseAchievement))
                            achievement.gameObject.SetActive(false);
                        else
                            achievement.gameObject.SetActive(true);
                    }
                    else if (Database.Menu == AdvancedAchievementSystem.DropDownMenu.Incomplete)
                    {
                        if (!baseAchievement.Complete && !IsAchievementHidden(baseAchievement))
                            achievement.gameObject.SetActive(true);
                        else
                            achievement.gameObject.SetActive(false);
                    }
                    else if (Database.Menu == AdvancedAchievementSystem.DropDownMenu.Completed)
                    {
                        if (baseAchievement.Complete)
                            achievement.gameObject.SetActive(true);
                        else
                            achievement.gameObject.SetActive(false);
                    }
                }
                else
                    achievement.gameObject.SetActive(false);
            }
        }

        protected virtual bool IsAchievementHidden(BaseAchievement achievement)
        {
            return achievement.HiddenAchievement;
        }

        protected virtual bool IsAchievementComplete(BaseAchievement achievement)
        {
            return achievement.Complete;
        }

        public virtual void AchievementDropDownMenu(TMP_Dropdown dropDown)
        {
            Database.Menu = (AdvancedAchievementSystem.DropDownMenu)dropDown.value;
            SearchAchievements();
        }

        #endregion Achievement Search Methods

        #region Save and Load Methods

        public virtual void LoadAchievements(string file)
        {
            if (File.Exists(SavePath() + file))
            {
                Debug.Log("Loading: " + SavePath() + file);
                Database = Instantiate(databaseToLoad) as AdvancedAchievementSystem;
                GenerateFields();

                using (StreamReader stream = new StreamReader(SavePath() + file))
                {
                    string json = stream.ReadToEnd();
                    MergeAchievementsFromJson(json);
                }

                CalculateAchievementPoints();
                SetupAchievementsUI();
            }
        }

        public virtual void LoadAchievements()
        {
            if (File.Exists(SavePath() + fileName))
            {
                Debug.Log("Loading: " + SavePath() + fileName);
                Database = Instantiate(databaseToLoad) as AdvancedAchievementSystem;
                GenerateFields();

                using (StreamReader stream = new StreamReader(SavePath() + fileName))
                {
                    string json = stream.ReadToEnd();
                    MergeAchievementsFromJson(json);
                }

                CalculateAchievementPoints();
                SetupAchievementsUI();
            }
        }

        public virtual void LoadAchievementsFromJson(string json)
        {
            Database = Instantiate(databaseToLoad) as AdvancedAchievementSystem;
            GenerateFields();
            MergeAchievementsFromJson(json);
            CalculateAchievementPoints();
            SetupAchievementsUI();
        }

        protected virtual void MergeAchievementsFromJson(string json)
        {
            var loadedAchievements = JsonUtility.FromJson<BaseAchievement.AchievementContainer>(json).Achievements;

            foreach (var loadedAchievement in loadedAchievements)
            {
                var existingAchievement = Database.Achievements.Find(a => a.Achievement == loadedAchievement.Achievement);
                if (existingAchievement != null)
                    existingAchievement.FromData(loadedAchievement);
            }
        }

        public virtual void SaveAchievements()
        {
            if (!Directory.Exists(SavePath()))
            {
                Debug.Log("Directory not exists, Creating directory: " + SavePath());
                Directory.CreateDirectory(SavePath());
            }

            if (fileName != string.Empty)
            {
                Debug.Log("Saving: " + SavePath() + fileName);

                string json = SaveAchievementsToJson();

                using (StreamWriter stream = new StreamWriter(SavePath() + fileName))
                {
                    stream.Write(json);
                }
            }
        }

        public virtual void SaveAchievements(string file)
        {
            if (!Directory.Exists(SavePath()))
            {
                Debug.Log("Directory not exists, Creating directory: " + SavePath());
                Directory.CreateDirectory(SavePath());
            }

            if (file != string.Empty)
            {
                Debug.Log("Saving: " + SavePath() + file);
                string json = SaveAchievementsToJson();

                using (StreamWriter stream = new StreamWriter(SavePath() + file))
                {
                    stream.Write(json);
                }
            }
        }

        public virtual string SaveAchievementsToJson()
        {
            BaseAchievement.AchievementContainer container = new BaseAchievement.AchievementContainer();
            foreach (var achievement in Database.Achievements)
                container.Achievements.Add(achievement.ToData());
            return JsonUtility.ToJson(container, true);
        }

        public virtual string SaveAchievementsJson(string file)
        {
            SaveAchievements(file);
            return SaveAchievementsToJson();
        }

        public virtual void CreateDirectory()
        {
            if (!Directory.Exists(SavePath()))
            {
                Debug.Log("Directory Does not Exists: " + SavePath() + " Crafting a new path");
                Directory.CreateDirectory(SavePath());
            }
        }

        protected virtual string SavePath()
        {
            return Application.dataPath + savePath.Replace(Application.dataPath, "");
        }



        #endregion Save and Load Methods

        #region Utility Methods

        public static bool ContainsDelegate(Action EventHandler, Delegate prospectiveHandler)
        {
            foreach (Delegate existingHandler in EventHandler.GetInvocationList())
            {
                if (existingHandler == prospectiveHandler)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual Achievement GetAchievement(AchievementList achievement)
        {
            if (Achievements.TryGetValue(achievement, out var ach))
                return ach;

            return null;
        }

        #endregion Utility Methods

        #endregion Methods
    }
}
