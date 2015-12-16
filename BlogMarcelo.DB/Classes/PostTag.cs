using BlogMarcelo.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMarcelo.DB.Classes
{
    public class PostTag: ClasseBase
    {
        public int IdPost { get; set; }
        public string Tag { get; set; }

        public virtual TagClass TagClass { get; set; }
        public virtual Post Post { get; set; }
    }
}
