
// Type: Intermech.PropertyEditors.IPossibleValuesHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Data;


namespace Intermech.PropertyEditors;

/// <summary>
/// для получения списка допустимых значений атрибута в контекстах объектов и связей // ObjectPropDescriptorHolder
/// </summary>
public interface IPossibleValuesHolder
{
  DataTable GetPossibleValues(ITypeDescriptorContext context);
}
