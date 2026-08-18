
// Type: Intermech.ApplicabilityOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Опции допустимой применяемости типов объектов</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_179")]
    [Category("Misc")]
    [Flags]
    public enum ApplicabilityOptions
    {
      /// <summary>Нет опций</summary>
      [CustomDescription("Attribute.Interfaces_180")] None = 0,
      /// <summary>
      /// Разрешить создание более одной связи с тем же объектом
      /// </summary>
      [CustomDescription("Attribute.Interfaces_181")] EnableMultiLink = 1,
      /// <summary>Тип связи по умолчанию</summary>
      [CustomDescription("Attribute.Interfaces_182")] DefaultRelation = 2,
      /// <summary>Синхронный перевод связанных объектов на шаги ЖЦ</summary>
      [CustomDescription("ChangeLCStep")] ChangeLCStep = 4,
      /// <summary>Синхронизировать идентификационные атрибуты</summary>
      [CustomDescription("SyncIdentifiers")] SyncIdentifiers = 8,
      /// <summary>Создавать итерации дочерних объектов</summary>
      [CustomDescription("CreateSnapshotChild")] CreateSnapshotChild = 16, // 0x00000010
      /// <summary>Синхронное завершение изменений связанных объектов</summary>
      [CustomDescription("SyncCheckin")] SyncCheckin = 32, // 0x00000020
      /// <summary>Разрешить конкретизацию пользователям</summary>
      [CustomDescription("SoftInstantiation")] SoftInstantiation = 64, // 0x00000040
      /// <summary>Запретить копировать связи при создании версии</summary>
      [CustomDescription("DisableCopy2Version")] DisableCopy2Version = 128, // 0x00000080
      /// <summary>Разрешить автоматическую конкретизацию</summary>
      [CustomDescription("AutoInstantiation")] AutoInstantiation = 256, // 0x00000100
      /// <summary>Копировать атрибуты в дочерний объект</summary>
      [CustomDescription("CopyAttributes2Child")] CopyAttributes2Child = 512, // 0x00000200
      /// <summary>
      /// Автоматическая классификация дочернего объекта, при создании в составе родительского объекта
      /// </summary>
      [CustomDescription("AutoClassificationChildObject")] AutoClassificationChildObject = 1024, // 0x00000400
    }
}
