//------------------------------------------------------------------------------
// <auto-generated>
//    這個程式碼是由範本產生。
//
//    對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//    如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoomDoorControlServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblEngineRoomLog
    {
        public long FlowID { get; set; }
        public string ERNo { get; set; }
        public string ControlID { get; set; }
        public short Result { get; set; }
        public System.DateTime StartTime { get; set; }
        public Nullable<System.DateTime> Endtime { get; set; }
        public string Memo { get; set; }
        public string ABA { get; set; }
        public Nullable<short> TypeID { get; set; }
        public Nullable<short> TypeCode { get; set; }
        public string NVRFile { get; set; }
    }
}
