// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.AnalyzerChangesSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Используется для пометки документов, в которые анализатор вносил правки. Используется как фильтр при сохранении измененных файлов
/// документов на диск - будут сохранены только те документы, которые имеют правки анализатора.
/// </summary>
/// <remarks>
/// Необходимость в таком фильтре возникла из-за того, что у некоторых приложений документы могут изменяться просто при открытии без
/// каких-либо действий со стороны пользователя (например, SolidEdge). Сейчас этот фильтр отключен, так как логика работы интеграторов
/// изменилась. Теперь интеграторы обрабатывают только указанные пользователем документы, а не все документы, имеющие несохраненные изменения.
/// </remarks>
public sealed class AnalyzerChangesSection
{
  private static readonly AnalyzerChangesSection Instance = new AnalyzerChangesSection();
  private static readonly BooleanSwitch enableSwitch = new BooleanSwitch("Tools.SaveIntegratorOnlyChanges", "", "0");

  /// <summary>
  /// Помечает документ, как имеющий несохраненные изменения.
  /// </summary>
  /// <param name="documentItem">Объект документа</param>
  public static void Mark(SectionEntity documentItem)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    if (!AnalyzerChangesSection.enableSwitch.Enabled || documentItem.Sections.Contains<AnalyzerChangesSection>())
      return;
    documentItem.Sections.Set((object) AnalyzerChangesSection.Instance);
  }

  /// <summary>
  /// Проверяет, был ли документ помечен, как имеющий несохраненные изменения.
  /// </summary>
  /// <param name="documentItem">Объект документа</param>
  public static bool IsMarked(SectionEntity documentItem)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    return !AnalyzerChangesSection.enableSwitch.Enabled || documentItem.Sections.Contains<AnalyzerChangesSection>();
  }

  /// <summary>Создает объект.</summary>
  private AnalyzerChangesSection()
  {
  }
}
