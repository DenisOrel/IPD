
// Type: Intermech.Interfaces.MetadataUpdates.UpdateScriptAccessRight
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.MetadataUpdates
{
    /// <summary>
    /// Значение записи по безопастности, прочитанное из скрипта автообновления метаданных.
    /// </summary>
    public sealed class UpdateScriptAccessRight
    {
      /// <summary>Идентификатор права доступа</summary>
      public int RightID;
      /// <summary>
      ///  Типа права:
      /// 0 - по умолчанию;
      /// 1 - не разрешено;
      /// 2 - разрешено;
      /// 3 - запрещено.
      ///  </summary>
      public int RightType;
      /// <summary>
      /// Идентификатор объекта (пользователь, группа, роль...), которой назначили это право доступа.
      /// </summary>
      public Guid UserID;
      /// <summary>Ид. юзера, кот. назначил эти права доступа</summary>
      public Guid OwnerID;
      /// <summary>
      /// Ид. родительской записи о правах. 0 - если запись собственная, а не унаследованная.
      /// </summary>
      public int ParentKey;
      /// <summary>Дата и время начала действия прав доступа.</summary>
      public DateTime BeginDate;
      /// <summary>Дата завершения действия прав доступа</summary>
      public DateTime EndDate;
    }
}
