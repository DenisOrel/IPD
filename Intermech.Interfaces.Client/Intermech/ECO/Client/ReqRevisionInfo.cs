// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ReqRevisionInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.ECO.Client;

public class ReqRevisionInfo
{
  public RequireClass reqType;
  public bool wantsECO;
  public bool wantsCJRecord;

  public ReqRevisionInfo(ReqRevision info)
  {
    this.reqType = ReqRevisionInfo.GetRequireInfo(info, out this.wantsECO, out this.wantsCJRecord);
  }

  public static RequireClass GetRequireInfo(
    ReqRevision info,
    out bool wantsECO,
    out bool wantsCJRec)
  {
    int num = (int) info;
    switch (num)
    {
      case 1:
      case 2:
        num += 8;
        break;
    }
    wantsECO = (num & 8) != 0;
    wantsCJRec = (num & 16 /*0x10*/) != 0;
    return (RequireClass) (num & 3);
  }
}
