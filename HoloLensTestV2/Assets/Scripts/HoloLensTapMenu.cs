using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;

public class HoloLensTapMenu : MonoBehaviour
{
    private Vector3 direction;
    GestureRecognizer gesture;

    void Awake()
    {
        gesture = new GestureRecognizer();
        gesture.Tapped += (args) =>
        {
            RaycastHit hitInfo;
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            Debug.DrawLine(headPosition, headPosition + gazeDirection * 20, Color.red, 100.0f);

            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
            {
                if (hitInfo.collider.gameObject.name == "Play")
                {
                    SceneManager.LoadScene(1);
                }
                else if (hitInfo.collider.gameObject.name == "Play Again")
                {
                    SceneManager.LoadScene(1);
                }
                else if (hitInfo.collider.gameObject.name == "Menu")
                {
                    SceneManager.LoadScene(0);
                }
                else if (hitInfo.collider.gameObject.name == "Quit")
                {
                    Application.Quit();
                }
            }
            else
            {
                print("No Hit");
            }
        };
        gesture.StartCapturingGestures();
    }
}


