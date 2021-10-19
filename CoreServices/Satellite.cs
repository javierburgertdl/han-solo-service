namespace CoreServices
{
	public class Satellite
	{
		public string Name { get; set; }
		public double Distance { get; set; }
		public string[] Message { get; set; }
		public Position Position { get; set; }
	}

	public class Position
	{
		public double X { get; set; }
		public double Y { get; set; }
	}
}
