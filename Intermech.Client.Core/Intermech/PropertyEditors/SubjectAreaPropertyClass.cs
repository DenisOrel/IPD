
// Type: Intermech.PropertyEditors.SubjectAreaPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using Intermech.Localization;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

/// <summary>
/// 
/// </summary>
[Editor(typeof (SubjectAreaEditor), typeof (UITypeEditor))]
public class SubjectAreaPropertyClass
{
  private string areas = string.Empty;

  /// <summary>
  /// 
  /// </summary>
  public string Areas => this.areas;

  /// <summary>Конструктор.</summary>
  /// <param name="svalue"></param>
  public SubjectAreaPropertyClass(string svalue) => this.areas = svalue;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString() => SubjectAreaPropertyClass.ToString(this.areas);

  public override bool Equals(object obj)
  {
    return obj is SubjectAreaPropertyClass ? ((SubjectAreaPropertyClass) obj).Areas == this.areas : base.Equals(obj);
  }

  public override int GetHashCode() => this.areas.GetHashCode();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  public static string ToString(string value)
  {
    string empty = string.Empty;
    if (value.Length == 0)
      return LocalizationHolder.rm.GetString("Client.Core_116");
    for (int index = 0; index < value.Length; ++index)
    {
      string namebyId = DataHolders.SubjectAreasHolder.GetNamebyID(value[index]);
      if (!(namebyId == string.Empty))
      {
        empty += namebyId;
        if (index < value.Length - 1)
          empty += ", ";
      }
    }
    return empty;
  }
}
