using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mail", menuName = "NPC/MailData")]
public class MailData : ScriptableObject
{
    public int season;
    public int day;
    public int year;
    public Date date;
    public string subject;

    public string sender;
    [TextArea] public string content;
    public bool hasGift;
    public Item gift;
    public bool hasMoney;
    public int money;

    private void OnValidate()
    {
        date = new Date(season, day, year);
    }
}
