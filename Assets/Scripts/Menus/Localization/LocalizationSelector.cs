using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Menus.Localization
{
    public class LocalizationSelector : MonoBehaviour
    {
        private int currentLocaleId
        {
            get => PlayerPrefs.GetInt("Locale");
            set => PlayerPrefs.SetInt("Locale", value);
        }
        
        public void Awake()
        {
            ChangeLocale(currentLocaleId);
        }

        public void ChangeLocale(int localeID)
        {
            currentLocaleId = localeID;
            StartCoroutine(SetLocale(localeID));
        }
        
        IEnumerator SetLocale(int localeId)
        {
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
        }
    }
}