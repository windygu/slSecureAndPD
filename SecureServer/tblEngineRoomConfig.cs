
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
    
public partial class tblEngineRoomConfig
{

    public tblEngineRoomConfig()
    {

        this.tblControllerConfig = new HashSet<tblControllerConfig>();

        this.tblEntranceGuardConfig = new HashSet<tblEntranceGuardConfig>();

        this.tblERPlane = new HashSet<tblERPlane>();

        this.tblPDConfig = new HashSet<tblPDConfig>();

        this.tblCCTVConfig = new HashSet<tblCCTVConfig>();

        this.tblNVRConfig = new HashSet<tblNVRConfig>();

    }


    public int ERID { get; set; }

    public string ERName { get; set; }

    public string LineID { get; set; }

    public string Direction { get; set; }

    public double GPSX { get; set; }

    public double GPSY { get; set; }

    public string ERNo { get; set; }

    public string CallOpenDoor { get; set; }



    public virtual ICollection<tblControllerConfig> tblControllerConfig { get; set; }

    public virtual ICollection<tblEntranceGuardConfig> tblEntranceGuardConfig { get; set; }

    public virtual ICollection<tblERPlane> tblERPlane { get; set; }

    public virtual ICollection<tblPDConfig> tblPDConfig { get; set; }

    public virtual ICollection<tblCCTVConfig> tblCCTVConfig { get; set; }

    public virtual ICollection<tblNVRConfig> tblNVRConfig { get; set; }

}

}
