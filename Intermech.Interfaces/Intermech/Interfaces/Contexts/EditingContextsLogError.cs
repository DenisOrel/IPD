
// Type: Intermech.Interfaces.Contexts.EditingContextsLogError
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.Contexts
{
    /// <summary>
    /// Код ошибки, возникшей при работе с контекстом редактирования
    /// </summary>
    [Serializable]
    public enum EditingContextsLogError
    {
      /// <summary>
      /// Тип данного объекта не позволяет создавать версии объектов в рамках текущего проекта и контекста редактирования
      /// </summary>
      [CustomDescription("Attribute.Interfaces_487")] NoncontextsObjectType = -6, // 0xFFFFFFFA
      /// <summary>
      /// Данный объект не является версионным, его недопустимо добавлять в контексты редактирования
      /// </summary>
      [CustomDescription("Attribute.Interfaces_486")] NonversionObject = -5, // 0xFFFFFFFB
      /// <summary>
      /// Данная версия объекта является контекстом редактирования, её недопустимо добавлять в контексты редактирования
      /// </summary>
      [CustomDescription("Attribute.Interfaces_485")] IsEditingContext = -4, // 0xFFFFFFFC
      /// <summary>
      /// Данная версия объекта принадлежит другому контексту редактирования
      /// </summary>
      [CustomDescription("Attribute.Interfaces_484")] ExistsAnotherVersion = -3, // 0xFFFFFFFD
      /// <summary>
      /// В контексте либо в любом из связанных контекстов присутствует другая версия данного объекта
      /// </summary>
      [CustomDescription("Attribute.Interfaces_483")] ExistsAnotherVersionLinked = -2, // 0xFFFFFFFE
      /// <summary>
      /// Данная версия объекта присутствует в текущем контексте редактирования
      /// </summary>
      [CustomDescription("Attribute.Interfaces_482")] ExistsVersion = -1, // 0xFFFFFFFF
      /// <summary>Нет ошибок</summary>
      [Description("")] None = 0,
    }
}
