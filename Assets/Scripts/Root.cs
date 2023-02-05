using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public LineRenderer line;
    private int vertexIndex = 1;
    public Transform rootTip;
    public float speed = 0.1f; // The speed of the movement
    public Vector2 randomLimits;
    MeshCollider meshCollider;
    private bool stuck;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        rootTip.position = transform.position;
        StartCoroutine(IncreaseIndex());
        Invoke(nameof(Split), Random.Range(4f, 12f));
        meshCollider = GetComponent<MeshCollider>();
    }

    void Update()
    {
        if (stuck) return;
        if (GameController.Instance.gameOver) {
            stuck = true;
            return;
        }
        line.SetPosition(vertexIndex, rootTip.localPosition);
        line.widthMultiplier += 0.01f * Time.deltaTime;

        Mesh mesh = new Mesh();
        line.BakeMesh(mesh);
        meshCollider.sharedMesh = mesh;
    }

    IEnumerator IncreaseIndex()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            line.positionCount++;
            vertexIndex++;
            line.SetPosition(vertexIndex, rootTip.localPosition);
        }
    }

    void Split()
    {
        if (stuck) return;
        GameObject root1 = Instantiate(GameController.Instance.rootPrefab, rootTip.position, Quaternion.identity);
        root1.GetComponent<Root>().randomLimits = new Vector2(-0.7f, 0f);
        GameObject root2 = Instantiate(GameController.Instance.rootPrefab, rootTip.position, Quaternion.identity);
        root2.GetComponent<Root>().randomLimits = new Vector2(0f, 0.7f);
        Destroy(rootTip.gameObject);
        Destroy(this);
    }

    public void Stop()
    {
        stuck = true;
        StopAllCoroutines();
    }
}
