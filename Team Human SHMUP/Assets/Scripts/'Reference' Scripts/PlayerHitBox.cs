// Decompiled with JetBrains decompiler
// Type: PlayerHitBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 856A15C1-8344-4F86-8FC2-8971B0BA3EE1
// Assembly location: C:\Users\DanielsSickPC\Desktop\Shmup2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
  private GameManager gameManager;
  private CubeController player;

  private void Start()
  {
    this.gameManager = Object.FindObjectOfType<GameManager>();
    this.player = Object.FindObjectOfType<CubeController>();
  }

  private void Update()
  {
  }

  private void OnTriggerEnter(Collider otherObject)
  {
    if (!(otherObject.tag == "EnemyLazor"))
      return;
    this.gameManager.P2LifeRemove();
    this.player.fireMode = 0;
  }
}
