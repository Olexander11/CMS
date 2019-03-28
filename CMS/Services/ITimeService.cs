using CMS.Models.Feeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Services
{
    public interface ITimeService
    {
    int TimeIntervalHours { get; set; }
    void DoService();
    
    }
    
}
