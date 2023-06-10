using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] private int _slotsCount = 4;
	public string[] Slots { get; private set; }

	public int[] InSlotCount { get; private set; }

	public void Awake()
	{
		Slots = new string[_slotsCount];
		InSlotCount = new int[_slotsCount];
		LoadInventory();
		GlobalEventControl.ItemDrop += RemoveItem;
	}

	public void TryToPutItem (string ItemName, GameObject ItemObject, bool IsStaketable)
	{

		for (int i = 0; i < _slotsCount; i++)
		{
			if (Slots[i] == "" || Slots[i] == null || (Slots[i] == ItemName && IsStaketable))
			{
				AddItem(ItemName, i);
				GameObject l = ItemObject;
				gameObject.GetComponent<PlayerMainScript>().ItemList.Remove(ItemObject);
				Destroy(l);
				break;
			}
		}
	}

	public void AddItem (string ItemName, int SlotNumber)
	{
		if (Slots[SlotNumber] == ItemName)
		{
			InSlotCount[SlotNumber] += 1;
		}
		else
		{
			Slots[SlotNumber] = ItemName;
			InSlotCount[SlotNumber] = 1;
		}
		SaveInventory();
		GlobalEventControl.UpdateInventory();
	}
    public void RemoveItem (int SlotNumber)
	{
		Slots[SlotNumber] = "";
		SaveInventory();
		GlobalEventControl.UpdateInventory();
	}
	
	private void SaveInventory()
	{
		try
		{
			var sw = new StreamWriter(Application.dataPath + "/Resources/Inventory.sg");
			for (int i = 0; i < Slots.Length; i++)
			{
				sw.WriteLine("" + Slots[i]);
				sw.WriteLine("" + InSlotCount[i]);
			}
			sw.Flush();
			sw.Close();
		}
		catch (System.Exception)
		{

			throw;
		}
	}
	private void LoadInventory()
	{
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Resources/Inventory.sg", true);
		if (streamReader != null)
		{
			while (!streamReader.EndOfStream)
			{
				for (int i = 0; i < Slots.Length; i++)
				{
					Slots[i] = "" + System.Convert.ToString(streamReader.ReadLine());
					InSlotCount[i] = 0 + int.Parse(streamReader.ReadLine());
				}
			}
		}
		streamReader.Close();
	}
}
