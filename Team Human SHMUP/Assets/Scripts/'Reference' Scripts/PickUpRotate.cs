﻿// Decompiled with JetBrains decompiler
// Type: PickUpRotate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 856A15C1-8344-4F86-8FC2-8971B0BA3EE1
// Assembly location: C:\Users\DanielsSickPC\Desktop\Shmup2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PickUpRotate : MonoBehaviour
{
  private float rotationSpeed = 100f;
  private float movementSpeed = 25f;
  private Transform myTransform;
  public Material[] material;
  private int rand;

  private void Start()
  {
    this.rand = Random.Range(1, 4);
    this.myTransform = this.transform;
    this.GetComponent<Renderer>().material = this.material[this.rand - 1];
  }

  private void Update()
  {
    this.myTransform.position += Time.deltaTime * this.movementSpeed * this.transform.forward;
    this.transform.Rotate(0.0f, 0.0f, this.rotationSpeed * Time.deltaTime);
    Object.Destroy((Object) this.gameObject);
  }

  private void OnTriggerEnter(Collider coll)
  {
    if (!(coll.gameObject.tag == "Player"))
      return;
    MonoBehaviour.print((object) "Herbalerb");
    Object.Destroy((Object) this.gameObject);
  }
}
