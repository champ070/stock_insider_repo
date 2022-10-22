using Microsoft.AspNetCore.Mvc;
using stock_promo.Models;
using stock_promo.Repository;

namespace stock_promo.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            return View(new FileUpload());
        }

        // POST: Stock/Create
        [HttpPost]
        public ActionResult GetStockListWithoutAnyFilter(FileUpload x)
        {
            try
            {
                FileManipulationRepo requiredFile = new FileManipulationRepo();
                var req = requiredFile.GetStockListWithoutFilter(x?.File);
                return View(req);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult GetFilteredInsiderData(FileUpload x)
        {
            try
            {
                FileManipulationRepo requiredFile = new FileManipulationRepo();
                var req = requiredFile.GetStockListWithoutFilter(x?.File);
                var filtered = requiredFile.GetFilteredStock(req);
                return View(filtered);
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        [Route("GetFilteredInsiderData/{x}")]
        public ActionResult GetFilteredInsiderData(string x)
        {
            try
            {
                return View(x);
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult GetFilteredStockName(FileUpload x)
        {
            try
            {
                FileManipulationRepo requiredFile = new FileManipulationRepo();
                var req = requiredFile.GetStockListWithoutFilter(x?.File);
                var filtered = requiredFile.GetFilteredStock(req);
                var finalfilter = requiredFile.GetFilteredStockName(filtered);
                return View(finalfilter);
            }
            catch
            {
                return View();
            }
        }
    }
}
