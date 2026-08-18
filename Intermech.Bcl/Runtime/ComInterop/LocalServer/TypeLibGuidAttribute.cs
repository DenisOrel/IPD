
// Type: Intermech.Runtime.ComInterop.LocalServer.TypeLibGuidAttribute
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Этот атрибут используется при создании управляемых COM-объектов. Он позволяет указать идентификатор библиотеки типов, в которой описан
    /// интерфейс COM-объекта. Явное указание библиотеки типов используется в тех случаях, когда модуль расширения IPS использует более одной
    /// библиотеки типов.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class TypeLibGuidAttribute : Attribute
    {
      private readonly Guid typeLibId;
      private readonly Version requiredVersion;

      /// <summary>
      /// Создает атрибут с пустым идентификатором библиотеки типов.
      /// </summary>
      public TypeLibGuidAttribute()
      {
        this.typeLibId = Guid.Empty;
        this.requiredVersion = (Version) null;
      }

      /// <summary>Создает атрибут.</summary>
      /// <param name="typeLibId">Идентификатор бибилиотеки типов, используемой реализуемым COM-объектом</param>
      public TypeLibGuidAttribute(string typeLibId)
      {
        this.typeLibId = new Guid(typeLibId);
        this.requiredVersion = (Version) null;
      }

      /// <summary>Создает атрибут.</summary>
      /// <param name="typeLibId">Идентификатор бибилиотеки типов, используемой реализуемым COM-объектом</param>
      /// <param name="majorVersion">Старший номер версии библиотеки типов</param>
      /// <param name="minorVersion">Младший номер версии библиотеки типов</param>
      public TypeLibGuidAttribute(string typeLibId, int majorVersion, int minorVersion)
      {
        if (majorVersion < 0)
          throw new ArgumentOutOfRangeException(nameof (majorVersion));
        if (minorVersion < 0)
          throw new ArgumentOutOfRangeException(nameof (minorVersion));
        this.typeLibId = new Guid(typeLibId);
        this.requiredVersion = new Version(majorVersion, minorVersion);
      }

      /// <summary>
      /// Возвращает идентификатор библиотеки типов, используемой реализуемым COM-объектом. Может быть равен Guid.Empty, если
      /// COM-объект должен быть зарегистрирован без указания библиотеки типов.
      /// </summary>
      public Guid TypeLibId => this.typeLibId;

      /// <summary>
      /// Возвращает требуемую версию библиотеки типов. Может быть равно null, если проверка существования библиотеки типов
      /// указанной версии не требуется.
      /// </summary>
      public Version RequiredVersion => this.requiredVersion;
    }
}
