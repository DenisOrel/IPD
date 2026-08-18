// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.ComboBoxExtension
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual;

internal static class ComboBoxExtension
{
  public static void BindEnumToCombobox<T>(
    [NotNull] this ComboBox comboBox,
    T defaultSelection,
    Func<T, bool> filter = null)
    where T : Enum
  {
    List<KeyValuePair<T, string>> list = Enum.GetValues(typeof (T)).Cast<T>().Select<T, KeyValuePair<T, string>>((Func<T, KeyValuePair<T, string>>) (value => new KeyValuePair<T, string>(value, Attribute.GetCustomAttribute((MemberInfo) value.GetType().GetField(value.ToString()), typeof (DescriptionAttribute)) is DescriptionAttribute customAttribute ? customAttribute.Description : (string) null))).Where<KeyValuePair<T, string>>((Func<KeyValuePair<T, string>, bool>) (item => filter == null || filter(item.Key))).OrderBy<KeyValuePair<T, string>, string>((Func<KeyValuePair<T, string>, string>) (item => item.Value)).ToList<KeyValuePair<T, string>>();
    comboBox.DataSource = (object) list;
    comboBox.DisplayMember = "Value";
    comboBox.ValueMember = "Key";
    foreach (KeyValuePair<T, string> keyValuePair in list)
    {
      if (keyValuePair.Key.ToString() == defaultSelection.ToString())
      {
        comboBox.SelectedItem = (object) keyValuePair;
        break;
      }
    }
  }

  public static void SetSelectedEnumValue<T>([NotNull] this ComboBox comboBox, T selection)
  {
    foreach (object obj in comboBox.Items)
    {
      if (obj is KeyValuePair<T, string> keyValuePair && !(keyValuePair.Key.ToString() != selection.ToString()))
      {
        comboBox.SelectedItem = obj;
        break;
      }
    }
  }
}
