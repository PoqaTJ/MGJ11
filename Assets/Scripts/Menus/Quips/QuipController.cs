using System;
using Dialogs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menus.Quips
{
    public class QuipController: MonoBehaviour
    {
        [SerializeField] private GameObject _quipUI;
        [SerializeField] private Image _characterImage;
        [SerializeField] private TMP_Text _text;
        
        private const float _showDuration = 2f;
        private float hideTime = 0f;
        
        // temp hopefully
        [SerializeField] private Sprite _tomoyaSprite;
        [SerializeField] private Sprite _akariSprite;
        [SerializeField] private Sprite _butterflySprite;
        [SerializeField] private Sprite _bigbadSprite;
        
        public void Show(string text, Sprite image)
        {
            _characterImage.sprite = image;
            _text.text = text;
            _quipUI.SetActive(true);
            hideTime = Time.timeSinceLevelLoad + _showDuration;
        }

        private void Update()
        {
            if (_quipUI.activeInHierarchy && Time.timeSinceLevelLoad > hideTime)
            {
                _quipUI.SetActive(false);
            }
        }

        public void Show(QuipDefinition definition)
        {
            Show(definition.dialog.Text, GetImage(definition.dialog.Character));
        }
        
        private Sprite GetImage(DialogCharacter character)
        {
            Sprite ret = null;
            switch (character)
            {
                case DialogCharacter.Akari:
                    ret = _akariSprite;
                    break;
                case DialogCharacter.Tomoya:
                    ret = _tomoyaSprite;
                    break;
                case DialogCharacter.Butterfly:
                    ret = _butterflySprite;
                    break;
                case DialogCharacter.BigBad:
                    ret = _bigbadSprite;
                    break;
            }

            return ret;
        }
    }
}