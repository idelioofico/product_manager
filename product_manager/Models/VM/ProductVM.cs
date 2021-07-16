using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_manager.Models.VM
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> ProductCategory { get; set; }
    }
}
