
// Type: Intermech.Interfaces.IDBAttributableTypeInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Базовый интерфейс для типов объектов и связей, позволяющий работать с коллекциями атрибутов, назначенных этим типам
    /// </summary>
    public interface IDBAttributableTypeInfo
    {
      /// <summary>
      /// Список допустимых атрибутов для данного типа объектов или связей
      /// </summary>
      IDBAttribute4TypeInfoCollection Attributes { get; }

      /// <summary>
      /// Список допустимых атрибутов для данного типа объектов или связей, отфильтрованный
      /// в соответствии с правами доступа и настройками профиля пользователя
      /// </summary>
      IDBAttribute4TypeInfoCollection VisibleAttributes { get; }

      /// <summary>
      /// Контроль набора атрибутов
      /// false - допускается добавлять к объектам/связям данного типа только разрешенные атрибуты.
      /// true - допускается добавлять любые атрибуты.
      /// </summary>
      bool AnyAttributes { get; }

      /// <summary>
      /// Возвращает true, если у объектов/связи данного типа может быть атрибут attributeID
      /// </summary>
      bool HasAttribute(int attributeID);

      /// <summary>
      /// Возвращает описатель типа атрибута номер attributeID применительно к данному типу объектов/связей.
      /// Если тип не может принимать такие атрибуты, то функция возвращает null.
      /// </summary>
      IDBAttributeTypeInfo GetAttributeType(int attributeID);

      /// <summary>
      /// Возвращает описатель типа атрибута с именем attributeName применительно к данному типу объектов/связей.
      /// Если тип не может принимать такие атрибуты, то функция возвращает null.
      /// </summary>
      IDBAttributeTypeInfo GetAttributeType(string attributeName);
    }
}
