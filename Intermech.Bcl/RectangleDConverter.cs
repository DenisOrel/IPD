
// Type: Intermech.RectangleDConverter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech
{
    /// <summary>Преобразует объект RectangleD из одного типа данных в другой. Этот класс доступен с помощью объекта TypeDescriptor.</summary>
    public class RectangleDConverter : TypeConverter
    {
      /// <summary>Определяет, может ли конвертер преобразовывать заданный тип источника объекта в собственный тип.</summary>
      /// <param name="context">Контекст средства форматирования. Данный объект используется для получения дополнительных сведений о среде, из которой вызывается этот конвертер. Значением параметра может быть пустая ссылка (Nothing в Visual Basic), поэтому всегда следует выполнять проверку. Свойства объекта контекста также могут возвращать значение пустой ссылке (Nothing).</param>
      /// <param name="sourceType">Тип, из которого требуется сделать преобразование.</param>
      /// <returns>Метод возвращает значение true, если объект может быть преобразован; в противном случае — false.</returns>
      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
      {
        return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
      }

      /// <summary>Получает значение, показывающее, может ли данный конвертер преобразовать тип объекта с помощью контекста в конечный тип.</summary>
      /// <param name="context">Объект ITypeDescriptorContext, предоставляющий контекст формата.</param>
      /// <param name="destinationType">Объект Type, который представляет нужный результат конвертирования.</param>
      /// <returns>Метод возвращает значение true, если конвертер может выполнить преобразование; в противном случае — false.</returns>
      /// <remarks>Данный параметр context используется для получения дополнительных сведений о среде, из которой вызывается этот конвертер. Это может быть пустая ссылка (Nothing в Visual Basic), поэтому всегда следует выполнять проверку. Свойства объекта контекста также могут возвращать пустую ссылку (Nothing). </remarks>
      public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
      {
        return destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
      }

      /// <summary>Преобразовывает указанный объект в объект RectangleD. </summary>
      /// <param name="context">Контекст средства форматирования. Данный объект используется для получения дополнительных сведений о среде, из которой вызывается этот конвертер. Значением параметра может быть пустая ссылка (Nothing в Visual Basic), поэтому всегда следует выполнять проверку. Свойства объекта контекста также могут возвращать значение пустой ссылке (Nothing).</param>
      /// <param name="culture">Объект, содержащий сведения о культурной среде, например язык, календарь и культурные соглашения, связанные с конкретной культурной средой. На основании стандарта RFC 1766.</param>
      /// <param name="value">Преобразуемый объект.</param>
      /// <returns>Преобразованный объект. Если конвертация не может быть выполнена, генерирует исключение.</returns>
      /// <exception cref="T:System.ArgumentException">Преобразование не может быть выполнено.</exception>
      public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
      {
        if (!(value is string))
          return base.ConvertFrom(context, culture, value);
        string str = ((string) value).Trim();
        if (str.Length == 0)
          return (object) null;
        if (culture == null)
          culture = CultureInfo.CurrentCulture;
        char ch = culture.TextInfo.ListSeparator[0];
        string[] strArray = str.Split(ch);
        double[] numArray = new double[strArray.Length];
        TypeConverter converter = TypeDescriptor.GetConverter(typeof (double));
        for (int index = 0; index < numArray.Length; ++index)
          numArray[index] = (double) converter.ConvertFromString(context, culture, strArray[index]);
        return numArray.Length == 4 ? (object) new RectangleD(numArray[0], numArray[1], numArray[2], numArray[3]) : throw new ArgumentException("Failed to parse text. Expected text in the format: x, y, width, height.", nameof (value));
      }

      /// <summary>Преобразовывает указанный объект в указанный тип.</summary>
      /// <param name="context">Контекст средства форматирования. Данный объект используется для получения дополнительных сведений о среде, из которой вызывается этот конвертер. Значением параметра может быть пустая ссылка (Nothing в Visual Basic), поэтому всегда следует выполнять проверку. Свойства объекта контекста также могут возвращать значение пустой ссылке (Nothing).</param>
      /// <param name="culture">Объект, содержащий сведения о культурной среде, например язык, календарь и культурные соглашения, связанные с конкретной культурной средой. It is based on the RFC 1766 standard.</param>
      /// <param name="value">Преобразуемый объект.</param>
      /// <param name="destinationType">Тип, в который требуется преобразовать объект.</param>
      /// <returns>Преобразованный объект.</returns>
      /// <remarks>Наиболее распространенный тип, в который и из которого преобразуют объекты, — строка. Реализация по умолчанию вызывает метод ToString объекта, если объект действителен и конечный тип — строка. Если этот метод не может преобразовать тип объекта в конечный тип, генерирует исключение ArgumentException.</remarks>
      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        if (destinationType != typeof (string) || !(value is RectangleD))
          return base.ConvertTo(context, culture, value, destinationType);
        if (culture == null)
          culture = CultureInfo.CurrentCulture;
        RectangleD rectangleD = (RectangleD) value;
        string separator = culture.TextInfo.ListSeparator + " ";
        TypeConverter converter = TypeDescriptor.GetConverter(typeof (double));
        string[] strArray = new string[4]
        {
          converter.ConvertToString(context, culture, (object) rectangleD.X),
          converter.ConvertToString(context, culture, (object) rectangleD.Y),
          converter.ConvertToString(context, culture, (object) rectangleD.Width),
          converter.ConvertToString(context, culture, (object) rectangleD.Height)
        };
        return (object) string.Join(separator, strArray);
      }
    }
}
