//------------------------------------------------------------------------------
// <auto-generated>
//    這個程式碼是由範本產生。
//
//    對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//    如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SecureServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCardCommandLog
    {
        public long FlowID { get; set; }
        public string ABA { get; set; }
        public string ControlID { get; set; }
        public Nullable<System.DateTime> Timestamp { get; set; }
        public string CommandType { get; set; }
        public bool IsSuccess { get; set; }
        public string Describe { get; set; }
        public string CardType { get; set; }
    }
}
