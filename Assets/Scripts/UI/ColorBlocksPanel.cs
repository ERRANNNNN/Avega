using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ColorBlocksPanel : MonoBehaviour
{
    [SerializeField] private Cube cube;
    [SerializeField] private Transform colorBlockTF;
    [SerializeField] private Transform colorBlocksPanelTF;
    private List<Color> colors = new List<Color>();
    public List<ColorBlock> colorBlocks = new List<ColorBlock>();

    private void Start()
    {
        colors = cube.materials.Select(material => material.color).ToList();

        foreach (Color color in colors)
        {
            ColorBlock colorBlock = Instantiate(colorBlockTF, colorBlocksPanelTF).GetComponent<ColorBlock>();
            colorBlocks.Add(colorBlock);
            colorBlock.SetColor(color);
        }
    }
}
