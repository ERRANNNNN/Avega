using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorBlock : MonoBehaviour
{
    public Color color;
    [SerializeField] private Image blockImage;
    [SerializeField] private TextMeshProUGUI blockCountText;
    private int count = 0;

    private void Start()
    {
        Debug.Log(blockCountText);
        blockCountText.text = count.ToString();
    }

    public void SetColor(Color color)
    {
        this.color = color;
        blockImage.color = color;
    }

    public void changeCount()
    {
        count++;
        blockCountText.text = count.ToString();
    }
}
