using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string _itemName;
    public string ItemName => _itemName;
    [SerializeField] private bool _isStaketable = false;
    public bool IsStaketable => _isStaketable;

    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMainScript>().ItemList.Add(gameObject);
    }
}
