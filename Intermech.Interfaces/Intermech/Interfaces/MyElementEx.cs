
// Type: Intermech.Interfaces.MyElementEx
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для хранения описания какого-либо объекта, а также нескольких его свойств
    /// </summary>
    [Serializable]
    public class MyElementEx : ICloneable, IComparable
    {
      /// <summary>Объектное значение элемента</summary>
      public object Value;
      /// <summary>Текстовое описание элемента</summary>
      public string Caption = string.Empty;
      /// <summary>Какой-либо флажок для элемента</summary>
      public bool ElementBool;
      /// <summary>Какой-либо флажок для элемента</summary>
      public bool ElementBool2;
      /// <summary>Какой-либо флажок для элемента</summary>
      public bool ElementBool3;
      /// <summary>Int64-идентификатор элемента</summary>
      public long ElementID64;
      /// <summary>Int32-идентификатор элемента</summary>
      public int ElementID32;
      /// <summary>Guid элемента</summary>
      public Guid ElementGuid = Guid.Empty;
      /// <summary>Дополнительные пользовательские данные</summary>
      public ArrayList Tags = new ArrayList(0);

      /// <summary>Создать пустой экземпляр класса</summary>
      public MyElementEx()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="AValue">Значение элемента</param>
      /// <param name="ACaption">Текстовое описание элемента</param>
      /// <param name="AnElementBool">Какой-либо флажок для элемента</param>
      /// 
      ///             /// <param name="AnElementBool2">Какой-либо флажок для элемента</param>
      /// 
      ///             /// <param name="AnElementBool3">Какой-либо флажок для элемента</param>
      /// <param name="AnElement64">Int64-идентификатор элемента</param>
      /// <param name="AnElement32">Int32-идентификатор элемента</param>
      /// <param name="AnElementGuid">Guid элемента</param>
      /// <param name="TheTags">Пользовательские данные</param>
      public MyElementEx(
        object AValue,
        string ACaption,
        bool AnElementBool,
        bool AnElementBool2,
        bool AnElementBool3,
        long AnElement64,
        int AnElement32,
        Guid AnElementGuid,
        params object[] TheTags)
      {
        this.Value = AValue;
        this.Caption = ACaption;
        this.ElementBool = AnElementBool;
        this.ElementBool2 = AnElementBool2;
        this.ElementBool3 = AnElementBool3;
        this.ElementID64 = AnElement64;
        this.ElementID32 = AnElement32;
        this.ElementGuid = AnElementGuid;
        if (this.Tags == null)
          this.Tags = new ArrayList(0);
        this.Tags.Clear();
        if (TheTags == null || TheTags.Length == 0)
          return;
        for (int index = 0; index < TheTags.Length; ++index)
          this.Tags.Add(TheTags[index]);
      }

      /// <summary>Очистка полей</summary>
      public void Clear()
      {
        this.Value = (object) null;
        this.Caption = string.Empty;
        this.ElementBool = false;
        this.ElementBool2 = false;
        this.ElementBool3 = false;
        this.ElementID64 = 0L;
        this.ElementID32 = 0;
        this.ElementGuid = new Guid();
        this.Tags.Clear();
      }

      /// <summary>Перекрытый метод для возвращения заголовка</summary>
      /// <returns></returns>
      public override string ToString()
      {
        if (this.Caption != null && this.Caption.Length > 0)
          return this.Caption;
        if (this.ElementGuid.ToString().Length > 0)
          return this.ElementGuid.ToString();
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
        object[] objArray = (object[]) null;
        if (this.Tags.Count > 0)
        {
          objArray = new object[this.Tags.Count];
          this.Tags.CopyTo((Array) objArray);
        }
        return (object) new MyElementEx(this.Value, this.Caption, this.ElementBool, this.ElementBool2, this.ElementBool3, this.ElementID64, this.ElementID32, this.ElementGuid, objArray);
      }

      public int CompareTo(object obj)
      {
        return obj is MyElementEx myElementEx ? this.Caption.CompareTo(myElementEx.Caption) : throw new ArgumentException("Object is not a MyElementEx");
      }
    }
}
