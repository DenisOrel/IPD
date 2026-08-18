
// Type: Intermech.Search.Configuration.ConfigurationOptionRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;


namespace Intermech.Search.Configuration
{
    public sealed class ConfigurationOptionRepository : IConfigurationOptionRepository
    {
      public event EventHandler<ConfigurationOptionChangedEventArgs> OptionChanged;

      public object Find(ConfigurationOptionKey optionKey, DBConfigMode? mode = null)
      {
        ConfigurationOptionInfo optionInfo = !(optionKey == (ConfigurationOptionKey) null) ? ServiceLocator.Get<IConfigurationOptionInfoProvider>().Get(optionKey) : throw new ArgumentNullException(nameof (optionKey));
        if (!mode.HasValue)
          mode = new DBConfigMode?(optionInfo.Mode);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBConfigurations configurations = sessionKeeper.Session.Configurations;
          if (optionInfo.Type == typeof (bool))
            return this.LoadBool(configurations, optionInfo, mode.Value);
          if (optionInfo.Type == typeof (DateTime))
            return this.LoadDateTime(configurations, optionInfo, mode.Value);
          if (optionInfo.Type == typeof (double))
            return this.LoadDouble(configurations, optionInfo, mode.Value);
          if (optionInfo.Type == typeof (long))
            return this.LoadInteger(configurations, optionInfo, mode.Value);
          if (optionInfo.Type == typeof (string))
            return this.LoadString(configurations, optionInfo, mode.Value);
          if (optionInfo.Type == typeof (Font))
            return this.LoadFont(configurations, optionInfo, mode.Value);
          if (optionInfo.Type == typeof (List<int>))
            return this.LoadList(configurations, optionInfo, mode.Value);
          return optionInfo.Type == typeof (long[]) ? this.LoadInt64Array(configurations, optionInfo, mode.Value) : (object) null;
        }
      }

      public void AddOrUpdate(ConfigurationOptionKey optionKey, object optionValue, DBConfigMode? mode = null)
      {
        ConfigurationOptionInfo optionInfo = !(optionKey == (ConfigurationOptionKey) null) ? ServiceLocator.Get<IConfigurationOptionInfoProvider>().Get(optionKey) : throw new ArgumentNullException("key");
        if (!mode.HasValue)
          mode = new DBConfigMode?(optionInfo.Mode);
        if (optionValue == null)
          optionValue = optionInfo.DefaultValue;
        if (optionValue == null && optionInfo.Type.IsValueType)
          optionValue = Activator.CreateInstance(optionInfo.Type);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          IDBConfigurations configurations = session.Configurations;
          if (optionInfo.Type == typeof (bool))
            this.SaveBool(session, configurations, optionInfo, mode.Value, (bool) optionValue);
          else if (optionInfo.Type == typeof (DateTime))
            this.SaveDateTime(session, configurations, optionInfo, mode.Value, (DateTime) optionValue);
          else if (optionInfo.Type == typeof (double))
            this.SaveDouble(session, configurations, optionInfo, mode.Value, (double) optionValue);
          else if (optionInfo.Type == typeof (long))
            this.SaveLong(session, configurations, optionInfo, mode.Value, (long) optionValue);
          else if (optionInfo.Type == typeof (string))
            this.SaveString(session, configurations, optionInfo, mode.Value, optionValue as string);
          else if (optionInfo.Type == typeof (Font))
            this.SaveFont(session, configurations, optionInfo, mode.Value, optionValue as Font);
          else if (optionInfo.Type == typeof (List<int>))
            this.SaveList(session, configurations, optionInfo, mode.Value, optionValue as List<int>);
          else if (optionInfo.Type == typeof (long[]))
            this.SaveInt64Array(session, configurations, optionInfo, mode.Value, optionValue as long[]);
        }
        EventHandler<ConfigurationOptionChangedEventArgs> optionChanged = this.OptionChanged;
        if (optionChanged == null)
          return;
        optionChanged((object) this, new ConfigurationOptionChangedEventArgs(optionKey, optionValue));
      }

      private object LoadString(
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode)
      {
        return (object) configurations.ReadString(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, (string) optionInfo.DefaultValue, mode);
      }

      private object LoadString(
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        string defaultValue,
        DBConfigMode mode)
      {
        return (object) configurations.ReadString(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, defaultValue, mode);
      }

      private object LoadInteger(
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode)
      {
        return (object) configurations.ReadInteger(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, (long) optionInfo.DefaultValue, mode);
      }

      private object LoadDouble(
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode)
      {
        return (object) configurations.ReadDouble(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, (double) optionInfo.DefaultValue, mode);
      }

      private object LoadDateTime(
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode)
      {
        return (object) configurations.ReadDateTime(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, (DateTime) optionInfo.DefaultValue, mode);
      }

      private object LoadBool(
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode)
      {
        return (object) configurations.ReadBool(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, (bool) optionInfo.DefaultValue, mode);
      }

      private object LoadFont(
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode)
      {
        string str = this.LoadString(configurations, optionInfo, (string) null, mode) as string;
        if (string.IsNullOrEmpty(str))
          return (object) null;
        try
        {
          string[] strArray = str.Split('|');
          string familyName = strArray[0];
          float single = Convert.ToSingle(strArray[1].Replace(",", "."), (IFormatProvider) CultureInfo.InvariantCulture);
          FontStyle fontStyle = (FontStyle) Enum.Parse(typeof (FontStyle), strArray[2]);
          GraphicsUnit graphicsUnit = (GraphicsUnit) Enum.Parse(typeof (GraphicsUnit), strArray[3]);
          byte num1 = Convert.ToByte(strArray[4]);
          bool boolean = Convert.ToBoolean(strArray[5]);
          double emSize = (double) single;
          int style = (int) fontStyle;
          int unit = (int) graphicsUnit;
          int gdiCharSet = (int) num1;
          int num2 = boolean ? 1 : 0;
          return (object) new Font(familyName, (float) emSize, (FontStyle) style, (GraphicsUnit) unit, (byte) gdiCharSet, num2 != 0);
        }
        catch
        {
          return (object) null;
        }
      }

      private object LoadList(
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode)
      {
        if (!(this.LoadString(configurations, optionInfo, (string) null, mode) is string str))
          return optionInfo.DefaultValue ?? (object) new List<int>(0);
        if (str == string.Empty)
          return (object) new List<int>(0);
        return (object) ((IEnumerable<string>) str.Split('|')).Select<string, int>((Func<string, int>) (o => Convert.ToInt32(o))).ToList<int>();
      }

      private object LoadInt64Array(
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode)
      {
        string str = this.LoadString(configurations, optionInfo, (string) null, mode) as string;
        if (string.IsNullOrEmpty(str))
          return optionInfo.DefaultValue ?? (object) new long[0];
        return (object) ((IEnumerable<string>) str.Split('|')).Select<string, long>((Func<string, long>) (o => Convert.ToInt64(o))).ToArray<long>();
      }

      private void SaveString(
        IUserSession userSession,
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode,
        string value)
      {
        if (value == null)
          value = "";
        if (this.IsUserMode(mode))
        {
          configurations.WriteString(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, value, userSession.UserID);
        }
        else
        {
          if (!this.IsGlobalMode(mode))
            return;
          configurations.WriteString(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, value, 0L);
        }
      }

      private void SaveLong(
        IUserSession userSession,
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode,
        long value)
      {
        if (this.IsUserMode(mode))
        {
          configurations.WriteInteger(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, value, userSession.UserID);
        }
        else
        {
          if (!this.IsGlobalMode(mode))
            return;
          configurations.WriteInteger(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, value, 0L);
        }
      }

      private void SaveDouble(
        IUserSession userSession,
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode,
        double value)
      {
        if (this.IsUserMode(mode))
        {
          configurations.WriteDouble(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, value, userSession.UserID);
        }
        else
        {
          if (!this.IsGlobalMode(mode))
            return;
          configurations.WriteDouble(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, value, 0L);
        }
      }

      private void SaveDateTime(
        IUserSession userSession,
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode,
        DateTime value)
      {
        if (this.IsUserMode(mode))
        {
          configurations.WriteDateTime(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, value, userSession.UserID);
        }
        else
        {
          if (!this.IsGlobalMode(mode))
            return;
          configurations.WriteDateTime(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, value, 0L);
        }
      }

      private void SaveBool(
        IUserSession userSession,
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode,
        bool value)
      {
        if (this.IsUserMode(mode))
        {
          configurations.WriteBool(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, value, userSession.UserID);
        }
        else
        {
          if (!this.IsGlobalMode(mode))
            return;
          configurations.WriteBool(optionInfo.Key.Module, optionInfo.Key.Section, optionInfo.Key.Name, value, 0L);
        }
      }

      private void SaveFont(
        IUserSession userSession,
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode,
        Font font)
      {
        if (font == null)
        {
          this.SaveString(userSession, configurations, optionInfo, mode, "");
        }
        else
        {
          string str = $"{font.FontFamily.Name}|{font.Size.ToString((IFormatProvider) CultureInfo.InvariantCulture)}|{(object) font.Style}|{(object) font.Unit}|{(object) font.GdiCharSet}|{font.GdiVerticalFont.ToString()}";
          this.SaveString(userSession, configurations, optionInfo, mode, str);
        }
      }

      private void SaveList(
        IUserSession userSession,
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode,
        List<int> list)
      {
        if (list == null)
        {
          this.SaveString(userSession, configurations, optionInfo, mode, "");
        }
        else
        {
          string str = string.Join<int>("|", (IEnumerable<int>) list);
          this.SaveString(userSession, configurations, optionInfo, mode, str);
        }
      }

      private void SaveInt64Array(
        IUserSession userSession,
        IDBConfigurations configurations,
        ConfigurationOptionInfo optionInfo,
        DBConfigMode mode,
        long[] list)
      {
        if (list == null)
        {
          this.SaveString(userSession, configurations, optionInfo, mode, "");
        }
        else
        {
          string str = string.Join<long>("|", (IEnumerable<long>) list);
          this.SaveString(userSession, configurations, optionInfo, mode, str);
        }
      }

      private bool IsUserMode(DBConfigMode mode)
      {
        return mode == DBConfigMode.GlobalAndUser || mode == DBConfigMode.UserAndGlobal || mode == DBConfigMode.UserOnly;
      }

      private bool IsGlobalMode(DBConfigMode mode) => mode == DBConfigMode.GlobalOnly;
    }
}
