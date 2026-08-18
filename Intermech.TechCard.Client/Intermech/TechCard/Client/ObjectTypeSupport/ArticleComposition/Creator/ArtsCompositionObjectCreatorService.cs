// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Creator.ArtsCompositionObjectCreatorService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Protection;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.TcObjectsTypes.ArtsComposition;
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Creator;

/// <summary>
/// Класс для реализации службы создания объектов типа "Единица состава изделия"
/// </summary>
internal class ArtsCompositionObjectCreatorService : TechCardBaseObjectCreatorService
{
  /// <summary>Custom creation object</summary>
  /// <param name="objectTypeId"></param>
  /// <param name="templateObjectId"></param>
  /// <param name="relationTypeIDs"></param>
  /// <param name="relatedObjectIDs"></param>
  /// <param name="startDate"></param>
  /// <param name="isVersion"></param>
  /// <returns></returns>
  public override long CreateObjectDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (objectTypeId == -1)
      return 0;
    if (relatedObjectIDs == null || relatedObjectIDs.Length == sc_19383.ssp_techcard_19384(579489240))
      return base.CreateObjectDialog(objectTypeId, templateObjectId, relationTypeIDs, relatedObjectIDs, startDate, isVersion);
    long relatedObjectId = relatedObjectIDs[0];
    if (relatedObjectId == 0L)
      return 0;
    IDBTypedObjectID dbTypedObjectId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      dbTypedObjectId = (IDBTypedObjectID) TechcardClientUtils.GetDBTypedObjectID(sessionKeeper.Session.GetObject(relatedObjectId, true));
    ArtsCompositionContextCommandProvider.ContextCommandsHandler commandsMethod;
    if (MetaDataHelper.IsObjectTypeChildOf(objectTypeId, TechCardConsts.ObjectTypes.SobirEdinicaID))
    {
      commandsMethod = new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_AddAssemblingRootCompNode);
    }
    else
    {
      if (!MetaDataHelper.IsObjectTypeChildOf(objectTypeId, TechCardConsts.ObjectTypes.KomlEdinicaID))
        return 0;
      commandsMethod = new ArtsCompositionContextCommandProvider.ContextCommandsHandler(ArtsCompositionContextCommandProvider.Command_AddAccessorySelectedNode);
    }
    ArtsCompositionContextCommandProvider.Command_AddBase(dbTypedObjectId, (IServiceProvider) ApplicationServices.Container, commandsMethod, false);
    return 0;
  }

  /// <summary>Вызывать ли собственный диалог ?</summary>
  /// <param name="objectTypeId"></param>
  /// <param name="templateObjectId"></param>
  /// <param name="relationTypeIDs"></param>
  /// <param name="relatedObjectIDs"></param>
  /// <param name="startDate"></param>
  /// <param name="isVersion"></param>
  /// <returns></returns>
  public override bool AcceptDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (isVersion || this._creatorExtraParams != null && this._creatorExtraParams.RawMode || templateObjectId != 0L && templateObjectId != (long) -sc_19383.ssp_techcard_19385(722752622))
      return false;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeId);
    if (objectType == null || objectType.VersionsMode == ObjectVersionModes.Abstract)
      return false;
    TechObjectCreationMode objectCreationMode = TechObjectCreationMode.Default;
    this._creationModeCache[objectTypeId] = objectCreationMode;
    if (relatedObjectIDs == null || relatedObjectIDs.Length == sc_19383.ssp_techcard_19386(108352687))
      return false;
    QuickObjectInfo objectInfo;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      objectInfo = sessionKeeper.Session.GetObjectInfo(relatedObjectIDs[0]);
    if (objectInfo.Empty)
      return false;
    return MetaDataHelper.IsObjectTypeChildOf(objectInfo.ObjectTypeID, TechCardConsts.ObjectTypes.SobirEdinicaID) || MetaDataHelper.IsObjectTypeChildOf(objectInfo.ObjectTypeID, TechCardConsts.ObjectTypes.KomlEdinicaID);
  }
}
