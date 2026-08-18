
// Type: Intermech.Kernel.Search.QueryConsts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Класс констант, используемых в выборках объектов и связей
    /// </summary>
    public class QueryConsts
    {
      /// <summary>Только подсчитать количество записей в выборке</summary>
      public const int RecordCountOnly = 0;
      /// <summary>Вернуть все записи выборки</summary>
      public const int All = -1;
      /// <summary>
      /// Вернуть столько записей, сколько установлено в системе по умолчанию (св-во IUserSession.MaxRows)
      /// </summary>
      public const int Default = -2;
      /// <summary>
      /// Вернуть все записи включая удаленные и из других предметных областей
      /// </summary>
      public const int Hidden = -3;
      /// <summary>Алиас главной таблицы объектов для Select</summary>
      public const string SystemTableAlias = "SystemTableAlias";
      /// <summary>
      /// Поле записывается в условия для опрделения их отношения к пакетному чтению
      /// </summary>
      public static PacketCondition IsPacketCondition = new PacketCondition();
    }
}
