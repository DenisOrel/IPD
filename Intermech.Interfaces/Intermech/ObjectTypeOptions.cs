
// Type: Intermech.ObjectTypeOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Опции, регулирующие поведение типов объектов</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_146")]
    [Category("Misc")]
    [Flags]
    public enum ObjectTypeOptions
    {
      /// <summary>Нет опций</summary>
      [CustomDescription("Attribute.Interfaces_147")] None = 0,
      /// <summary>
      /// Возможна рассылка уведомлений о действиях над объектами
      /// </summary>
      [CustomDescription("Attribute.Interfaces_148")] NotificationsEnabled = 1,
      /// <summary>Возможен выпуск изделий по документу</summary>
      [CustomDescription("Attribute.Interfaces_149")] ReleaseArticlesEnabled = 2,
      /// <summary>Объекты выпускаются в рамках текущего проекта</summary>
      [CustomDescription("Attribute.Interfaces_150")] CurrentProjectEnabled = 4,
      /// <summary>
      /// Проверять права доступа у родительского объекта, используя связь по умолчанию
      /// </summary>
      [CustomDescription("CheckParentAccess")] CheckParentAccess = 8,
      /// <summary>Локальный тип объектов</summary>
      [CustomDescription("LocalObjectType")] LocalObjectType = 16, // 0x00000010
      /// <summary>Запрет создания объектов командами Навигатора</summary>
      [CustomDescription("DisableManualCreate")] DisableManualCreate = 32, // 0x00000020
      /// <summary>Разрешить создание итераций</summary>
      [CustomDescription("CreateSnapshots")] CreateSnapshots = 64, // 0x00000040
      /// <summary>Объекты доступны для обсуждения</summary>
      [CustomDescription("ForumEnabled")] ForumEnabled = 128, // 0x00000080
      /// <summary>
      /// Включать новые версии в текущий контекст редактирования
      /// </summary>
      [CustomDescription("AutoContextEnabled")] AutoContextEnabled = 256, // 0x00000100
      /// <summary>Мандатное разграничение доступа</summary>
      [CustomDescription("MandateAccess")] MandateAccess = 512, // 0x00000200
      /// <summary>Индексировать таблицу атрибутов</summary>
      [CustomDescription("AttributesIndex")] AttributesIndex = 1024, // 0x00000400
      /// <summary>Разрешить создание итераций в автоматическом режиме</summary>
      [CustomDescription("AutoCreateSnapshots")] AutoCreateSnapshots = 2048, // 0x00000800
      /// <summary>Запрет создания объектов по прототипу</summary>
      [CustomDescription("DisablePrototyping")] DisablePrototyping = 4096, // 0x00001000
      /// <summary>Расширенная регистрация событий в журнале</summary>
      [CustomDescription("ExtendedAudit")] ExtendedAudit = 8192, // 0x00002000
      /// <summary>Разрешить редактирование объектов в IPS WebInterface</summary>
      [CustomDescription("EnableWebEdit")] EnableWebEdit = 16384, // 0x00004000
    }
}
