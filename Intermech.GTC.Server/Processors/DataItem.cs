// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.DataItem
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

#nullable disable
namespace Intermech.GTC.Server.Processors;

internal class DataItem
{
  public DataItem(string productId, string p21FileName, string gtcClassId)
  {
    this.ProductId = productId;
    this.P21FileName = p21FileName;
    this.GtcClassId = gtcClassId;
  }

  public string ProductId { get; private set; }

  public string P21FileName { get; private set; }

  public string GtcClassId { get; private set; }
}
