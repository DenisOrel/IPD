
// Type: Intermech.Interfaces.WebPortal.AttributeValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Структура с инфой по значению атрибута</summary>
    [Serializable]
    public class AttributeValue
    {
      /// <summary>Порядковый номер знаяения</summary>
      private int _inListID;
      /// <summary>Строковая составляющая</summary>
      private string _stringValue;
      /// <summary>Вещественная составляющая</summary>
      private double _doubleValue;
      /// <summary>Временная составляющая в InvariantCulture</summary>
      private string _dateTimeValue;
      /// <summary>Целочисленная составляющая</summary>
      private long _integerValue;
      /// <summary>
      /// Составляющая, хранящая тип упаковки файла (для блобов)
      /// </summary>
      private ArcMethods _arcMethod;
      /// <summary>Составляющая, хранящая имя файла (для блобов)</summary>
      private string _fileName;
      /// <summary>Составляющая, хранящая описание значения</summary>
      private string _description;
      /// <summary>Составляющая, хранящая некий глобальный идентификатор</summary>
      private string _guidValue;
      /// <summary>Автор</summary>
      private string _author;
      /// <summary>Тип файла в файловом шкафу</summary>
      private FileTypes _fileType;
      /// <summary>Флаг того что значение атрибута равно NULL</summary>
      private bool _isEmpty;

      /// <summary>Порядковый номер знаяения</summary>
      public int InListID
      {
        get => this._inListID;
        set => this._inListID = value;
      }

      /// <summary>Строковая составляющая</summary>
      public string StringValue
      {
        get => this._stringValue;
        set
        {
          this._stringValue = value;
          this._isEmpty = false;
        }
      }

      /// <summary>Строковая составляющая</summary>
      public string Description
      {
        get => this._description;
        set
        {
          this._description = value;
          this._isEmpty = false;
        }
      }

      /// <summary>Вещественная составляющая</summary>
      public double DoubleValue
      {
        get => this._doubleValue;
        set
        {
          this._doubleValue = value;
          this._isEmpty = false;
        }
      }

      /// <summary>Временная составляющая в InvariantCulture</summary>
      public string DateTimeValue
      {
        get => this._dateTimeValue;
        set
        {
          this._dateTimeValue = value;
          this._isEmpty = false;
        }
      }

      /// <summary>Целочисленная составляющая</summary>
      public long IntegerValue
      {
        get => this._integerValue;
        set
        {
          this._integerValue = value;
          this._isEmpty = false;
        }
      }

      /// <summary>
      /// Составляющая, хранящая тип упаковки файла (для блобов)
      /// </summary>
      public ArcMethods ArcMethod
      {
        get => this._arcMethod;
        set => this._arcMethod = value;
      }

      /// <summary>Составляющая, хранящая имя файла (для блобов)</summary>
      public string FileName
      {
        get => this._fileName;
        set => this._fileName = value;
      }

      /// <summary>Тип файла (для блобов)</summary>
      public FileTypes FileType
      {
        get => this._fileType;
        set => this._fileType = value;
      }

      /// <summary>Автор файла (для блобов)</summary>
      public string FileAuthor
      {
        get => this._author;
        set => this._author = value;
      }

      /// <summary>Составляющая, хранящая некий глобальный идентификатор</summary>
      public string GuidValue
      {
        get => this._guidValue;
        set
        {
          this._guidValue = value;
          this._isEmpty = false;
        }
      }

      /// <summary>Флаг того что значение атрибута равно NULL</summary>
      public bool IsEmpty => this._isEmpty;

      public AttributeValue()
      {
        this._inListID = 0;
        this._stringValue = string.Empty;
        this._doubleValue = double.MinValue;
        this._dateTimeValue = string.Empty;
        this._integerValue = long.MinValue;
        this._fileName = string.Empty;
        this._arcMethod = ArcMethods.NotPacked;
        this._guidValue = string.Empty;
        this._description = string.Empty;
        this._author = string.Empty;
        this._fileType = FileTypes.ftNormal;
        this._isEmpty = true;
      }
    }
}
