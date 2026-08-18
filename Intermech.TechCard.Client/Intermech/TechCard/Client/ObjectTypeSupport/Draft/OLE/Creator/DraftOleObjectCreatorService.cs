// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.Draft.OLE.Creator.DraftOleObjectCreatorService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.Draft.OLE.Creator;

/// <summary>Сервис создания объектов типа "Эскиз OLE"</summary>
internal class DraftOleObjectCreatorService : TechObjectCreatorRiderCustomService
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="newObjectId"></param>
  /// <returns></returns>
  public override bool AfterCreate(long newObjectId)
  {
    if (this._creatorArgs == null || this._creatorArgs.IsVersion || this._creatorArgs.TemplateObjectIDs != null && ((IEnumerable<long>) this._creatorArgs.TemplateObjectIDs).Any<long>((Func<long, bool>) (item => item != 0L && item != -1L)))
      return true;
    Stream stream;
    if (!DraftOleEditDialog.CreateOle(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_79"), (object) Math.Abs(newObjectId)), false, out stream))
      return false;
    using (DraftOleClass draftOleClass = new DraftOleClass())
    {
      draftOleClass.ObjectId = newObjectId;
      draftOleClass.DataStream = stream;
      draftOleClass.SaveData();
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      IDictionary<ObjectCreatePages, bool> visiblePages = base.VisiblePages;
      if (!visiblePages.ContainsKey(ObjectCreatePages.Relations))
        visiblePages.Add(ObjectCreatePages.Relations, true);
      else
        visiblePages[ObjectCreatePages.Relations] = true;
      return visiblePages;
    }
  }
}
