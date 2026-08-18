// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.UI.FileNameBasedTypeSelector
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Mvp;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Tools.UI;

public sealed class FileNameBasedTypeSelector
{
  private IEnumerable<LocalId<int>> typeFilter;
  private static readonly List<FileNameRule> rules = new List<FileNameRule>();
  private static readonly IComparer<FileNameRule> ruleComparer = (IComparer<FileNameRule>) new FileNameBasedTypeSelector.RelevanceComparer();
  private static readonly object permTableKey = new object();

  public LocalId<int> Select(string filePath)
  {
    UICommandInfo cmd = UIVars.UICommand.Value;
    if (cmd == null)
      return this.VisualSelect();
    Dictionary<FileNameRule, bool> permissionsTable = this.GetPermissionsTable(cmd);
    List<FileNameRule> rules = FileNameBasedTypeSelector.FindRules((Predicate<FileNameRule>) (rule => rule.IsMatch(filePath) && this.InTypeFilter(rule.ObjectType)));
    int index = 0;
    bool flag1 = true;
    while (index < rules.Count)
    {
      FileNameRule fileNameRule = rules[index];
      bool flag2;
      if (permissionsTable.TryGetValue(fileNameRule, out flag2))
      {
        if (flag2)
          return fileNameRule.ObjectType;
        ++index;
      }
      else
      {
        ApplyFileNameRulePresenter nameRulePresenter1 = new ApplyFileNameRulePresenter();
        nameRulePresenter1.Rule = fileNameRule;
        MvpContext.ViewService.ShowModal((IPresenter) nameRulePresenter1);
        if (!nameRulePresenter1.Success)
          throw new AbortException();
        switch (nameRulePresenter1.UserAnswer)
        {
          case FileNameRuleAction.AllowForAll:
            permissionsTable.Add(fileNameRule, true);
            return fileNameRule.ObjectType;
          case FileNameRuleAction.AllowForCurrent:
            return fileNameRule.ObjectType;
          case FileNameRuleAction.DenyForCurrent:
            flag1 = false;
            break;
          case FileNameRuleAction.DenyForAll:
            permissionsTable.Add(fileNameRule, true);
            break;
          case FileNameRuleAction.EditRuleAndRecheck:
            EditFileNameRulePresenter nameRulePresenter2 = new EditFileNameRulePresenter();
            nameRulePresenter2.Rule = fileNameRule;
            MvpContext.ViewService.ShowModal((IPresenter) nameRulePresenter2);
            if (nameRulePresenter2.Success)
            {
              FileNameBasedTypeSelector.ReplaceRule(fileNameRule, nameRulePresenter2.Rule);
              if (nameRulePresenter2.Rule.IsMatch(filePath))
                return nameRulePresenter2.Rule.ObjectType;
              rules = FileNameBasedTypeSelector.FindRules((Predicate<FileNameRule>) (rule => rule.IsMatch(filePath)));
              index = -1;
              break;
            }
            break;
        }
        ++index;
      }
    }
    LocalId<int> objectType = this.VisualSelect();
    if (flag1)
      FileNameBasedTypeSelector.AddRule(new FileNameRule(Path.GetExtension(filePath), objectType));
    return objectType;
  }

  private Dictionary<FileNameRule, bool> GetPermissionsTable(UICommandInfo cmd)
  {
    object permissionsTable;
    if (!cmd.Tags.TryGetValue(FileNameBasedTypeSelector.permTableKey, out permissionsTable))
    {
      permissionsTable = (object) new Dictionary<FileNameRule, bool>();
      cmd.Tags.Add(FileNameBasedTypeSelector.permTableKey, permissionsTable);
    }
    return (Dictionary<FileNameRule, bool>) permissionsTable;
  }

  private LocalId<int> VisualSelect()
  {
    if (this.OnVisualSelect != null)
    {
      SelectObjectTypeArgs e = new SelectObjectTypeArgs();
      this.OnVisualSelect((object) this, e);
      if (e.ObjectType != null && this.InTypeFilter(e.ObjectType))
        return e.ObjectType;
    }
    throw new AbortException();
  }

  private bool InTypeFilter(LocalId<int> objectType)
  {
    return this.typeFilter == null || CollectionUtils.Exists<LocalId<int>>(this.typeFilter, (Predicate<LocalId<int>>) (filterItem => filterItem == objectType));
  }

  public IEnumerable<LocalId<int>> TypeFilter
  {
    get => this.typeFilter;
    set => this.typeFilter = value;
  }

  public event EventHandler<SelectObjectTypeArgs> OnVisualSelect;

  private static List<FileNameRule> FindRules(Predicate<FileNameRule> match)
  {
    lock (FileNameBasedTypeSelector.rules)
      return FileNameBasedTypeSelector.rules.FindAll(match);
  }

  private static void AddRule(FileNameRule newRule)
  {
    lock (FileNameBasedTypeSelector.rules)
    {
      int index = FileNameBasedTypeSelector.rules.BinarySearch(newRule, FileNameBasedTypeSelector.ruleComparer);
      if (index >= 0)
      {
        if (newRule.ObjectType == FileNameBasedTypeSelector.rules[index].ObjectType)
          return;
        FileNameBasedTypeSelector.rules[index] = newRule;
      }
      else
        FileNameBasedTypeSelector.rules.Insert(~index, newRule);
    }
  }

  private static void ReplaceRule(FileNameRule oldRule, FileNameRule newRule)
  {
    lock (FileNameBasedTypeSelector.rules)
    {
      int index = FileNameBasedTypeSelector.rules.BinarySearch(oldRule, FileNameBasedTypeSelector.ruleComparer);
      if (index >= 0)
        FileNameBasedTypeSelector.rules.RemoveAt(index);
      FileNameBasedTypeSelector.AddRule(newRule);
    }
  }

  private sealed class RelevanceComparer : IComparer<FileNameRule>
  {
    private readonly PathComparer nameComparer = new PathComparer();

    public int Compare(FileNameRule x, FileNameRule y)
    {
      if (x == null)
        throw new ArgumentNullException(nameof (x));
      if (y == null)
        throw new ArgumentNullException(nameof (y));
      int num1 = this.nameComparer.Compare(y.Extension, x.Extension);
      if (num1 != 0)
        return num1;
      int num2 = this.nameComparer.Compare(y.NamePattern, x.NamePattern);
      return num2 != 0 ? num2 : this.ComparePath(x, y);
    }

    private int ComparePath(FileNameRule x, FileNameRule y)
    {
      switch ((y.Directory == null || !Path.IsPathRooted(y.Directory) ? 0 : 2) + (x.Directory == null || !Path.IsPathRooted(x.Directory) ? 0 : 1))
      {
        case 0:
          return 0;
        case 1:
          return -1;
        case 2:
          return 1;
        case 3:
          if (PathUtils.IsPlacedIn(y.Directory, x.Directory))
            return 1;
          return PathUtils.IsPlacedIn(x.Directory, y.Directory) ? -1 : this.nameComparer.Compare(y.Directory, x.Directory);
        default:
          throw new InvalidOperationException();
      }
    }
  }
}
