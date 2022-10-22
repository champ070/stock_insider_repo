using System.ComponentModel.DataAnnotations;

namespace stock_promo.Models
{
    public class FileUpload
    {
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
    }
}
