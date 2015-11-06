// Decompiled with JetBrains decompiler
// Type: Enemy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 856A15C1-8344-4F86-8FC2-8971B0BA3EE1
// Assembly location: C:\Users\DanielsSickPC\Desktop\Shmup2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Enemy : MonoBehaviour
{
  private float movementSpeed = 20f;
  private float lazorFireRate = 2f;
  private float health = 20f;
  private float rotationSpeed = 2f;
  public GameObject pickUp;
  public GameObject EnemyLazor;
  private GameObject[] players;
  private GameObject closestPlayer;
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
    this.players = GameObject.FindGameObjectsWithTag("Player");
  }

  private void Update()
  {
    this.Move();
    this.CheckIfDead();
    this.CheckIfOffScreen();
    this.LookAtPlayer();
    if ((double) Time.time <= (double) this.lazorFireTime)
      return;
    Object.Instantiate((Object) this.EnemyLazor, this.transform.position, this.transform.rotation);
    this.lazorFireTime = Time.time + this.lazorFireRate;
  }

  private void LookAtPlayer()
  {
    this.FindClosestPlayer();
    if (!((Object) this.closestPlayer != (Object) null) || (double) this.closestPlayer.transform.position.z >= (double) this.myTransform.position.z)
      return;
    this.targetRotation = Quaternion.LookRotation(this.closestPlayer.transform.position - this.myTransform.position);
    this.adjRotationSpeed = Mathf.Min(this.rotationSpeed * Time.deltaTime, 1f);
    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.targetRotation, this.adjRotationSpeed);
  }

  public void TakeDamage(float damage)
  {
    this.health -= damage;
  }

  private void Move()
  {
    this.myTransform.position += Time.deltaTime * this.movementSpeed * this.transform.forward;
  }

  private void RandomPickUp()
  {
    if (Random.Range(1, 100) > 5)
      return;
    Object.Instantiate((Object) this.pickUp, this.transform.position, new Quaternion(180f, 0.0f, 0.0f, 0.0f));
  }

  private void CheckIfDead()
  {
    if ((double) this.health > 0.0)
      return;
    this.RemoveEnemy();
    this.RandomPickUp();
  }

  private void CheckIfOffScreen()
  {
    if ((double) this.transform.position.z >= -(double) this.gameManager.zBoundry - 20.0)
      return;
    --this.gameManager.enemyNumb;
    Object.Destroy((Object) this.gameObject);
  }

  private void RemoveEnemy()
  {
    ++this.gameManager.enemiesKilled;
    --this.gameManager.enemyNumb;
    Object.Destroy((Object) this.gameObject);
  }

  private void FindClosestPlayer()
  {
    for (int index = 0; index < this.players.Length; ++index)
    {
      if ((Object) this.players[index] != (Object) null)
      {
        if (index == 0)
          this.closestPlayer = this.players[0];
        if ((double) Vector3.Distance(this.myTransform.position, this.players[index].transform.position) <= (double) Vector3.Distance(this.myTransform.position, this.closestPlayer.transform.position))
          this.closestPlayer = this.players[index];
      }
    }
  }
}
