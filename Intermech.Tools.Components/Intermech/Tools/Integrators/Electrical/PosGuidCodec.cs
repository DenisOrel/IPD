// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.PosGuidCodec
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using System;
using System.Text;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Кодек для позиционного обозначения</summary>
public class PosGuidCodec
{
  private string _id;
  private string _partName;
  private readonly char _separator = '#';

  public PosGuidCodec(string id, string partName)
  {
    this._id = id;
    this._partName = partName;
  }

  public string Encode(Guid posGuid)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(posGuid.ToString());
    stringBuilder.Append(this._separator);
    stringBuilder.Append(this._id);
    stringBuilder.Append(this._separator);
    stringBuilder.Append(this._partName);
    return stringBuilder.ToString();
  }

  public Guid Decode(string parameterValue)
  {
    string[] strArray = parameterValue.Split(this._separator);
    return strArray.Length == 3 && GuidHelper.IsGuid(strArray[0]) && this._id.Equals(strArray[1]) && this._partName.Equals(strArray[2]) ? new Guid(strArray[0]) : Guid.Empty;
  }
}
