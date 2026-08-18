// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSEventType
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS;

public enum AVSEventType
{
  /// <summary>НЕт события</summary>
  Empty,
  /// <summary>Изменили строку</summary>
  ChangeRow,
  /// <summary>Добавили строку</summary>
  AddRow,
  /// <summary>Удалили строку</summary>
  RemoveRow,
  /// <summary>Игнорировали обновление</summary>
  SkipUpdateRowField,
}
