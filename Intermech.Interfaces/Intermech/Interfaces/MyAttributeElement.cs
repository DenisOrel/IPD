
// Type: Intermech.Interfaces.MyAttributeElement
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Класс для хранения значения атрибута объекта</summary>
    [Serializable]
    public sealed class MyAttributeElement : ICloneable
    {
      /// <summary>ID атрибута</summary>
      public int ID;
      /// <summary>GUID атрибута</summary>
      public string GUID = string.Empty;
      /// <summary>Значение атрибута</summary>
      public object Value = (object) "";
      /// <summary>Название атрибута</summary>
      public string Caption = "";
      /// <summary>Какие-то пользовательские данные</summary>
      public object Tag;

      /// <summary>Создать пустой экземпляр класса</summary>
      public MyAttributeElement()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="AnID">ID атрибута</param>
      /// <param name="AGUID">GUID атрибута</param>
      /// <param name="AnValue">Значение элемента</param>
      /// <param name="ACaption">Текстовое описание элемента</param>
      /// <param name="ATag">Пользовательские данные</param>
      public MyAttributeElement(int AnID, string AGUID, object AnValue, string ACaption, object ATag)
      {
        this.ID = AnID;
        this.GUID = AGUID;
        this.Value = AnValue;
        this.Caption = ACaption;
        this.Tag = ATag;
      }

      /// <summary>Перекрытый метод для возвращения заголовка</summary>
      /// <returns></returns>
      public override string ToString()
      {
        if (this.Caption.Length > 0)
          return this.Caption;
        try
        {
          return Convert.ToString(this.Value);
        }
        catch
        {
        }
        return "";
      }

      /// <summary>Сделать клон объекта</summary>
      /// <returns>Вернёт 100% копию объекта</returns>
      public object Clone()
      {
        return (object) new MyAttributeElement(this.ID, this.GUID, this.Value, this.Caption, this.Tag);
      }
    }
}
