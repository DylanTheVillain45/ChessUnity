using UnityEngine;

public class Tile : MonoBehaviour
{
    public int y;
    public int x;

    public void OnMouseDown()
    {
        MoveManager.instance.HandleClick(this.gameObject);
    }
}