//------------------------------------------------------------------------------
// <auto-generated>
//    這個程式碼是由範本產生。
//
//    對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//    如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace test
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPDConfig
    {
        public tblPDConfig()
        {
            this.tblPDAlarmLog = new HashSet<tblPDAlarmLog>();
        }
    
        public string PDName { get; set; }
        public Nullable<int> ERID { get; set; }
        public string LineID { get; set; }
        public string Direction { get; set; }
        public Nullable<int> mile_m { get; set; }
        public string IP { get; set; }
        public Nullable<int> Port { get; set; }
        public Nullable<double> GPSX { get; set; }
        public Nullable<double> GPSY { get; set; }
        public Nullable<int> R0 { get; set; }
        public Nullable<int> S0 { get; set; }
        public Nullable<int> T0 { get; set; }
        public Nullable<int> R1 { get; set; }
        public Nullable<int> S1 { get; set; }
        public Nullable<int> T1 { get; set; }
        public Nullable<int> NO_Loop { get; set; }
        public Nullable<int> L0 { get; set; }
        public Nullable<int> L1 { get; set; }
        public Nullable<int> L2 { get; set; }
        public Nullable<int> L3 { get; set; }
        public Nullable<int> L4 { get; set; }
        public Nullable<int> Cabinet { get; set; }
        public Nullable<int> Comm_state { get; set; }
        public Nullable<int> type { get; set; }
        public string Memo { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> PlaneID { get; set; }
        public string Mapping_DeviceName { get; set; }
        public string Classify { get; set; }
        public Nullable<int> WorkInfo { get; set; }
    
        public virtual tblEngineRoomConfig tblEngineRoomConfig { get; set; }
        public virtual ICollection<tblPDAlarmLog> tblPDAlarmLog { get; set; }
    }
}
