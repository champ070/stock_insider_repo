namespace stock_promo.Models
{
    public class FilteredDataOutputDto
    {
        public string? Symbol { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? ShareholdingIncrease { get; set; }
        public DateTime? FirstDateOfAllotment { get; set; }
        public DateTime? LastDateOfAllotment { get; set; }
        public int NumberOfTxn { get; set; }

    }
}
