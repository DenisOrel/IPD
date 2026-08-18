
// Type: Intermech.Interfaces.Configuration.ConfigurationManager
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Interfaces.Configuration
{
    public class ConfigurationManager : IPersistableConfigurationManager, IConfigurationManager
    {
      private ConfigurationCollection _configurations;
      private string _appName;

      public event ConfigurationLoadedEventHandler ConfigurationLoaded;

      public event ConfigurationBeforeSaveEventHandler ConfigurationBeforeSave;

      /// <summary>
      /// Событие, которое вызывается в процессе загрузки из stream каждого свойства конфигурации.
      /// Оно позволяет корректировать, преобразовывать или игнорировать значения свойств.
      /// </summary>
      public event EventHandler<ConfigurationPropertyLoadingEventArgs> PropertyLoading;

      /// <summary>
      /// Событие, которое вызывается после успешной загрузки из stream каждого свойства конфигурации.
      /// </summary>
      public event EventHandler<ConfigurationPropertyEventArgs> PropertyLoaded;

      public ConfigurationManager(string appName)
      {
        this._configurations = new ConfigurationCollection();
        if (string.IsNullOrEmpty(appName))
          this._appName = this.GetType().Assembly.GetName().Name;
        else
          this._appName = appName;
      }

      public IConfiguration Create(string name)
      {
        this.Delete(name);
        IConfiguration configuration = (IConfiguration) new Intermech.Interfaces.Configuration.Configuration(name);
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
        }
      }

      private void OnConfigurationLoaded()
      {
        if (this.ConfigurationLoaded == null)
          return;
        this.ConfigurationLoaded((IConfigurationManager) this);
      }

      private void OnConfigurationBeforeSave()
      {
        if (this.ConfigurationBeforeSave == null)
          return;
        this.ConfigurationBeforeSave((IConfigurationManager) this);
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
          string str = reader.LocalName;
          string propertyValue = reader.Value;
          if (this.PropertyLoading != null)
          {
            ConfigurationPropertyLoadingEventArgs e = new ConfigurationPropertyLoadingEventArgs(configuration, str, propertyValue);
            this.PropertyLoading((object) this, e);
            if (e.CanAdd)
            {
              if (e.Value != propertyValue)
                propertyValue = e.Value;
            }
            else
            {
              str = (string) null;
              propertyValue = (string) null;
            }
          }
          if (str != null)
          {
            configuration.SetProperty(str, propertyValue);
            if (this.PropertyLoaded != null)
              this.PropertyLoaded((object) this, new ConfigurationPropertyEventArgs(configuration, str, propertyValue));
          }
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
}
