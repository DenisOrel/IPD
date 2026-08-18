
// Type: Intermech.Interfaces.PDMPluginGuids
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Коллекция общих Guid-ов для серверного и клиентского плагинов "Intermech.Pdm"
    /// </summary>
    public static class PDMPluginGuids
    {
      /// <summary>
      /// Guid типа объектов "Точка заказа"
      /// несистемный, т.к. используется на заводе в таком виде
      /// </summary>
      public static readonly Guid orderPointGuid = new Guid("2a21a11a-dcd8-4704-83f4-0d8943c35b2a");
      /// <summary>
      /// Guid связи "Состав точки заказа"
      /// несистемный, т.к. используется на заводе в таком виде
      /// </summary>
      public static readonly Guid orderPointCompositionRelationGuid = new Guid("f4d919fe-0bce-4164-84c8-b4b32150dbc7");
      /// <summary>Guid тип связи "Изделие-заготовка"</summary>
      public static readonly Guid linkZagotRelationGuid = new Guid("cadd9404-306c-11d8-b4e9-00304f19f545");
      /// <summary>Guid атрибута "Ссылка на сборочную единицу"</summary>
      public static readonly Guid assemblyUnitRefAttrGuid = new Guid("cadd9521-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Guid серверного плагина "Intermech.Pdm.Server" - управляет статусами подбора версий по сериям и датам
      /// </summary>
      public const string serverPdmVersionAppls = "{14BE37A7-84F7-44CB-97AA-15A713C703E0}";
      /// <summary>
      /// Запрет плагину "Intermech.Pdm.Server" добавлять статусы подбора версий по сериям и датам в столбец "Статусы элемента"
      /// </summary>
      public const string serverPdmVersionApplsDisable = "{C96D8F98-D79E-42CB-9A0C-60C6C321C052}";
      /// <summary>
      /// Guid серверного плагина "Intermech.Pdm.Server", также управляет статусами допустимых замен
      /// </summary>
      public const string serverPdmGuid = "cad005f4-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Запрет плагину "Intermech.Pdm.Server" добавлять статусы допустимых замен в столбец "Статусы элемента"
      /// </summary>
      public const string serverPdmGuidDisable = "cad005f9-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid серверного плагина "Intermech.Pdm.Server" - управляет статусами контекстов состава
      /// </summary>
      public const string serverPdmGuidContexts = "cad005fc-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Запрет плагину "Intermech.Pdm.Server" добавлять статусы контекстов состава в столбец "Статусы элемента"
      /// </summary>
      public const string serverPdmGuidDisableContexts = "cad005f9-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid серверного плагина "Intermech.Pdm.Server" - управляет статусами скрытых составов
      /// </summary>
      public const string serverPdmGuidHiddenCompositions = "cad005fe-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Запрет плагину "Intermech.Pdm.Server" добавлять статусы скрытых составов в столбец "Статусы элемента"
      /// </summary>
      public const string serverPdmGuidDisableHiddenCompositions = "cad005ff-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid серверного плагина "Intermech.Pdm.Server" - управляет статусами общей и переменной частей исполнения
      /// </summary>
      public const string serverPdmGuidArticlesCompositions = "{793BEF65-E7BC-40B5-A0FA-003472E7F548}";
      /// <summary>
      /// Запрет плагину "Intermech.Pdm.Server" добавлять статусы общей и переменной частей исполнения в столбец "Статусы элемента"
      /// </summary>
      public const string serverPdmGuidDisableArticlesCompositions = "{7F92D8D5-8B09-4893-8A5F-FE1DAB481A23}";
      /// <summary>Guid клиентского плагина "Intermech.Pdm"</summary>
      public const string clientPdmGuid = "cad005f3-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Запрет плагину "Intermech.Pdm" добавлять статусы в столбец "Статусы элемента"
      /// </summary>
      public const string clientPdmGuidDisable = "cad005f8-306c-11d8-b4e9-00304f19f545";
      /// <summary>Guid клиентского плагина "Intermech.Interfaces.Pdm"</summary>
      public const string clientInterfacesPdmGuid = "cad00600-306c-11d8-b4e9-00304f19f545";
      /// <summary>
      /// Guid кнопки "Подбор по сериям и датам", он же ключ в настройках фильтрации по сериям и датам
      /// </summary>
      public const string buttonSeriesDatesGuid = "{E2390B62-E0BA-4F7E-89CC-1E9E33F0BB5C}";
      /// <summary>
      /// Guid ключа, блокирующего фильтрацию по сериям и датам, даже если её попросили
      /// </summary>
      public const string blockSeriesDatesGuid = "{02C00D9C-738E-42AB-A905-454BBD0644AD}";
      /// <summary>
      /// Guid кнопки "Актуальные заменители", он же ключ в настройках фильтрации - фильтрация допустимых замен
      /// </summary>
      public const string buttonSubstitutesGuid = "{82E381A1-8952-416A-B303-F81BA2945F8F}";
      /// <summary>
      /// Guid ключа, блокирующего допустимые замены, даже если их попросили
      /// </summary>
      public const string blockSubstitutesGuid = "{2FACA180-73B8-4F24-9928-5623661BBBE6}";
      /// <summary>
      /// Словарь вида [(Int64)Номер группы] =&gt; [(Int64)Номер актуального заменителя в группе]
      /// для того, чтобы задать актуальные заменители в составе с номерами заменителей, отличными
      /// от нуля
      /// </summary>
      public const string substitutesActualsGuid = "{7C2D15CB-FD98-4A41-A036-6D3E5AF3FD1B}";
      /// <summary>
      /// Ключ в настройках фильтрации - фильтрация скрытого состава.
      /// По данному ключу задаётся режим фильтрации скрытых составов.
      /// Ключ применяется в поле Tags параметров запроса в базу данных.
      /// Ссылается на поле (HiddenCompositionFiltrationMode).
      /// </summary>
      public const string buttonHiddenCompositionGuid = "{54C2DCB9-63C7-4736-867B-1EA7539B7645}";
      /// <summary>
      /// Сохраним свойство Checked кнопки "Не показывать скрытый состав"
      /// </summary>
      public const string buttonHiddenComposition1Guid = "{4545B911-6878-4625-AA9E-33B6ACE8CDCF}";
      /// <summary>
      /// Сохраним свойство Checked кнопки "Не показывать объекты со скрытым составом"
      /// </summary>
      public const string buttonHiddenComposition2Guid = "{86C8373B-7537-40E1-8F02-24444C4FED7A}";
      /// <summary>
      /// Guid ключа, блокирующего скрытый состав, даже если его попросили
      /// </summary>
      public const string blockHiddenCompositionGuid = "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}";
      /// <summary>
      /// Guid кнопки "Контекст состава", он же ключ в настройках фильтрации - фильтрация контекстов состава
      /// </summary>
      public const string buttonContextCompositionGuid = "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}";
      /// <summary>
      /// Guid ключа, блокирующего контексты состава, даже если их попросили
      /// </summary>
      public const string blockContextCompositionGuid = "{529FFE92-FDA7-48B8-AADF-ADB1EE6EF584}";
      /// <summary>
      /// Guid ключа, который сигнализирует о том, что плагином включен режим добавления статусов
      /// </summary>
      public const string SubstitutesStatusesGuidFound = "{A568A877-0F03-460F-A2F4-7ACB5C674BDC}";
      /// <summary>
      /// Список ID вновь добавленных колонок (которые потом надо удалить) - допустимые замены
      /// </summary>
      public const string SubstitutesStatusesGuidColumns = "{A670B318-4A9B-45D3-B49A-122C61B8CB6E}";
      /// <summary>
      /// 
      /// </summary>
      public static readonly Guid CategoryCompareObjectsRootGuid = new Guid("{EB02F690-FA01-490f-AEF8-B49CCB48E1C2}");
      /// <summary>INode guids</summary>
      public static readonly Guid CategoryCompareObjectGuid = new Guid("{2E21DED5-6F00-41a1-988B-6112FA209645}");
      /// <summary>INode guids</summary>
      public static readonly Guid CategoryInstanceGuid = new Guid("{FC1ECAAD-D3BA-46a1-93D0-1017E7B2CF83}");
      /// <summary>INode guids</summary>
      public static readonly Guid CategoryContainsGuid = new Guid("{C6EB2F31-7A1B-470f-8B26-08ABA0533979}");
      /// <summary>SubstitutesNode guids</summary>
      public static readonly Guid CategorySubstitutesGuid = new Guid("{7EDFFBEE-91DA-4E90-A673-AC459506370B}");
      /// <summary>???</summary>
      public static readonly Guid ContainsColumnSchemeGuid = new Guid("{5F10DA23-1DFD-4c04-9D5F-21B4621BBA4F}");
      /// <summary>???</summary>
      public static readonly Guid CategoryArticlesGuid = new Guid("{97E84835-AEB4-4755-B903-64401FDFE1AE}");
      /// <summary>
      /// По данному ключу в расширенных настройках пользователя (в кэше IVersionRulesCacheService)
      /// хранится список List[Int64] идентификаторов объектов (F_ID), состав которых скрыт
      /// </summary>
      public const string HiddenCompositionObjects = "{9D621C68-0820-47EC-9ABB-CC7D2EF820F6}";
    }
}
