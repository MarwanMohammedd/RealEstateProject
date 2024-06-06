using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaProject.Domain.Entities;

namespace VillaProject.Presentation.ViewModels
{
    public class AmenityViewModel
    {
        public Amenity? Amenity { get; set; } 
        public IEnumerable<SelectListItem>? AmenityList { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}