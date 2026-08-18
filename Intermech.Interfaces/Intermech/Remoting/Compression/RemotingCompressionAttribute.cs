
// Type: Intermech.Remoting.Compression.RemotingCompressionAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Remoting.Compression
{
    /// <summary>
    /// Позволяет управлять сжатием сетевого трафика remoting на уровне отдельных методов, свойств или целых типов.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Interface, Inherited = true)]
    public class RemotingCompressionAttribute : Attribute
    {
      private readonly bool enableCompression;

      /// <summary>Создает атрибут.</summary>
      /// <param name="enableCompression">Признак, разрешающий или запрещающий сжатие сетевого трафика для метода, свойства или целого типа</param>
      public RemotingCompressionAttribute(bool enableCompression)
      {
        this.enableCompression = enableCompression;
      }

      /// <summary>
      /// Возвращает признак, разрешающий или запрещающий сжатие сетевого трафика для метода, свойства или целого типа.
      /// </summary>
      public bool EnableCompression => this.enableCompression;
    }
}
