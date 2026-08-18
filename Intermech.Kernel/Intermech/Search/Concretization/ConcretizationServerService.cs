// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Concretization.ConcretizationServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Intermech.Search.Concretization;

public sealed class ConcretizationServerService : LongLifeObject, IConcretizationServerService
{
  public bool CanSetObjectVersionIDInComposition(Guid userSessionGuid, long relationID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !RelationHelper.IsUnknownRelationID(relationID) ? this.CanSetObjectVersionIDInComposition(relationID) : throw new ArgumentException();
  }

  public string SetObjectVersionIDInComposition(
    Guid userSessionGuid,
    Tuple<long, long>[] relationIDObjectVersionIDTuples)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (relationIDObjectVersionIDTuples == null || relationIDObjectVersionIDTuples.Length == 0 || RelationHelper.IsAnyUnknownRelationID(((IEnumerable<Tuple<long, long>>) relationIDObjectVersionIDTuples).Select<Tuple<long, long>, long>((Func<Tuple<long, long>, long>) (o => o.Item1))))
        throw new ArgumentException();
      return this.SetObjectVersionIDInComposition(relationIDObjectVersionIDTuples);
    }
  }

  private bool CanSetObjectVersionIDInComposition(long relationID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(relationID);
      if (relation.RelationType == ConcretizationConstants.ProductDocumentationRelationTypeID)
        return false;
      IDBObject dbObject = sessionKeeper.Session.GetObject(relation.ProjID);
      if (dbObject.ObjectModifyMode == ObjectModifyModes.InBase || dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.CheckoutBy == sessionKeeper.Session.UserID)
        return true;
      IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(relation.TypeID, Constants.ExplicitPartVersionIDAttributeTypeID);
      return attribute4RelationType != null && attribute4RelationType.Options.HasFlag((Enum) AttributeOptions.ModifyInBase);
    }
  }

  private string SetObjectVersionIDInComposition(
    Tuple<long, long>[] relationIDObjectVersionIDTuples)
  {
    int num = 0;
    Dictionary<long, string> dictionary = new Dictionary<long, string>();
    foreach (Tuple<long, long> objectVersionIdTuple in relationIDObjectVersionIDTuples)
    {
      try
      {
        this.SetObjectVersionIDInComposition(objectVersionIdTuple.Item1, objectVersionIdTuple.Item2);
        ++num;
      }
      catch (Exception ex)
      {
        dictionary.Add(objectVersionIdTuple.Item1, ex.Message);
      }
    }
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("Выполнение команды завершено.{0}Количество успешно обработанных связей {1}.{0}", (object) Environment.NewLine, (object) num);
    stringBuilder.AppendFormat("Количество связей обработанных с ошибками {0}.{1}", (object) dictionary.Count, (object) Environment.NewLine);
    if (dictionary.Count > 0)
    {
      stringBuilder.AppendFormat("Ошибки:{0}", (object) Environment.NewLine);
      foreach (KeyValuePair<long, string> keyValuePair in dictionary)
        stringBuilder.AppendFormat("cвязь {0}: {1},{2}", (object) keyValuePair.Key, (object) keyValuePair.Value, (object) Environment.NewLine);
    }
    return stringBuilder.ToString();
  }

  private void SetObjectVersionIDInComposition(long relationID, long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(relationID, true);
      IDBObject dbObject = sessionKeeper.Session.GetObject(relation.ProjID, true);
      IDBObject objectById = sessionKeeper.Session.GetObjectByID(relation.PartID, true);
      if (!ObjectTypeApplicabilityHelper.IsSoftConcretizationMode(dbObject.TypeID, relation.TypeID, objectById.TypeID))
        throw new Exception($"Невозможно выполнить абстрагирование/конкретизацию. Для связи '{RelationTypeHelper.GetRelationTypeName(relation.TypeID)}' в конфигураторе запрещена конкретизация пользователем");
      if (this.IsFixingBaseVersion(relation.ProjID))
        throw new Exception($"Невозможно выполнить абстрагирование/конкретизацию. Для шага жизненного цикла '{LifecycleStepHelper.GetLifecycleStepName(dbObject.LCStep)}' в конфигураторе установлена опция фиксации базовой версии");
      relation.SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(Constants.ExplicitPartVersionIDAttributeTypeID, (object) Math.Abs(objectVersionID))
      });
    }
  }

  private bool IsFixingBaseVersion(long projectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int lcStep = sessionKeeper.Session.GetObject(projectVersionID, true).LCStep;
      return sessionKeeper.Session.GetLifecycleStep(lcStep).Options.HasFlag((Enum) LCStepOptions.BaseVersion);
    }
  }
}
