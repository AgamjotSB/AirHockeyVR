using TMPro;
using UnityEngine;

public class PaddleVR : MonoBehaviour
{

	[SerializeField] Transform target;

	float arenaExtents = 10f, extents = 2f;

    private void Awake() {
    }

    void Update() {
        Move();
    }

	public void StartNewGame () {
	}

    public void Move ()
	{
		Vector3 p = transform.position;
		p.x = target.position.x;
		float limit = arenaExtents - extents;
		p.x = Mathf.Clamp(p.x, -limit, limit);
		transform.position = p;
		p = transform.localPosition;
		p.x = Mathf.Clamp(p.x, -limit, limit);
		transform.localPosition = p;
	}

}
