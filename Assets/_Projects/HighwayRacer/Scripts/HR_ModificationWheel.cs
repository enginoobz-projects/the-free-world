﻿using UnityEngine;
using UnityEngine.UI;

namespace Project.HighwayRacer {
  public class HR_ModificationWheel : MonoBehaviour {
    public int wheelIndex;

    private int wheelPrice {
      get { return HR_Wheels.Instance.wheels[wheelIndex].price; }
    }

    private bool unlocked;
    private Text priceLabel;
    private Image priceImage;

    void Start() {
      priceLabel = GetComponentInChildren<Text>();
      priceImage = priceLabel.GetComponentInParent<Image>();
      unlocked = HR_Wheels.Instance.wheels[wheelIndex].unlocked;
    }

    public void OnClick() {
      if (!unlocked) {
        BuyWheel();
        return;
      }

      HR_ModHandler handler = GameObject.FindObjectOfType<HR_ModHandler>();

      handler.ChangeWheels(wheelIndex);
    }

    void Update() {
      string currentWheelString = wheelIndex.ToString();

      if (wheelPrice <= 0 && !unlocked) {
        PlayerPrefs.SetInt(HR_MainMenuHandler.Instance.currentCar.transform.name + "OwnedWheel" + currentWheelString,
          1);
        unlocked = true;
      }

      if (PlayerPrefs.HasKey(HR_MainMenuHandler.Instance.currentCar.transform.name + "OwnedWheel" +
                             currentWheelString) ||
          HR_Wheels.Instance.wheels[wheelIndex].unlocked)
        unlocked = true;
      else
        unlocked = false;

      if (!unlocked) {
        if (!priceImage.gameObject.activeSelf)
          priceImage.gameObject.SetActive(true);
        if (priceLabel.text != wheelPrice.ToString())
          priceLabel.text = wheelPrice.ToString();
      }
      else {
        if (priceImage.gameObject.activeSelf)
          priceImage.gameObject.SetActive(false);
        if (priceLabel.text != "UNLOCKED")
          priceLabel.text = "UNLOCKED";
      }
    }

    void BuyWheel() {
      int playerCoins = PlayerPrefs.GetInt("Currency");
      string currentWheelString = wheelIndex.ToString();

      if (playerCoins >= wheelPrice) {
        HR_ModHandler handler = GameObject.FindObjectOfType<HR_ModHandler>();
        handler.BuyProperty(wheelPrice,
          HR_MainMenuHandler.Instance.currentCar.transform.name + "OwnedWheel" + currentWheelString);
      }
    }
  }
}