// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ModParm
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// Base abstract class for storing modifier parms in tree nodes
/// </summary>
public abstract class ModParm
{
  public ModParm()
  {
  }

  public ModParm(ref ModParmData modData)
  {
  }

  public abstract void Clear();

  public abstract void SetData(ref ModParmData modData);

  public abstract void FillModParmData(ref ModParmData modData);

  public abstract void WriteToXML(ref XmlTextWriter writer);

  public abstract void LoadFromXML(XmlNode node, int modTag);

  public abstract bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs);

  public abstract bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs);

  public abstract bool FixIdentsComplete(IUserSession ius);

  public abstract bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius);

  /// <summary>
  /// Обработать событие слияния атрибутов - заменить один атрибут на другой.
  /// </summary>
  /// <param name="fromAttribute">Заменяемый атрибут</param>
  /// <param name="toAttribute">Заменяющий атрибут</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns>true, если что-то изменилось при переводе</returns>
  public virtual bool PerformAttrCombine(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session)
  {
    return false;
  }
}
