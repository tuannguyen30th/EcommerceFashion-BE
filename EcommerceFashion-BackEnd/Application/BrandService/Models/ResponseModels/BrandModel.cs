using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BrandService.Models.ResponseModels
{
    public class BrandModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string VitualPath { get; set; }
    }
}
