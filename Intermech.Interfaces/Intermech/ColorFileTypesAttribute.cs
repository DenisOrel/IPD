
// Type: Intermech.ColorFileTypesAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Drawing;
using System.Globalization;


namespace Intermech
{
    /// <summary>цвет имени файла и(или), цвет имени устаревшего файла</summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ColorFileTypesAttribute : Attribute
    {
      public ColorFileTypesAttribute(string color, string outdated)
      {
        this.Color = ColorFileTypesAttribute.ToColor(color);
        this.Obsolete = ColorFileTypesAttribute.ToColor(outdated);
      }

      public ColorFileTypesAttribute(string color)
        : this(color, color)
      {
      }

      /// <summary>цвет имени файла</summary>
      public Color Color { get; private set; }

      /// <summary>цвет имени устаревшего файла</summary>
      public Color Obsolete { get; private set; }

      private static Color ToColor(string value)
      {
        try
        {
          return value[0] != '#' ? Color.FromName(value.Replace("Color.", "")) : Color.FromArgb((value.Length <= 7 ? -16777216 /*0xFF000000*/ : 0) + int.Parse(value.Substring(1), NumberStyles.HexNumber));
        }
        catch (Exception ex)
        {
        }
        return Color.Black;
      }
    }
}
