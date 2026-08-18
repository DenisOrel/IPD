
// Type: Intermech.Interfaces.FiltrationSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Contexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>Класс для хранения настроек фильтрации состава</summary>
    [Serializable]
    public sealed class FiltrationSettings : IAssignable, ICloneable, IFiltrationSettings
    {
      /// <summary>
      /// Уникальный объект-ключ владельца данного правила.
      /// Значение должно быть сериализуемым!
      /// </summary>
      private string FOwnerID = string.Empty;
      /// <summary>Информация о текущем контексте редактирования</summary>
      private CurrentEditingContext FEditingContext = CurrentEditingContext.Empty;
      /// <summary>
      /// Название правила подбора версий, используемого в данных настройках фильтрации
      /// </summary>
      private string FCaption = string.Empty;
      /// <summary>
      /// Если настройки фильтрации используют правило, связанное с объектом "Правило" из БД,
      /// вэтом поле хранится идентификатор версии данного объекта
      /// </summary>
      private long FRuleID;
      /// <summary>
      /// Экземпляр текущего правила подбора версий. Поле заполняется только тогда,
      /// когда правило подбора не связано с объектом "Правило" из БД, а назначается
      /// из группирующего объекта, т.п.
      /// </summary>
      private VersionsRule FCurrentRule;
      /// <summary>
      /// Дата и время последнего доступа к настройкам фильтрации (свойство нужно для сборки мусора)
      /// </summary>
      private DateTime FLastAccess;
      /// <summary>
      /// Дата и время последней модификации настроек фильтрации (свойство нужно для проверки, когда в последний раз менялись настройки)
      /// </summary>
      private DateTime FLastChangeTime;
      /// <summary>
      /// Коллекция пар значений [Int64 OBJECT_ID] = [Int32 index]
      /// </summary>
      private Dictionary<long, int> FRuleVars = new Dictionary<long, int>(0);
      /// <summary>
      /// Ссылка на интерфейс коллекции сериализуемых значений [Ключ]=[Значение],
      /// где ключ - Guid в виде строки, а значение - любой сериализуемый объект
      /// В тегах предполагается хранить какие-либо данные, которые надо "прицепить"
      /// к текущим настройкам фильтрации
      /// </summary>
      private HybridDictionary FTags = new HybridDictionary(0);

      /// <summary>
      /// Уникальный объект-ключ владельца данного правила.
      /// Значение должно быть сериализуемым!
      /// </summary>
      public string OwnerID
      {
        [DebuggerStepThrough] get => this.FOwnerID;
        set => this.FOwnerID = value;
      }

      /// <summary>Информация о текущем контексте редактирования</summary>
      public CurrentEditingContext EditingContext
      {
        [DebuggerStepThrough] get => this.FEditingContext;
        set
        {
          this.FEditingContext = value != null ? value : throw new ArgumentNullException(nameof (value));
        }
      }

      /// <summary>
      /// Название правила подбора версий, используемого в данных настройках фильтрации
      /// </summary>
      public string Caption
      {
        [DebuggerStepThrough] get => this.FCaption;
      }

      /// <summary>
      /// Если настройки фильтрации используют правило, связанное с объектом "Правило" из БД,
      /// вэтом поле хранится идентификатор версии данного объекта
      /// </summary>
      public long RuleID
      {
        [DebuggerStepThrough] get => this.FRuleID;
      }

      /// <summary>
      /// Экземпляр текущего правила подбора версий. Поле заполняется только тогда,
      /// когда правило подбора не связано с объектом "Правило" из БД, а назначается
      /// из группирующего объекта, т.п.
      /// </summary>
      public VersionsRule CurrentRule
      {
        get => this.FCurrentRule;
        set
        {
          this.FCurrentRule = value != null ? value.Clone() as VersionsRule : (VersionsRule) null;
          if (this.FCurrentRule != null)
          {
            this.FCaption = !this.FCurrentRule.HasVariableValues() ? this.FCurrentRule.RuleObjectCaption : $"{this.FCurrentRule.RuleObjectCaption} - {this.FCurrentRule.GetDisplayValue(2)}";
            this.FRuleID = this.FCurrentRule.RuleObjectID;
          }
          else
          {
            this.FCaption = string.Empty;
            this.FRuleID = 0L;
          }
          this.LastChangeTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Индекс основного варианта значений переменных для текущего правила подбора версий
      /// </summary>
      public int CurrentRuleVars
      {
        get => this.FCurrentRule == null ? -1 : this[this.FCurrentRule.RuleObjectID];
        set
        {
          if (this.FCurrentRule == null)
            return;
          this[this.FCurrentRule.RuleObjectID] = value;
          this.FCaption = !this.FCurrentRule.HasVariableValues() ? this.FCurrentRule.GetDisplayValue(0).ToString() : this.FCurrentRule.GetDisplayValue(2).ToString();
          this.LastChangeTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Получить или установить основной вариант значения переменных.
      /// Rule_Object_ID - это OBJECT_ID правила подбора версий
      /// </summary>
      public int this[long Rule_Object_ID]
      {
        get
        {
          lock (this)
          {
            if (!this.FRuleVars.ContainsKey(Rule_Object_ID))
              return -1;
            int num = this.FRuleVars[Rule_Object_ID];
            if (num < 0)
              num = -1;
            return num;
          }
        }
        set
        {
          lock (this)
          {
            if (this.FRuleVars.ContainsKey(Rule_Object_ID))
              this.FRuleVars.Remove(Rule_Object_ID);
            if (value >= 0)
              this.FRuleVars.Add(Rule_Object_ID, value);
            this.LastChangeTime = DateTime.UtcNow;
          }
        }
      }

      /// <summary>
      /// Дата и время UTC последнего доступа к настройкам фильтрации (свойство нужно для сборки мусора)
      /// </summary>
      public DateTime LastAccess
      {
        [DebuggerStepThrough] get => this.FLastAccess;
        [DebuggerStepThrough] set => this.FLastAccess = value;
      }

      /// <summary>
      /// Дата и время UTC последней модификации настроек фильтрации (свойство нужно для проверки, когда в последний раз менялись настройки)
      /// </summary>
      public DateTime LastChangeTime
      {
        [DebuggerStepThrough] get => this.FLastChangeTime;
        [DebuggerStepThrough] set => this.FLastChangeTime = value;
      }

      /// <summary>
      /// Ссылка на интерфейс коллекции значений [Ключ]=[Значение],
      /// где [Ключ] - это значение OBJECT_ID правила подбора версий,
      /// а [Значение] - это номер основного варианта значений переменных
      /// </summary>
      public Dictionary<long, int> RuleVars
      {
        [DebuggerStepThrough] get => this.FRuleVars;
        set => this.FRuleVars = value ?? new Dictionary<long, int>(0);
      }

      /// <summary>
      /// Ссылка на интерфейс коллекции сериализуемых значений [Ключ]=[Значение],
      /// где ключ - Guid в виде строки, а значение - любой сериализуемый объект
      /// В тегах предполагается хранить какие-либо данные, которые надо "прицепить"
      /// к текущим настройкам фильтрации
      /// </summary>
      public HybridDictionary Tags
      {
        [DebuggerStepThrough] get => this.FTags;
      }

      /// <summary>Создать пустой экземпляр класса CurrentVersionRule</summary>
      public FiltrationSettings()
      {
        this.FOwnerID = string.Empty;
        if (this.FRuleVars == null)
          this.FRuleVars = new Dictionary<long, int>(0);
        this.FLastAccess = DateTime.UtcNow;
        this.LastChangeTime = this.FLastAccess;
        this.FCaption = string.Empty;
        this.FRuleID = 0L;
        if (this.FTags != null)
          return;
        this.FTags = new HybridDictionary(0, true);
      }

      /// <summary>Создать экземпляр класса с указанным идентификатором</summary>
      /// <param name="ownerID">Идентификатор</param>
      public FiltrationSettings(Guid ownerID)
        : this()
      {
        this.FOwnerID = ownerID.ToString();
      }

      /// <summary>Очистить настройки фильтрации</summary>
      public void Clear()
      {
        lock (this)
        {
          this.FOwnerID = string.Empty;
          this.FEditingContext = CurrentEditingContext.Empty;
          this.FCurrentRule = (VersionsRule) null;
          this.FCaption = string.Empty;
          this.FRuleID = 0L;
          if (this.FRuleVars == null)
            this.FRuleVars = new Dictionary<long, int>();
          this.FRuleVars.Clear();
          if (this.FTags == null)
            this.FTags = new HybridDictionary(0, true);
          this.LastChangeTime = DateTime.UtcNow;
        }
      }

      /// <summary>
      /// Скопировать все поля объекта Source в данный экземпляр объекта.
      /// Если Source == null, то данный экземпляр будет очищен.
      /// </summary>
      /// <param name="Source">Из этого объекта будут скопированы настройки фильтрации</param>
      public void Assign(IFiltrationSettings Source)
      {
        if (this == Source)
          return;
        this.Clear();
        if (Source == null)
          return;
        lock (Source)
        {
          lock (this)
          {
            this.FOwnerID = Source.OwnerID;
            this.FEditingContext = Source.EditingContext;
            this.FCurrentRule = Source.CurrentRule != null ? Source.CurrentRule.Clone() as VersionsRule : (VersionsRule) null;
            this.FLastAccess = Source.LastAccess;
            this.FRuleVars.Clear();
            this.FCaption = Source.Caption;
            this.FRuleID = Source.RuleID;
            if (this.FTags == null)
              this.FTags = new HybridDictionary(0, true);
            this.FTags.Clear();
            if (Source.RuleVars != null)
            {
              foreach (KeyValuePair<long, int> ruleVar in Source.RuleVars)
                this.FRuleVars.Add(ruleVar.Key, ruleVar.Value);
            }
            if (Source.Tags != null)
            {
              IDictionaryEnumerator enumerator = Source.Tags.GetEnumerator();
              if (enumerator != null)
              {
                enumerator.Reset();
                while (enumerator.MoveNext())
                  this.FTags[enumerator.Key is ICloneable ? ((ICloneable) enumerator.Key).Clone() : enumerator.Key] = enumerator.Value is ICloneable ? ((ICloneable) enumerator.Value).Clone() : enumerator.Value;
              }
            }
            this.LastChangeTime = DateTime.UtcNow;
          }
        }
      }

      /// <summary>
      /// Скопировать все поля объекта Source в данный экземпляр объекта.
      /// Если Source == null, то данный экземпляр будет очищен.
      /// </summary>
      /// <param name="Source">Из этого объекта будут скопированы настройки фильтрации</param>
      public void Assign(FiltrationSettings Source)
      {
        if (this == Source)
          return;
        this.Clear();
        if (Source == null)
          return;
        IFiltrationSettings Source1 = (IFiltrationSettings) Source;
        if (Source1 == null)
          return;
        this.Assign(Source1);
      }

      /// <summary>
      /// Скопировать все поля объекта Source в данный экземпляр объекта.
      /// Если Source == null, то данный экземпляр будет очищен.
      /// </summary>
      /// <param name="Source">Из этого объекта будут скопированы настройки фильтрации</param>
      public void Assign(object Source)
      {
        if (this == Source)
          return;
        this.Clear();
        if (Source == null || !(Source is IFiltrationSettings Source1))
          return;
        this.Assign(Source1);
      }

      /// <summary>Сделать клон объекта</summary>
      /// <returns>Вернёт 100% копию объекта</returns>
      public object Clone()
      {
        FiltrationSettings filtrationSettings = new FiltrationSettings();
        filtrationSettings.Assign(this);
        return (object) filtrationSettings;
      }
    }
}
