// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.ImMaterial
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Imbase.API;

[ComVisible(true)]
[Guid("AD16BDB8-CBCD-4C34-8BBE-4B70349FD7E6")]
[ProgId("IPS.ImMaterial")]
[ClassInterface(ClassInterfaceType.None)]
[ComDefaultInterface(typeof (IImMaterial))]
public class ImMaterial : FreeThreadedObject, IImMaterial
{
  private EWorkViewerMode _mode;
  private readonly IInvokeService _invoker;

  public ImMaterial()
  {
    this._invoker = ServiceUtils.GetService<IInvokeService>((object) ServicesManager.ServiceContainer, true);
  }

  public void SetCurrentSystem(int lSystemFlag)
  {
  }

  public void SetWorkMode(int lWorkMode) => this._mode = (EWorkViewerMode) lWorkMode;

  public void SetInitData(string bsTypesizeImKey, string bsMaterialImKey, int lProfileFolderLevel)
  {
  }

  public void SetInitData2(string bsImKey)
  {
  }

  public void RunViewer(out string pbsSelectedObjDefinition, out int plIsImKey)
  {
    plIsImKey = 0;
    pbsSelectedObjDefinition = string.Empty;
    if ((this._mode & EWorkViewerMode.WVM_Coating) == EWorkViewerMode.WVM_Coating)
      pbsSelectedObjDefinition = this._invoker.InvokeFunc<string>(-1, (Func<string>) (() => this.GetCoatingOrGlue(EWorkViewerMode.WVM_Coating)));
    else if ((this._mode & EWorkViewerMode.WVM_Glue) == EWorkViewerMode.WVM_Glue)
    {
      pbsSelectedObjDefinition = this._invoker.InvokeFunc<string>(-1, (Func<string>) (() => this.GetCoatingOrGlue(EWorkViewerMode.WVM_Glue)));
    }
    else
    {
      string tempKey = this._invoker.InvokeFunc<string>(-1, (Func<string>) (() => this.GetMaterial()));
      if (string.IsNullOrEmpty(tempKey))
        return;
      plIsImKey = 1;
      string empty = string.Empty;
      CadmechHelper.CreateObjectFromTempKey(tempKey, ref empty);
      pbsSelectedObjDefinition = empty;
    }
  }

  public void GetDescriptionByKey(string bsImKey, out string pbsDescription)
  {
    if (bsImKey.Length == 38 && bsImKey.StartsWith("IG"))
      bsImKey = bsImKey.Substring(2);
    Guid guid = new Guid(bsImKey);
    pbsDescription = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectById = sessionKeeper.Session.GetObjectByID(guid, true);
      pbsDescription = objectById.Caption;
    }
  }

  public void GetImbaseMenuItems(string bsImKey, int lFolder, ref string pbsMenuItems)
  {
  }

  public void ExecuteCommand(string bsImKey, int lCommand)
  {
  }

  public void ClearBase()
  {
  }

  public void CanWorkWithBase(out int plValidBase) => plValidBase = 1;

  public void GetMaterialKeyByTypesize(string bsImKey, out string pbsMaterialImKey)
  {
    pbsMaterialImKey = string.Empty;
  }

  public void RunViewer2(out string pbsSelectedObjImKey, out string pbsSelectedObjDescription)
  {
    pbsSelectedObjImKey = string.Empty;
    pbsSelectedObjDescription = string.Empty;
    if ((this._mode & EWorkViewerMode.WVM_Coating) == EWorkViewerMode.WVM_Coating)
    {
      pbsSelectedObjDescription = this._invoker.InvokeFunc<string>(-1, (Func<string>) (() => this.GetCoatingOrGlue(EWorkViewerMode.WVM_Coating)));
    }
    else
    {
      if ((this._mode & EWorkViewerMode.WVM_Glue) != EWorkViewerMode.WVM_Glue)
        return;
      pbsSelectedObjDescription = this._invoker.InvokeFunc<string>(-1, (Func<string>) (() => this.GetCoatingOrGlue(EWorkViewerMode.WVM_Glue)));
    }
  }

  private string GetCoatingOrGlue(EWorkViewerMode mode)
  {
    string coatingOrGlue = string.Empty;
    if (ServicesManager.GetService(typeof (IIMHSelector)) is IIMHSelector service)
      coatingOrGlue = mode == EWorkViewerMode.WVM_Coating ? service.SelectCoatingDesignation() : service.SelectGlueDesignation();
    return coatingOrGlue;
  }

  private string GetMaterial()
  {
    string empty = string.Empty;
    if (ServicesManager.GetService(typeof (IIMHSelector)) is IIMHSelector service)
    {
      List<string> stringList = service.SelectMaterial(false, false);
      if (stringList.Count > 0)
        empty = stringList[0];
    }
    return empty;
  }
}
