using TMPro;
using UnityEngine;

public class PaddleAI : MonoBehaviour
{
	[SerializeField, Min(0f)]
	float
		speed = 10f,
		maxTargetingBias = 0.75f;

	[SerializeField] Transform target;

	float arenaExtents = 10f, extents = 4f, targetingBias;

    private void Awake() {
        ChangeTargetingBias();
    }

    void Update() {
        Move();
    }

	public void StartNewGame () {
		ChangeTargetingBias();
	}

    public void Move ()
	{
		Vector3 p = transform.localPosition;
		p.x = AdjustByAI(p.x, target.localPosition.x);
		float limit = arenaExtents - extents;
		p.x = Mathf.Clamp(p.x, -limit, limit);
		transform.localPosition = p;
	}

	float AdjustByAI (float x, float target)
	{
		target += targetingBias * extents;
		if (x < target)
		{
			return Mathf.Min(x + speed * Time.deltaTime, target);
		}
		return Mathf.Max(x - speed * Time.deltaTime, target);
	}

	void ChangeTargetingBias () =>
		targetingBias = Random.Range(-maxTargetingBias, maxTargetingBias);

}
