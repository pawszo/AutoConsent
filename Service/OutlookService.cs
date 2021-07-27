using AutoConsent.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace AutoConsent
{
    public class OutlookService : IInputService
    {
        private readonly IRepository _repository;

        public OutlookService(IRepository repository)
        {
            _repository = repository;
        }
        
        public bool GetRecords()
        {
            bool isSuccess;
            var records = new List<MailRecord>();
            var files = Directory.GetFiles(_repository.Items[Constants.Keys.Directory.ToString()] as string, @"*.msg");
            var app = new Application();
            try
            {
                
                var filesEnum = files.GetEnumerator();
                while (filesEnum.MoveNext())
                {
                    MailRecord record = new MailRecord();
                    string file = filesEnum.Current as string;
                    var mail = app.Session.OpenSharedItem(file) as MailItem;

                    record.ConfirmationDate = mail.ReceivedTime;

                    Regex fullBodyRegex = new Regex(Constants.FullBodyPattern, RegexOptions.Singleline);
                    var match = fullBodyRegex.Match(mail.Body);
                    record.FullName = match.Groups[1].Value;
                    record.Telephone = match.Groups[2].Value;
                    record.Email = match.Groups[3].Value;
                    record.Id = match.Groups[4].Value;
                    record.Consents.AddRange(GetConsents(match.Groups[5].Value));
                    records.Add(record);
                }
                _repository.Items.Add(Constants.Keys.Records.ToString(), records);
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            finally
            {
                app.Quit();
                app = null;
                Marshal.CleanupUnusedObjectsInCurrentContext();
            }
            return isSuccess;
        }
        private IEnumerable<string> GetConsents(string text)
        {
            IList<string> consents = new List<string>();
            string cleanBody = text.Split(new string[] { @"--" }, StringSplitOptions.None)[0].Trim();
            Regex consentsRegex = new Regex(Constants.ConsentsPattern);
            var matches = consentsRegex.Matches(cleanBody);
            var matchEnum = matches.GetEnumerator();
            while(matchEnum.MoveNext())
            {
                var current = matchEnum.Current as Match;
                consents.Add(current.Value);
            }
            return consents;

        }
    }
}
