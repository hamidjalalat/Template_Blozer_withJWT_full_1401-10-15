using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
   public class SamplePaydarRepository: Repository<Models.SamplePaydar>, ISamplePaydarRepository
    {
        internal SamplePaydarRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }

    }
}
