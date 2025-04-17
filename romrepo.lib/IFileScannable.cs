using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace romrepo.lib
{
    public interface IFileScannable
    {
        string GetPath();
        DateTime GetLastUpdated();
    }
}
