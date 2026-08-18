
// Type: Intermech.Interfaces.IObjectClassificator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    public interface IObjectClassificator
    {
      /// <summary>Получить расчитанные атрибуты</summary>
      /// <param name="objectID">для объекта (F_OBJECT_ID)</param>
      /// <returns></returns>
      AttributeValues[] GetClasificatorAttributes(long objectID);

      /// <summary>Классифицировать объекты</summary>
      /// <param name="objectIDs">ID объектов</param>
      /// <returns></returns>
      ClassifiedError ClassifyObjects(long[] objectIDs);

      /// <summary>Пропуск неклассифицируемых объектов</summary>
      bool SkipNonClassified { set; }

      /// <summary>Проклассифицированные объекты</summary>
      List<ClassifiedObjectInfo> ClassifiedObjects { get; }

      /// <summary>Непроклассифицированные объекты</summary>
      long[] NonClassifiedObjects { get; }

      bool ObligatoryCalculated { get; }
    }
}
