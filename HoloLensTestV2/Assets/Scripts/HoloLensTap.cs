using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;
using TMPro;


public class HoloLensTap : MonoBehaviour
{
    private Vector3 direction;
    public TextMeshProUGUI score;
    private static int TheScore = 0;
    public TextMeshProUGUI time;
    private float TheTime = 60.0f;
    private DateTime localDate;

    GestureRecognizer gesture;

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;

    IEnumerator RemoveLine()
    {
        yield return new WaitForSeconds(0.25f);
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(0);
    }

    void Awake()
    {
        gesture = new GestureRecognizer();
        gesture.Tapped += (args) =>
        {
            RaycastHit hitInfo;
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            Debug.DrawLine(headPosition, headPosition + gazeDirection * 20, Color.red, 10.0f);
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetVertexCount(2);
            lineRenderer.SetPosition(0, headPosition + gazeDirection * 2 - new Vector3(-5, 10, 0));
            lineRenderer.SetPosition(1, headPosition + gazeDirection * 20);


            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
            {
                print("Hit");
                TheScore++;
                score.SetText("Score: " + TheScore);
                Destroy(hitInfo.collider.gameObject);
            }
            else
            {
                print("No Hit");
            }
            StartCoroutine(RemoveLine());
        };
        gesture.StartCapturingGestures();
    }

    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 2;

        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

        score.SetText("Score: " + 0);

        localDate = DateTime.Now;

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            score.SetText("You Scored: " + TheScore);
            TheScore = 0;
        }
    }

    void Update()
    {
        int timeRemaining = (int)(TheTime - (DateTime.Now - localDate).TotalSeconds);
        if (timeRemaining > 0 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            time.SetText("Time Remaining: " + timeRemaining);
        }

        else if (timeRemaining <= 0 && SceneManager.GetActiveScene().buildIndex != 2)
        {
            SceneManager.LoadScene(2);
        }
    }
}
