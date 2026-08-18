// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.IAttributePropertyDescriberService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.PropertyEditors;

/// <summary>
/// Служба регистрации обработчиков атрибутов для ObjectPropertyGrid
/// </summary>
public interface IAttributePropertyDescriberService
{
  void RegisterDescriber(
    int attributeId,
    IAttributePropertyDescriber iAttributePropertyDescriber);

  void UnregisterDescriber(int attributeId);

  IAttributePropertyDescriber GetDescriber(int attributeId);
}
