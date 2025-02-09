using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Menus.Localization
{
    public class LocalizationSelector : MonoBehaviour
    {
        public void ChangeLocale(int localeID)
        {
            StartCoroutine(SetLocale(localeID));
        }
        
        IEnumerator SetLocale(int localeId)
        {
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
        }
    }
}