// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.FormDesigner.CAD.Classes.ExternalCADAttrBase
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.FormDesigner.CAD.Classes;

/// <summary>
/// 
/// </summary>
internal class ExternalCADAttrBase
{
  protected string _name;
  protected Dictionary<string, string> _params;

  /// <summary>Constructor</summary>
  /// <param name="name"></param>
  public ExternalCADAttrBase(string name)
  {
    this._name = name;
    this._params = new Dictionary<string, string>();
  }

  /// <summary>
  /// 
  /// </summary>
  public string Name
  {
    get => this._name;
    set => this._name = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public Dictionary<string, string> Params
  {
    get => this._params;
    set => this._params = value;
  }
}
