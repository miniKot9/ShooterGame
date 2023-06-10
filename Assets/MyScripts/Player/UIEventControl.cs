using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEventControl : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI TextOfAmmoCount;
	[SerializeField] private Image[] _slotsImages;
	[SerializeField] private TextMeshProUGUI[] _textOfSlotsCount;
	private Inventory PlayerInventory;

	private void Awake()
	{
		GlobalEventControl.UpdateInventory += UpdateInventory;
	}
	private void Start()
	{
		PlayerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
		UpdateInventory();
	}

	public void ButtonToShoot()
	{
		GlobalEventControl.ShootAction();
		UpdateInventory();
	}

	private void UpdateInventory ()
	{
		TextOfAmmoCount.text = "Ammo : " + GlobalEventControl.PlayerAmmo;
		for (int i = 0; i < _slotsImages.Length; i++)
		{
			if (PlayerInventory.Slots[i] != null && PlayerInventory.Slots[i] != "")
			{
				_slotsImages[i].color = Color.white;
				_slotsImages[i].sprite = Resources.Load<Sprite>("items/weapons/" + PlayerInventory.Slots[i]);

				if (PlayerInventory.InSlotCount[i] > 1) _textOfSlotsCount[i].text = "" + PlayerInventory.InSlotCount[i];
				else _textOfSlotsCount[i].text = "";
			}
			else
			{
				_slotsImages[i].color = new Color(255, 255, 255, 0.3f);
				_slotsImages[i].sprite = null;
				_textOfSlotsCount[i].text = "";
			}
		}
	}

	public void ItemDrop (int SlotNumber)
	{
		GlobalEventControl.ItemDrop(SlotNumber);
	}
}
