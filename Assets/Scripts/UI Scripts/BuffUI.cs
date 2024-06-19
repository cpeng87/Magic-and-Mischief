using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUI : MonoBehaviour
{
    public GameObject buffPanel;
    public GameObject buffIconPrefab;
    public List<GameObject> buffIcons = new List<GameObject>();

    void Start()
    {
        BuffEventHandler.OnBuffChanged += SetupBuffs;
        SetupBuffs();
    }

    private void ClearBuffs()
    {
        foreach (Transform buff in buffPanel.transform)
        {
            Destroy(buff.gameObject);
        }
    }
    private void SetupBuffs()
    {
        ClearBuffs();
        List<Buff> buffs = GameManager.instance.player.bm.activeBuffs;
        int counter = 0;
        foreach (Buff buff in buffs)
        {
            GameObject newBuffIcon = Instantiate(buffIconPrefab, buffPanel.transform);
            BuffSlotUI buffSlotUI = newBuffIcon.GetComponent<BuffSlotUI>();
            buffSlotUI.SetValues(buff.buffImage, buff.Duration);
            buffSlotUI.buffID = counter;

            counter += 1;
        }
    }

    private void OnDestroy()
    {
        BuffEventHandler.OnBuffChanged -= SetupBuffs;
    }
}
