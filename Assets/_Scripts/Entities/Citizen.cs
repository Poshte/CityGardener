public class Citizen
{
    public House Home { get; set; }
	public bool Employed { get; set; }

	public Citizen(House house)
	{
		Home = house;
	}
}
