
// Type: Intermech.Interfaces.ColorWidth
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Serialization;


namespace Intermech.Interfaces
{
    /// <summary>Класс для описания  настроек толщины для цвета в Acad</summary>
    [DebuggerDisplay("[{Used ? 1 : 0}] {AcadIndex,d} {Width}")]
    [Serializable]
    public class ColorWidth
    {
      /// <summary>толщина для цвета(мм)</summary>
      private float width;

      /// <summary>индекс цвета в Acad</summary>
      [XmlAttribute("Id")]
      public byte AcadIndex { get; set; }

      /// <summary>использовано для прорисовки</summary>
      [XmlAttribute("Used")]
      public bool Used { get; set; }

      /// <summary>толщина для цвета(мм)</summary>
      [XmlAttribute("Width")]
      public float Width
      {
        get => this.width;
        set => this.width = (double) value < 0.0 ? 0.0f : value;
      }

      /// <summary>конструктор</summary>
      public ColorWidth()
      {
        this.AcadIndex = (byte) 0;
        this.Width = 0.0f;
        this.Used = false;
      }

      /// <summary>Класс для описания  настроек толщины для цвета в Acad</summary>
      /// <param name="varUsed">использовано для прорисовки</param>
      /// <param name="varAcadIndex">индекс цвета в Acad(1-255)</param>
      /// <param name="varWidth">толщина для цвета(мм)</param>
      public ColorWidth(bool varUsed, byte varAcadIndex, float varWidth)
      {
        this.AcadIndex = varAcadIndex;
        this.Width = varWidth;
        this.Used = varUsed;
      }

      /// <summary>Заполнить поля класса на основе строки из настроек</summary>
      /// <param name="settingsString">строка из настроек "{used? "1":"0"}|{acadIndex}|{width}"</param>
      public ColorWidth(string settingsString)
        : this()
      {
        string[] strArray = settingsString.Split('¦');
        if (strArray.Length < 3)
          return;
        this.Used = strArray[0] == "1";
        this.AcadIndex = Convert.ToByte(strArray[1]);
        this.Width = Convert.ToSingle(strArray[2], (IFormatProvider) new CultureInfo("en-US"));
      }

      /// <summary>строка для записи в настройки</summary>
      /// <returns>строка для настроек "{used? "1":"0"}|{acadIndex}|{width}"</returns>
      public override string ToString()
      {
        return $"{(this.Used ? (object) "1" : (object) "0")}¦{this.AcadIndex.ToString()}¦{this.Width.ToString((IFormatProvider) new CultureInfo("en-US"))}";
      }
    }
}
