using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManage : MonoBehaviour {

    public GameObject inventoryScreen;

    private void Awake()
    {
        inventoryScreen.SetActive(false);
    }

    public void LateUpdate()
    {
        checkPause();
    }

    void checkPause()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            pauseGame(inventoryScreen.activeInHierarchy);
        }
    }

    void pauseGame(bool active)
    {
        Time.timeScale = System.Convert.ToSingle(active);
        inventoryScreen.SetActive(!active);
    }





















}
