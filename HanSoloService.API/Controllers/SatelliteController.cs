using CoreServices;
using HanSoloService.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HanSoloService.API.Controllers
{
	[ApiController]
	public class SatelliteController : ControllerBase
	{
		private readonly ILogger<SatelliteController> _logger;
		private static Position _positionKenobi = new Position { X = -500, Y = -200 };
		private static Position _positionSkywalker = new Position { X = 100, Y = -100 };
		private static Position _positionSato = new Position { X = 500, Y = 100 };
		private static IDictionary<string, Position> _satellitePositions = new Dictionary<string, Position> { { "kenobi", _positionKenobi }, { "skywalker", _positionSkywalker }, { "sato", _positionSato } };

		private readonly IMessageService _messageService;
		private readonly ILocationService _locationService;

		private static IDictionary<string, Satellite> _satellites = new Dictionary<string, Satellite>();

		public SatelliteController(ILogger<SatelliteController> logger, IMessageService messageService, ILocationService locationService)
		{
			_logger = logger;
			_locationService = locationService;
			_messageService = messageService;
		}

		[HttpPost]
		[Route("/topsecret")]
		public IActionResult PostSatellitesInfo([FromBody] SatellitesDto satellitesDto)
		{
			try
			{
				var satellites = Map(satellitesDto.Satellites);
				satellites.ForEach(s => {
					Position position;
					if (!_satellitePositions.TryGetValue(s.Name, out position))
					{
						throw new Exception("Satellite name not exists");
					}
					s.Position = position;
				});
			
				var position = _locationService.GetLocation(satellites);
				var message = _messageService.GetMessage(satellites);
				return Ok(new { position = position, message = message });
			}
			catch(Exception ex)
			{
				_logger.LogError(ex.Message);
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("/topsecret-split/{satelliteName}")]
		public IActionResult PostSatellitesInfoSplit([FromRoute]string satelliteName, [FromBody] SatelliteLiteDto satelliteDto)
		{
			try
			{
				var satellite = Map(satelliteDto);
				Position position;
				if (!_satellitePositions.TryGetValue(satelliteName, out position))
				{
					throw new Exception("Satellite name not exists");
				}
				satellite.Name = satelliteName;
				satellite.Position = position;

				//TODO: agregar un metodo PUT para actualizar los valores de un satelite. Por el momento en el metodo POST se agrega o modifica un satellite.
				if (_satellites.ContainsKey(satelliteName))
				{
					_satellites[satelliteName] = satellite;
				}
				else
				{
					_satellites.Add(satelliteName, satellite);
				}

				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("/topsecret-split")]
		public IActionResult GetSatellitesInfoSplit()
		{
			try
			{
				var satelliteList = _satellites.Values.ToList();
				var position = _locationService.GetLocation(satelliteList);
				var message = _messageService.GetMessage(satelliteList);
				return Ok(new { position = position, message = message });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return NotFound();
			}
		}

		private List<Satellite> Map(List<SatelliteDto> satellitesDto)
		{
			return satellitesDto
				.Select(s => new Satellite
				{
					Name = s.Name,
					Distance = s.Distance,
					Message = s.Message
				}).ToList();
		}

		private Satellite Map(SatelliteLiteDto satelliteDto)
		{
			return new Satellite
			{
				Distance = satelliteDto.Distance,
				Message = satelliteDto.Message
			};
		}
	}
}
