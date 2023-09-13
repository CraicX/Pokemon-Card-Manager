using System.ComponentModel.DataAnnotations.Schema;

namespace PokeCardManager.Data;
public class FolderData
{
    public int id
    {
        get; set;
    }

    public string name
    {
        get; set;
    }

    public string folderType
    {
        get; set;
    }

    public string icon
    {
        get; set;
    }

    public List<FolderCardMap> CardMaps
    {
        get; set;
    }
}
