using Common.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace ShopSection.Application.Contracts.AirlineApp
{
    public class SaveAirline : BaseEfSaveModel
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public IFormFile Picture { get; set; }
    }

    public class ViewAirline : BaseEfViewModel
    {
        public string Info { get; set; }
        public string Picture { get; set; }
    }

    public class SearchAirline : BaseEfSearchModel
    {
    }
}
