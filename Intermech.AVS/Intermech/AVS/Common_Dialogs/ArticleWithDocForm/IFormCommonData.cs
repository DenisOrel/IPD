// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.IFormCommonData
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Интерфейс на общие данные для закладок</summary>
internal interface IFormCommonData
{
  bool GetReadOnly(string fieldName);

  void SetReadOnly(string fieldName, bool readOnly);

  /// <summary>Обозначение</summary>
  string Designation { get; set; }

  /// <summary>Наименование</summary>
  string Name { get; set; }

  /// <summary>Полное наименование(Для БЧ)</summary>
  string FullName { get; set; }

  /// <summary>Код ОКП</summary>
  string OKPCode { get; set; }

  /// <summary>Формат</summary>
  string Format { get; set; }

  /// <summary>Смотри</summary>
  string Smotri { get; set; }

  /// <summary>Позиционное обозначение</summary>
  string PosDesignation { get; set; }

  /// <summary>Зона</summary>
  string Zona { get; set; }

  /// <summary>Позиция</summary>
  string Position { get; set; }

  /// <summary>Примечание</summary>
  string Note { get; set; }

  /// <summary>Материал</summary>
  MaterialInfo Material { get; set; }

  /// <summary>Размеры</summary>
  string Size { get; set; }

  /// <summary>Подбор</summary>
  bool Podbor { get; set; }

  /// <summary>Тип связи</summary>
  int RelationType { get; set; }

  /// <summary>Классификатор</summary>
  long ClassifierID { get; set; }

  /// <summary>Количество</summary>
  MeasuredValue Count { get; set; }

  event CommonDataChangedDelegate Changed;

  /// <summary>Функция проверки общих данных</summary>
  void Check();
}
