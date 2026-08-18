
// Type: Intermech.Interfaces.AttributeValues
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, содержащий идентификатор(ы) атрибуты + его значение(я)
    /// </summary>
    [Serializable]
    public class AttributeValues : ICloneable, IEquatable<AttributeValues>
    {
      /// <summary>Идентификатор атрибута</summary>
      public int AttributeID;
      /// <summary>Название атрибута</summary>
      public string AttributeName;
      /// <summary>Guid атрибута</summary>
      public Guid AttributeGuid;
      /// <summary>Псевдоним атрибута</summary>
      public string AttributeAlias;
      /// <summary>Тип данных атрибута</summary>
      public FieldTypes AttributeType;
      /// <summary>Значения атрибута</summary>
      public object[] Values;
      /// <summary>
      /// Строковые расшифровки значений атрибута (например, в Values[0] сидит идентификатор владельца объекта,
      /// а в Captions[0] - его имя). Поле заполняется сервером если включен флаг GetAttributeValuesModes.IncludeCaptions.
      /// Если расшифровка для данного атрибута не имеет смысла, то Captions==null
      /// </summary>
      public object[] Descriptions;
      /// <summary>Режим работы атрибута со списковыми значениями</summary>
      public MultiValueModes MultipleValued;
      /// <summary>Тип вычисления атрибута</summary>
      public ComputeValueModes ComputeMode;
      /// <summary>
      /// Если = true, то точно только для чтения, иначе зависит от состояния CheckAccess при получении AttributeValues[]
      /// </summary>
      public bool ReadOnly;
      /// <summary>Имя группы, в которую входит атрибут</summary>
      public string GroupName;
      /// <summary>
      /// Если инициализирован в true, то актуальны только AttributeID и Values,
      /// т.к. данный экземпляр AttributeValues создан заново, а не получен от сервера.
      /// </summary>
      public bool IsNew;
      /// <summary>
      /// Если == false, то при невозможности записи данного атрибута в базу систем не генерирует исключение,
      /// что позволяет при пакетной записи не прерывать запись всех атрибутов в случае если атрибут со
      /// значением ThrowSetException == false записать не получается.
      /// </summary>
      public bool ThrowSetException = true;

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="attributeID">Идентификатор атрибута</param>
      /// <param name="attributeType">Тип данных атрибута</param>
      /// <param name="multipleValued">Режим работы атрибута со списковыми значениями</param>
      /// <param name="computeMode">Тип вычисления атрибута</param>
      public AttributeValues(
        int attributeID,
        FieldTypes attributeType,
        MultiValueModes multipleValued,
        ComputeValueModes computeMode)
        : this(attributeID, attributeType, multipleValued)
      {
        this.ComputeMode = computeMode;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="attributeID"></param>
      /// <param name="attributeType"></param>
      /// <param name="multipleValued"></param>
      public AttributeValues(int attributeID, FieldTypes attributeType, MultiValueModes multipleValued)
      {
        this.AttributeID = attributeID;
        this.AttributeType = attributeType;
        this.AttributeName = (string) null;
        this.Values = (object[]) null;
        this.AttributeAlias = (string) null;
        this.AttributeGuid = Guid.Empty;
        this.ReadOnly = false;
        this.GroupName = "";
        this.MultipleValued = multipleValued;
        this.Descriptions = (object[]) null;
      }

      public AttributeValues(int attributeID)
      {
        this.AttributeID = attributeID;
        this.AttributeType = FieldTypes.ftUnknown;
        this.AttributeName = (string) null;
        this.Values = (object[]) null;
        this.AttributeAlias = (string) null;
        this.AttributeGuid = Guid.Empty;
        this.ReadOnly = false;
        this.GroupName = "";
        this.MultipleValued = MultiValueModes.SingleValue;
        this.Descriptions = (object[]) null;
      }

      public AttributeValues(int attributeID, object initValue)
      {
        this.AttributeID = attributeID;
        this.AttributeType = FieldTypes.ftUnknown;
        this.AttributeName = (string) null;
        if (initValue is Array)
          this.Values = (object[]) initValue;
        else
          this.Values = new object[1]{ initValue };
        this.AttributeAlias = (string) null;
        this.AttributeGuid = Guid.Empty;
        this.ReadOnly = false;
        this.GroupName = "";
        this.MultipleValued = MultiValueModes.SingleValue;
        this.Descriptions = (object[]) null;
      }

      public AttributeValues(
        int attributeID,
        FieldTypes attributeType,
        MultiValueModes multipleValued,
        object[] initValues)
      {
        this.AttributeID = attributeID;
        this.AttributeType = attributeType;
        this.AttributeName = (string) null;
        this.Values = initValues;
        this.AttributeAlias = (string) null;
        this.AttributeGuid = Guid.Empty;
        this.ReadOnly = false;
        this.GroupName = "";
        this.MultipleValued = multipleValued;
        this.Descriptions = (object[]) null;
      }

      /// <summary>
      /// Возвращает нулевое значение из массива значений атрибутов
      /// </summary>
      public object Value => this.Values[0];

      /// <summary>
      /// Возвращает строковое представление нулевого значения из массива значений атрибутов
      /// </summary>
      public string AsString => this.Values[0].ToString();

      /// <summary>
      /// Возвращает целочисленное представление нулевого значения из массива значений атрибутов
      /// </summary>
      public long AsInteger => Convert.ToInt64(this.Values[0]);

      public override bool Equals(object obj)
      {
        return !(obj is AttributeValues other) ? base.Equals(obj) : this.Equals(other, false);
      }

      public bool Equals(AttributeValues other, bool contentOnly)
      {
        if (other == null)
          throw new ArgumentNullException("p");
        if (contentOnly)
        {
          if (this.AttributeID != other.AttributeID)
            return false;
        }
        else if ((this.AttributeID != other.AttributeID || !(this.AttributeName == other.AttributeName) || !(this.AttributeGuid == other.AttributeGuid) || !(this.AttributeAlias == other.AttributeAlias) || this.AttributeType != other.AttributeType || this.MultipleValued != other.MultipleValued || this.ReadOnly != other.ReadOnly || !(this.GroupName == other.GroupName) || this.IsNew != other.IsNew ? 0 : (this.ComputeMode == other.ComputeMode ? 1 : 0)) == 0)
          return false;
        return AttributeValues.ValuesEquals(this.Values, other.Values);
      }

      public override int GetHashCode()
      {
        int hashCode = 0;
        if (this.Values == null || this.Values == DBNull.Value || this.Values.ToString() == string.Empty)
          return hashCode;
        for (int index = 0; index < this.Values.Length; ++index)
        {
          if (this.Values[index] != null && this.Values[index] != DBNull.Value)
            hashCode ^= this.Values[index].GetHashCode();
        }
        return hashCode;
      }

      /// <summary>Сравнивает два массива значений атрибутов.</summary>
      /// <param name="x">Первый массив значений</param>
      /// <param name="y">Второй массив значений</param>
      /// <returns>true, если массивы эквивалентны, иначе - массивы различаются</returns>
      public static bool ValuesEquals(object[] x, object[] y)
      {
        bool flag1 = x == null;
        bool flag2 = y == null;
        if (flag1 & flag2)
          return true;
        if (flag1 && !flag2 || !flag1 & flag2 || x.Length != y.Length)
          return false;
        if (x.Length == 0)
          return true;
        for (int index = 0; index < x.Length; ++index)
        {
          if (!AttributeValues.ValueEquals(x[index], y[index]))
            return false;
        }
        return true;
      }

      /// <summary>Сравнивает два значения атрибута.</summary>
      /// <param name="x">Первое значение</param>
      /// <param name="y">Второе значение</param>
      /// <returns>true, если значения эквивалентны, иначе - значения различаются</returns>
      public static bool ValueEquals(object x, object y)
      {
        bool flag1 = AttributeValues.IsNullOrEmptyString(x);
        bool flag2 = AttributeValues.IsNullOrEmptyString(y);
        if (flag1 & flag2)
          return true;
        return (!flag1 || flag2) && !(!flag1 & flag2) && x.Equals(y);
      }

      public static bool IsNullOrEmptyString(object value)
      {
        return value == null || value == DBNull.Value || string.Empty.Equals(value);
      }

      public object Clone()
      {
        AttributeValues attributeValues = new AttributeValues(this.AttributeID);
        attributeValues.AttributeName = this.AttributeName;
        attributeValues.AttributeGuid = this.AttributeGuid;
        attributeValues.AttributeAlias = this.AttributeAlias;
        attributeValues.AttributeType = this.AttributeType;
        attributeValues.MultipleValued = this.MultipleValued;
        attributeValues.ReadOnly = this.ReadOnly;
        attributeValues.GroupName = this.GroupName;
        attributeValues.Values = (object[]) this.Values.Clone();
        for (int index = 0; index < attributeValues.Values.Length; ++index)
        {
          if (attributeValues.Values[index] != null && attributeValues.Values[index] != DBNull.Value && !(attributeValues.Values[index] is ValueType))
          {
            if (!(attributeValues.Values[index] is ICloneable))
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("Interfaces_509"), (object) attributeValues.Values[index].GetType().ToString(), (object) this.AttributeName));
            attributeValues.Values[index] = (attributeValues.Values[index] as ICloneable).Clone();
          }
        }
        if (this.Descriptions != null)
          attributeValues.Descriptions = (object[]) this.Descriptions.Clone();
        attributeValues.IsNew = this.IsNew;
        attributeValues.ComputeMode = this.ComputeMode;
        attributeValues.ThrowSetException = this.ThrowSetException;
        return (object) attributeValues;
      }

      public bool Equals(AttributeValues other) => this.Equals(other, false);
    }
}
