﻿using UnityEngine;

namespace Project.HighwayRacer {
  public class HR_LensFlare : MonoBehaviour {
    private Light parentLight;
    private LensFlare flare;

    void Awake() {
      parentLight = GetComponentInParent<Light>();
      flare = GetComponent<LensFlare>();
    }


    void Update() {
      if (!parentLight || !flare)
        return;

      flare.color = parentLight.color;
      flare.brightness = parentLight.intensity / 15f;
    }
  }
}