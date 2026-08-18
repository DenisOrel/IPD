
// Type: Intermech.Client.Core.History.SampleDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.History;

/// <summary>
/// 
/// </summary>
internal class SampleDescriptor
{
  private string _description = string.Empty;
  private object _value;

  /// <summary>
  /// 
  /// </summary>
  public string Description => this._description;

  /// <summary>
  /// 
  /// </summary>
  public object Value => this._value;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="description"></param>
  /// <param name="value"></param>
  public SampleDescriptor(string description, object value)
  {
    this._description = description;
    this._value = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return obj.GetType().Equals(typeof (SampleDescriptor)) ? this.GetHashCode().Equals(obj.GetHashCode()) : base.Equals(obj);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => this._description.GetHashCode();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString() => this._description;
}
