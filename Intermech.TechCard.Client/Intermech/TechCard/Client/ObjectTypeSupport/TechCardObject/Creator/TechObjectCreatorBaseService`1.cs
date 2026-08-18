// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator.TechObjectCreatorBaseService`1
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;

/// <summary>
/// Класс для реализации службы создания объектов с поддержкой базового контрола
/// </summary>
internal class TechObjectCreatorBaseService<TControl> : TechObjectCreatorRiderCustomService where TControl : TechObjectCreatorBaseControl
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="createdObject"></param>
  /// <param name="propPageIndex"></param>
  /// <returns></returns>
  public override Dictionary<UserControl, int> AddPages(object createdObject, int propPageIndex)
  {
    if (!(createdObject is CreatedObjectItem createdObjectItem))
      return (Dictionary<UserControl, int>) null;
    Dictionary<UserControl, int> dictionary = new Dictionary<UserControl, int>();
    if ((this._creatorExtraParams == null || !this._creatorExtraParams.RawMode) && !createdObjectItem.IsVersion)
    {
      int num = 0;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (ObjectsClassifyHelper.GetClassifierType(sessionKeeper.Session, ((IEnumerable<int>) this._creatorArgs.ObjectTypeIDs).FirstOrDefault<int>()) != ObjectsClassifyType.None)
          num = 1;
      }
      ConstructorInfo constructor = typeof (TControl).GetConstructor(new System.Type[2]
      {
        typeof (CreatedObjectItem),
        typeof (IObjectCreatorParams)
      });
      dictionary.Add((UserControl) constructor.Invoke(new object[2]
      {
        (object) createdObjectItem,
        (object) this._creatorExtraParams
      }), num);
    }
    return dictionary;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="newObjectId"></param>
  /// <returns></returns>
  public override bool AfterCreate(long newObjectId) => true;

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
