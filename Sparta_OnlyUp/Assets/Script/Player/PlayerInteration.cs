using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteration : MonoBehaviour
{
    [Header("===Interation Position===")]
    public Transform interationPosi;
    [SerializeField] private float maxdistance;

    [SerializeField] private int currItemIdx = -1;

    private void Start()
    {
        maxdistance = 2f;
        StartCoroutine(Raycast());
    }

    IEnumerator Raycast() 
    {
        while (true) 
        {
            Interactive();
            yield return new WaitForSeconds(0.02f);
        }
    
    }

    private void Interactive() 
    {
        RaycastHit hit;
        Ray ray = new Ray(interationPosi.position , interationPosi.forward);

        Debug.DrawRay(ray.origin , ray.direction * maxdistance , Color.red);

        if(Physics.Raycast(ray, out hit , maxdistance , LayerManager.Instance.InterativeLayer)) 
        {
            // 같은 아이템 raycast하면 
            if (currItemIdx == hit.transform.GetComponent<ItemCrops>().ItemNum)
                return;

            // hit된 아이템으로 
            currItemIdx = hit.transform.GetComponent<ItemCrops>().ItemNum;
        }
        else 
        {
            // raycast를 벗어나면 -1로
            currItemIdx = -1;
        }
    }
}
