using UnityEngine;
using System.Collections;

public class GridScript : MonoBehaviour
{

  public Transform CellPrefab;
  public Vector3 Size;
  public Transform[,] Grid;

  // Use this for initialization
  void Start()
  {
    CreateGrid();
	SetRandomNumbers();
	SetAdjacents();
  }

  void CreateGrid()
  {
	Grid = new Transform[(int)Size.x, (int)Size.z];
    for (int x = 0; x < Size.x; x++)
    {
      for (int z = 0; z < Size.z; z++)
      {
		Transform newCell;
		newCell = (Transform)Instantiate(CellPrefab, new Vector3(x, 0, z), Quaternion.identity);
		newCell.name = string.Format("({0}, 0, {1})", x, z);
		newCell.parent = transform;
		newCell.GetComponent<CellScript>().Position = new Vector3(x, 0, z);
		Grid[x, z] = newCell;
      }
    }
  }

  void SetRandomNumbers()
  {
	foreach(Transform child in transform)
	{
	  int weight = Random.Range(0, 10);
	  child.GetComponentInChildren<TextMesh>().text = weight.ToString();
	  child.GetComponent<CellScript>().Weight = weight;
	}
  }


  void SetAdjacents()
  {
	for (int x = 0; x < Size.x; x++)
	{
	  for (int z = 0; z < Size.z; z++)
	  {
	    Transform cell;
		cell = Grid[x, z];
		CellScript cscript = cell.GetComponent<CellScript>();
		
		if (x - 1 >= 0)
		{
		  cscript.Adjacents.Add(Grid[x - 1, z]);
		}

		if (x + 1 < Size.x)
		{
		  cscript.Adjacents.Add(Grid[x + 1, z]);
		}

		if (z - 1 >= 0)
		{
		  cscript.Adjacents.Add(Grid[x, z - 1]);
		}
		
		if (z + 1 < Size.z)
		{
		  cscript.Adjacents.Add(Grid[x, z + 1]);	
		}

	  }
	}
  }

  void Update()
  {
	if (Input.GetKeyDown(KeyCode.F1))
	{
		Application.LoadLevel(0);
	}
  }
}
