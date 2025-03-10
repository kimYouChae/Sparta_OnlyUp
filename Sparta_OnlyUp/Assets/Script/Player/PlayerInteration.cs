using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteration : MonoBehaviour
{
    [Header("===Interation Position===")]
    public Transform interationPosi;
    [SerializeField] private float maxdistance;
    [SerializeField] private int currItemIdx = -1;

<<<<<<< Updated upstream
=======
    [SerializeField] private GameObject currInteracItem;

    public int CurrItemIdx { get => currItemIdx;  }
    public GameObject InteracItem { get => currInteracItem; set => currInteracItem = value; }

>>>>>>> Stashed changes
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
            currInteracItem = hit.transform.gameObject;
        }
        else 
        {
            // raycast를 벗어나면 -1로
            currItemIdx = -1;
            currInteracItem = null;
        }
    }

    // 상호작용 키 눌렀을 때 
    public void PlayerInputInteratKey() 
    {
        // 획득 아이템 없으면 return
        if (currItemIdx <= -1)
            return;

        // 현재 아이템과 index 넘기기 
        PlayerManager.Instance.PlayerUseItem.ObtainItem(currInteracItem , currItemIdx);
    }
}
