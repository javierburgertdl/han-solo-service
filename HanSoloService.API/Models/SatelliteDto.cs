using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HanSoloService.API.Models
{
	public class SatelliteDto
	{
		public string Name { get; set; }
		public double Distance { get; set; }
		public string[] Message { get; set; }
	}
}
