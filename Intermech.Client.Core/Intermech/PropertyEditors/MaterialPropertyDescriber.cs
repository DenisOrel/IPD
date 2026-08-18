
// Type: Intermech.PropertyEditors.MaterialPropertyDescriber
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.PropertyEditors;


namespace Intermech.PropertyEditors;

/// <summary>Класс для регистрации редактора для атрибута Материал</summary>
public class MaterialPropertyDescriber : AttributablePropertyDescriber
{
  public override object GetPropDescriptorEditor(int attributeId)
  {
    return (object) new MaterialEditor(attributeId);
  }
}
