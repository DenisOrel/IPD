// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.AltiumConsts
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Константы, используемые в интеграторе</summary>
public static class AltiumConsts
{
  /// <summary>
  /// Расширение файла проекта, содержащий ссылки на основные файлы одного изделия (блока, модуля),
  /// а также ссылки на дополнительные и сгенерированный файлы.
  /// </summary>
  public const string ProjectFileExtension = ".PrjPcb";
  /// <summary>
  /// Расширение файла схемы. Основной файл, содержащий информацию об одном листе электрической (структурной, функциональной и т.п.).
  /// </summary>
  public const string SchFileExtension = ".SchDoc";
  /// <summary>Расширение файла pcb.</summary>
  public const string PcbFileExtension = ".PcbDoc";
  /// <summary>
  /// Расширение файлов, разработанных в модуле Altium Designer Draftsman
  /// </summary>
  public const string PCBDwfFileExtension = ".PCBDwf";
  /// <summary>
  /// Имя виртуального атрибута компонента схемы, указывающий его тип. Под этим именем оне передается в IPS.
  /// </summary>
  public const string AttributeComponentKind = "ComponentKind";
  /// <summary>Обозначение схемы</summary>
  public const string SchemaKind = "SCH";
  public const string AddInVersion = "1.0.0.0";
}
