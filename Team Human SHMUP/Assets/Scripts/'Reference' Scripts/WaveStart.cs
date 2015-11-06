// Decompiled with JetBrains decompiler
// Type: WaveStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 856A15C1-8344-4F86-8FC2-8971B0BA3EE1
// Assembly location: C:\Users\DanielsSickPC\Desktop\Shmup2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class WaveStart : MonoBehaviour
{
  private void Start()
  {
    this.transform.DetachChildren();
  }

  private void Update()
  {
    Object.Destroy((Object) this.gameObject);
  }
}
