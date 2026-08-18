
// Type: Intermech.Interfaces.IGuidService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    public interface IGuidService
    {
      /// <summary>Возвращает следующий системный GUID</summary>
      /// <param name="categoryType">категория объекта</param>
      /// <param name="objectName">наименование объекта</param>
      /// <param name="note"> комментарии</param>
      /// <returns></returns>
      Guid GenerateNextSystemGuid(int categoryType, string objectName, string note);
    }
}
