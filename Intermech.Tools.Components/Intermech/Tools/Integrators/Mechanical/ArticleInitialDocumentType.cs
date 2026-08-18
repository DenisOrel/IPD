// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleInitialDocumentType
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Описывает тип исходного документа для изделия. Такой документ содержит всю необходимую интегратору информацию об изделие для
/// создания/обновления изделия в базе IPS.
/// </summary>
public enum ArticleInitialDocumentType
{
  /// <summary>
  /// У изделия есть исходный документ и он является частью документации на это изделие
  /// </summary>
  Normal,
  /// <summary>
  /// У изделия есть исходный документ, но он не является частью документации на это изделие
  /// </summary>
  Hidden,
  /// <summary>
  /// Изделие создает интегратором без использования исходного документа. Источником информации об изделии служит что-то другое
  /// </summary>
  None,
}
