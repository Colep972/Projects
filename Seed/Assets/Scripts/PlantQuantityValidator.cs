using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantQuantityValidator : MonoBehaviour
{
    [SerializeField] private TMP_InputField quantityInputField;
    [SerializeField] private Button confirmButton;
    private bool firstSell;

    private void Start()
    {
        quantityInputField.onValueChanged.AddListener(OnInputChanged);
        confirmButton.onClick.AddListener(OnConfirmClicked);
        firstSell = false;
    }

    private void OnInputChanged(string input)
    {
        if (PlantDropdownUI.Instance == null) return;

        PlantsData selectedPlant = PlantDropdownUI.Instance.GetCurrentPlant();
        if (selectedPlant == null) return;

        if (int.TryParse(input, out int value))
        {
            if (value > selectedPlant.number)
            {
                PnjTextDisplay.Instance.DisplayMessagePublic("The max is " + selectedPlant.number);
                quantityInputField.text = selectedPlant.number.ToString();
            }
        }
        else
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("Invalid number ! ");
        }
    }

    private void OnConfirmClicked()
    {
        if (PlantDropdownUI.Instance == null) return;

        PlantsData selectedPlant = PlantDropdownUI.Instance.GetCurrentPlant();
        if (selectedPlant == null)
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("No plant selected.");
            return;
        }

        if (!int.TryParse(quantityInputField.text, out int quantity))
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("Please enter a valid number ! ");
            return;
        }

        if (quantity <= 0)
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("Quantity must be greater than 0.");
            return;
        }

        if (quantity > selectedPlant.number)
        {
            PnjTextDisplay.Instance.DisplayMessagePublic("You only have " + selectedPlant.number + " plants.");
            return;
        }

        if (!firstSell)
        {
            MoneyManager.Instance.FirstSell(quantity);
            firstSell = true;
        }
        else
        {
            MoneyManager.Instance.SellPlants(quantity);
        }
    }
}
