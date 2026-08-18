
// Type: Intermech.Interfaces.IMSApplicabilityExtension
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Расширение для работы IMSApplicability. Позволяет получить настройки применяемости по всем дочерним типам для текущих правил
    /// </summary>
    public static class IMSApplicabilityExtension
    {
      /// <summary>
      /// Разрешенные применяемости всех дочерних типов объектов
      /// </summary>
      /// <param name="applicabilities"></param>
      /// <returns></returns>
      public static IEnumerable<ApplicabilitiesKey> GetEnableChildApplicabilitiesKey(
        this IEnumerable<IMSApplicability> applicabilities)
      {
        return (IEnumerable<ApplicabilitiesKey>) applicabilities.GetChildObjTypeApplicabilityModes().Where<KeyValuePair<ApplicabilitiesKey, IMSApplicability>>((Func<KeyValuePair<ApplicabilitiesKey, IMSApplicability>, bool>) (a => a.Value.ApplicabilityMode != ApplicabilityModes.Disabled)).Select<KeyValuePair<ApplicabilitiesKey, IMSApplicability>, ApplicabilitiesKey>((Func<KeyValuePair<ApplicabilitiesKey, IMSApplicability>, ApplicabilitiesKey>) (a => a.Key)).ToList<ApplicabilitiesKey>();
      }

      /// <summary>
      /// Запрещенные применяемости всех дочерних типов объектов
      /// </summary>
      /// <param name="applicabilities"></param>
      /// <returns></returns>
      public static IEnumerable<ApplicabilitiesKey> GetDisableChildChildApplicabilitiesKey(
        this IEnumerable<IMSApplicability> applicabilities)
      {
        return (IEnumerable<ApplicabilitiesKey>) applicabilities.GetChildObjTypeApplicabilityModes().Where<KeyValuePair<ApplicabilitiesKey, IMSApplicability>>((Func<KeyValuePair<ApplicabilitiesKey, IMSApplicability>, bool>) (a => a.Value.ApplicabilityMode == ApplicabilityModes.Disabled)).Select<KeyValuePair<ApplicabilitiesKey, IMSApplicability>, ApplicabilitiesKey>((Func<KeyValuePair<ApplicabilitiesKey, IMSApplicability>, ApplicabilitiesKey>) (a => a.Key)).ToList<ApplicabilitiesKey>();
      }

      /// <summary>
      /// Настройки обязательности связи для всех дочерних типов объектов
      /// </summary>
      /// <param name="applicabilities"></param>
      /// <returns></returns>
      public static IDictionary<ApplicabilitiesKey, IMSApplicability> GetChildObjTypeApplicabilityModes(
        this IEnumerable<IMSApplicability> applicabilities)
      {
        Dictionary<ApplicabilitiesKey, IMSApplicability> applicabilityModes = new Dictionary<ApplicabilitiesKey, IMSApplicability>();
        if (applicabilities == null || !applicabilities.Any<IMSApplicability>())
          return (IDictionary<ApplicabilitiesKey, IMSApplicability>) applicabilityModes;
        foreach (IMSApplicability applicability in applicabilities)
        {
          ApplicabilitiesKey key = new ApplicabilitiesKey(applicability.InObjectType, applicability.ChildObjectTypeID, applicability.RelationTypeID);
          applicabilityModes[key] = applicability;
        }
        foreach (IMSApplicability applicability1 in applicabilities)
        {
          foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive(applicability1.ChildObjectTypeID))
          {
            ApplicabilitiesKey key = new ApplicabilitiesKey(applicability1.InObjectType, num, applicability1.RelationTypeID);
            if (!applicabilityModes.ContainsKey(key))
            {
              IMSApplicability applicability2 = MetaDataHelper.GetApplicability(applicability1.InObjectType, num, applicability1.RelationTypeID);
              if (applicability2 != null)
                applicabilityModes[key] = applicability2;
            }
          }
        }
        return (IDictionary<ApplicabilitiesKey, IMSApplicability>) applicabilityModes;
      }
    }
}
