namespace PokeCardManager.Data;
public class Filter
{
    public FilterTypes Type { get; set; }

    public string Value { get; set; }

    public string Title { get; set; }

    public int Hash => GetHashCode(); 

    public string GetImage
    {
        get
        {
        
            Dictionary<string, string> rarityImages = new()
            {
                { "Common"         , "common"         },
                { "Uncommon"       , "uncommon"       },
                { "Promo"          , "promo"          },
                { "Radiant Rare"   , "rare-radiant"   },
                { "Double Rare"    , "double-rare"    },
                { "Rare"           , "rare"           },
                { "Rare ACE"       , "rare-ace"       },
                { "Rare BREAK"     , "rare-break"     },
                { "LEGEND"         , "legend"         },
                { "Hyper Rare"     , "hyper-rare"     },
                { "Rare Holo"      , "holo-rare"      },
                { "Rare Holo EX"   , "rare-holo-ex"   },
                { "Rare Holo GX"   , "gx"             },
                { "Rare Holo LV.X" , "rare-holo-lv-x" },
                { "Rare Holo Star" , "pokemon-star"   },
                { "Rare Holo V"    , "v"              },
                { "Rare Holo VMAX" , "vmax"           },
                { "Rare Holo VSTAR", "vstar"          },
                { "Rare Prime"     , "rare-prime"     },
                { "Rare Prism Star", "prism-star"     },
                { "Rare Rainbow"   , "rare-rainbow"   },
                { "Rare Secret"    , "rare-secret"    },
                { "Rare Shiny"     , "shiny-rare"     },
                { "Rare Shining"   , "shining-holo"   },
                { "Rare Shiny GX"  , "rare-shiny-gx"  },
                { "Rare Ultra"     , "rare-ultra"     },
                { "Ultra Rare"     , "ultra-rare"     },
                { "Amazing Rare"   , "amazing-rare"   },
                { "Illustration Rare"     , "illustration-rare"     },
                { "Classic Collection"     , "rare-classic"     },
                { "Special Illustration Rare"     , "special-illustration-rare"     },
                { "Trainer Gallery Rare Holo"     , "trainer-gallery-holo-rare"     },
            };

            if (rarityImages.TryGetValue(Title, out var image)) return image;

            return "";
        }

    }

    public enum FilterTypes
    {
        SubTypes,
        Rarities,
        SuperTypes,
        ElementTypes,
        SetName,
    }
}
