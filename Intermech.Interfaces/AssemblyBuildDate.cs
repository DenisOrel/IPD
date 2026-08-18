
// Type: AssemblyBuildDate
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;


/// <summary>
/// Атрибут, значение которого поможет определить дату компиляции сборки (UTC).
/// Значение атрибута будет заполняться специальной программой перед компиляцией.
/// </summary>
public class AssemblyBuildDate : DescriptionAttribute
{
  /// <summary>Создать экземпляр атрибута</summary>
  /// <param name="description">Текстовое значение</param>
  public AssemblyBuildDate(string description) => this.DescriptionValue = description;

  /// <summary>Год компиляции проекта</summary>
  public string AssemblyBuildYear
  {
    get
    {
      string from = StringsHelper.ExtractFrom(StringsHelper.ExtractFrom(this.DescriptionValue, ".", string.Empty), ".", string.Empty);
      return string.IsNullOrEmpty(from) ? DateTime.Now.Year.ToString() : from;
    }
  }
}
