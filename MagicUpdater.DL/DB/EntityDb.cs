using MagicUpdater.DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdaterMonitor.Base
{
	public class EntityDb : MagicUpdaterEntities
	{
		public EntityDb() : base(true)
		{
			
		}
	}
}
