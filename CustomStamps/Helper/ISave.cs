using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CustomStamps
{
    public interface ISave
    {
        string Save(MemoryStream fileStream);
    }
}
