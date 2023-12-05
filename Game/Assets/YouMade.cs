using UnityEngine;
using TMPro;

public class YouMade : MonoBehaviour
{
    [SerializeField] private TMP_Text _drinkText;
    [SerializeField] private TMP_Text _descriptionText;

    private void Update()
    {
        _drinkText.text = StaticData.currentDrink;
        _descriptionText.text = StaticData.drinkDescription;
    }
}
