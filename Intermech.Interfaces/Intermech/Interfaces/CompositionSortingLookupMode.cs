
// Type: Intermech.Interfaces.CompositionSortingLookupMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Режимы поиска типов объектов по правилам сортировки</summary>
    public enum CompositionSortingLookupMode
    {
      /// <summary>Меньше или равно текущему</summary>
      [Obsolete("Use LessOrEqual instead. Will be removed in IPS 9.0", true)] Less = 0,
      /// <summary>Меньше или равно текущему</summary>
      LessOrEqual = 0,
      /// <summary>Больше или равно текущему</summary>
      GreaterOrEqual = 1,
      /// <summary>Больше или равно текущему</summary>
      [Obsolete("Use GreaterOrEqual instead. Will be removed in IPS 9.0", true)] More = 1,
      /// <summary>Строго меньше текущего</summary>
      LessOnly = 2,
      /// <summary>Строго больше текущего</summary>
      GreaterOnly = 3,
    }
}
