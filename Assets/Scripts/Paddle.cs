using TMPro;
using UnityEngine;

public class Paddle : MonoBehaviour
{
	static readonly int
		emissionColorId = Shader.PropertyToID("_EmissionColor"),
		faceColorId = Shader.PropertyToID("_FaceColor"),
		timeOfLastHitId = Shader.PropertyToID("_TimeOfLastHit");

	[SerializeField]
	TextMeshPro scoreText;

	[SerializeField]
	MeshRenderer goalRenderer;

	[SerializeField, ColorUsage(true, true)]
	Color goalColor = Color.white;

	[SerializeField, Min(0f)]
	float
		minExtents = 4f,
		maxExtents = 4f;

	int score;

	float extents;

	Material goalMaterial, paddleMaterial, scoreMaterial;

	void Awake ()
	{
		goalMaterial = goalRenderer.material;
		goalMaterial.SetColor(emissionColorId, goalColor);
		paddleMaterial = GetComponent<MeshRenderer>().material;
		scoreMaterial = scoreText.fontMaterial;
		SetScore(0);
	}

	public void StartNewGame ()
	{
		SetScore(0);
	}

	public bool HitBall (float ballX, float ballExtents, out float hitFactor)
	{
		hitFactor =
			(ballX - transform.localPosition.x) /
			(extents + ballExtents);

		bool success = -1f <= hitFactor && hitFactor <= 1f;
		if (success)
		{
			paddleMaterial.SetFloat(timeOfLastHitId, Time.time);
		}
		return success;
	}

	public bool ScorePoint (int pointsToWin)
	{
		goalMaterial.SetFloat(timeOfLastHitId, Time.time);
		SetScore(score + 1, pointsToWin);
		return score >= pointsToWin;
	}

	void SetScore (int newScore, float pointsToWin = 1000f)
	{
		score = newScore;
		scoreText.SetText("{0}", newScore);
		scoreMaterial.SetColor(faceColorId, goalColor * (newScore / pointsToWin));
		SetExtents(Mathf.Lerp(maxExtents, minExtents, newScore / (pointsToWin - 1f)));
	}

	void SetExtents (float newExtents)
	{
		extents = newExtents;
		Vector3 s = transform.localScale;
		s.x = 2f * newExtents;
		transform.localScale = s;
	}
}
