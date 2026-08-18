// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Zagot.ZagotInTpObjectCreatorControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.Services.ClassifyObject;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Zagot;

internal class ZagotInTpObjectCreatorControl : ZagotObjectCreatorControl
{
  /// <summary>
  /// 
  /// </summary>
  public ZagotInTpObjectCreatorControl()
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="createdObject"></param>
  /// <param name="creatorExtraParams"></param>
  public ZagotInTpObjectCreatorControl(
    CreatedObjectItem createdObject,
    IObjectCreatorParams creatorExtraParams)
    : base(createdObject, creatorExtraParams)
  {
    this.GroupArticleVisible = false;
  }

  protected override void LoadContextObjectData()
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <returns></returns>
  protected override void ClassifyObjName(IUserSession session)
  {
    ITechCardClassifyObjectService service = ServiceUtils.GetService<ITechCardClassifyObjectService>((object) ApplicationServices.Container, true);
    ObjInfoItem classifyObjectItem = new ObjInfoItem(this.CreatedObject.ObjectID, this.CreatedObject.ObjectTypeID);
    ObjInfoItem contextObjectItem = (ObjInfoItem) null;
    IEnumerable<ObjInfoItem> objInfoItems = (IEnumerable<ObjInfoItem>) null;
    if ((this._creatorExtraParams is TechObjectCreatorParams creatorExtraParams ? creatorExtraParams.Items : (ISelectedItems) null) != null)
    {
      contextObjectItem = creatorExtraParams.Items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData ? new ObjInfoItem(itemData.Value, itemData.ObjectType) : (ObjInfoItem) null;
      IEnumerable<RelObjInfoItem> relObjInfoItems;
      if (TechcardClientControlsUtils.GetItemsApplicabilityInfo(creatorExtraParams.Items, (IServiceProvider) ApplicationServices.Container, out relObjInfoItems))
        objInfoItems = (IEnumerable<ObjInfoItem>) relObjInfoItems.Select<RelObjInfoItem, ObjInfoItem>((Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo)).ToList<ObjInfoItem>();
    }
    ITechCardClassifyObjectService classifyObjectService1 = service;
    IUserSession session1 = session;
    TechCardClassifyObjectAttributeParams classifyParams1 = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.DesignationAttrTypeID, classifyObjectItem, contextObjectItem);
    classifyParams1.ExtraContextObjInfoItems = objInfoItems;
    TechCardClassifyObjectDesignationStrategy classifyStrategy1 = new TechCardClassifyObjectDesignationStrategy();
    string str1;
    ref string local1 = ref str1;
    int num1 = classifyObjectService1.ClassifyObjectAttribute(session1, classifyParams1, (ITechCardClassifyObjectStrategy) classifyStrategy1, out local1) ? 1 : 0;
    ITechCardClassifyObjectService classifyObjectService2 = service;
    IUserSession session2 = session;
    TechCardClassifyObjectAttributeParams classifyParams2 = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.NameAttrTypeID, classifyObjectItem, contextObjectItem);
    classifyParams2.ExtraContextObjInfoItems = objInfoItems;
    TechCardClassifyObjectNameStrategy classifyStrategy2 = new TechCardClassifyObjectNameStrategy();
    string str2;
    ref string local2 = ref str2;
    int num2 = classifyObjectService2.ClassifyObjectAttribute(session2, classifyParams2, (ITechCardClassifyObjectStrategy) classifyStrategy2, out local2) ? 1 : 0;
    if ((num1 | num2) == 0)
      return;
    this.ObjectName = str2;
    this.ObjectDesignation = str1;
  }
}
