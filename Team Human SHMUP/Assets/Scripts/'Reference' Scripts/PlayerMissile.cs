// Decompiled with JetBrains decompiler
// Type: PlayerMissile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 856A15C1-8344-4F86-8FC2-8971B0BA3EE1
// Assembly location: C:\Users\DanielsSickPC\Desktop\Shmup2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
  private float projectileSpeed = 150f;
  private float rotationSpeed = 10f;
  private float lifeTimeDuration = 1f;
  private float damage = 20f;
  private Transform myTransform;
  private GameObject closestEnemyUnit;
  private GameManager gameManager;
  private float lifeTime;

  private void Start()
  {
    this.myTransform = this.transform;
    this.gameManager = Object.FindObjectOfType<GameManager>();
    this.lifeTime = Time.time + this.lifeTimeDuration;
  }

  private void Update()
  {
    this.myTransform.position += Time.deltaTime * this.projectileSpeed * this.transform.forward;
    this.closestEnemyUnit = this.FindClosestEnemyUnit();
    if ((Object) this.closestEnemyUnit != (Object) null)
      this.myTransform.rotation = Quaternion.Slerp(this.myTransform.rotation, Quaternion.LookRotation(this.closestEnemyUnit.transform.position - this.myTransform.position), this.rotationSpeed * Time.deltaTime);
    if ((double) Time.time <= (double) this.lifeTime)
      return;
    Object.Destroy((Object) this.gameObject);
  }

  private GameObject FindClosestEnemyUnit()
  {
    float num = float.PositiveInfinity;
    Vector3 position = this.myTransform.position;
    foreach (GameObject gameObject in this.gameManager.enemyUnitList)
    {
      float sqrMagnitude = (gameObject.transform.position - position).sqrMagnitude;
      if ((double) sqrMagnitude < (double) num)
      {
        this.closestEnemyUnit = gameObject;
        num = sqrMagnitude;
      }
    }
    return this.closestEnemyUnit;
  }

  private void OnTriggerEnter(Collider otherObject)
  {
    if (!(otherObject.tag == "Enemy"))
      return;
    otherObject.GetComponent<Enemy>().TakeDamage(this.damage);
    Object.Destroy((Object) this.gameObject);
  }
}
