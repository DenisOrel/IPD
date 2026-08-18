// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Editor.ConfigurationManager
// Assembly: IMDocumentEditor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 105C08B1-9CA8-4A5F-8603-7439747D5610
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\IMDocumentEditor\IMDocumentEditor.exe

using Intermech.Interfaces.Configuration;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Document.Editor;

public sealed class ConfigurationManager : IConfigurationManager
{
  private ConfigurationCollection _configurations;
  private string _appName;

  public event ConfigurationLoadedEventHandler ConfigurationLoaded;

  public event ConfigurationBeforeSaveEventHandler ConfigurationBeforeSave;

  public ConfigurationManager(string appName)
  {
    this._configurations = new ConfigurationCollection();
    if (appName == string.Empty)
      this._appName = this.GetType().Assembly.GetName().Name;
    else
      this._appName = appName;
  }

  public IConfiguration Create(string name)
  {
    this.Delete(name);
    IConfiguration configuration = (IConfiguration) new Intermech.Document.Editor.Configuration(name);
    this._configurations.Add(configuration);
    return configuration;
  }

  public void Delete(string name)
  {
    for (int index = this._configurations.Count - 1; index >= 0; --index)
    {
      if (this._configurations[index].Name == name)
        this._configurations.RemoveAt(index);
    }
  }

  private void OnException(string message, Exception e)
  {
    int num = (int) MessageBox.Show($"{message}\n{e.Message}");
  }

  public void Load(Stream stream)
  {
    try
    {
      stream.Position = 0L;
      XmlTextReader reader = new XmlTextReader(stream);
      if (reader.IsEmptyElement)
      {
        if (reader.IsStartElement(this._appName))
          reader.ReadStartElement(this._appName);
      }
      else if (reader.IsStartElement(this._appName))
      {
        reader.ReadStartElement(this._appName);
        while (reader.IsStartElement())
          this.ReadConfiguration(this.Create(reader.LocalName), reader);
        if (!reader.EOF)
          reader.ReadEndElement();
      }
      reader.Close();
      this.OnConfigurationLoaded();
    }
    catch (XmlException ex)
    {
      this.OnException("Invalid XML configuration file.", (Exception) ex);
    }
  }

  private void OnConfigurationLoaded()
  {
    ConfigurationLoadedEventHandler configurationLoaded = this.ConfigurationLoaded;
    if (configurationLoaded == null)
      return;
    configurationLoaded((IConfigurationManager) this);
  }

  private void OnConfigurationBeforeSave()
  {
    ConfigurationBeforeSaveEventHandler configurationBeforeSave = this.ConfigurationBeforeSave;
    if (configurationBeforeSave == null)
      return;
    configurationBeforeSave((IConfigurationManager) this);
  }

  public IConfiguration Open(string name)
  {
    foreach (IConfiguration configuration in this._configurations)
    {
      if (name == configuration.Name)
        return configuration;
    }
    return (IConfiguration) null;
  }

  private IConfiguration ReadConfiguration(IConfiguration configuration, XmlTextReader reader)
  {
    if (reader.IsEmptyElement)
    {
      this.ReadProperties(reader, configuration);
      reader.ReadStartElement();
    }
    else
    {
      this.ReadProperties(reader, configuration);
      reader.ReadStartElement();
      while (reader.IsStartElement())
        this.ReadConfiguration(configuration.Add(reader.LocalName), reader);
      reader.ReadEndElement();
    }
    return configuration;
  }

  private void ReadProperties(XmlTextReader reader, IConfiguration configuration)
  {
    if (!reader.HasAttributes)
      return;
    for (int i = 0; i < reader.AttributeCount; ++i)
    {
      reader.MoveToAttribute(i);
      configuration.SetProperty(reader.LocalName, reader.Value);
    }
  }

  public void Save(Stream stream)
  {
    this.OnConfigurationBeforeSave();
    XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
    writer.Formatting = Formatting.Indented;
    writer.WriteStartDocument();
    writer.WriteStartElement(this._appName);
    foreach (IConfiguration configuration in (IEnumerable) this.Configurations)
      this.WriteConfiguration(writer, configuration);
    writer.WriteEndElement();
    writer.WriteEndDocument();
    writer.Flush();
  }

  private void WriteConfiguration(XmlTextWriter writer, IConfiguration configuration)
  {
    writer.WriteStartElement(configuration.Name);
    foreach (IConfigurationProperty property in configuration.Properties)
      writer.WriteAttributeString(property.Name, property.Value);
    foreach (IConfiguration configuration1 in (IEnumerable) configuration.Configurations)
      this.WriteConfiguration(writer, configuration1);
    writer.WriteEndElement();
  }

  public IConfigurationCollection Configurations => (IConfigurationCollection) this._configurations;

  public void Clear() => this._configurations.Clear();
}
