
// Type: Intermech.Interfaces.Briefcase.IBriefcase
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>Интерфейс на портфель</summary>
    public interface IBriefcase
    {
      /// <summary>Показать панель портфеля</summary>
      void ShowView(int indexTab);

      /// <summary>Добавить выделенные объекты на панель</summary>
      /// <param name="ea"></param>
      bool AddIntoExportList(ExportAttribute[] ea);

      /// <summary>Показать форму для экспорта МЕТАДАННЫХ</summary>
      void ShowMetagataExportForm();

      /// <summary>Показать форму для экспорта БД</summary>
      void ShowExportDBForm();
    }
}
