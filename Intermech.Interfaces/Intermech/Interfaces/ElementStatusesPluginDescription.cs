
// Type: Intermech.Interfaces.ElementStatusesPluginDescription
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, который кратко описывает плагин, управляющий статусами элементов
    /// </summary>
    [Serializable]
    public class ElementStatusesPluginDescription : IElementStatusesPluginDescription
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
      internal int FElementStatesBits;
      /// <summary>Guid плагина (в виде строки)</summary>
      internal string FPluginGuid = string.Empty;
      /// <summary>
      /// Guid (в виде строки), который может запретить плагину добавлять свои статусы в столбец "Статусы элемента".
      /// Для этого в поле Tags параметров запроса надо добавить следующее значение:
      /// DBRecordSetParams.Tags[PluginDisableGuid] = true;
      /// </summary>
      internal string FPluginDisableGuid = string.Empty;
      /// <summary>Название плагина</summary>
      internal string FPluginName = string.Empty;
      /// <summary>Описание статусов, которые устанавливает плагин</summary>
      internal string FStatusesDescription = string.Empty;

      /// <summary>
      /// Количество бит, которое нужно плагину для работы со статусами элемента.
      /// Каждому элементу (объект, связь) по запросу может сопоставляться виртуальный атрибут "Статусы элемента"
      /// (SystemGUIDs.virtualAttributeElementStatuses, ObligatoryObjectAttributes.F_ELEMENT_STATUSES).
      /// В данном атрибуте в виде строки хранится битовый массив статусов элемента.
      /// Каждый плагин может читать и устанавливать эти статусы.
      /// При своей регистрации плагин заявляет то количество бит, которое ему нужно для хранения
      /// своих статусов элемента.
      /// </summary>
      public int ElementStatesBits => this.FElementStatesBits;

      /// <summary>Guid плагина (в виде строки)</summary>
      public string PluginGuid => this.FPluginGuid;

      /// <summary>
      /// Guid (в виде строки), который может запретить плагину добавлять свои статусы в столбец "Статусы элемента".
      /// Для этого в поле Tags параметров запроса надо добавить следующее значение:
      /// DBRecordSetParams.Tags[PluginDisableGuid] = true;
      /// </summary>
      public string PluginDisableGuid => this.FPluginDisableGuid;

      /// <summary>Название плагина</summary>
      public string PluginName => this.FPluginName;

      /// <summary>Описание статусов, которые устанавливает плагин</summary>
      public string StatusesDescription => this.FStatusesDescription;

      public bool IsFlags { get; set; }

      /// <summary>Конструктор</summary>
      /// <param name="AnElementStatesBits">Количество бит, требуемое для статусов (не более 32 бит)</param>
      /// <param name="APluginGuid">Guid плагина</param>
      /// <param name="APluginDisableGuid">Guid (в виде строки), который может запретить плагину добавлять свои статусы в столбец "Статусы элемента".
      /// <param name="APluginName">Название плагина</param>
      /// Для этого в поле Tags параметров запроса надо добавить следующее значение:
      /// DBRecordSetParams.Tags[PluginDisableGuid] = true;</param>
      /// <param name="AStatusesDescription">Описание статусов, управляемых плагином</param>
      public ElementStatusesPluginDescription(
        int AnElementStatesBits,
        string APluginGuid,
        string APluginDisableGuid,
        string APluginName,
        string AStatusesDescription)
      {
        this.FElementStatesBits = AnElementStatesBits;
        if (this.FElementStatesBits > 32 /*0x20*/)
          this.FElementStatesBits = 32 /*0x20*/;
        this.FPluginGuid = APluginGuid;
        this.FPluginDisableGuid = APluginDisableGuid;
        this.FPluginName = APluginName;
        this.FStatusesDescription = AStatusesDescription;
      }

      /// <summary>
      /// Найти в указанной таблице столбец с атрибутом "Статусы элемента"
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static int GetStatusesColumnIndex(ref DataTable source)
      {
        if (source == null)
          return -1;
        int statusesColumnIndex = source.Columns.IndexOf("cad005f1-306c-11d8-b4e9-00304f19f545");
        if (statusesColumnIndex < 0)
          statusesColumnIndex = source.Columns.IndexOf(ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_ELEMENT_STATUSES));
        if (statusesColumnIndex < 0)
          statusesColumnIndex = source.Columns.IndexOf(-77.ToString());
        if (statusesColumnIndex < 0)
        {
          for (int index = 0; index < source.Columns.Count; ++index)
          {
            if (source.Columns[index].DataType == typeof (byte[]))
            {
              statusesColumnIndex = index;
              break;
            }
          }
        }
        return statusesColumnIndex;
      }
    }
}
