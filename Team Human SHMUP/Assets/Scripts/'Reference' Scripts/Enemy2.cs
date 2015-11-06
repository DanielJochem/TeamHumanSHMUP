// Decompiled with JetBrains decompiler
// Type: Enemy2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 856A15C1-8344-4F86-8FC2-8971B0BA3EE1
// Assembly location: C:\Users\DanielsSickPC\Desktop\Shmup2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Enemy2 : MonoBehaviour
{
  private float movementSpeed = 20f;
  private float lazorFireRate = 2f;
  private float health = 20f;
  private float rotationSpeed = 2f;
  private int numberOfShots = 1;
  private float angleRange = 45f;
  public GameObject pickUp;
  public GameObject EnemyLazor;
  private GameObject player;
  private GameManager gameManager;
  private Transform myTransform;
  private float lazorFireTime;
  private float adjRotationSpeed;
  private Quaternion targetRotation;

  private void Start()
  {
    this.myTransform = this.transform;
    this.gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    ++this.gameManager.enemyNumb;
    this.player = GameObject.FindGameObjectWithTag("Player");
  }

  private void Update()
  {
    this.Move();
    this.CheckIfDead();
    this.checkIfOffScreen();
    if ((double) this.player.transform.position.z < (double) this.myTransform.position.z)
    {
      this.targetRotation = Quaternion.LookRotation(this.player.transform.position - this.myTransform.position);
      this.adjRotationSpeed = Mathf.Min(this.rotationSpeed * Time.deltaTime, 1f);
      this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.targetRotation, this.adjRotationSpeed);
    }
    if ((double) Time.time <= (double) this.lazorFireTime)
      return;
    float num = this.angleRange / (float) (this.numberOfShots - 1);
    for (int index = 0; index < this.numberOfShots; ++index)
    {
      MonoBehaviour.print((object) "Ping");
      Object.Instantiate((Object) this.EnemyLazor, this.transform.position, Quaternion.Euler(0.0f, 0.0f, num * (float) index));
    }
    this.lazorFireTime = Time.time + this.lazorFireRate;
  }

  public void TakeDamage(float damage)
  {
    this.health -= damage;
  }

  private void Move()
  {
    this.myTransform.position += Time.deltaTime * this.movementSpeed * this.transform.forward;
  }

  private void randomPickUp()
  {
    if (Random.Range(1, 100) > 5)
      return;
    Object.Instantiate((Object) this.pickUp, this.transform.position, new Quaternion(180f, 0.0f, 0.0f, 0.0f));
  }

  private void CheckIfDead()
  {
    if ((double) this.health > 0.0)
      return;
    this.removeEnemy();
    this.randomPickUp();
  }

  private void checkIfOffScreen()
  {
    if ((double) this.transform.position.z >= -(double) this.gameManager.zBoundry - 20.0)
      return;
    this.removeEnemy();
  }

  private void removeEnemy()
  {
    ++this.gameManager.enemiesKilled;
    --this.gameManager.enemyNumb;
    Object.Destroy((Object) this.gameObject);
  }
}
