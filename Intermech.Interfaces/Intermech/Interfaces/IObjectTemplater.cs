
// Type: Intermech.Interfaces.IObjectTemplater
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Projects;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для наполнения состава и атрибутов объектов по шаблонам
    /// </summary>
    public interface IObjectTemplater
    {
      /// <summary>Заполняет состав объекта объектами из шаблона</summary>
      /// <param name="IDs">Список идентификаторов объектов</param>
      /// <param name="TemplateID">Ид. объекта-шаблона</param>
      /// <returns>Словарь в котором ключ будет тип объекта, а значение список классов состоящих из идентификатора объекта и списка его атрибутов</returns>
      Dictionary<int, List<CreatedProjectData>> AddTemplateObjects(ArrayList IDs, long TemplateID);

      /// <summary>Заполняет состав объекта объектами из шаблона</summary>
      /// <param name="TemplateID">Ид. объекта-шаблона</param>
      /// <returns>Словарь в котором ключ будет тип объекта, а значение список классов состоящих из идентификатора объекта и списка его атрибутов</returns>
      Dictionary<int, List<CreatedProjectData>> AddTemplateObjects(long TemplateID);
    }
}
