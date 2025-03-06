using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcavesServices
{
    public static class McavesConstants
    {
        public static string UserInSession = "CurrentUser";
        public static string CompanyInSession = "CompanyInSession";
        public static string DDLSelect = "--Select--";
        public static string VoucherNoField = "vchno";

        public static string DispMessage = "Msg";
        public static string DispMessageType = "MsgType";

        public const string Insert = "I";
        public const string Update = "U";
        public const string Delete = "D";
        public const string View = "V";

    }

    public static class NotificationConstants
    {
        public static int PatientEntry = 1;
        public static int PatientTransfer = 2;
        public static int ChildTransfer = 3;
        public static int Others = 10;
    }
}
