// Decompiled with JetBrains decompiler
// Type: GameManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 856A15C1-8344-4F86-8FC2-8971B0BA3EE1
// Assembly location: C:\Users\DanielsSickPC\Desktop\Shmup2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public float zBoundry = 50f;
  public float xBoundry = 145f;
  public int innerClip = 30;
  public int P1Lives = 3;
  public int P2Lives = 3;
  public GameObject player1GameObject;
  public GameObject player2GameObject;
  private GameObject P1Spawn;
  private GameObject P2Spawn;
  public float zBoundryMoving;
  public int enemyNumb;
  public GameObject P1LifeUIObject;
  public GameObject P2LifeUIObject;
  public GameObject[] enemyUnitList;
  public GameObject enemiesKilledUIObject;
  public int enemiesKilled;
  public string[] controllers;

  private void Start()
  {
    this.P1Spawn = GameObject.FindGameObjectWithTag("P1Spawner");
    this.P2Spawn = GameObject.FindGameObjectWithTag("P2Spawner");
    this.controllers = Input.GetJoystickNames();
    Object.Instantiate((Object) this.player1GameObject, this.P1Spawn.transform.position, this.P1Spawn.transform.rotation);
    if (!(this.controllers[1] != string.Empty))
      return;
    Object.Instantiate((Object) this.player2GameObject, this.P2Spawn.transform.position, this.P2Spawn.transform.rotation);
    MonoBehaviour.print((object) ("Player 1:" + this.controllers[0]));
    MonoBehaviour.print((object) ("Player 2:" + this.controllers[1]));
  }

  private void Update()
  {
    if (this.enemyNumb == 0)
      Time.timeScale = 1f;
    this.enemyUnitList = GameObject.FindGameObjectsWithTag("Enemy");
    this.enemiesKilledUIObject.GetComponent<Text>().text = "Enemies Killed:" + (object) this.enemiesKilled;
    this.P1LifeUIObject.GetComponent<Text>().text = "P1 Lives:" + (object) this.P1Lives;
    this.P2LifeUIObject.GetComponent<Text>().text = "P2 Lives:" + (object) this.P2Lives;
  }

  public void P1LifeRemove()
  {
    --this.P1Lives;
  }

  public void P2LifeRemove()
  {
    --this.P2Lives;
  }
}
