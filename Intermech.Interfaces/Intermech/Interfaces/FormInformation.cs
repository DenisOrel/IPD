
// Type: Intermech.Interfaces.FormInformation
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Diagnostics;
using System.Text;


namespace Intermech.Interfaces
{
    /// <summary>Класс для хранения информации о форме.</summary>
    [Serializable]
    public class FormInformation
    {
      private bool _hasFormula;
      [NonSerialized]
      private static char _delimiter = Encoding.Unicode.GetChars(new byte[2]
      {
        (byte) 1,
        (byte) 0
      })[0];

      /// <summary>Заголовок формы.</summary>
      public string Caption { get; set; }

      /// <summary>
      /// Идентификатор пользователя, который взял форму на редактирование.
      /// </summary>
      public long CheckOutBy { get; set; }

      /// <summary>Условие.</summary>
      public object FormulaData { get; set; }

      /// <summary>Задано ли на форму условие.</summary>
      public bool HasFormula
      {
        [DebuggerStepThrough] get => this._hasFormula;
        set
        {
          this._hasFormula = value;
          if (value)
            return;
          this.FormulaData = (object) null;
        }
      }

      /// <summary>Идентификатор формы.</summary>
      public long ID { get; private set; }

      /// <summary>Идентификатор типа объекта.</summary>
      public int TypeID { get; private set; }

      /// <summary>
      /// 
      /// </summary>
      public int OrderIndex { get; set; }

      /// <summary>Конструктор.</summary>
      /// <param name="iDBObj">Объект</param>
      public FormInformation(IDBObject iDBObj)
        : this(iDBObj, false)
      {
        this._hasFormula = iDBObj.GetAttributeByGuid(new Guid("cad00064-306c-11d8-b4e9-00304f19f545"), false) != null;
      }

      /// <summary>Конструктор.</summary>
      /// <param name="id">Идентификатор формы</param>
      /// <param name="caption">Заголовок формы</param>
      /// <param name="hasFormula">Задано ли на форму условие</param>
      public FormInformation(long id, string caption, bool hasFormula)
      {
        this.CheckOutBy = 0L;
        this.TypeID = -1;
        this.ID = id;
        this.Caption = caption;
        this._hasFormula = hasFormula;
      }

      /// <summary>Конструктор.</summary>
      /// <param name="id">Идентификатор формы</param>
      /// <param name="caption">Заголовок формы</param>
      /// <param name="hasFormula">Задано ли на форму условие</param>
      /// <param name="typeID">Идентификатор типа объекта</param>
      /// <param name="checkOutBy">Идентификатор пользователя, взявшего объект на изменение</param>
      public FormInformation(long id, string caption, bool hasFormula, int typeID, long checkOutBy)
      {
        this.ID = id;
        this.Caption = caption;
        this._hasFormula = hasFormula;
        this.TypeID = typeID;
        this.CheckOutBy = checkOutBy;
      }

      /// <summary>Конструктор.</summary>
      /// <param name="id">Идентификатор формы</param>
      /// <param name="caption">Заголовок формы</param>
      /// <param name="hasFormula">Задано ли на форму условие</param>
      /// <param name="formula">Условие для формы</param>
      /// <param name="typeID">Идентификатор типа объекта</param>
      /// <param name="checkOutBy">Идентификатор пользователя, взявшего объект на изменение</param>
      private FormInformation(
        long id,
        string caption,
        bool hasFormula,
        object formula,
        int typeID,
        long checkOutBy)
      {
        this.ID = id;
        this.Caption = caption;
        this.FormulaData = formula;
        this._hasFormula = hasFormula;
        this.TypeID = typeID;
        this.CheckOutBy = checkOutBy;
      }

      /// <summary>Конструктор.</summary>
      /// <param name="iDBObj">Объект</param>
      /// <param name="hasFormula">Задано ли на форму условие</param>
      public FormInformation(IDBObject iDBObj, bool hasFormula)
      {
        this.ID = iDBObj.ObjectID;
        this.Caption = iDBObj.Caption;
        this._hasFormula = hasFormula;
        this.TypeID = iDBObj.TypeID;
        this.CheckOutBy = iDBObj.CheckoutBy;
      }

      /// <summary>Конструктор.</summary>
      /// <param name="id">Идентификатор формы</param>
      /// <param name="iDBObj">Объект</param>
      public FormInformation(long id, IDBObject iDBObj)
        : this(iDBObj)
      {
        this.ID = id;
      }

      /// <summary>Конструктор.</summary>
      /// <param name="id">Идентификатор формы</param>
      /// <param name="iDBObj">Объект</param>
      /// <param name="hasFormula">Задано ли на форму условие</param>
      /// &gt;
      public FormInformation(long id, IDBObject iDBObj, bool hasFormula)
        : this(iDBObj, hasFormula)
      {
        this.ID = id;
      }

      /// <summary>Конструктор.</summary>
      /// <param name="id">Идентификатор формы</param>
      /// <param name="iDBObj">Объект</param>
      /// <param name="formulaData">Условие</param>
      /// &gt;
      public FormInformation(long id, IDBObject iDBObj, object formulaData)
        : this(iDBObj, formulaData != null)
      {
        this.ID = id;
        this.FormulaData = formulaData;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="formID"></param>
      /// <returns></returns>
      public FormInformation CloneWithActualID(long formID)
      {
        return new FormInformation(formID, this.Caption, this._hasFormula, this.FormulaData, this.TypeID, this.CheckOutBy);
      }

      /// <summary>Сохранение в строку для дальнейшего разбора.</summary>
      /// <param name="forParse">true - если нужен дальнейший разбор строки</param>
      /// <returns>Строка</returns>
      public string ToString(bool forParse)
      {
        if (!forParse)
          return this.ToString();
        string str = this.Caption.Replace(',', FormInformation._delimiter);
        return string.Join(",", this.ID.ToString(), str, this._hasFormula.ToString());
      }

      /// <summary>Функция для разбора информации о форме из строки.</summary>
      /// <param name="value">Строка для разбора</param>
      /// <returns>Информация о форме, null в случае ошибки</returns>
      public static FormInformation Parse(string value)
      {
        FormInformation formInformation = (FormInformation) null;
        string[] strArray = value.Split(',');
        if (strArray != null && strArray.Length >= 3)
        {
          string caption = strArray[1].Replace(FormInformation._delimiter, ',');
          formInformation = new FormInformation(Convert.ToInt64(strArray[0]), caption, Convert.ToBoolean(strArray[2]));
        }
        return formInformation;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode() => this.ID.GetHashCode();

      /// <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
        return !(obj is FormInformation formInformation) ? base.Equals(obj) : this.ID == formInformation.ID;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
        return !string.IsNullOrEmpty(this.Caption) ? this.Caption : string.Format(LocalizationHolder.rm.GetString("Interfaces_58"), (object) this.ID);
      }
    }
}
