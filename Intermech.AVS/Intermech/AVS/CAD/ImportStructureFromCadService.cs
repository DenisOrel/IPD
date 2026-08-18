// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.CAD.ImportStructureFromCadService
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Cadmech.Integrator;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.CAD;

/// <summary>
/// Реализация интерфейса отвечающего за импорт структуры изделия из CAD-систем.
/// </summary>
internal class ImportStructureFromCadService : IImportStructureFromCadService
{
  /// <summary>
  /// Вызывается после того как интегратор обработал текстовый файл, переданный из CAD системы.
  /// </summary>
  /// <param name="structData">Данные о составе сборочного чертежа</param>
  void IImportStructureFromCadService.EditDrawingSpec(StructData structData)
  {
    if (AVSPlugin.IInvokeService == null)
      return;
    AVSWindow avsWin = AVSPlugin.IInvokeService.InvokeFunc<AVSWindow>(-1, (Func<AVSWindow>) (() => AVSPlugin.Instance.OpenAVSWindow(new OpenAVSDocArgs(structData.BaseProjectId, false))));
    AVSDocument avsDocument = avsWin.AVSDocument;
    AVSPlugin.IInvokeService.InvokeAction(-1, (Action) (() => this.TransferDocFormat(structData, avsWin)));
    AVSPlugin.IInvokeService.InvokeAction(-1, (Action) (() =>
    {
      avsWin.EnableWorkCompleteMode();
      ServiceUtils.GetService<IMainFormUpdate>((object) ServicesManager.ServiceContainer, true).MainForm.Activate();
      while (avsWin.IsInContainer)
        Application.DoEvents();
    }));
    new CaptureSpecChangesTask().Execute(structData, avsDocument);
  }

  private void TransferDocFormat(StructData structData, AVSWindow avsWin)
  {
    AVSDocument avsDocument = avsWin.AVSDocument;
    AvsRowAttributeInfo attrInfo = new AvsRowAttributeInfo(false, AvsIDCache.Attr_Format);
    foreach (PartData part in structData.Spec.Parts)
    {
      if (!string.IsNullOrEmpty(part.DocumentFormat))
      {
        foreach (AVSRow avsRow in avsDocument.GetAvsRowsByObjectId(part.ObjectId))
          avsRow.SetFieldValue(attrInfo, -1, -1, (object) part.DocumentFormat, false, false, true, true, false, false);
      }
    }
  }
}
