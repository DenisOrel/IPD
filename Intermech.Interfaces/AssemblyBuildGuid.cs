using System.ComponentModel;


/// <summary>
/// Атрибут, значение которого поможет определить Guid компиляции сборки.
/// Значение атрибута будет заполняться специальной программой перед компиляцией.
/// </summary>
public class AssemblyBuildGuid : DescriptionAttribute
{
  /// <summary>Создать экземпляр атрибута</summary>
  /// <param name="description">Текстовое значение</param>
  public AssemblyBuildGuid(string description) => this.DescriptionValue = description;
}
