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
    
    public partial class vwR23DeviceStateLog
    {
        public long FlowID { get; set; }
        public short TypeID { get; set; }
        public short TypeCode { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<int> ReaderID { get; set; }
        public string ControlID { get; set; }
        public string SingalName { get; set; }
        public string ABA { get; set; }
        public string Explain { get; set; }
        public string Memo { get; set; }
        public Nullable<short> ControlType { get; set; }
        public Nullable<int> ERID { get; set; }
        public string IP { get; set; }
        public Nullable<int> Port { get; set; }
        public string EntranceCode { get; set; }
        public string Loop { get; set; }
        public Nullable<bool> IsEnable { get; set; }
        public Nullable<double> X { get; set; }
        public Nullable<double> Y { get; set; }
        public Nullable<double> ScaleX { get; set; }
        public Nullable<double> ScaleY { get; set; }
        public Nullable<double> Rotation { get; set; }
        public Nullable<int> PlaneID { get; set; }
        public Nullable<int> TriggerCCTVID { get; set; }
        public Nullable<int> RTUBaseAddress { get; set; }
        public Nullable<int> RTURegisterLength { get; set; }
        public Nullable<int> Comm_state { get; set; }
        public string R23_ADAM { get; set; }
        public Nullable<short> R23DoorOpen_DI { get; set; }
        public Nullable<short> R23DoorOpen_DO { get; set; }
        public string ERName { get; set; }
        public string LineID { get; set; }
        public string Direction { get; set; }
        public Nullable<double> GPSX { get; set; }
        public Nullable<double> GPSY { get; set; }
        public string ERNo { get; set; }
        public string CallOpenDoor { get; set; }
    }
}
