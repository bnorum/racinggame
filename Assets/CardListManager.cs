using System.Collections.Generic;
using UnityEngine;

public class CardListManager : MonoBehaviour
{

    public GameObject LayoutGroup;
    public GameObject listPrefab;

    public List<CardSchema> schemaList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        schemaList.AddRange(Resources.LoadAll<CardSchema>(""));

        foreach(CardSchema schema in schemaList) {
            GameObject listObject = Instantiate(listPrefab, LayoutGroup.transform);
            listObject.GetComponent<CardListEntry>().card = schema;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
