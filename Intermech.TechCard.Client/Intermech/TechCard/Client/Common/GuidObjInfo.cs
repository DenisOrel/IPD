// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Common.GuidObjInfo
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;

#nullable disable
namespace Intermech.TechCard.Client.Common;

/// <summary>Guid object's info structure</summary>
/// <summary>Constructor</summary>
/// <param name="data"></param>
/// <param name="caption"></param>
public class GuidObjInfo(Guid data, string caption) : BaseObjInfo((object) data, caption)
{
  /// <summary>Constructor</summary>
  public GuidObjInfo()
    : this(Guid.Empty, string.Empty)
  {
  }

  /// <summary>Object's value</summary>
  public virtual Guid Value
  {
    get => (Guid) this._value;
    set => this._value = (object) value;
  }
}
