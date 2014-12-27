using System;

public struct Coord2
{
	public readonly static Coord2 ZERO;

	public int offset_x;

	public int offset_y;

	public float scale_x;

	public float scale_y;

	static Coord2()
	{
		Coord2.ZERO = new Coord2(0, 0, 0f, 0f);
	}

	public Coord2(int newOffset_x, int newOffset_y)
	{
		this.offset_x = newOffset_x;
		this.offset_y = newOffset_y;
		this.scale_x = 0f;
		this.scale_y = 0f;
	}

	public Coord2(float newScale_x, float newScale_y)
	{
		this.offset_x = 0;
		this.offset_y = 0;
		this.scale_x = newScale_x;
		this.scale_y = newScale_y;
	}

	public Coord2(int newOffset_x, int newOffset_y, float newScale_x, float newScale_y)
	{
		this.offset_x = newOffset_x;
		this.offset_y = newOffset_y;
		this.scale_x = newScale_x;
		this.scale_y = newScale_y;
	}
}