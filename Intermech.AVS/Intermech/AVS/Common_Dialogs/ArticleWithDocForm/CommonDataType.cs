// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.CommonDataType
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Тип общих данных</summary>
[Flags]
internal enum CommonDataType
{
  /// <summary>Нет</summary>
  None = 0,
  /// <summary>Все</summary>
  All = 1,
  /// <summary>Обозначение</summary>
  Designation = 2,
  /// <summary>Наименование</summary>
  Name = 4,
  /// <summary>ОКП код</summary>
  OKPCode = 8,
  /// <summary>Формат</summary>
  Format = 16, // 0x00000010
  /// <summary>Материал</summary>
  Material = 32, // 0x00000020
  /// <summary>Размеры</summary>
  Size = 64, // 0x00000040
  /// <summary>Подбор</summary>
  Podbor = 128, // 0x00000080
}
