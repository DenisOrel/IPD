// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.SeriesDates
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Sets;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Expert;

public class SeriesDates
{
  public long contextId = -1;
  public Dictionary<long, SeriesDates.MainIzdel> izdList;
  public List<SeriesDates.ErrInfo> errList;
  public static readonly string guidAttrRevSeriesDates = "cadd9506-306c-11d8-b4e9-00304f19f545";
  public static readonly string guidAttrSeriesDatesAppl = "cadd940c-306c-11d8-b4e9-00304f19f545";
  public static readonly int seriesDatesApplId;
  public static Dictionary<int, bool> serDatesAppl = new Dictionary<int, bool>();

  static SeriesDates()
  {
    SeriesDates.seriesDatesApplId = MetaDataHelper.GetAttributeTypeID(SeriesDates.guidAttrSeriesDatesAppl);
  }

  public SeriesDates(long contId)
  {
    this.contextId = contId;
    this.izdList = new Dictionary<long, SeriesDates.MainIzdel>();
    this.errList = new List<SeriesDates.ErrInfo>();
  }

  public static bool HasSeriesDatesApplicability(IUserSession ius, int objTypeId)
  {
    if (SeriesDates.serDatesAppl.ContainsKey(objTypeId))
      return SeriesDates.serDatesAppl[objTypeId];
    IDBObjectType objectType = ius.GetObjectType(objTypeId, false);
    if (objectType == null)
    {
      SeriesDates.serDatesAppl.Add(objTypeId, false);
      return false;
    }
    bool flag = objectType.Attributes.GetAttributeByID(SeriesDates.seriesDatesApplId, false) != null;
    SeriesDates.serDatesAppl.Add(objTypeId, flag);
    return flag;
  }

  public void LoadContextObjects(IUserSession session)
  {
    IDBEditingContextsObject editingContextsObject = (IDBEditingContextsObject) session.GetObject(this.contextId, false);
    if (editingContextsObject == null)
      return;
    List<long> versionsId = editingContextsObject.GetEditingContextsObjectContainer(true, true).GetVersionsID(true, session.UserID);
    IDBAttribute attributeByGuid1 = editingContextsObject.GetAttributeByGuid(new Guid(SeriesDates.guidAttrRevSeriesDates));
    string val = attributeByGuid1 != null ? Convert.ToString(attributeByGuid1.Value) : "";
    if (this.izdList.Count > 0)
      this.izdList.Clear();
    for (int index = versionsId.Count - 1; index >= 0; --index)
    {
      long objectID = versionsId[index];
      IDBObject objectActualCopy = session.GetObjectActualCopy(objectID, false);
      if ((objectActualCopy == null ? 1 : (!SeriesDates.HasSeriesDatesApplicability(session, objectActualCopy.ObjectType) ? 1 : 0)) != 0)
        versionsId.RemoveAt(index);
    }
    foreach (long objectID in versionsId)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(objectID, false);
      string caption = objectActualCopy.Caption;
      long objectId = objectActualCopy.ObjectID;
      string cNo = "??";
      IDBAttribute attributeByGuid2 = objectActualCopy.GetAttributeByGuid(new Guid("cad00770-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid2 != null && attributeByGuid2.Value != DBNull.Value)
        cNo = Convert.ToString(attributeByGuid2.Value);
      SeriesDatesApplicabilityCollection applicabilityCollection = new SeriesDatesApplicabilityCollection((object) objectActualCopy);
      long key = Math.Abs(objectID);
      if (applicabilityCollection.Items.Count <= 0 && val != "")
        applicabilityCollection.FromString(val);
      if (applicabilityCollection.Items.Count == 0)
        applicabilityCollection.Items.Add(new SeriesDatesApplicability());
      foreach (SeriesDatesApplicability sda in applicabilityCollection.Items)
      {
        if (sda.MainObjectID != 0L && sda.MainObjectID != -1L)
        {
          long num = Math.Abs(sda.MainObjectID);
          SeriesDates.SeriesDatesRec seriesDatesRec = new SeriesDates.SeriesDatesRec(objectId, cNo, objectActualCopy.ObjectType, sda, caption);
          if (this.izdList.ContainsKey(num))
          {
            SeriesDates.MainIzdel izd = this.izdList[num];
            if (izd.allRecs.ContainsKey(key))
            {
              izd.allRecs[key].sda = sda;
              izd.allRecs[key].objTypeId = objectActualCopy.ObjectType;
            }
            else
              izd.allRecs.Add(Math.Abs(objectID), seriesDatesRec);
          }
          else
          {
            SeriesDates.MainIzdel mainIzdel = new SeriesDates.MainIzdel(session, num);
            this.izdList.Add(num, mainIzdel);
            mainIzdel.allRecs.Add(Math.Abs(objectID), seriesDatesRec);
          }
        }
      }
    }
    foreach (long num1 in versionsId)
    {
      foreach (long allObjectVersions in session.GetAllObjectVersionsList(num1, false, false, false))
      {
        if (Math.Abs(allObjectVersions) != Math.Abs(num1))
        {
          IDBObject source = session.GetObject(allObjectVersions, false);
          if (source != null)
          {
            string cNo = "??";
            IDBAttribute attributeByGuid3 = source.GetAttributeByGuid(new Guid("cad00770-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid3 != null && attributeByGuid3.Value != DBNull.Value)
              cNo = Convert.ToString(attributeByGuid3.Value);
            foreach (SeriesDatesApplicability sda in new SeriesDatesApplicabilityCollection((object) source).Items)
            {
              if (sda.MainObjectID != 0L && sda.MainObjectID != -1L)
              {
                long num2 = Math.Abs(sda.MainObjectID);
                SeriesDates.MainIzdel mainIzdel;
                if (this.izdList.ContainsKey(num2))
                {
                  mainIzdel = this.izdList[num2];
                }
                else
                {
                  mainIzdel = new SeriesDates.MainIzdel(session, num2);
                  this.izdList.Add(num2, mainIzdel);
                }
                long key = Math.Abs(num1);
                SeriesDates.SeriesDatesRec seriesDatesRec;
                if (mainIzdel.allRecs.ContainsKey(key))
                {
                  seriesDatesRec = mainIzdel.allRecs[key];
                }
                else
                {
                  QuickObjectInfo objectInfo = session.GetObjectInfo(num1);
                  seriesDatesRec = new SeriesDates.SeriesDatesRec(num1, "??", (SeriesDatesApplicability) null, objectInfo.Caption);
                  mainIzdel.allRecs.Add(key, seriesDatesRec);
                }
                SeriesDates.VersionRec versionRec = new SeriesDates.VersionRec(allObjectVersions, cNo, sda, source.Caption);
                seriesDatesRec.otherVersions.Add(versionRec);
              }
            }
          }
        }
      }
    }
  }

  /// <summary>Создать общий список пересечений</summary>
  /// <param name="onlyPrimary">Не принимать во внимание пересечения, не задевающие версии в этом извещении</param>
  /// <returns>Количество ошибок</returns>
  public int CheckForErrors(bool onlyPrimary)
  {
    this.errList.Clear();
    foreach (long key in this.izdList.Keys)
    {
      SeriesDates.MainIzdel izd = this.izdList[key];
      if (!izd.IsEmpty)
        this.CheckOneIzdForErrors(izd, onlyPrimary);
    }
    return this.errList.Count;
  }

  public void CheckOneIzdForErrors(SeriesDates.MainIzdel mi, bool onlyPrimary)
  {
    long izdelId = mi.izdelId;
    this.errList.Clear();
    List<long> list = mi.allRecs.Keys.ToList<long>();
    for (int index1 = 0; index1 < list.Count; ++index1)
    {
      ApplicabilityBy? kind = new ApplicabilityBy?();
      SeriesDates.SeriesDatesRec allRec = mi.allRecs[list[index1]];
      if (allRec.sda != null)
      {
        this.PerformApplicabilityBy(ref kind, allRec.sda.Applicability, izdelId, allRec.verId, -1);
        this.CompactSet(allRec.sda.Set);
      }
      if (allRec.sda == null || allRec.sda.Set.IsEmpty)
        this.errList.Add(new SeriesDates.ErrInfo(SeriesDates.SeriesDatesErrType.sdeEmptyDiap, izdelId, list[index1]));
      if (!onlyPrimary)
      {
        for (int index2 = 0; index2 < allRec.otherVersions.Count; ++index2)
        {
          SeriesDates.VersionRec otherVersion = allRec.otherVersions[index2];
          if (otherVersion.sda != null)
          {
            this.PerformApplicabilityBy(ref kind, otherVersion.sda.Applicability, izdelId, allRec.verId, index2);
            this.CompactSet(otherVersion.sda.Set);
            if (this.CheckSelfIntersection(otherVersion.sda))
              this.errList.Add(new SeriesDates.ErrInfo(SeriesDates.SeriesDatesErrType.sdeApplicabilityIntersects, izdelId, list[index1], index2));
          }
        }
      }
      if (this.CheckSelfIntersection(allRec.sda))
        this.errList.Add(new SeriesDates.ErrInfo(SeriesDates.SeriesDatesErrType.sdeApplicabilityIntersects, izdelId, list[index1]));
      if (allRec.sda != null)
      {
        for (int index3 = 0; index3 < allRec.otherVersions.Count; ++index3)
        {
          SeriesDates.VersionRec otherVersion = allRec.otherVersions[index3];
          if (otherVersion.sda != null && this.FindIntersections(allRec.sda.Set, otherVersion.sda.Set))
            this.errList.Add(new SeriesDates.ErrInfo(SeriesDates.SeriesDatesErrType.sdeIntersects, izdelId, list[index1], index3));
        }
      }
    }
    ISet set = (ISet) null;
    for (int index4 = 0; index4 < list.Count; ++index4)
    {
      SeriesDates.SeriesDatesRec allRec = mi.allRecs[list[index4]];
      if (allRec.sda != null)
      {
        if (set == null)
          set = allRec.sda.Applicability != ApplicabilityBy.Series ? (ISet) new Set<DateTime>() : (ISet) new Set<int>();
        bool flag = set.CanAdd(allRec.sda.Set);
        if (flag)
        {
          set.Add(allRec.sda.Set);
          for (int index5 = 0; index5 < allRec.otherVersions.Count; ++index5)
          {
            SeriesDates.VersionRec otherVersion = allRec.otherVersions[index5];
            if (otherVersion.sda != null)
            {
              if (!set.CanAdd(otherVersion.sda.Set))
              {
                flag = false;
                break;
              }
              set.Add(otherVersion.sda.Set);
            }
          }
        }
        if (flag)
        {
          if (set is Set<int>)
            ((Set<int>) set).Compact();
          else
            ((Set<DateTime>) set).Compact();
          if (set.Count > 1)
            this.errList.Add(new SeriesDates.ErrInfo(SeriesDates.SeriesDatesErrType.sdeHole, izdelId, list[index4]));
        }
      }
    }
  }

  private void PerformApplicabilityBy(
    ref ApplicabilityBy? kind,
    ApplicabilityBy now,
    long mainIzd,
    long verId,
    int secondIndex)
  {
    if (!kind.HasValue)
    {
      kind = new ApplicabilityBy?(now);
    }
    else
    {
      ApplicabilityBy? nullable = kind;
      ApplicabilityBy applicabilityBy = now;
      if (nullable.GetValueOrDefault() == applicabilityBy & nullable.HasValue)
        return;
      this.errList.Add(new SeriesDates.ErrInfo(SeriesDates.SeriesDatesErrType.sdeMixedSerieDate, mainIzd, verId, secondIndex));
    }
  }

  private void CompactSet(ISet set)
  {
    if (set is Set<int>)
      ((Set<int>) set).Compact();
    if (!(set is Set<DateTime>))
      return;
    ((Set<DateTime>) set).Compact();
  }

  public bool CheckSelfIntersection(SeriesDatesApplicability sda)
  {
    return sda != null && sda.Set != null && (sda.Set is Set<int> && this.CheckSetIntersections(sda.Set as Set<int>) || sda.Set is Set<DateTime> && this.CheckSetIntersections(sda.Set as Set<DateTime>));
  }

  private bool CheckSetIntersections(Set<int> set)
  {
    List<IRange<int>> ranges = set.Ranges;
    for (int index1 = 0; index1 < ranges.Count - 1; ++index1)
    {
      for (int index2 = index1 + 1; index2 < ranges.Count; ++index2)
      {
        if (((Int32Range) ranges[index1]).IsIntersect(ranges[index2]))
          return true;
      }
    }
    return false;
  }

  private bool CheckSetIntersections(Set<DateTime> set)
  {
    List<IRange<DateTime>> ranges = set.Ranges;
    for (int index1 = 0; index1 < ranges.Count - 1; ++index1)
    {
      for (int index2 = index1 + 1; index2 < ranges.Count; ++index2)
      {
        if (((DateTimeRange) ranges[index1]).IsIntersect(ranges[index2]))
          return true;
      }
    }
    return false;
  }

  public bool FindIntersections(ISet set_1, ISet set_2)
  {
    return set_1 is Set<int> && set_2 is Set<int> && ((Set<int>) set_1).IsIntersectsWith(set_2) || set_1 is Set<DateTime> && set_2 is Set<DateTime> && ((Set<DateTime>) set_1).IsIntersectsWith(set_2);
  }

  public class VersionRec
  {
    public long verId = -1;
    public SeriesDatesApplicability sda;
    public string Description = "";
    public string changeNo = "??";

    public VersionRec(long verId, string cNo)
    {
      this.verId = verId;
      this.changeNo = cNo;
    }

    public VersionRec(long verId, string cNo, SeriesDatesApplicability sda)
    {
      this.verId = verId;
      this.changeNo = cNo;
      this.sda = sda;
      if (sda == null || sda.Set != null)
        return;
      sda.Set = (ISet) new Set<int>();
    }

    public VersionRec(long verId, string cNo, SeriesDatesApplicability sda, string Desc)
    {
      this.verId = verId;
      this.changeNo = cNo;
      this.sda = sda;
      this.Description = Desc;
      if (sda == null || sda.Set != null)
        return;
      sda.Set = (ISet) new Set<int>();
    }
  }

  public class SeriesDatesRec : SeriesDates.VersionRec
  {
    public int objTypeId = -1;
    public List<SeriesDates.VersionRec> otherVersions;

    public SeriesDatesRec(long verId, string cNo)
      : base(verId, cNo)
    {
      this.otherVersions = new List<SeriesDates.VersionRec>();
    }

    public SeriesDatesRec(long verId, string cNo, SeriesDatesApplicability sda)
      : base(verId, cNo, sda)
    {
      this.otherVersions = new List<SeriesDates.VersionRec>();
    }

    public SeriesDatesRec(long verId, string cNo, SeriesDatesApplicability sda, string Desc)
      : base(verId, cNo, sda, Desc)
    {
      this.otherVersions = new List<SeriesDates.VersionRec>();
    }

    public SeriesDatesRec(long verId, string cNo, int objTypeId, SeriesDatesApplicability sda)
      : base(verId, cNo, sda)
    {
      this.objTypeId = objTypeId;
      this.otherVersions = new List<SeriesDates.VersionRec>();
    }

    public SeriesDatesRec(
      long verId,
      string cNo,
      int objTypeId,
      SeriesDatesApplicability sda,
      string Desc)
      : base(verId, cNo, sda, Desc)
    {
      this.objTypeId = objTypeId;
      this.otherVersions = new List<SeriesDates.VersionRec>();
    }
  }

  public class MainIzdel
  {
    public long izdelId = -1;
    public int izdType = -1;
    public string Description = "";
    public Dictionary<long, SeriesDates.SeriesDatesRec> allRecs;

    public MainIzdel(IUserSession ius, long Id)
    {
      this.izdelId = Id;
      if (Id != -1L)
      {
        IDBObject dbObject = ius.GetObject(Id, false);
        if (dbObject != null)
        {
          this.izdType = dbObject.ObjectType;
          this.Description = dbObject.Caption;
        }
      }
      this.allRecs = new Dictionary<long, SeriesDates.SeriesDatesRec>();
    }

    public bool IsEmpty => this.izdelId == -1L;
  }

  public enum SeriesDatesErrType
  {
    [CustomDescription("Expert.Error_0001")] sdeMixedSerieDate,
    [CustomDescription("Expert.Error_0002")] sdeApplicabilityIntersects,
    [CustomDescription("Expert.Error_0003")] sdeIntersects,
    [CustomDescription("Expert.Error_0004")] sdeEmptyDiap,
    [CustomDescription("Expert.Error_0005")] sdeHole,
  }

  public class ErrInfo
  {
    public SeriesDates.SeriesDatesErrType errType;
    public long mainObjectId = -1;
    public long primaryVerId = -1;
    public int secondIndex = -1;
    public string errMessage = "";

    public ErrInfo(SeriesDates.SeriesDatesErrType eType, long mainOId)
    {
      this.errType = eType;
      this.mainObjectId = mainOId;
    }

    public ErrInfo(SeriesDates.SeriesDatesErrType eType, long mainOId, long primId)
    {
      this.errType = eType;
      this.mainObjectId = mainOId;
      this.primaryVerId = primId;
    }

    public ErrInfo(SeriesDates.SeriesDatesErrType eType, long mainOId, long primId, int appIndex2)
    {
      this.errType = eType;
      this.mainObjectId = mainOId;
      this.primaryVerId = primId;
      this.secondIndex = appIndex2;
    }

    /// <summary>Заполняем сообщение об ошибке</summary>
    /// <returns>Сообщение об ошибке</returns>
    public string ComposeMessage(SeriesDates sd)
    {
      string str1 = "";
      SeriesDates.MainIzdel mainIzdel = (SeriesDates.MainIzdel) null;
      if (this.mainObjectId != -1L && sd.izdList.ContainsKey(this.mainObjectId))
      {
        mainIzdel = sd.izdList[this.mainObjectId];
        str1 = $"{mainIzdel.Description} [{Convert.ToString(mainIzdel.izdelId)}]";
      }
      string str2 = "";
      string str3 = "";
      if (mainIzdel != null && this.primaryVerId != -1L && mainIzdel.allRecs.ContainsKey(this.primaryVerId))
      {
        SeriesDates.SeriesDatesRec allRec = mainIzdel.allRecs[this.primaryVerId];
        str2 = Convert.ToString(this.primaryVerId);
        if (this.secondIndex != -1 && this.secondIndex < allRec.otherVersions.Count)
          str3 = Convert.ToString(allRec.otherVersions[this.secondIndex].verId);
      }
      switch (this.errType)
      {
        case SeriesDates.SeriesDatesErrType.sdeMixedSerieDate:
          this.errMessage = string.Format(LocalizationHolder.rm.GetString("Expert_232"), (object) str1);
          break;
        case SeriesDates.SeriesDatesErrType.sdeApplicabilityIntersects:
          this.errMessage = string.Format(LocalizationHolder.rm.GetString("Expert_233"), (object) str1, (object) str2);
          break;
        case SeriesDates.SeriesDatesErrType.sdeIntersects:
          this.errMessage = string.Format(LocalizationHolder.rm.GetString("Expert_234"), (object) str1, (object) str2, (object) str3);
          break;
        case SeriesDates.SeriesDatesErrType.sdeEmptyDiap:
          this.errMessage = string.Format(LocalizationHolder.rm.GetString("Expert_235"), (object) str1, (object) str2);
          break;
        case SeriesDates.SeriesDatesErrType.sdeHole:
          this.errMessage = string.Format(LocalizationHolder.rm.GetString("Expert_236"), (object) str1, (object) str2);
          break;
      }
      return this.errMessage;
    }

    public long GetSecondVerId(SeriesDates sd)
    {
      if (this.mainObjectId == -1L || sd.izdList.ContainsKey(this.mainObjectId))
        return -1;
      SeriesDates.MainIzdel izd = sd.izdList[this.mainObjectId];
      if (izd != null && this.primaryVerId != -1L && izd.allRecs.ContainsKey(this.primaryVerId))
      {
        SeriesDates.SeriesDatesRec allRec = izd.allRecs[this.primaryVerId];
        if (this.secondIndex != -1 && this.secondIndex < allRec.otherVersions.Count)
          return allRec.otherVersions[this.secondIndex].verId;
      }
      return -1;
    }
  }
}
