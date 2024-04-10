using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private Button ReturnButton;

    public void OnReturnTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
