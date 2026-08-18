
// Type: Intermech.Interfaces.IDBConfigurations
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для чтения и записи настроек пользователей и системы IPS
    /// </summary>
    public interface IDBConfigurations
    {
      /// <summary>Проверка на существование параметра</summary>
      /// <param name="ModuleName">Имя модуля, которому принадлежит параметр (до 20 символов)</param>
      /// <param name="SectionID">Имя секции, в которой записан параметр (до 32 символов)</param>
      /// <param name="ParamName">Имя параметра (до 32 символов)</param>
      /// <param name="configMode">Метод чтения параметра (DBConfigMode.UserOnly – только из настроек текущего пользователя, DBConfigMode.GlobalOnly – только из общих настроек, DBConfigMode.UserAndGlobal – сначала производит поиск настройки у текущего пользователя, а в случае ее отсутствия – в общих настройках, DBConfigMode.GlobalAndUser – ищет сперва глобальную настройку, а потом пользовательскую).</param>
      /// <returns>Результат проверки</returns>
      bool ParameterPresent(
        string ModuleName,
        string SectionID,
        string ParamName,
        DBConfigMode configMode);

      /// <summary>Прочитать строковый параметр</summary>
      /// <param name="ModuleName">Имя модуля, которому принадлежит параметр (до 20 символов)</param>
      /// <param name="SectionID">Имя секции, в которой записан параметр (до 32 символов)</param>
      /// <param name="ParamName">Имя параметра (до 32 символов)</param>
      /// <param name="DefaultValue">Значение параметра (до 450 символов)</param>
      /// <param name="configMode">Метод чтения параметра (DBConfigMode.UserOnly – только из настроек текущего пользователя, DBConfigMode.GlobalOnly – только из общих настроек, DBConfigMode.UserAndGlobal – сначала производит поиск настройки у текущего пользователя, а в случае ее отсутствия – в общих настройках, DBConfigMode.GlobalAndUser – ищет сперва глобальную настройку, а потом пользовательскую).</param>
      /// <returns>Значение строкового параметра или значение по умолчанию</returns>
      string ReadString(
        string ModuleName,
        string SectionID,
        string ParamName,
        string DefaultValue,
        DBConfigMode configMode);

      /// <summary>Прочитать целочисленный параметр</summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="DefaultValue">Значение параметра по умолчанию.</param>
      /// <param name="configMode">Метод чтения параметра.</param>
      /// <returns>Значение целочисленного параметра или значение по умолчанию</returns>
      long ReadInteger(
        string ModuleName,
        string SectionID,
        string ParamName,
        long DefaultValue,
        DBConfigMode configMode);

      /// <summary>Прочитать вещественный параметр</summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="DefaultValue">Значение параметра по умолчанию.</param>
      /// <param name="configMode">Метод чтения параметра.</param>
      /// <returns>Значение вещественного параметра или значение по умолчанию</returns>
      double ReadDouble(
        string ModuleName,
        string SectionID,
        string ParamName,
        double DefaultValue,
        DBConfigMode configMode);

      /// <summary>Прочитать логический параметр</summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="DefaultValue">Значение параметра по умолчанию.</param>
      /// <param name="configMode">Метод чтения параметра.</param>
      /// <returns>Значение логического параметра или значение по умолчанию</returns>
      bool ReadBool(
        string ModuleName,
        string SectionID,
        string ParamName,
        bool DefaultValue,
        DBConfigMode configMode);

      /// <summary>Прочитать дату</summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="DefaultValue">Значение параметра по умолчанию.</param>
      /// <param name="configMode">Метод чтения параметра.</param>
      /// <returns>Значение даты или значение по умолчанию</returns>
      DateTime ReadDateTime(
        string ModuleName,
        string SectionID,
        string ParamName,
        DateTime DefaultValue,
        DBConfigMode configMode);

      /// <summary>Записать строковый параметр.</summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="Value">Записываемое значение</param>
      /// <returns></returns>
      int WriteString(string ModuleName, string SectionID, string ParamName, string Value);

      /// <summary>
      /// Записать строковый параметр в конфигурацию указанного пользователя.
      /// </summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="Value">Записываемое значение</param>
      /// <param name="userID">Идентификатор объекта пользователя. Если идентификатор равен 0, то записывается секция общих для всех пользователей настроек. Для записи общих настроек необходимы права административной роли.</param>
      /// <returns></returns>
      int WriteString(
        string ModuleName,
        string SectionID,
        string ParamName,
        string Value,
        long userID);

      /// <summary>
      /// Записать строковый параметр непосредственно в таблицу БД, минуя кэши (и не обновляя их)
      /// </summary>
      /// <param name="aModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="aSectionID">Имя секции</param>
      /// <param name="aParamName">Имя параметра</param>
      /// <param name="aValue">Записываемое значение</param>
      /// <param name="oldValue">Предыдущее значение (обновляет, только если в таблице сейчас хранится предыдущее значение)</param>
      /// <param name="aUserID">Идентификатор объекта пользователя. Если идентификатор равен 0, то записывается секция общих для всех пользователей настроек. Для записи общих настроек необходимы права административной роли.</param>
      /// <returns>true, если запись прошла</returns>
      bool WriteStringNoCache(
        string aModuleName,
        string aSectionID,
        string aParamName,
        string aValue,
        string oldValue,
        long aUserID);

      /// <summary>Записать целочисленный параметр.</summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="Value">Записываемое значение</param>
      /// <returns></returns>
      int WriteInteger(string ModuleName, string SectionID, string ParamName, long Value);

      /// <summary>
      /// Записать целочисленный параметр в конфигурацию указанного пользователя.
      /// </summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="Value">Записываемое значение</param>
      /// <param name="userID">Идентификатор объекта пользователя</param>
      /// <returns></returns>
      int WriteInteger(
        string ModuleName,
        string SectionID,
        string ParamName,
        long Value,
        long userID);

      /// <summary>Записать вещественный параметр</summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="Value">Записываемое значение</param>
      /// <returns></returns>
      int WriteDouble(string ModuleName, string SectionID, string ParamName, double Value);

      /// <summary>
      /// Записать вещественный параметр в конфигурацию указанного пользователя.
      /// </summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="Value">Записываемое значение</param>
      /// <param name="userID">Идентификатор объекта пользователя</param>
      /// <returns></returns>
      int WriteDouble(
        string ModuleName,
        string SectionID,
        string ParamName,
        double Value,
        long userID);

      /// <summary>Записать логический параметр</summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="Value">Записываемое значение</param>
      /// <returns></returns>
      int WriteBool(string ModuleName, string SectionID, string ParamName, bool Value);

      /// <summary>
      /// Записать логический параметр в конфигурацию указанного пользователя.
      /// </summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="Value">Записываемое значение</param>
      /// <param name="userID">Идентификатор объекта пользователя</param>
      /// <returns></returns>
      int WriteBool(string ModuleName, string SectionID, string ParamName, bool Value, long userID);

      /// <summary>Записать дату</summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="Value">Записываемое значение</param>
      /// <returns></returns>
      int WriteDateTime(string ModuleName, string SectionID, string ParamName, DateTime Value);

      /// <summary>Записать дату в конфигурацию указанного пользователя.</summary>
      /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="ParamName">Имя параметра</param>
      /// <param name="Value">Записываемое значение</param>
      /// <param name="userID">Идентификатор объекта пользователя</param>
      /// <returns></returns>
      int WriteDateTime(
        string ModuleName,
        string SectionID,
        string ParamName,
        DateTime Value,
        long userID);

      /// <summary>
      /// Записывает конфигурационный файл config_file с именем config_info.FileName в конфигурационные данные
      /// текущего пользователя. Если такой файл уже существует, то он перезаписывается.
      /// </summary>
      /// <param name="config_info">Описание конфигурационного файла</param>
      /// <param name="config_file">Конфигурационный файл</param>
      void WriteConfigData(BlobInformation config_info, byte[] config_file);

      /// <summary>
      /// Считывает конфигурационный файл из настроек текущего пользователя. Если такого файла в настройках нет,
      /// то он создается нулевой длины.
      /// </summary>
      /// <param name="config_name">Имя конфигурационного файла</param>
      /// <param name="config_info">Описание конфигурационного файла</param>
      /// <param name="config_file">Конфигурационный файл-результат</param>
      void LoadConfigData(string config_name, out BlobInformation config_info, out byte[] config_file);

      /// <summary>
      /// Записывает конфигурационный файл config_file с именем config_info.FileName в конфигурационные
      /// данные пользователя userID. Для общих конфигураций следует задавать userID = 0.
      /// </summary>
      /// <param name="config_info">Описание конфигурационного файла</param>
      /// <param name="config_file">Конфигурационный файл</param>
      /// <param name="userID">Идентификатор объекта пользователя</param>
      void WriteConfigData(BlobInformation config_info, byte[] config_file, long userID);

      /// <summary>
      /// Считывает конфигурационный файл с именем config_name. config_info - информация о файле,
      /// config_file - тело файла. Файл читается из настроек пользователя userID.
      /// Для общих конфигураций следует задавать userID = 0.
      /// </summary>
      /// <param name="config_name">Имя конфигурационного файла</param>
      /// <param name="config_info">Описание конфигурационного файла</param>
      /// <param name="config_file">Конфигурационный файл</param>
      /// <param name="userID">Идентификатор объекта пользователя</param>
      void LoadConfigData(
        string config_name,
        out BlobInformation config_info,
        out byte[] config_file,
        long userID);

      /// <summary>
      /// Возвращает атрибут, в котором хранятся конфигурационные файлы текущего пользователя.
      /// Поле Index атрибута устанавливается в нужную позицию, соответствующую имени файла с данными data_name.
      /// Если такого файла среди значений атрибута нет, то он добавляется к списку значений,
      /// а поле Index устанавливается в соответствующую этому файлу позицию.
      /// </summary>
      /// <param name="data_name">Имя конфигурационного файла</param>
      /// <returns>Атрибут, в котором хранится указанный конфигурационный файл</returns>
      IDBAttribute GetConfigAttribute(string data_name);

      /// <summary>
      /// Прочитать содержимое секции SectionID для пользователя userID.
      /// </summary>
      /// <param name="ModuleName">Имя модуля</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="userID">Идентификатор пользователя. Если равен 0, то читается секция общих для всех пользователей настроек.</param>
      /// <returns>Возвращаемая таблица содержит список названий параметров F_PARAM_NAME и их значений F_VALUE.</returns>
      DataTable ReadSection(string ModuleName, string SectionID, long userID);

      /// <summary>
      /// Записывает содержимое секции SectionID модуля ModuleName. table содержит строковые поля
      /// с именами (колонка 0) и значениями параметров (колонка 1).
      /// </summary>
      /// <param name="ModuleName">Имя модуля</param>
      /// <param name="SectionID">Имя секции</param>
      /// <param name="table">Таблица с именами (колонка 0) и значениями параметров (колонка 1)</param>
      /// <param name="userID">Идентификатор пользователя. Если равен 0, то удаляется секция общих для всех пользователей настроек.</param>
      void WriteSection(string ModuleName, string SectionID, DataTable table, long userID);

      /// <summary>
      /// Метод читает строковое значение непосредственно из таблицы конфигураций а БД (минуя серверный кэш)
      /// </summary>
      string ReadStringNoCache(
        string aModuleName,
        string aSectionID,
        string aParamName,
        bool isGlobalParam);
    }
}
