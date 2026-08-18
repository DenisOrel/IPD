
// Type: Intermech.Interfaces.PDMConfiguratorPluginGuids
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Коллекция общих Guid-ов для серверного и клиентского плагинов "Intermech.PdmConfigurator"
    /// </summary>
    public static class PDMConfiguratorPluginGuids
    {
      /// <summary>
      /// Guid серверного плагина "Intermech.PdmConfigurator.Server"
      /// </summary>
      public const string serverPdmConfiguratorGuid = "cad005f6-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Запрет плагину "Intermech.PdmConfigurator.Server" добавлять статусы конфигуратора составов в столбец "Статусы элемента"
      /// </summary>
      public const string serverPdmConfiguratorGuidDisable = "cad005fb-306c-11d8-b4e9-00304f19f545";
      /// <summary>Guid клиентского плагина "Intermech.PdmConfigurator"</summary>
      public const string clientPdmConfiguratorGuid = "cad005f5-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Запрет плагину "Intermech.PdmConfigurator" добавлять статусы в столбец "Статусы элемента"
      /// </summary>
      public const string clientPdmConfiguratorGuidDisable = "cad005fa-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid кнопки "Конфигуратор составов", он же ключ в настройках фильтрации - конфигурация составов
      /// </summary>
      public const string buttonConfigureCompositionGuid = "{89F3DEDD-EE3A-4A42-ADD0-55BF26E622E1}";
      /// <summary>
      /// Guid ключа, блокирующего конфигуратор состава, даже если его включили
      /// </summary>
      public const string blockConfigureCompositionGuid = "{0422E069-0A1D-4235-85E8-C52C3516CFC1}";
      /// <summary>
      /// Список ID вновь добавленных колонок (которые потом надо удалить) - конфигуратор составов IPS
      /// </summary>
      public const string NewColumns = "{32C584B7-5063-4101-890D-E30C5F7BE12B}";
    }
}
