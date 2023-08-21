using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeCard;
[Table("Folders")]
public class FolderData
{
    [Column("id")]
    public int id { get; set; }

    [Column("name")]
    public string name { get; set; }

    [Column("folderType")]
    public string folderType { get; set; }

    [Column("icon")]
    public string icon { get; set; }

    public List<FolderCardMap> CardMaps { get; set; }
}
