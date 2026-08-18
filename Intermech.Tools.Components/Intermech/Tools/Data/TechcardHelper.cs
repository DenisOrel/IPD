// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.TechcardHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Kernel.Search;
using System;
using System.Data;

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>Обслуживает технологические заготовки.</summary>
internal static class TechcardHelper
{
  /// <summary>
  /// Проверяет, совпадает ли идентификатор указанного типа объекта с идентификатором технологических заготовок.
  /// </summary>
  /// <param name="objectType">Идентификатор типа проверяемого объекта</param>
  /// <returns>true, если это технологическая заготовка</returns>
  public static bool IsTechBlank(int objectType)
  {
    return objectType == TechcardHelper.InternalIDCache.TechBlankType.Id;
  }

  /// <summary>
  /// Находит в базе IPS заготовку по значению нормализованного индекса.
  /// </summary>
  /// <param name="text">Обозначение или наименование заготовки</param>
  /// <returns>Идентификатор объекта заготовки или Intermech.Consts.UnknownObjectId</returns>
  public static long FindTechBlankId(string text)
  {
    ObjectLocatorResult techBlank = TechcardHelper.FindTechBlank(text);
    return techBlank == null ? 0L : techBlank.ObjectId;
  }

  /// <summary>
  /// Находит в базе IPS заготовку по значению нормализованного индекса.
  /// </summary>
  /// <param name="text">Обозначение или наименование заготовки</param>
  /// <returns>Описатель объекта заготовки или null</returns>
  public static ObjectLocatorResult FindTechBlank(string text)
  {
    if (text == null)
      throw new ArgumentNullException(nameof (text));
    ConditionStructure conditionStructure = new ConditionStructure(IDCache.Default.NormalizedId.Id, RelationalOperators.Equal, (object) text, LogicalOperators.NONE, 0, true);
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.Conditions = new ConditionStructure[1]
    {
      conditionStructure
    };
    paramSet.RecordCount = 1;
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      dataTable = sessionKeeper.Session.GetObjectCollection(TechcardHelper.InternalIDCache.TechBlankType.Id).Select(paramSet);
    if (dataTable.Rows.Count == 0)
      return (ObjectLocatorResult) null;
    DataRow row = dataTable.Rows[0];
    return new ObjectLocatorResult(Convert.ToInt64(row[0]), Convert.ToInt32(row[1]));
  }

  private static class InternalIDCache
  {
    /// <summary>
    /// Метаданные для типа объектов "Технологические объекты\Заготовки"
    /// </summary>
    public static readonly ObjectTypeResolver TechBlankType = MetadataResolvers.Factory.ObjectTypeResolver(new Guid("CAD001DA-306C-11D8-B4E9-00304F19F545"));
  }
}
