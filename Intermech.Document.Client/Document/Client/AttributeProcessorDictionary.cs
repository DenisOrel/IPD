// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.AttributeProcessorDictionary
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.PropertyEditors.AttrProcessor;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Вспомогательный класс для кэша AttributeProcessor</summary>
public class AttributeProcessorDictionary : Dictionary<long, AttributeProcessor>
{
  /// <summary>Initializes a new instance of the Dictionary
  ///     class that is empty, has the default initial capacity, and uses the default
  ///     equality comparer for the key type.
  /// </summary>
  public AttributeProcessorDictionary()
  {
  }

  /// <summary>Initializes a new instance of the Dictionary class that contains elements copied from the
  /// specified IDictionary and uses the default equality comparer for the key type.
  /// </summary>
  /// <param name="dictionary">The Dictionary whose elements are
  /// copied to the new Dictionary.</param>
  /// <Exceptions>
  ///   System.ArgumentException:
  ///     dictionary contains one or more duplicate keys.
  /// 
  ///   System.ArgumentNullException:
  ///     dictionary is null.
  /// </Exceptions>
  public AttributeProcessorDictionary(IDictionary<long, AttributeProcessor> dictionary)
    : base(dictionary)
  {
  }

  /// <summary>Initializes a new instance of the Dictionary
  ///     class that is empty, has the default initial capacity, and uses the specified
  ///     IEqualityComparer.
  /// </summary>
  /// <param name="comparer">The IEqualityComparer implementation to use
  /// when comparing keys, or null to use the default EqualityComparer
  /// for the type of the key.</param>
  public AttributeProcessorDictionary(IEqualityComparer<long> comparer)
    : base(comparer)
  {
  }

  /// <summary>Initializes a new instance of the Dictionary
  ///     class that is empty, has the specified initial capacity, and uses the default
  ///     equality comparer for the key type.
  /// </summary>
  /// <param name="capacity">The initial number of elements that the Dictionary
  /// can contain.</param>
  /// <Exceptions>
  ///   System.ArgumentOutOfRangeException:
  ///     capacity is less than 0.
  /// </Exceptions>
  public AttributeProcessorDictionary(int capacity)
    : base(capacity)
  {
  }

  /// <summary>Initializes a new instance of the Dictionary
  ///     class that contains elements copied from the specified IDictionary
  ///     and uses the specified IEqualityComparer.
  /// </summary>
  /// <param name="dictionary">
  ///     The System.Collections.Generic.IDictionary TKey, TValue whose elements are
  ///     copied to the new System.Collections.Generic.Dictionary TKey,TValue.
  /// </param>
  /// <param name="comparer">
  ///     The System.Collections.Generic.IEqualityComparer T  implementation to use
  ///     when comparing keys, or null to use the default System.Collections.Generic.EqualityComparer T
  ///     for the type of the key.
  /// </param>
  /// <Exceptions>
  ///   System.ArgumentException:
  ///     dictionary contains one or more duplicate keys.
  /// 
  ///   System.ArgumentNullException:
  ///     dictionary is null.
  /// </Exceptions>
  public AttributeProcessorDictionary(
    IDictionary<long, AttributeProcessor> dictionary,
    IEqualityComparer<long> comparer)
    : base(dictionary, comparer)
  {
  }

  /// <summary>Initializes a new instance of the Dictionary
  ///     class that is empty, has the specified initial capacity, and uses the specified
  ///     IEqualityComparer.
  /// </summary>
  /// <param name="capacity">The initial number of elements that the Dictionary can contain.</param>
  /// <param name="comparer">The IEqualityComparer implementation to use
  /// when comparing keys, or null to use the default EqualityComparer
  /// for the type of the key.</param>
  /// <Exceptions>System.ArgumentOutOfRangeException: capacity is less than 0.</Exceptions>
  public AttributeProcessorDictionary(int capacity, IEqualityComparer<long> comparer)
    : base(comparer)
  {
  }

  /// <summary>Initializes a new instance of the Dictionary class with serialized data.</summary>
  /// <param name="info">A SerializationInfo object containing the information
  /// required to serialize the Dictionary.</param>
  /// <param name="context">A StreamingContext structure containing the
  /// source and destination of the serialized stream associated with the Dictionary.</param>
  protected AttributeProcessorDictionary(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
