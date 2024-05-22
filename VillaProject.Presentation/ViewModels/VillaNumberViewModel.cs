using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaProject.Domain.Entities;

namespace VillaProject.Presentation.ViewModels
{
    public class VillaNumberViewModel
    {
        public VillaNumber? VillaNumber { get; set; } 
        public IEnumerable<SelectListItem>? VillaList { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}