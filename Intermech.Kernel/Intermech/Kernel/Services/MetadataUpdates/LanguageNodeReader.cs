// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.LanguageNodeReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class LanguageNodeReader(
  XmlNode node,
  IUserSession userSession,
  IEventLogHelper eHelper,
  string curDirectory,
  IObligatoryObjectsRegistryService obligatoryObjects,
  Guid languageGuid) : NodeReader(node, userSession, eHelper, curDirectory, obligatoryObjects, languageGuid, (IPropertyFactory) new PropertyFactory())
{
  protected override void OnRead(out int categoryID, out object id)
  {
    IDBLanguageType language = this.session.GetLanguage(this.GUID, false);
    if (language == null)
    {
      int num = (int) this.session.GetLanguageCollection().Create(this.propertyFactory.GetPropertyValue<string>("F_LANGUAGE_NAME"), this.GUID, this.propertyFactory.GetPropertyValue<string>("F_CULTURE_ID", string.Empty));
      language = this.session.GetLanguage(this.GUID);
      language.IsDefaultLanguage = this.propertyFactory.GetPropertyValue<bool>("F_DEFAULT", false);
    }
    else
    {
      language.LanguageName = this.propertyFactory.GetObligatoryPropertyValue<string>("F_LANGUAGE_NAME", language.LanguageName);
      language.CultureID = this.propertyFactory.GetObligatoryPropertyValue<string>("F_CULTURE_ID", language.CultureID);
      language.IsDefaultLanguage = this.propertyFactory.GetObligatoryPropertyValue<bool>("F_DEFAULT", language.IsDefaultLanguage);
    }
    categoryID = 9;
    id = (object) language.LanguageID;
  }
}
