using CsvHelper;
using stock_promo.Models;
using System.Data;
using System.Text;

namespace stock_promo.Repository
{
    public class FileManipulationRepo : IFileManipulationRepo
    {
        public List<InsiderData> GetStockListWithoutFilter(IFormFile? file)
        {
            int rowNum = 0;
            List<InsiderData> data = new List<InsiderData>();

            try
            {
                string path = "C:\\Users\\bjati\\Downloads\\insider";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = Path.GetFileName(file?.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                string csvData = System.IO.File.ReadAllText(filePath);
                DataTable dt = new DataTable();
                bool firstRow = true;
                StringBuilder newColoumn = new StringBuilder();
                StringBuilder oldColoumn = new StringBuilder();
                int colCount = 0;
                foreach (var row in csvData.Split('\n'))
                {
                    if(colCount > 27)
                    {
                        break;
                    }
                    oldColoumn.Append(row);
                    var rowData = row.TrimEnd();
                    newColoumn.Append(rowData);
                    colCount = colCount + 1;
                }
                var abc = newColoumn.ToString();
                int indexOfXRBL = csvData.IndexOf("XBRL ");
                csvData = csvData.Remove(0, indexOfXRBL + 8);
                csvData = csvData.Insert(0,abc);
                foreach (string row in csvData.Split('\n'))
                {
                    rowNum +=  1;
                    if (!string.IsNullOrEmpty(row))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            if (firstRow)
                            {
                                foreach (string cell in row.Split(','))
                                {
                                    dt.Columns.Add(cell.Trim());
                                }
                                firstRow = false;
                            }
                            else
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (string cell in row.Split(","))
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.Trim();
                                    i++;
                                }
                            }
                        }
                    }
                }
                var list = dt.AsEnumerable();
                int ii = 0;
                foreach (var cell in list)
                {
                    DateTime.TryParse(cell?.ItemArray[14]?.ToString(), out DateTime dATEOFALLOTMENTACQUISITIONFROM);
                    DateTime.TryParse(cell?.ItemArray[15]?.ToString(), out DateTime dATEOFALLOTMENTACQUISITIONTO);
                    DateTime.TryParse(cell?.ItemArray[16]?.ToString(), out DateTime dATEOFINITMATIONTOCOMPANY);
                    DateTime.TryParse(cell?.ItemArray[25]?.ToString(), out DateTime bROADCASTEDATEANDTIME);

                    InsiderData dataItem = new InsiderData();
                    dataItem.SYMBOL = cell?.ItemArray[0]?.ToString();
                    dataItem.COMPANY = cell?.ItemArray[1]?.ToString();
                    dataItem.REGULATION = cell?.ItemArray[2]?.ToString();
                    dataItem.CATEGORYOFPERSON = cell?.ItemArray[3]?.ToString();

                    dataItem.TYPEOFSECURITYPRIOR = cell?.ItemArray[4]?.ToString();
                    dataItem.NOOFSECURITYPRIOR = cell?.ItemArray[5]?.ToString();

                    dataItem.SHAREHOLDINGPRIOR = (CheckForNumber(cell?.ItemArray[6]?.ToString())) ? Convert.ToDecimal(cell?.ItemArray[6]) : 0;
                    dataItem.TYPEOFSECURITYACQUIREDDISPLOSED = cell?.ItemArray[7]?.ToString();
                    dataItem.NOOFSECURITIESACQUIREDDISPLOSED = (CheckForNumber(cell?.ItemArray[8]?.ToString())) ? Convert.ToInt64(cell?.ItemArray[8]) : 0;
                    dataItem.VALUEOFSECURITYACQUIREDDISPLOSED = (CheckForNumber(cell?.ItemArray[9]?.ToString())) ?  Convert.ToInt64(cell?.ItemArray[9]) : 0;
                    dataItem.ACQUISITIONDISPOSALTRANSACTIONTYPE = cell?.ItemArray[10]?.ToString();
                    dataItem.TYPEOFSECURITYPOST = cell?.ItemArray[11]?.ToString();
                    dataItem.NOOFSECURITYPOST = cell?.ItemArray[12]?.ToString();
                    dataItem.SHAREHOLDINGPOST = (CheckForNumber(cell?.ItemArray[13]?.ToString())) ? Convert.ToDecimal(cell?.ItemArray[13]) : 0;
                    dataItem.DATEOFALLOTMENTACQUISITIONFROM = dATEOFALLOTMENTACQUISITIONFROM;
                    dataItem.DATEOFALLOTMENTACQUISITIONTO = dATEOFALLOTMENTACQUISITIONTO;
                    dataItem.DATEOFINITMATIONTOCOMPANY = dATEOFINITMATIONTOCOMPANY;
                    dataItem.MODEOFACQUISITION = cell?.ItemArray[17]?.ToString();
                    dataItem.DERIVATIVETYPESECURITY = cell?.ItemArray[18]?.ToString();
                    dataItem.DERIVATIVECONTRACTSPECIFICATION = cell?.ItemArray[19]?.ToString();
                    dataItem.NOTIONALVALUEBUY = cell?.ItemArray[20]?.ToString();
                    dataItem.NUMBEROFUNITSCONTRACTLOTSIZEBUY = cell?.ItemArray[21]?.ToString();
                    dataItem.NOTIONALVALUESELL = cell?.ItemArray[22]?.ToString();
                    dataItem.NUMBEROFUNITSCONTRACTLOTSIZESELL = cell?.ItemArray[23]?.ToString();
                    dataItem.REMARK = cell?.ItemArray[24]?.ToString();
                    dataItem.BROADCASTEDATEANDTIME = bROADCASTEDATEANDTIME;
                    dataItem.XBRL = cell?.ItemArray[26]?.ToString();

                    data.Add(dataItem);                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return data;
        }

        public List<InsiderData> GetFilteredStock(List<InsiderData>? ListOfStock)
        {
            List<InsiderData>? filteredStock = new List<InsiderData>();
            filteredStock = ListOfStock?
                            .Where( x => (x.CATEGORYOFPERSON == ApplicationConstant.Category1 
                                        || x.CATEGORYOFPERSON == ApplicationConstant.Category2) 
                                        && (x.ACQUISITIONDISPOSALTRANSACTIONTYPE == ApplicationConstant.ACQUISITIONDISPOSALTRANSACTIONTYPE1)
                                        && x.TYPEOFSECURITYPRIOR == ApplicationConstant.TYPEOFSECURITYPRIOR
                                        && x.MODEOFACQUISITION == ApplicationConstant.MODEOFACQUISITION).ToList();
            filteredStock = filteredStock.OrderBy(x => x.SYMBOL).ToList();

            foreach (var item in filteredStock)
            {
                item.Buyprice = (decimal)(item.VALUEOFSECURITYACQUIREDDISPLOSED/item.NOOFSECURITIESACQUIREDDISPLOSED);
                item.IncreaseShareholding = item.SHAREHOLDINGPOST - item.SHAREHOLDINGPRIOR;
            }
            
            return filteredStock;
        }
        public List<FilteredDataOutputDto> GetFilteredStockName(List<InsiderData>? ListOfStock)
        {
            List<FilteredDataOutputDto>? stockList = new List<FilteredDataOutputDto>();
            var filteredStock = ListOfStock.GroupBy(x => x.SYMBOL).ToList();
            int count = 0;
            foreach (var item in filteredStock)
            {
                decimal? IncreaseShareHolding = 0;
                long? qtyBought = 0;
                long? txnValue = 0;
                foreach (var partInitial in item)
                {
                    IncreaseShareHolding = IncreaseShareHolding + partInitial.SHAREHOLDINGPOST - partInitial.SHAREHOLDINGPRIOR;
                    qtyBought += partInitial.NOOFSECURITIESACQUIREDDISPLOSED;
                    txnValue += partInitial.VALUEOFSECURITYACQUIREDDISPLOSED;
                }
                var stock = ListOfStock.Where(s => s.SYMBOL == item.Key);
                stockList.Add(new FilteredDataOutputDto
                {
                    Symbol = item.Key,
                    BuyPrice = txnValue/qtyBought,
                    ShareholdingIncrease = Math.Round(IncreaseShareHolding ?? 0, 2),
                    FirstDateOfAllotment = item.Min(s => s.DATEOFALLOTMENTACQUISITIONFROM),
                    LastDateOfAllotment = item.Max(s => s.DATEOFALLOTMENTACQUISITIONFROM),
                    NumberOfTxn = item.Count()
                });
            }
            stockList = stockList.OrderByDescending(s => s.ShareholdingIncrease).ToList();
            return stockList;
        }

        public bool CheckForNumber(string percentage)
        {
            if (percentage.Contains("."))
            {
                var numberArr = percentage.Split('.');
                foreach (var item in numberArr)
                {
                    if (!item.All(char.IsDigit))
                    {
                        return false;
                    }
                }
            }
            else if (!percentage.All(char.IsDigit) || string.IsNullOrEmpty(percentage))
            {
                return false;
            }
            return true;
        }

    }
}
