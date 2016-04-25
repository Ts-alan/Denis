using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;

namespace Sciencecom.Models.Scheduler
{
    public class Job : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (SciencecomEntities _context = new SciencecomEntities())
            {
                for (int i=0;i< _context.Surfaces.Count();i++)
                {
                    if (_context.Surfaces.ElementAt(i).RentFrom != null &&
                        _context.Surfaces.ElementAt(i).RentUntil != null)
                    {
                        if (_context.Surfaces.ElementAt(i).RentFrom < DateTime.Now &&
                            DateTime.Now < _context.Surfaces.ElementAt(i).RentUntil)
                        {
                            _context.Surfaces.ElementAt(i).isFreeOrSocial = false;
                        }
                        if (DateTime.Now > _context.Surfaces.ElementAt(i).RentUntil)
                        {
                            _context.Surfaces.ElementAt(i).isFreeOrSocial = true;
                        }
                    }
                }
            } 
    }
    }
}