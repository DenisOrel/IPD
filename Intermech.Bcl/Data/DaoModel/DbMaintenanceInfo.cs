
// Type: Intermech.Data.DaoModel.DbMaintenanceInfo
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Diagnostics;


namespace Intermech.Data.DaoModel
{
    /// <summary>
    /// Параметры режима обслуживания базы данных и информация о ней.
    /// </summary>
    public class DbMaintenanceInfo
    {
      private readonly bool newDatabase;

      /// <summary>Создает объект.</summary>
      /// <param name="newDatabase">Признак, что это новый, только что созданный экземпляр базы данных</param>
      internal DbMaintenanceInfo(bool newDatabase) => this.newDatabase = newDatabase;

      /// <summary>
      /// Признак, что это новый, только что созданный экземпляр базы данных.
      /// </summary>
      public bool NewDatabase
      {
        [DebuggerStepThrough] get => this.newDatabase;
      }
    }
}
