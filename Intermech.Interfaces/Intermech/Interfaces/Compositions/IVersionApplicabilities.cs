
// Type: Intermech.Interfaces.Compositions.IVersionApplicabilities
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Интерфейс, позволяющий проверить применяемость версии объекта в составе
    /// </summary>
    public interface IVersionApplicabilities
    {
      /// <summary>
      /// Выполнить проверку применяемости указанной версии по дате и(или) номеру серии
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="objectID">Идентификатор проверяемой версии объекта</param>
      /// <param name="masterArticle">Идентификатор версии головного изделия (Intermech.Consts.UnknownObjectId, если не требуется головное изделие)</param>
      /// <param name="date">Дата для проверки. Если проверка на дату не требуется, следует указать значение DateTime.MinValue</param>
      /// <param name="series">Номер серии для проверки. Если проверка на серию не требуется, следует указать значение Int32.MinValue</param>
      /// <returns>Статус указанной версии</returns>
      ObjectFiltrationState CheckApplicabilities(
        IUserSession session,
        long objectID,
        long masterArticle,
        DateTime date,
        int series);
    }
}
