// Decompiled with JetBrains decompiler
// Type: Intermech.ImbaseExcelUnloader.Client.ImbaseUnloadBackGroundTask
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Intermech.ImbaseExcelUnloader.Client;

public class ImbaseUnloadBackGroundTask : ExtendedBackgroundTaskBase
{
  private string _FileName;
  private List<Guid> _AttrGuidLst;
  private UnloadFlags _Flags;
  private HashSet<IDBTypedObjectID> _Items;
  private ImbaseUnloadBackGroundTask.LoadHandler _handler;

  public ImbaseUnloadBackGroundTask(
    string AFileName,
    HashSet<IDBTypedObjectID> AItems,
    List<Guid> AAttrGuidLst,
    UnloadFlags AFlags)
  {
    this._FileName = AFileName;
    this._Items = AItems;
    this._AttrGuidLst = AAttrGuidLst;
    this._Flags = AFlags;
  }

  private void CallBack(IAsyncResult ar)
  {
    this._AsyncResult = (IAsyncResult) null;
    this._Terminated = true;
    StringBuilder res = this._handler.EndInvoke(ar);
    if (res.Length > 0)
      ServiceHolder.IInvokeService.InvokeAction(-1, (Action) (() =>
      {
        string category = LocalizationHolder.rm.GetString("Imbase_ExportToExcel");
        ServiceHolder.OutputView.ClearText(category);
        ServiceHolder.OutputView.WriteString(category, res.ToString());
        ServiceHolder.OutputView.Activate(category);
      }));
    this.OnChanged(BackgroundTaskChangedType.Dispose);
  }

  private StringBuilder ExportData()
  {
    StringBuilder AErrorMessages = new StringBuilder();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        using (ExcelWriter excelWriter = new ExcelWriter(this._Items, this._AttrGuidLst, this._Flags, AErrorMessages, (IExtendedBackgroundTask) this))
          excelWriter.GenerateFile(session, this._FileName);
      }
    }
    catch (Exception ex)
    {
      AErrorMessages.AppendLine(ex.Message);
    }
    return AErrorMessages;
  }

  public override void Resume()
  {
    lock (this._LockObject)
    {
      if (this._AsyncResult == null)
      {
        this._Terminated = this._Stopped = this._Paused = false;
        this._handler = new ImbaseUnloadBackGroundTask.LoadHandler(this.ExportData);
        this._AsyncResult = this._handler.BeginInvoke(new AsyncCallback(this.CallBack), (object) null);
      }
      else
        this._Paused = false;
    }
    this.OnChanged(BackgroundTaskChangedType.State);
  }

  private delegate StringBuilder LoadHandler();
}
