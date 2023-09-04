using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeCard;
public class FolderCardMap
{
    public int cardId { get; set; }
    
    public decimal cost { get; set; } 

    public DateTime date { get; set; }

    public int quantity { get; set; }

    public string options { get; set; }
}
