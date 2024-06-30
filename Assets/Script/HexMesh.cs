using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour {
	MeshCollider meshCollider;
	Mesh hexMesh;
	List<Vector3> vertices;
	List<Color> colors;
	List<int> triangles;
	public void Triangulate (HexCell[] cells) {
		colors = new List<Color>();
		hexMesh.Clear();
		vertices.Clear();
		triangles.Clear();
		for (int i = 0; i < cells.Length; i++) {
			Triangulate(cells[i]);
		}
		hexMesh.vertices = vertices.ToArray();
		hexMesh.triangles = triangles.ToArray();
		hexMesh.RecalculateNormals();
		meshCollider.sharedMesh = hexMesh;
		hexMesh.colors = colors.ToArray();
		Debug.Log(""+vertices.Count+" "+colors.Count);
	}
	void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3) {
		int vertexIndex = vertices.Count;
		vertices.Add(v1);
		vertices.Add(v2);
		vertices.Add(v3);
		triangles.Add(vertexIndex);
		triangles.Add(vertexIndex + 1);
		triangles.Add(vertexIndex + 2);
	}
	void Triangulate (HexCell cell) {
		Vector3 center = cell.transform.localPosition;
		for (int i = 0; i < 6; i++) {
			AddTriangle(
				center,
				center + HexMetrics.corners[i],
				center + HexMetrics.corners[i + 1]
			);
			AddTriangleColor(cell.color);
		}
	}
	void AddTriangleColor (Color color) {
		colors.Add(color);
		colors.Add(color);
		colors.Add(color);
	}
	void Awake () {
		GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
		meshCollider = gameObject.AddComponent<MeshCollider>();
		vertices = new List<Vector3>();
		triangles = new List<int>();
		colors = new List<Color>();
	}
	
}