
// Type: Intermech.Interfaces.Dictionary.DictRule
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech.Interfaces.Dictionary
{
    /// <summary>Класс для хранения одного правила (условия)</summary>
    [Serializable]
    public class DictRule : ISerializable
    {
      private DictVOP _dictVop;
      private long _dictValue;
      private DictROP _dictRop;
      private long _value1;
      private long _value2;

      /// <summary>Конструктор</summary>
      public DictRule()
      {
      }

      /// <summary>Базовый оператор</summary>
      public DictVOP VOP
      {
        get => this._dictVop;
        set => this._dictVop = value;
      }

      /// <summary>
      /// Значение для базового оператора
      /// используется только с (DictVOP.Div и DICTVOP.Mod)
      /// </summary>
      public long VOPValue
      {
        get => this._dictValue;
        set => this._dictValue = value;
      }

      /// <summary>Оператор отношения</summary>
      public DictROP ROP
      {
        get => this._dictRop;
        set => this._dictRop = value;
      }

      /// <summary>Первое значение для опреатора отношение</summary>
      public long ROPValue1
      {
        get => this._value1;
        set => this._value1 = value;
      }

      /// <summary>
      /// Второе значение для оператора отношения
      /// используется для интервалов
      /// </summary>
      public long ROPValue2
      {
        get => this._value2;
        set => this._value2 = value;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
        string str1 = string.Empty;
        switch (this._dictVop)
        {
          case DictVOP.Value:
            str1 = $"{EnumTypeHelper.GetCaption((Enum) this._dictVop)}";
            break;
          case DictVOP.Div:
          case DictVOP.Mod:
            str1 = string.Format(LocalizationHolder.rm.GetString("Interfaces_13"), (object) EnumTypeHelper.GetCaption((Enum) this._dictVop), (object) this._dictValue);
            break;
        }
        string empty = string.Empty;
        string str2;
        switch (this._dictRop)
        {
          case DictROP.In:
          case DictROP.NotIn:
            str2 = string.Format(LocalizationHolder.rm.GetString("Interfaces_14"), (object) EnumTypeHelper.GetCaption((Enum) this._dictRop), (object) this._value1, (object) this._value2);
            break;
          default:
            str2 = $"{EnumTypeHelper.GetCaption((Enum) this._dictRop)} {this._value1}";
            break;
        }
        return $"{str1} {str2}";
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      protected DictRule(SerializationInfo info, StreamingContext context)
      {
        if (info.GetInt32("Version") < 100)
          return;
        this._dictVop = (DictVOP) EnumTypeHelper.GetEnumValue(typeof (DictVOP), info.GetString("DictVOP"), (object) DictVOP.Value);
        this._dictValue = info.GetInt64("DictValue");
        this._dictRop = (DictROP) EnumTypeHelper.GetEnumValue(typeof (DictROP), info.GetString("DictROP"), (object) DictROP.Equal);
        this._value1 = info.GetInt64("Value1");
        this._value2 = info.GetInt64("Value2");
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        info.AddValue("Version", 101);
        info.AddValue("DictVOP", (object) EnumTypeHelper.GetCaption((Enum) this._dictVop));
        info.AddValue("DictValue", this._dictValue);
        info.AddValue("DictROP", (object) EnumTypeHelper.GetCaption((Enum) this._dictRop));
        info.AddValue("Value1", this._value1);
        info.AddValue("Value2", this._value2);
      }
    }
}
