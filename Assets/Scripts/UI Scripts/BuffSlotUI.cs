using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuffSlotUI : MonoBehaviour, IPointerClickHandler
{
    public int buffID;
    public Image icon;
    public Slider timeSlider;

    private void FixedUpdate()
    {
        UpdateSlider(GameManager.instance.player.bm.activeBuffs[buffID].RemainingTime);
    }

    public void SetValues(Sprite newIcon, float duration)
    {
        icon.sprite = newIcon;
        timeSlider.maxValue = duration;
        timeSlider.value = duration;
    }

    public void UpdateSlider(float newVal)
    {
        timeSlider.value = newVal;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            CancelBuff();
        }
    }

    private void CancelBuff()
    {
        GameManager.instance.player.bm.RemoveBuff(buffID);
    }

}
