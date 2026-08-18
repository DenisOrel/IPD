// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.ActiveDocumentVisualStateItem
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Intermech.Runtime.ComInterop.Proxies;
using System.IO;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Элемент состояния UI CAD-системы, обеспечивающий сохранение и восстановление активного документа.
/// </summary>
/// <remarks>Реализация не является thread safe.</remarks>
public sealed class ActiveDocumentVisualStateItem : ApplicationVisualStateItem<ICadProxy>
{
  private string savedActiveDocumentFile;

  /// <summary>
  /// Заполняет элемент, сохраняя текущее состояние UI приложения.
  /// </summary>
  /// <param name="cadSystem">Объект приложения</param>
  protected override void DoSaveState(ICadProxy cadSystem)
  {
    base.DoSaveState(cadSystem);
    ICadDocumentProxy activeDocument = cadSystem.TryGetActiveDocument();
    if (activeDocument != null && !activeDocument.IsNew)
      this.savedActiveDocumentFile = activeDocument.GetMasterFile();
    else
      this.savedActiveDocumentFile = (string) null;
  }

  /// <summary>
  /// Восстанавливает элемент, используя сохраненное состояние UI приложения.
  /// </summary>
  /// <param name="cadSystem">Объект приложения</param>
  protected override void DoRestoreState(ICadProxy cadSystem)
  {
    base.DoRestoreState(cadSystem);
    if (string.IsNullOrEmpty(this.savedActiveDocumentFile))
      return;
    ICadDocumentProxy cadDocumentProxy = cadSystem.FindOpenDocument(this.savedActiveDocumentFile);
    if (cadDocumentProxy == null && File.Exists(this.savedActiveDocumentFile))
      cadDocumentProxy = cadSystem.OpenDocument(this.savedActiveDocumentFile);
    if (cadDocumentProxy != null)
      return;
    cadDocumentProxy.Activate();
  }
}
