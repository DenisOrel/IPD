
// Type: AssemblyVersionString
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.ComponentModel;


/// <summary>
/// Атрибут, значение которого поможет определить строку с версией сборки.
/// Значение атрибута будет заполняться специальной программой перед компиляцией.
/// </summary>
public class AssemblyVersionString : DescriptionAttribute
{
  /// <summary>Создать экземпляр атрибута</summary>
  /// <param name="description">Текстовое значение</param>
  public AssemblyVersionString(string description) => this.DescriptionValue = description;
}
