
// Type: Intermech.Interfaces.Document.ISpecificationSaveService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Compositions;


namespace Intermech.Interfaces.Document
{
    /// <summary>Для сохранения точных спецификаций</summary>
    public interface ISpecificationSaveService
    {
      /// <summary>Создать точную спецификацию и сохранить её
      /// (1002954 сохранение точных спецификаций)</summary>
      /// <param name="objectType">Тип объекта</param>
      /// <param name="objectId">Идентификатор версии объекта</param>
      /// <param name="configureCompositionRoot">Корень конфигурации состава</param>
      /// <param name="filtrationOwnerID">Владелец настроек фильтрации</param>
      /// <param name="designation">Cуффикc для обозначения в спецификациях</param>
      /// <param name="filePath">Куда сохраняются файлы объекта, для которого создаём спицификацию</param>
      /// <param name="createFolder">Создавать ли для спецификации отдельную папку</param>
      void SaveSpecification(
        int objectType,
        long objectId,
        RelationPair configureCompositionRoot,
        string filtrationOwnerID,
        string designation,
        string filePath,
        bool createFolder);
    }
}
