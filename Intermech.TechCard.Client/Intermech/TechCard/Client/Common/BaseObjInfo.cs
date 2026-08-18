// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Common.BaseObjInfo
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.Common;

/// <summary>Base object's info structure</summary>
public class BaseObjInfo
{
  /// <summary>Object's value</summary>
  protected object _value;
  /// <summary>Object's caption</summary>
  protected string _caption;

  /// <summary>Initialize class data</summary>
  protected virtual void InitializeData()
  {
  }

  /// <summary>Constructor</summary>
  public BaseObjInfo()
    : this((object) null, string.Empty)
  {
  }

  /// <summary>Constructor</summary>
  /// <param name="data"></param>
  /// <param name="caption"></param>
  public BaseObjInfo(object data, string caption)
  {
    this._value = data;
    this._caption = caption;
  }

  /// <summary>Object's value</summary>
  public virtual object Value
  {
    get => this._value;
    set => this._value = value;
  }

  /// <summary>Object's caption</summary>
  public virtual string Caption
  {
    get => this._caption;
    set => this._caption = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString() => this.Caption;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode()
  {
    return this.Value == null ? base.GetHashCode() : this.Value.GetHashCode();
  }
}
