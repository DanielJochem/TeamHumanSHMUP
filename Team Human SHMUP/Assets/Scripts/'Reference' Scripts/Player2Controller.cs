// Decompiled with JetBrains decompiler
// Type: Player2Controller
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 856A15C1-8344-4F86-8FC2-8971B0BA3EE1
// Assembly location: C:\Users\DanielsSickPC\Desktop\Shmup2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Player2Controller : MonoBehaviour
{
  public float moveSpeed = 100f;
  private float lazorFireRate = 0.1f;
  private Transform myTransform;
  private Vector3 playerPosition;
  private GameManager gameManager;
  public GameObject playerMissile;
  public GameObject lazor;
  private float lazorFireTime;
  public int fireMode;
  public GameObject weaponBarrel;
  private string useSpecial;
  public GameObject[] muzzle;
  private float aimingX;
  private float aimingY;
  private float tiltX;
  private float tiltZ;
  public GameObject playerModel;

  private void Start()
  {
    this.myTransform = this.transform;
    this.gameManager = Object.FindObjectOfType<GameManager>();
  }

  private void Update()
  {
    this.Movement();
    this.CheckBoundry();
  }

  private void Movement()
  {
    this.playerPosition = this.myTransform.position;
    if ((double) Input.GetAxis("P2_Vertical") != 0.0)
    {
      float axis = Input.GetAxis("P2_Vertical");
      this.playerPosition.z = this.playerPosition.z + axis * this.moveSpeed * Time.deltaTime;
      this.tiltZ = axis * 20f;
    }
    else
      this.tiltZ = 0.0f;
    if ((double) Input.GetAxis("P2_Horizontal") != 0.0)
    {
      float axis = Input.GetAxis("P2_Horizontal");
      this.playerPosition.x = this.playerPosition.x + axis * this.moveSpeed * Time.deltaTime;
      this.tiltX = axis * 20f;
    }
    else
      this.tiltX = 0.0f;
    this.aimingX = (double) Input.GetAxis("P2_Mouse X") == 0.0 ? 0.0f : -Input.GetAxis("P2_Mouse X");
    this.aimingY = (double) Input.GetAxis("P2_Mouse Y") == 0.0 ? 0.0f : -Input.GetAxis("P2_Mouse Y");
    this.weaponBarrel.transform.rotation = Quaternion.Euler(this.myTransform.rotation.x, Mathf.Atan2(this.aimingX, this.aimingY) * 57.29578f, this.myTransform.rotation.z);
    this.playerModel.transform.rotation = Quaternion.Euler(this.tiltZ, 0.0f, -this.tiltX);
    if ((double) Input.GetAxis("P2_Fire1") != 0.0 && (double) Time.time > (double) this.lazorFireTime)
      this.fireWeapons();
    if ((double) Input.GetAxis("P2_Fire2") != 0.0)
    {
      if ((double) Time.time > (double) this.lazorFireTime)
      {
        MonoBehaviour.print((object) "NO ACTION MAPPED: (P2_Fire2)");
        Time.timeScale = 2f;
        this.lazorFireTime = Time.time + this.lazorFireRate;
      }
    }
    else
      Time.timeScale = 1f;
    this.myTransform.position = this.playerPosition;
  }

  private void CheckBoundry()
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

  private void OnTriggerEnter(Collider otherObject)
  {
    if (!(otherObject.tag == "EnemyLazor"))
      return;
    this.gameManager.P2LifeRemove();
    if (this.gameManager.P2Lives != 0)
      return;
    Object.Destroy((Object) this.gameObject);
  }

  private void fireWeapons()
  {
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
          Object.Instantiate((Object) this.playerMissile, this.muzzle[index].transform.position, this.muzzle[index].transform.rotation);
        break;
    }
    this.lazorFireTime = Time.time + this.lazorFireRate;
  }
}
