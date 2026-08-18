// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.SpecialRule
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Workflow;

[Flags]
public enum SpecialRule
{
  None = 0,
  /// <summary>Включает обработку числовых атрибутов как ссылочных</summary>
  ObjectLinkAttribute = 1,
  /// <summary>
  /// Указывает, что нужно создавать объекты "Неполный ссылочный объект" при импорте из портфеля значений атрибутов, ссылающихся на объекты, которых нет в текущей базе
  /// </summary>
  CreateSurrogateObjects = 2,
}
