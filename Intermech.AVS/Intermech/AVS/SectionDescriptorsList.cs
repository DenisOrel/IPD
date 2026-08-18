// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SectionDescriptorsList
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary> Список дескрипторов разделов конструкторской документации (спецификации, ведомости) </summary>
public class SectionDescriptorsList : List<SectionDescriptor>
{
  /// <summary> Конструктор по-умолчанию </summary>
  public SectionDescriptorsList()
  {
  }

  /// <summary> Конструктор по-умолчанию </summary>
  public SectionDescriptorsList(int capacity)
    : base(capacity)
  {
  }

  /// <summary> Добавить новый дескриптор в список </summary>
  public SectionDescriptor AddNew(long id, string caption)
  {
    SectionDescriptor sectionDescriptor = new SectionDescriptor(this, id, caption);
    this.Add(sectionDescriptor);
    return sectionDescriptor;
  }
}
