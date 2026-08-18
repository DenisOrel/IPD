
// Type: Intermech.Interfaces.IElementStatusesPluginDescription
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Расширение для клиентских и серверных плагинов по управлению статусами элементов
    /// </summary>
    public interface IElementStatusesPluginDescription
    {
      /// <summary>
      /// Количество бит, которое нужно плагину для работы со статусами элемента.
      /// Каждому элементу (объект, связь) по запросу может сопоставляться виртуальный атрибут "Статусы элемента"
      /// (SystemGUIDs.virtualAttributeElementStatuses, ObligatoryObjectAttributes.F_ELEMENT_STATUSES).
      /// В данном атрибуте в виде строки хранится битовый массив статусов элемента.
      /// Каждый плагин может читать и устанавливать эти статусы.
      /// При своей регистрации плагин заявляет то количество бит, которое ему нужно для хранения
      /// своих статусов элемента.
      /// </summary>
      int ElementStatesBits { get; }

      /// <summary>Guid плагина (в виде строки)</summary>
      string PluginGuid { get; }

      /// <summary>
      /// Guid (в виде строки), который может запретить плагину добавлять свои статусы в столбец "Статусы элемента".
      /// Для этого в поле Tags параметров запроса надо добавить следующее значение:
      /// DBRecordSetParams.Tags[PluginDisableGuid] = true;
      /// </summary>
      string PluginDisableGuid { get; }

      /// <summary>Название плагина</summary>
      string PluginName { get; }

      /// <summary>Описание статусов, которые устанавливает плагин</summary>
      string StatusesDescription { get; }
    }
}
