// Decompiled with JetBrains decompiler
// Type: CubeController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 856A15C1-8344-4F86-8FC2-8971B0BA3EE1
// Assembly location: C:\Users\DanielsSickPC\Desktop\Shmup2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CubeController : MonoBehaviour
{
  public float moveSpeed = 100f;
  private float lazorFireRate = 0.1f;
  private Transform myTransform;
  private Vector3 playerPosition;
  private GameManager gameManager;
  public PlayerMissile playerMissile;
  public GameObject lazor;
  public GameObject[] muzzle;
  private float lazorFireTime;
  public int fireMode;

  private void Start()
  {
    this.myTransform = this.transform;
    this.gameManager = Object.FindObjectOfType<GameManager>();
  }

  private void Update()
  {
    this.Movement();
    this.fireLazors();
    this.checkBoundry();
  }

  private void Movement()
  {
    Time.timeScale = 0.1f;
    this.playerPosition = this.myTransform.position;
    if (Input.GetKey("a"))
    {
      this.playerPosition.x = this.playerPosition.x - this.moveSpeed * Time.deltaTime;
      Time.timeScale = 1f;
    }
    if (Input.GetKey("d"))
    {
      this.playerPosition.x = this.playerPosition.x + this.moveSpeed * Time.deltaTime;
      Time.timeScale = 1f;
    }
    if (Input.GetKey("w"))
    {
      this.playerPosition.z = this.playerPosition.z + this.moveSpeed * Time.deltaTime;
      Time.timeScale = 1f;
    }
    if (Input.GetKey("s"))
    {
      this.playerPosition.z = this.playerPosition.z - this.moveSpeed * Time.deltaTime;
      Time.timeScale = 1f;
    }
    if (Input.GetKey("space"))
      Time.timeScale = 2f;
    this.myTransform.position = this.playerPosition;
  }

  private void checkBoundry()
  {
    this.playerPosition = this.myTransform.position;
    if ((double) this.playerPosition.x <= -(double) this.gameManager.xBoundry)
      this.playerPosition.x = -this.gameManager.xBoundry;
    else if ((double) this.playerPosition.x >= (double) this.gameManager.xBoundry)
      this.playerPosition.x = this.gameManager.xBoundry;
    if ((double) this.playerPosition.z >= (double) this.gameManager.zBoundry)
      this.playerPosition.z = this.gameManager.zBoundry;
    else if ((double) this.playerPosition.z <= -(double) this.gameManager.zBoundry)
      this.playerPosition.z = -this.gameManager.zBoundry;
    this.myTransform.position = this.playerPosition;
  }

  private void fireLazors()
  {
    if (Input.GetMouseButton(0) && (double) Time.time > (double) this.lazorFireTime)
    {
      this.fireModes();
      this.lazorFireTime = Time.time + this.lazorFireRate;
    }
    if (!Input.GetMouseButton(0))
      return;
    Time.timeScale = 1f;
  }

  private void fireModes()
  {
    float y = (float) Random.Range(0, 360);
    switch (this.fireMode)
    {
      case 0:
        Object.Instantiate((Object) this.lazor, this.muzzle[0].transform.position, this.muzzle[0].transform.rotation);
        break;
      case 1:
        for (int index = 1; index < 3; ++index)
          Object.Instantiate((Object) this.lazor, this.muzzle[index].transform.position, this.muzzle[index].transform.rotation);
        break;
      case 2:
        for (int index = 0; index < this.muzzle.Length; ++index)
          Object.Instantiate((Object) this.lazor, this.muzzle[index].transform.position, this.muzzle[index].transform.rotation);
        break;
      case 3:
        for (int index = 0; index < this.muzzle.Length; ++index)
          Object.Instantiate((Object) this.playerMissile, this.muzzle[index].transform.position, Quaternion.Euler(0.0f, y, 0.0f));
        break;
    }
  }
}
