// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ClassificationHelper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Вспомогательный класс, позволяющий выполнить классификацию объектов
/// </summary>
internal static class ClassificationHelper
{
  /// <summary>Классификация указанного объекта</summary>
  /// <returns>Идентификатор классификатора</returns>
  public static long Classification(long classifierID, long objectID)
  {
    long classifierID1 = classifierID;
    if (classifierID1 == 0L)
      return classifierID1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ISelectionsService customService = sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
      if (customService.GetObjectClassificator((object) sessionKeeper.Session.SessionGUID, classifierID1) != null)
      {
        customService.IncludeObjects((object) sessionKeeper.Session.SessionGUID, classifierID, new long[1]
        {
          objectID
        });
        return classifierID1;
      }
    }
    return 0;
  }

  /// <summary>
  /// Получение атрибутов после классификации для указанного объекта
  /// </summary>
  /// <param name="classifierID">Идентификатор классификатора</param>
  /// <param name="objectID">Идентификатор классифицируемого объекта (заготовки)</param>
  /// <returns>Атрибуты после классификации</returns>
  public static AttributeValues[] GetClassificationAttributes(long classifierID, long objectID)
  {
    long classifierID1 = classifierID;
    if (classifierID1 == 0L)
      return (AttributeValues[]) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IObjectClassificator objectClassificator = (sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService).GetObjectClassificator((object) sessionKeeper.Session.SessionGUID, classifierID1);
      if (objectClassificator != null)
        return objectClassificator.GetClasificatorAttributes(objectID);
    }
    return (AttributeValues[]) null;
  }
}
