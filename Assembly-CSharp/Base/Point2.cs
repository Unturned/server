using System;

public struct Point2
{
	public readonly static Point2 ZERO;

	public readonly static Point2 NONE;

	public int x;

	public int y;

	static Point2()
	{
		Point2.ZERO = new Point2(0, 0);
		Point2.NONE = new Point2(-1, -1);
	}

	public Point2(int set_x, int set_y)
	{
		this.x = set_x;
		this.y = set_y;
	}
}