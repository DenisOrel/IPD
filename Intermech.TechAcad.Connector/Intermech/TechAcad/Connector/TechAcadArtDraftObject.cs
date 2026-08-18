// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadArtDraftObject
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.Controls;
using Intermech.TechAcad.Interfaces;
using System;

#nullable disable
namespace Intermech.TechAcad.Connector;

internal class TechAcadArtDraftObject(ObjInfoItem draftInfoItem, NavWindow navWindow) : 
  TechAcadDraftObject(draftInfoItem, navWindow)
{
  public override string Name
  {
    get => base.Name;
    set
    {
    }
  }

  public override ModifyMode ModifyMode => ModifyMode.CantModify;

  public override ITPObjectCollection ObjectCollection => (ITPObjectCollection) null;

  public override string Extract(int checkOutMode)
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._draftInfoItem))
      return string.Empty;
    try
    {
      ITechAcadService service = ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, true);
      return service != null ? service.ExtractPicture(this.DraftID) : string.Empty;
    }
    catch (Exception ex)
    {
      Plugin.LogError(sc_19149.ssp_techacad_19150() + (object) ex);
      throw;
    }
  }

  public override void Close(int needSave)
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._draftInfoItem))
      return;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._draftInfoItem.ObjectID, false);
        if (dbObject == null || dbObject.ObjectModifyMode == ObjectModifyModes.CantModify)
          return;
        ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, false)?.UnloadPicture(this.DraftID);
      }
    }
    catch (Exception ex)
    {
      Plugin.LogError(sc_19149.ssp_techacad_19151() + (object) ex);
      throw;
    }
  }

  public override void Save()
  {
  }

  public override void SaveStucture()
  {
  }
}
