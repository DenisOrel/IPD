
// Type: Intermech.Interfaces.BaseMeasureRuleHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Базовый класс предназначен для работы со строкой настроек ввода значений атрибутов, выраженных в единицах измерения
    /// </summary>
    public abstract class BaseMeasureRuleHelper
    {
      protected string _RuleString;
      protected object _Attribute;

      public BaseMeasureRuleHelper(string ruleString, object attribute)
      {
        this._RuleString = ruleString.Trim();
        this._Attribute = attribute;
      }

      /// <summary>
      /// Проверяет строку настроек на возможность записи в базу и возвращает выделенную из строки настроек
      /// формулу контроля правильности значения
      /// </summary>
      public string ValidateRuleString(string newRuleSettings)
      {
        newRuleSettings = newRuleSettings.Trim();
        if (newRuleSettings == string.Empty)
          return string.Empty;
        string[] strArray = newRuleSettings.Split(',');
        return strArray.Length == 1 || strArray.Length == 4 ? strArray[0] : throw new KernelExceptionID(243, (object) newRuleSettings);
      }

      /// <summary>Формула контроля правильности значения</summary>
      public string RuleFormula
      {
        get
        {
          if (this._RuleString == string.Empty)
            return string.Empty;
          return this._RuleString.Split(',')[0];
        }
      }

      /// <summary>Имя атрибута для сообщений об ошибках и логов</summary>
      protected abstract string ObjectName { get; }

      /// <summary>Свойство атрибута Размер</summary>
      protected abstract long SizeType { get; }

      /// <summary>Идентификатор единицы измерения по умолчанию</summary>
      public long DefaultMeasureID
      {
        get
        {
          if (this._RuleString != string.Empty)
          {
            string[] strArray = this._RuleString.Split(',');
            if (strArray.Length > 1)
            {
              MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(strArray[1]);
              return !descriptor.Empty ? descriptor.MeasureID : throw new KernelExceptionID(244, (object) strArray[1], (object) this.ObjectName);
            }
          }
          if (this.SizeType > 0L)
          {
            long baseMeasureId = MeasureHelper.GetBaseMeasureID(this.SizeType);
            if (baseMeasureId > 0L)
              return baseMeasureId;
          }
          return 0;
        }
      }

      /// <summary>
      /// Нужно ли записывать ид. единиц измерения в строковую часть атрибута
      /// </summary>
      public bool ShortNameInString
      {
        get
        {
          if (this._RuleString == string.Empty)
            return true;
          string[] strArray = this._RuleString.Split(',');
          return strArray.Length <= 2 || strArray[2].Trim() == "1";
        }
      }

      /// <summary>
      /// Приводить ли записываемые значения в единицу измерения по умолчанию
      /// </summary>
      public bool ConvertToDefaultMeasure
      {
        get
        {
          if (this._RuleString == string.Empty)
            return false;
          string[] strArray = this._RuleString.Split(',');
          return strArray.Length > 3 && strArray[3].Trim() == "1";
        }
      }
    }
}
