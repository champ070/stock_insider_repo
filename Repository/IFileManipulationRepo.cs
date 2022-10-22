using stock_promo.Models;

namespace stock_promo.Repository
{
    public interface IFileManipulationRepo
    {
        public List<InsiderData> GetStockListWithoutFilter(IFormFile? file);
        public List<InsiderData> GetFilteredStock(List<InsiderData>? ListOfStock);
        public List<FilteredDataOutputDto> GetFilteredStockName(List<InsiderData>? ListOfStock);



    }
}
