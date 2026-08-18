// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs.MeasureDialogResult
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs;

/// <summary>
/// 
/// </summary>
public enum MeasureDialogResult
{
  /// <summary>Добавить указанное количество</summary>
  Add,
  /// <summary>Добавить все доступное количество</summary>
  AddAllQuantity,
  /// <summary>Добавить указанное количество для всех объектов</summary>
  AddForAll,
  /// <summary>Добавить все доступное количество для всех объектов</summary>
  AddAllQuantityForAll,
  /// <summary>Отмена</summary>
  Cancel,
  /// <summary>Прервать добавление</summary>
  Terminate,
}
