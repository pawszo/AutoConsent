using AutoConsent.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConsent
{
    public class MailRecord : IModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public List<string> Consents { get; } = new List<string>();
        public DateTime ConfirmationDate { get; set; }
        public string Comment { get; set; }
    }
}
