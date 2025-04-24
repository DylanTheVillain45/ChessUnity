using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    public int y;
    public int x;
    public  TextMeshProUGUI text;

    public void OnMouseDown()
    {
        MoveManager.instance.HandleClick(this.gameObject);
    }

    public void SetText() {
        char file = (char)('a' + x);
        int rank = y + 1;

        text.text = file.ToString() + rank.ToString();
    }

    public void RemoveText() {
        if (text != null && text.transform.parent != null) {
            GameObject parent = text.gameObject.transform.parent.gameObject;
            Destroy(text);
            Destroy(parent);
        }
    }
}