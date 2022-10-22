namespace stock_promo.Models
{
    public class InsiderData
    {
        public string? SYMBOL { get; set; }
        public string? COMPANY { get; set; }
        public string? REGULATION { get; set; }
        public string? NAMEOFTHEACQUIRERDISPOSER { get; set; }
        public string? CATEGORYOFPERSON { get; set; }
        public string? TYPEOFSECURITYPRIOR { get; set; }
        public string? NOOFSECURITYPRIOR { get; set; }
        public decimal? SHAREHOLDINGPRIOR { get; set; }
        public string? TYPEOFSECURITYACQUIREDDISPLOSED { get; set; }
        public long? NOOFSECURITIESACQUIREDDISPLOSED { get; set; }
        public long? VALUEOFSECURITYACQUIREDDISPLOSED { get; set; }
        public string? ACQUISITIONDISPOSALTRANSACTIONTYPE  { get; set; }
        public string? TYPEOFSECURITYPOST { get; set; }
        public string? NOOFSECURITYPOST  { get; set; }
        public decimal? SHAREHOLDINGPOST { get; set; }
        public DateTime? DATEOFALLOTMENTACQUISITIONFROM { get; set; }
        public DateTime? DATEOFALLOTMENTACQUISITIONTO { get; set; }
        public DateTime? DATEOFINITMATIONTOCOMPANY { get; set; }
        public string? MODEOFACQUISITION { get; set; }
        public string? DERIVATIVETYPESECURITY { get; set; }
        public string? DERIVATIVECONTRACTSPECIFICATION { get; set; }
        public string? NOTIONALVALUEBUY { get; set; }
        public string? NUMBEROFUNITSCONTRACTLOTSIZEBUY { get; set; }
        public string? NOTIONALVALUESELL { get; set; }
        public string? NUMBEROFUNITSCONTRACTLOTSIZESELL { get; set; }
        public string? EXCHANGE { get; set; }
        public string? REMARK { get; set; }
        public DateTime? BROADCASTEDATEANDTIME  { get; set; }
        public string? XBRL { get; set; }
        public decimal? Buyprice { get; set; }
        public decimal? IncreaseShareholding { get; set; }
    }
}
