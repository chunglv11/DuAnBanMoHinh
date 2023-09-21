using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanMoHinh.Share.Models
{
    public class User : IdentityUser<Guid>
    {
        public DateTime? DateOfBirth { get; set; }
        [ForeignKey("RankId")]
        public virtual Rank Rank { get; set; }

    }
}
