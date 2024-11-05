using Isca_Travels.Areas.Admin.Models;
using Isca_Travels.Areas.Admin.ViewModel;
using System.Data;

namespace Isca_Travels.AutoMapper
{
    public static class  DomainMapper
    {
        public static Trip MappingToDomain(this TripVm tripVm)
        {
            return new Trip() {
                Notes = tripVm.Notes,
                Days = tripVm.Days,
                Description = tripVm.Description,
                Name = tripVm.Name,
                Price = tripVm.Price,
                ImageFile = tripVm.ImageFile
            };
        }

    }
}
