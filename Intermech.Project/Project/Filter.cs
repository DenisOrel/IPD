// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Filter
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Project;

/// <summary>Фильтр задач IMProject-а</summary>
public class Filter
{
  /// <summary>Условие фильтрации задач IMProject-а</summary>
  public class Condition
  {
    /// <summary>Источник значения которое участвует в фильтрации</summary>
    public class ValueSource
    {
      [CanBeEmpty]
      public readonly Guid AttributeGuid;
      [NotNull]
      [NotWhitespace]
      public readonly string PropertyName;
      [NotNull]
      [NotWhitespace]
      public readonly string DisplayName;
      /// <summary>Признак того, что свойство, описанное Guid найдено в БД</summary>
      internal readonly bool Found;

      private ValueSource([CanBeEmpty] Guid attributeGuid, bool found, [NotNull, NotWhitespace] string propertyName, [NotNull, NotWhitespace] string displayName)
      {
        this.AttributeGuid = attributeGuid;
        this.Found = found;
        this.PropertyName = propertyName;
        this.DisplayName = displayName;
      }
    }
  }

  [System.Flags]
  public enum Flags
  {
    None = 0,
    ShowInMenu = 1,
    Global = 2,
    ShowSummaryTasks = 4,
  }
}
