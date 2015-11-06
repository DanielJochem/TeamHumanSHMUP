// Decompiled with JetBrains decompiler
// Type: EnemySpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 856A15C1-8344-4F86-8FC2-8971B0BA3EE1
// Assembly location: C:\Users\DanielsSickPC\Desktop\Shmup2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  private GameManager gameManager;
  public GameObject[] Wave;
  private int waveNumber;
  private float waveDelay;

  private void Start()
  {
    this.waveDelay = 0.0f;
    this.gameManager = Object.FindObjectOfType<GameManager>();
    this.SpawnWave();
  }

  private void Update()
  {
    this.waveDelay += Time.deltaTime;
    if ((double) this.waveDelay <= 2.0 || this.gameManager.enemyNumb > 1)
      return;
    this.SpawnWave();
    this.waveDelay = 0.0f;
  }

  private void SpawnWave()
  {
    Object.Instantiate((Object) this.Wave[this.waveNumber], new Vector3(0.0f, 0.0f, this.gameManager.zBoundry), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    if (this.waveNumber < this.Wave.Length - 1)
    {
      ++this.waveNumber;
    }
    else
    {
      if (this.waveNumber < this.Wave.Length)
        return;
      this.waveNumber = this.Wave.Length - 1;
    }
  }
}
