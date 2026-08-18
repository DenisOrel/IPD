
// Type: Intermech.Interfaces.GetAttributeValuesModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Битовый перечислитель, управляющий режимами формирования массива структур AttributeValues
    /// IncludeName - заполняет поле AttributeName в AttributeValues
    /// IncludeGuid - заполняет поле AttributeGuid
    /// IncludeAlias - заполняет поле AttributeAlias
    /// IncludeBlobs - включает в возвращаемый список атрибутов атрибуты типа ftBlob, ftShortBlob и ftFile
    /// IncludeObligatoryAttributes - включает в возвращаемый список атрибутов обязательные атрибуты (ftSystem)
    /// IncludeGroupName - заполняет поле GroupName именем первой попавшейся группы атрибутов, куда включен данный атрибут
    /// CheckWriteAccess - заполняет поле ReadOnly (проверяя права, тип атрибута и т.п.)
    /// IncludeDescriptions - заполняет массив Descriptions для атрибутов, значение которых могут потребовать строковой расшифровки
    /// CheckVisibility - включать в список только атрибуты, видимые в интерфейсе данному пользователю
    /// IncludeOnlyInvisible - включать в список только атрибуты, невидимые в интерфейсе данному пользователю
    /// IncludeCaption - включает в список атрибутов заголовок объекта (применяется если не нужны остальные обязательные атрибуты)
    /// </summary>
    [Flags]
    public enum GetAttributeValuesModes
    {
      /// <summary>Зарезервировано</summary>
      None = 0,
      /// <summary>Заполняет поле AttributeName в AttributeValues</summary>
      IncludeName = 1,
      /// <summary>Заполняет поле AttributeGuid</summary>
      IncludeGuid = 2,
      /// <summary>Заполняет поле AttributeAlias</summary>
      IncludeAlias = 4,
      /// <summary>
      /// Включает в возвращаемый список атрибутов атрибуты типа ftBlob, ftShortBlob и ftFile
      /// </summary>
      IncludeBlobs = 8,
      /// <summary>
      /// Включает в возвращаемый список атрибутов обязательные атрибуты (ftSystem)
      /// </summary>
      IncludeObligatoryAttributes = 16, // 0x00000010
      /// <summary>
      /// Заполняет поле GroupName именем первой попавшейся группы атрибутов, куда включен данный атрибут
      /// </summary>
      IncludeGroupName = 32, // 0x00000020
      /// <summary>
      /// Заполняет поле ReadOnly (проверяя права, тип атрибута и т.п.)
      /// </summary>
      CheckWriteAccess = 64, // 0x00000040
      /// <summary>
      /// Заполняет массив Descriptions для атрибутов, значение которых могут потребовать строковой расшифровки
      /// </summary>
      IncludeDescriptions = 128, // 0x00000080
      /// <summary>
      /// Включать в список только атрибуты, видимые в интерфейсе данному пользователю
      /// </summary>
      CheckVisibility = 256, // 0x00000100
      /// <summary>
      /// Включать в список только атрибуты, невидимые в интерфейсе данному пользователю
      /// </summary>
      IncludeOnlyInvisible = 512, // 0x00000200
      /// <summary>
      /// Включает в список атрибутов заголовок объекта (применяется если не нужны остальные обязательные атрибуты)
      /// </summary>
      IncludeCaption = 1024, // 0x00000400
      /// <summary>
      /// Флаг установлен, если данные запрашиваются из настраиваемой формы
      /// </summary>
      RequestedByForm = 2048, // 0x00000800
      /// <summary>
      /// Для атрибутов типа ftBlob, ftShortBlob и ftFile в значении атрибута указывается идентификатор блоба
      /// </summary>
      BlobIdentifier = 4096, // 0x00001000
      /// <summary>
      /// В список атрибутов включаются виртуальные вычисляемые атрибуты ядра
      /// </summary>
      IncludeVirtualAttributes = 8192, // 0x00002000
      /// <summary>
      /// Проверять права на чтение данных из атрибута (в режиме показа всех атрибутов на закладке Свойства)
      /// </summary>
      CheckReadAccess = 16384, // 0x00004000
      /// <summary>Возвращать содержимое двоичных атрибутов</summary>
      IncludeBlobValues = 32768, // 0x00008000
    }
}
