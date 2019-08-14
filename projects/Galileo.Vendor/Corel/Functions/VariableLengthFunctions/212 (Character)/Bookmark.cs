using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galileo.Vendor.Corel
{
    public class Bookmark : CharacterGroupFunction
    {
        public BookmarkPacket info { get; set; }

        public Bookmark(WP6Document doc, int index)
            : base(doc, index)
        {
            info = new BookmarkPacket(doc, prefixIds[0]-1);

        }
    }
}
