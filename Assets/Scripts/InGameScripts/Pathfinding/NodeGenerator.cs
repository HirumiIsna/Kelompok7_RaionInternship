using UnityEngine;
using System.Collections.Generic;

public class NodeGenerator : MonoBehaviour

{
    public GameObject nodePrefab;
    public int width = 17;
    public int height = 17;
    public float spacing = 1.5f;

    public void GenerateGrid()
    {
        ClearGrid();

        Node[,] grid = new Node[width, height];
        Vector2 origin = transform.position;

        float offsetX = (width - 1) * spacing / 2f;
        float offsetY = (height - 1) * spacing / 2f;

        // ngegenerate node di grid sesuai variabel public diatas
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = origin + new Vector2(
                    x * spacing - offsetX,
                    y * spacing - offsetY
                );

                GameObject obj = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
                grid[x, y] = obj.GetComponent<Node>();
            }
        }

        // auto nyambungin node disekelilingnya
        for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Node node = grid[x, y];
                    node.connections = new List<Node>();

                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            // skip diri sendiri
                            if (dx == 0 && dy == 0)
                                continue;

                            int nx = x + dx;
                            int ny = y + dy;

                            // cek batas grid
                            if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                            {
                                node.connections.Add(grid[nx, ny]);
                            }
                        }
                    }
                }
            }
    }

    public void ClearGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (Application.isEditor)
                DestroyImmediate(transform.GetChild(i).gameObject);
            else
                Destroy(transform.GetChild(i).gameObject);
        }
    }
}
