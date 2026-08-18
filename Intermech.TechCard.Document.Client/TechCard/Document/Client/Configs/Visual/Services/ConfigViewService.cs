// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.Services.ConfigViewService
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.Services;

internal class ConfigViewService : IConfigViewService
{
  private readonly Dictionary<DocumentConfigElementType, Type> _registeredViewControllerTypes = new Dictionary<DocumentConfigElementType, Type>();

  private void InitViewControllers()
  {
    ((IEnumerable<Type>) Assembly.GetExecutingAssembly().GetTypes()).ToList<Type>().ForEach((Action<Type>) (type =>
    {
      IEnumerable<DocumentConfigElementEditorAttribute> customAttributes = type.GetCustomAttributes<DocumentConfigElementEditorAttribute>();
      if (!customAttributes.Any<DocumentConfigElementEditorAttribute>())
        return;
      foreach (DocumentConfigElementEditorAttribute elementEditorAttribute in customAttributes)
        this._registeredViewControllerTypes[elementEditorAttribute.ConfigElementType] = type;
    }));
  }

  public ConfigViewService() => this.InitViewControllers();

  public IConfigViewController CreateViewController(
    DocumentConfigElementType configType,
    IServiceProvider services)
  {
    Type type;
    if (this._registeredViewControllerTypes.TryGetValue(configType, out type))
    {
      if (Activator.CreateInstance(type, (object) services) is IConfigViewController instance)
        return instance;
    }
    return (IConfigViewController) null;
  }
}
