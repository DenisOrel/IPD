
// Type: Intermech.Search.Data.Repositories.IRelationObjectRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Search.Data.Repositories
{
    /// <summary>Репозиторий связей/объектов</summary>
    public interface IRelationObjectRepository
    {
      /// <summary>Искать часть состава</summary>
      /// <param name="relationID">Идентификатор связи</param>
      /// <returns>Часть состава</returns>
      RelationObject FindCompositionPart(long relationID);

      /// <summary>Искать часть состава</summary>
      /// <param name="relationID">Идентификатор связи</param>
      /// <param name="partVersionID">Идентификатор версии дочернего объекта</param>
      /// <returns>Часть состава</returns>
      RelationObject FindCompositionPart(long relationID, long partVersionID);

      /// <summary>Искать состав</summary>
      /// <param name="options">Опции</param>
      /// <returns>Состав</returns>
      RelationObjectCollection FindComposition(
        RelationObjectRepository.FindCompositionOptions options);
    }
}
