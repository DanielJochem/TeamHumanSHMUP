// Decompiled with JetBrains decompiler
// Type: Lazor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 856A15C1-8344-4F86-8FC2-8971B0BA3EE1
// Assembly location: C:\Users\DanielsSickPC\Desktop\Shmup2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Lazor : MonoBehaviour
{
  private float projectileSpeed = 400f;
  private float lifeTimeDuration = 0.8f;
  private float damage = 20f;
  private GameManager gameManager;
  private Transform myTransform;
  private float lifeTime;

  private void Start()
  {
    this.myTransform = this.transform;
    this.lifeTime = Time.time + this.lifeTimeDuration;
    this.gameManager = Object.FindObjectOfType<GameManager>();
  }

  private void Update()
  {
    this.myTransform.position += Time.deltaTime * this.projectileSpeed * this.transform.forward;
    if ((double) Time.time <= (double) this.lifeTime && (double) this.transform.position.z <= (double) this.gameManager.zBoundry)
      return;
    Object.Destroy((Object) this.gameObject);
  }

  private void OnTriggerEnter(Collider otherObject)
  {
    if (!(otherObject.tag == "Enemy"))
      return;
    otherObject.GetComponent<Enemy>().TakeDamage(this.damage);
    Object.Destroy((Object) this.gameObject);
  }
}
