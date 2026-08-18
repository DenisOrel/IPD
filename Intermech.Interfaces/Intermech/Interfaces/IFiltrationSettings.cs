
// Type: Intermech.Interfaces.IFiltrationSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для хранения настроек фильтрации состава</summary>
    public interface IFiltrationSettings
    {
      /// <summary>Уникальный объект-ключ владельца данного правила</summary>
      string OwnerID { get; }

      /// <summary>Информация о текущем контексте редактирования</summary>
      CurrentEditingContext EditingContext { get; set; }

      /// <summary>
      /// Название правила подбора версий, используемого в данных настройках фильтрации
      /// </summary>
      string Caption { get; }

      /// <summary>
      /// Если настройки фильтрации используют правило, связанное с объектом "Правило" из БД,
      /// вэтом поле хранится идентификатор версии данного объекта
      /// </summary>
      long RuleID { get; }

      /// <summary>
      /// Экземпляр текущего правила подбора версий. Поле заполняется только тогда,
      /// когда правило подбора не связано с объектом "Правило" из БД, а назначается
      /// из группирующего объекта, т.п.
      /// </summary>
      VersionsRule CurrentRule { get; set; }

      /// <summary>
      /// Индекс основного варианта значений переменных для текущего правила подбора версий
      /// </summary>
      int CurrentRuleVars { get; set; }

      /// <summary>
      /// Получить или установить основной вариант значения переменных.
      /// Index - это OBJECT_ID правила подбора версий
      /// </summary>
      int this[long Rule_Object_ID] { get; set; }

      /// <summary>
      /// Дата и время UTC последнего доступа к настройкам фильтрации (свойство нужно для сборки мусора)
      /// </summary>
      DateTime LastAccess { get; set; }

      /// <summary>
      /// Дата и время UTC последней модификации настроек фильтрации (свойство нужно для проверки, когда в последний раз менялись настройки)
      /// </summary>
      DateTime LastChangeTime { get; set; }

      /// <summary>
      /// Ссылка на интерфейс коллекции значений [Ключ]=[Значение],
      /// где [Ключ] - это значение OBJECT_ID правила подбора версий,
      /// а [Значение] - это номер основного варианта значений переменных
      /// </summary>
      Dictionary<long, int> RuleVars { get; }

      /// <summary>
      /// Ссылка на интерфейс коллекции сериализуемых значений [Ключ]=[Значение],
      /// где ключ - Guid в виде строки, а значение - любой сериализуемый объект
      /// В тегах предполагается хранить какие-либо данные, которые надо "прицепить"
      /// к текущим настройкам фильтрации
      /// </summary>
      HybridDictionary Tags { get; }

      /// <summary>Очистить настройки фильтрации</summary>
      void Clear();

      /// <summary>
      /// Скопировать все поля объекта Source в данный экземпляр объекта.
      /// Если Source == null, то данный экземпляр будет очищен.
      /// </summary>
      /// <param name="Source">Из этого объекта будут скопированы настройки фильтрации</param>
      void Assign(IFiltrationSettings Source);
    }
}
