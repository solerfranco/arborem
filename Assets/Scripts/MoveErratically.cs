using System.Collections;
using UnityEngine;

public class MoveErratically : MonoBehaviour
{
    private Vector3 direction;
    private Root root;
    private float timeSinceSpawn;

    private void Awake()
    {
        root = GetComponentInParent<Root>();
        InvokeRepeating(nameof(ChangeDirection), 0f, Random.Range(1f, 2.5f));
    }

    void Update()
    {
        transform.position += direction * root.speed * Time.deltaTime;
        timeSinceSpawn += Time.deltaTime;
    }

    void ChangeDirection()
    {
        direction = new Vector2(Random.Range(root.randomLimits.x, root.randomLimits.y), -0.25f).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(timeSinceSpawn > 1.5f && !other.isTrigger && other.transform != transform.parent && root != null)
        {
            root.Stop();
            Destroy(gameObject);
            print(other.transform.name);
            if (other.TryGetComponent<Trash>(out _))
            {
                GameController.Instance.RottenRoots++;
            }
        }
    }
}
