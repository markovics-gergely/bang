using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.BLL.Infrastructure.Queries.Catalog.Card.ViewModels
{
    public class CardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CardEffectType { get; set; }
    }
}
