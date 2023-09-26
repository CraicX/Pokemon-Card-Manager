using System.Collections.Generic;

namespace PokeCardManager.Data;
public class FolderData
{
    public int id { get; set; }

    public int parentId { get; set; }

    public int sortIndex { get; set; }

    public string name { get; set; }

    public string folderType { get; set; }

    public string icon { get; set; }

    public List<FolderCardMap> CardMaps { get; set; }
}
