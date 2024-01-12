using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.ViewModels
{
    public class CategoryVM
    {
        public Guid Id { get; set; }
        public string? CategoryName { get; set; }
        public Guid? IdCategoryPa { get; set; }
    }
}
