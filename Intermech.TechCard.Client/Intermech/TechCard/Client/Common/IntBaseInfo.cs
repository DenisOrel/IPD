// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Common.IntBaseInfo
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.Common;

/// <summary>Integer object's info structure</summary>
/// <summary>Constructor</summary>
/// <param name="data"></param>
/// <param name="caption"></param>
public class IntBaseInfo(long data, string caption) : BaseObjInfo((object) data, caption)
{
  /// <summary>Constructor</summary>
  public IntBaseInfo()
    : this(0L, string.Empty)
  {
  }

  /// <summary>Object's value</summary>
  public virtual long Value
  {
    get => (long) this._value;
    set => this._value = (object) value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return obj is IntBaseInfo intBaseInfo && this.Value.Equals(intBaseInfo.Value);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="data"></param>
  /// <returns></returns>
  public static long GetIntValue(IntBaseInfo data) => data == null ? 0L : data.Value;
}
