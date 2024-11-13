using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] MonoBehaviour playerControls;
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] MeshRenderer ShipVisible;
    [SerializeField] BoxCollider ShipCollision;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name} **Triggered by** {other.gameObject.name}");
        playerControls.enabled = false;
        explosionVFX.Play();
        ShipVisible.enabled = false;
        ShipCollision.enabled = false;
        Invoke("ReLoadLevel", 1f);
    }

    private void ReLoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings <= SceneManager.GetActiveScene().buildIndex + 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
