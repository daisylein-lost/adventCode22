using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace advent
{
   public class Point
    {
      public int xCoordinate;

      public int yCoordinate;

      public Point (int x, int y)
      {
        xCoordinate = x;
        yCoordinate = y;
      }
      public Point (string point)
      {
        var points = point.Trim().Split(',');
        xCoordinate = int.Parse(points[0]);
        yCoordinate = int.Parse(points[1]);
      }

      public override string ToString()
      {
        return $"({xCoordinate},{yCoordinate})";
      }

      public override bool Equals(object obj)
      {
        if (obj == null)
        {
            return false;
        }
        if (!(obj is Point))
        {
            return false;
        }
        return (this.xCoordinate == ((Point)obj).xCoordinate)
            && (this.yCoordinate == ((Point)obj).yCoordinate);
      }

      public override int GetHashCode()
      {
          return xCoordinate.GetHashCode() ^ yCoordinate.GetHashCode();
      }
    }
}