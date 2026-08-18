// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.FormType
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Тип отображаемого диалога</summary>
internal enum FormType
{
  /// <summary>Неизвестно</summary>
  None,
  /// <summary>Для единичных спецификаций и групповых А</summary>
  Single,
  /// <summary>Для групповых Б</summary>
  GroupB,
  /// <summary>Для бесчертежных</summary>
  NonDraft,
  /// <summary>Для бесчертежных формы Б</summary>
  NonDraftB,
  /// <summary>Для автомобильных единичных</summary>
  Autoprom_Single,
  /// <summary>Для автомобильных групповых Б</summary>
  Autoprom_GroupB,
  /// <summary>Для автомобильных бесчертежных</summary>
  Autoprom_NonDraft,
  /// <summary>Для автомобильных бесчертежных формы Б</summary>
  Autoprom_NonDraftB,
}
