
// Type: Intermech.Search.Discussions.DiscussionParser
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Intermech.Search.Discussions
{
    public sealed class DiscussionParser
    {
      private long _discussionVersionID;
      private DiscussionParser.Scanner _scanner;

      public MessageDto[] Parse(long discussionVersionID, string text)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(discussionVersionID))
          throw new ArgumentException();
        if (string.IsNullOrEmpty(text))
          throw new ArgumentException();
        this._discussionVersionID = discussionVersionID;
        this._scanner = new DiscussionParser.Scanner(text);
        return this.ParseMessages();
      }

      private MessageDto[] ParseMessages()
      {
        List<MessageDto> messageDtoList = new List<MessageDto>();
        if (this._scanner.CurrentToken == DiscussionParser.Token.Text && string.IsNullOrEmpty(this._scanner.CurrentText))
          this._scanner.MoveNext();
        while (this._scanner.CurrentToken != DiscussionParser.Token.Eot)
        {
          try
          {
            messageDtoList.Add(this.ParseMessage());
          }
          catch
          {
          }
        }
        return messageDtoList.ToArray();
      }

      private MessageDto ParseMessage()
      {
        DateTime? nullable = new DateTime?();
        if (this._scanner.CurrentToken == DiscussionParser.Token.DateTime)
        {
          nullable = this._scanner.CurrentDateTime;
          this._scanner.MoveNext();
        }
        Guid? currentGuid = this._scanner.CurrentGuid;
        this._scanner.MoveNext();
        DateTime? currentDateTime = this._scanner.CurrentDateTime;
        this._scanner.MoveNext();
        MessageDto message;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          message = new MessageDto();
          message.Id = new MessageIdDto()
          {
            AuthorVersionGuid = currentGuid.Value,
            CreationTimestamp = currentDateTime.Value,
            DiscussionVersionId = this._discussionVersionID
          };
          if (nullable.HasValue)
            message.LastModificationTimestamp = new DateTime?(nullable.Value);
          IDBObject dbObject = sessionKeeper.Session.GetObject(currentGuid.Value, false);
          if (dbObject != null)
            message.AuthorName = dbObject.Caption;
        }
        message.Caption = this._scanner.CurrentText;
        this._scanner.MoveNext();
        StringBuilder stringBuilder = new StringBuilder();
        do
        {
          stringBuilder.Append(this._scanner.CurrentText);
          this._scanner.MoveNext();
        }
        while ((this._scanner.CurrentToken != DiscussionParser.Token.Guid || this._scanner.NextToken != DiscussionParser.Token.DateTime || this._scanner.Next2Token != DiscussionParser.Token.Text || this._scanner.Next3Token != DiscussionParser.Token.Text) && (this._scanner.CurrentToken != DiscussionParser.Token.DateTime || this._scanner.NextToken != DiscussionParser.Token.Guid || this._scanner.Next2Token != DiscussionParser.Token.DateTime || this._scanner.Next3Token != DiscussionParser.Token.Text || this._scanner.Next4Token != DiscussionParser.Token.Text) && this._scanner.CurrentToken != DiscussionParser.Token.Users && this._scanner.CurrentToken != DiscussionParser.Token.Eot);
        message.Text = stringBuilder.ToString();
        if (this._scanner.CurrentToken == DiscussionParser.Token.Users)
        {
          message.CuriousUsers = this._scanner.CurrentUsers;
          this._scanner.MoveNext();
        }
        return message;
      }

      private enum Token
      {
        None,
        DateTime,
        Eot,
        Guid,
        Text,
        Users,
      }

      private sealed class Scanner
      {
        private static readonly Regex DateTimeRegex = new Regex("^([0-9]{1,4})[^0-9]([0-9]{1,2})[^0-9]([0-9]{1,4})[^0-9]([0-9]{1,2})[^0-9]([0-9]{1,2})[^0-9]([0-9]{1,2})[^0-9]?([AP]M)?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Tuple<DiscussionParser.Token, DateTime?, Guid?, Guid[], string> EotTuple = new Tuple<DiscussionParser.Token, DateTime?, Guid?, Guid[], string>(DiscussionParser.Token.Eot, new DateTime?(), new Guid?(), (Guid[]) null, (string) null);
        private int _currentPosition;
        private int _nextPosition = -1;
        private readonly string[] _textParts;
        private List<Tuple<DiscussionParser.Token, DateTime?, Guid?, Guid[], string>> _tuples = new List<Tuple<DiscussionParser.Token, DateTime?, Guid?, Guid[], string>>();

        public Scanner(string text)
        {
          this._textParts = !string.IsNullOrEmpty(text) ? text.Split('|') : throw new ArgumentNullException(nameof (text));
          this.AddNextTuple();
          this.AddNextTuple();
          this.AddNextTuple();
          this.AddNextTuple();
          this.AddNextTuple();
        }

        public DiscussionParser.Token CurrentToken => this._tuples[0].Item1;

        public DateTime? CurrentDateTime => this._tuples[0].Item2;

        public Guid? CurrentGuid => this._tuples[0].Item3;

        public DiscussionParser.Token NextToken => this._tuples[1].Item1;

        public DiscussionParser.Token Next2Token => this._tuples[2].Item1;

        public DiscussionParser.Token Next3Token => this._tuples[3].Item1;

        public DiscussionParser.Token Next4Token => this._tuples[4].Item1;

        public string CurrentText => this._tuples[0].Item5;

        public Guid[] CurrentUsers => this._tuples[0].Item4;

        public void MoveNext()
        {
          if (this._currentPosition >= this._textParts.Length)
            return;
          ++this._currentPosition;
          this._tuples.RemoveAt(0);
          this.AddNextTuple();
        }

        private void AddNextTuple()
        {
          ++this._nextPosition;
          this._tuples.Add(this.GetTupleAt(this._nextPosition));
        }

        private Tuple<DiscussionParser.Token, DateTime?, Guid?, Guid[], string> GetTupleAt(int position)
        {
          if (position >= this._textParts.Length)
            return DiscussionParser.Scanner.EotTuple;
          string textPart = this._textParts[position];
          Guid result = Guid.Empty;
          DateTime dateTime = DateTime.MinValue;
          if (this.TryParseDateTime(textPart, out dateTime))
            return new Tuple<DiscussionParser.Token, DateTime?, Guid?, Guid[], string>(DiscussionParser.Token.DateTime, new DateTime?(dateTime.ToUniversalTime()), new Guid?(), (Guid[]) null, (string) null);
          if (Guid.TryParse(textPart, out result))
            return new Tuple<DiscussionParser.Token, DateTime?, Guid?, Guid[], string>(DiscussionParser.Token.Guid, new DateTime?(), new Guid?(result), (Guid[]) null, (string) null);
          if (!textPart.StartsWith("users:"))
            return new Tuple<DiscussionParser.Token, DateTime?, Guid?, Guid[], string>(DiscussionParser.Token.Text, new DateTime?(), new Guid?(), (Guid[]) null, textPart);
          return new Tuple<DiscussionParser.Token, DateTime?, Guid?, Guid[], string>(DiscussionParser.Token.Users, new DateTime?(), new Guid?(), ((IEnumerable<string>) textPart.Replace("users:", "").Split(',')).Select<string, Guid>((Func<string, Guid>) (o => Guid.Parse(o))).ToArray<Guid>(), textPart);
        }

        private bool TryParseDateTime(string text, out DateTime dateTime)
        {
          if (DateTime.TryParse(text, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTime) || DateTime.TryParse(text, (IFormatProvider) new CultureInfo("ru"), DateTimeStyles.None, out dateTime))
            return true;
          Match match = DiscussionParser.Scanner.DateTimeRegex.Match(text);
          if (match.Groups.Count < 6)
            return false;
          int year = int.Parse(match.Groups[0].Value.Length == 4 ? match.Groups[0].Value : match.Groups[2].Value);
          int month = int.Parse(match.Groups[2].Value.Length != 4 ? match.Groups[1].Value : match.Groups[0].Value);
          int day = int.Parse(match.Groups[2].Value.Length != 4 ? match.Groups[2].Value : match.Groups[1].Value);
          int num = int.Parse(match.Groups[3].Value);
          int minute = int.Parse(match.Groups[4].Value);
          int second = int.Parse(match.Groups[5].Value);
          string str = match.Groups.Count > 6 ? match.Groups[6].Value.ToUpper() : string.Empty;
          dateTime = new DateTime(year, month, day, str != "PM" ? num : num + 12, minute, second);
          return true;
        }
      }
    }
}
