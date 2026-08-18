// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.Compositions.Loading.ObjectMeasuredInfo
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.Collections.Generic;


namespace Intermech.Kernel.Services.Compositions.Loading;

internal class ObjectMeasuredInfo
{
  public readonly int ObjectType;
  public readonly long ID;
  public readonly List<ShortMeasuredValue> Quantities;

  public ObjectMeasuredInfo(int objectType, long id, ShortMeasuredValue quantity)
  {
    this.ID = id;
    this.ObjectType = objectType;
    this.Quantities = new List<ShortMeasuredValue>(1)
    {
      quantity
    };
  }
}
