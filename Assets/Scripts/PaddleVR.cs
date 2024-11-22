using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PaddleVR : MonoBehaviour
{

	Transform target;

	float arenaExtents = 10f, extents = 2f;

    private void Awake() {
    }

    void Update() {
		if (target != null) {
			Move();
		}
	}

	public void StartNewGame () {
	}

    public void Move () {
		Vector3 p = transform.position;
		Vector3 intersect = new Plane(transform.up, p).RaycastPoint(new Ray(target.position, target.forward));
		p.x = intersect.x;
		float limit = arenaExtents - extents;
		p.x = Mathf.Clamp(p.x, -limit, limit);
		transform.position = p;
		p = transform.localPosition;
		p.x = Mathf.Clamp(p.x, -limit, limit);
		transform.localPosition = p;
	}

	public void startHover(SelectEnterEventArgs args) {
		if (target == null) {
			target = args.interactorObject.transform;
		}
	}

	public void endHover(SelectExitEventArgs args) {
		if (target == args.interactorObject.transform) {
			target = null;
		}
	}

}
