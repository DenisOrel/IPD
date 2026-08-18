// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.IEcoPropertiesService
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

#nullable disable
namespace Intermech.ECO.Client;

public interface IEcoPropertiesService
{
  IEcoProperties Current { get; set; }

  void SaveToBase(IEcoProperties properties);

  IEcoProperties LoadFromBase();
}
