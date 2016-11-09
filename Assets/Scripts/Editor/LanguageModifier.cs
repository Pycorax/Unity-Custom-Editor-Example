///
///     LanguageModifier.cs
///     ===========================================
///     Written by  Tng Kah Wei
///     For         Unity Custom Inspector Lecture for Trident College of IT
///
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

/// <summary>
/// Class for handling the switching between Languages in the editor
/// </summary>
[InitializeOnLoad]          // Tell Unity to call the static constructor on launch
public class LanguageModifier
{
    /// <summary>
    /// The types of strings in the UI that should change based on language
    /// </summary>
    public enum UIString
    {
        Yes,
        No,
        Others,
        LookAt,
        TranslatedLookAt,
        SpecOverviewTitle,
        SpecPrefabFolder,
        Refresh,
        Loading,
        Loaded,
        Strength,
        Agility,
        Accuracy,
        Open,
        Total,
        Min,
        Average,
        Max,
        Speed,
        SpeedShortForm,
        Damage,
        DamageShortForm
    }

    /// <summary>
    /// Stores a dictionary of key-localized string pairs
    /// </summary>
    private static List<Dictionary<UIString, string>> dictionary = new List<System.Collections.Generic.Dictionary<UIString, string>>();

    /// <summary>
    /// The current language
    /// </summary>
    public static Language Language { get; private set; }

    /// <summary>
    /// Static Constructor for setting up the language string bindings
    /// </summary>
    static LanguageModifier()
    {
        // Build Dictionary
        dictionary = new List<Dictionary<UIString, string>>();
        for (int i = 0; i < Enum.GetNames(typeof(UIString)).Length; ++i)
        {
            dictionary.Add(new Dictionary<UIString, string>());
        }

        // Set Up Language Strings
        addToDictionary(UIString.Yes, "Yes", "はい");
        addToDictionary(UIString.No, "No", "いえ");
        addToDictionary(UIString.Others, "Others", "別の");
        addToDictionary(UIString.LookAt, "LookAt", "見る");
        addToDictionary(UIString.TranslatedLookAt, "Translated Look At", "カメラの目標を変更");
        addToDictionary(UIString.SpecOverviewTitle, "Spec Overview", "全 Spec");
        addToDictionary(UIString.SpecPrefabFolder, "Spec Prefabs Folder", "Spec Prefabs フォルダ"); 
        addToDictionary(UIString.Refresh, "Refresh", "再読み込み");
        addToDictionary(UIString.Loading, "Loading", "読み込み");
        addToDictionary(UIString.Loaded, "Loaded", "完了");
        addToDictionary(UIString.Strength, "Strength", "力");
        addToDictionary(UIString.Agility, "Agility", "敏速");
        addToDictionary(UIString.Accuracy, "Accuracy", "正確さ");
        addToDictionary(UIString.Open, "Open", "開");
        addToDictionary(UIString.Total, "Total", "合計");
        addToDictionary(UIString.Min, "Min", "最小");
        addToDictionary(UIString.Average, "Average", "平均");
        addToDictionary(UIString.Max, "Max", "最大");
        addToDictionary(UIString.Speed, "Speed", "速度");
        addToDictionary(UIString.SpeedShortForm, "S", "速度");
        addToDictionary(UIString.Damage, "Damage", "力");
        addToDictionary(UIString.DamageShortForm, "D", "力");
    }

    /// <summary>
    /// Menu Item for switching to English
    /// </summary>
    [MenuItem("Language/English")]
    private static void English()
    {
        if (Language == Language.English) return;

        Language = Language.English;
        EditorUtility.DisplayDialog("Language", "Changed to English!", "Ok");
    }

    /// <summary>
    /// Menu Item for switching to Japanese
    /// </summary>
    [MenuItem("Language/日本語")]
    private static void Japanese()
    {
        if (Language == Language.日本語) return;

        Language = Language.日本語;
        EditorUtility.DisplayDialog("言語", "日本語に変更", "はい");
    }

    /// <summary>
    /// Function to get the string in the correct language
    /// </summary>
    /// <param name="key">The type of string to get</param>
    /// <returns>The string in the correct current language set</returns>
    public static string GetString(UIString key)
    {
        // Error Check
        if (!dictionary[(int) Language].ContainsKey(key) || dictionary[(int)Language][key] == "")
        {
            return "NO STRING";
        }

        return dictionary[(int)Language][key];
    }

    /// <summary>
    /// Function to add a key-string value to the dictionary
    /// </summary>
    /// <param name="key">The key to add this at</param>
    /// <param name="englishString">The English version of the string</param>
    /// <param name="japaneseString">The Japanese version of the string</param>
    private static void addToDictionary(UIString key, string englishString, string japaneseString = "")
    {
        dictionary[(int)Language.English].Add(key, englishString);
        dictionary[(int)Language.日本語].Add(key, japaneseString);
    }
}