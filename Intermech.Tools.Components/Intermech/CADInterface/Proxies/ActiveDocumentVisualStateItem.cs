// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ActiveDocumentVisualStateItem
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Runtime.ComInterop.Proxies;
using System.IO;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Элемент состояния UI CAD-системы, обеспечивающий сохранение и восстановление активного документа.
/// </summary>
/// <remarks>Реализация не является thread safe.</remarks>
internal sealed class ActiveDocumentVisualStateItem : ApplicationVisualStateItem<CADSystemProxy>
{
  private string savedActiveDocumentFile;

  /// <summary>
  /// Заполняет элемент, сохраняя текущее состояние UI CAD-системы.
  /// </summary>
  /// <param name="cadSystem">Объект CAD-системы</param>
  protected override void DoSaveState(CADSystemProxy cadSystem)
  {
    base.DoSaveState(cadSystem);
    CADDocumentProxy activeDocument = cadSystem.GetActiveDocument();
    if (activeDocument == null || string.IsNullOrEmpty(activeDocument.FullName))
      this.savedActiveDocumentFile = (string) null;
    else
      this.savedActiveDocumentFile = activeDocument.FullName;
  }

  /// <summary>
  /// Восстанавливает элемент, используя сохраненное состояние UI CAD-системы.
  /// </summary>
  /// <param name="cadSystem">Объект CAD-системы</param>
  protected override void DoRestoreState(CADSystemProxy cadSystem)
  {
    base.DoRestoreState(cadSystem);
    if (string.IsNullOrEmpty(this.savedActiveDocumentFile))
      return;
    CADDocumentProxy cadDocumentProxy = cadSystem.FindOpenDocument(this.savedActiveDocumentFile);
    if (cadDocumentProxy == null && File.Exists(this.savedActiveDocumentFile))
      cadDocumentProxy = cadSystem.OpenDocument(this.savedActiveDocumentFile, true);
    cadDocumentProxy?.Activate();
  }
}
