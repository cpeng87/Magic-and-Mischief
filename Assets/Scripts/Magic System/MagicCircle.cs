using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class MagicCircle : MonoBehaviour
{
    public UnityEngine.UI.Extensions.UICircle outerCircle;
    public UnityEngine.UI.Extensions.UILineConnector connectionLines;
    public UnityEngine.UI.Extensions.UILineConnector evenConnectionLines;
    public List<GameObject> innerCircles;

    public int numMaterials;
    public Color color;
    public float innerCircleDiameter;

    public void Start()
    {
        Make();   // testing
    }

    public void Make()
    {
        Reset();
        if (numMaterials < 2)
        {
            Debug.Log("Not enough materials for a magic circle (needs 2+).");
            return;
        }

        float angleIncrement = 360f / numMaterials;
        float radius = outerCircle.GetDiameter() / 2f;
        outerCircle.SetBaseColor(color);
        connectionLines.SetBaseColor(color);
        evenConnectionLines.SetBaseColor(color);

        for (int i = 0; i < numMaterials; i++)
        {
            float angle = 90 + angleIncrement * i;
            float rad = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(rad) * radius;
            float y = Mathf.Sin(rad) * radius;

            GameObject miniObj = new GameObject("InnerCircle " + i, typeof(RectTransform));
            miniObj.transform.SetParent(transform, false);

            var miniCircle = miniObj.AddComponent<UnityEngine.UI.Extensions.UICircle>();
            miniCircle.SetFill(false);
            miniCircle.SetBaseColor(color);

            RectTransform rect = miniObj.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(innerCircleDiameter, innerCircleDiameter);
            rect.anchoredPosition = new Vector2(x, y);

            innerCircles.Add(miniObj);
        }

        if (innerCircles.Count <= 4)
        {
            evenConnectionLines.gameObject.SetActive(false);
            RectTransform[] transforms = new RectTransform[innerCircles.Count + 1];
            for (int i = 0; i < innerCircles.Count + 1; i++)
            {
                transforms[i] = innerCircles[i % innerCircles.Count].GetComponent<RectTransform>(); 
            }

            connectionLines.transforms = transforms;
        }
        else if (innerCircles.Count % 2 == 0)
        {
            evenConnectionLines.gameObject.SetActive(true);
            RectTransform[] oddTransforms = new RectTransform[(innerCircles.Count / 2) + 1];
            RectTransform[] evenTransforms = new RectTransform[(innerCircles.Count / 2) + 1];
            for (int i = 0; i < innerCircles.Count + 2; i++)
            {
                if (i % 2 == 0)
                {
                    evenTransforms[i / 2] = innerCircles[i % innerCircles.Count].GetComponent<RectTransform>(); 
                }
                else
                {
                    oddTransforms[i / 2] = innerCircles[i % innerCircles.Count].GetComponent<RectTransform>(); 
                }
            }

            connectionLines.transforms = oddTransforms;
            evenConnectionLines.transforms = evenTransforms;
        }
        else
        {
            evenConnectionLines.gameObject.SetActive(false);
            RectTransform[] transforms = new RectTransform[innerCircles.Count + 1];
            for (int i = 0; i < (innerCircles.Count * 2) + 1; i = i + 2)
            {
                transforms[(i / 2)] = innerCircles[i % innerCircles.Count].GetComponent<RectTransform>(); 
            }

            connectionLines.transforms = transforms;
        }
    }

    public void Reset()
    {
        connectionLines.transforms = null;
        evenConnectionLines.transforms = null;
        foreach (GameObject circle in innerCircles)
        {
            Destroy(circle);
        }
        innerCircles = new List<GameObject>();
    }

    public void Add()
    {
        numMaterials += 1;
        Make();
    }

    public void Subtract()
    {
        if (numMaterials == 2)
        {
            Debug.Log("Can't go below 2 buddy");
        }
        else
        {
            numMaterials -= 1;
        }
        Make();
    }
}
