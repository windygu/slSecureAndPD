
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
    
public partial class tblPDAlarmLog
{

    public long FlowID { get; set; }

    public System.DateTime Timestamp { get; set; }

    public string PDName { get; set; }

    public string PDItem { get; set; }

    public string Memo { get; set; }

    public Nullable<int> Status { get; set; }



    public virtual tblPDConfig tblPDConfig { get; set; }

}

}
