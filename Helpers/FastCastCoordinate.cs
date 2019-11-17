using System;
using System.Globalization;

// Copyright: Microsoft. Porting of GeoCordinate library from .NET Framework 4.8 to .NET CORE 3.0

namespace FastCast
{
    public class FastCastCoordinate : IEquatable<FastCastCoordinate>
    {
        private double m_latitude = double.NaN;
        private double m_longitude = double.NaN;

        public static readonly FastCastCoordinate Unknown = new FastCastCoordinate();

        #region Constructors
        //
        // private constructor for creating single instance of FastCastCoordinate.Unknown
        //
        public FastCastCoordinate() { }

        public FastCastCoordinate(Double latitude, Double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        #endregion

        #region Errors
        private static string Argument_MustBeNonNegative = "The value of the parameter must be greater than or equal to zero.";
        private static string Argument_MustBeInRangeZeroTo360 = "The value of the parameter must be from 0.0 to 360.0.";
        private static string Argument_MustBeInRangeNegative90to90 = "The value of the parameter must be from - 90.0 to 90.0.";
        private static string Argument_MustBeInRangeNegative180To180 = "The value of the parameter must be from - 180.0 to 180.0.";
        private static string Argument_LatitudeOrLongitudeIsNotANumber = "The coordinate's latitude or longitude is not a number.";

        #endregion

        #region Properties

        public Double Latitude
        {
            get
            {
                return m_latitude;
            }
            set
            {
                if (value > 90.0 || value < -90.0)
                {
                    throw new ArgumentOutOfRangeException("Latitude", Argument_MustBeInRangeNegative90to90);
                }
                m_latitude = value;
            }
        }

        public Double Longitude
        {
            get
            {
                return m_longitude;
            }
            set
            {
                if (value > 180.0 || value < -180.0)
                {
                    throw new ArgumentOutOfRangeException("Longitude", Argument_MustBeInRangeNegative180To180);
                }
                m_longitude = value;
            }
        }

        public Boolean IsUnknown
        {
            get
            {
                return this.Equals(FastCastCoordinate.Unknown);
            }
        }

        #endregion

        #region Methods

        public Double GetDistanceTo(FastCastCoordinate other)
        {
            //  The Haversine formula according to Dr. Math.
            //  http://mathforum.org/library/drmath/view/51879.html

            //  dlon = lon2 - lon1
            //  dlat = lat2 - lat1
            //  a = (sin(dlat/2))^2 + cos(lat1) * cos(lat2) * (sin(dlon/2))^2
            //  c = 2 * atan2(sqrt(a), sqrt(1-a)) 
            //  d = R * c

            //  Where
            //    * dlon is the change in longitude
            //    * dlat is the change in latitude
            //    * c is the great circle distance in Radians.
            //    * R is the radius of a spherical Earth.
            //    * The locations of the two points in 
            //        spherical coordinates (longitude and 
            //        latitude) are lon1,lat1 and lon2, lat2.

            if (Double.IsNaN(this.Latitude) || Double.IsNaN(this.Longitude) ||
                Double.IsNaN(other.Latitude) || Double.IsNaN(other.Longitude))
            {
                throw new ArgumentException(Argument_LatitudeOrLongitudeIsNotANumber);
            }

            double dDistance = Double.NaN;

            double dLat1 = this.Latitude * (Math.PI / 180.0);
            double dLon1 = this.Longitude * (Math.PI / 180.0);
            double dLat2 = other.Latitude * (Math.PI / 180.0);
            double dLon2 = other.Longitude * (Math.PI / 180.0);

            double dLon = dLon2 - dLon1;
            double dLat = dLat2 - dLat1;

            // Intermediate result a.
            double a = Math.Pow(Math.Sin(dLat / 2.0), 2.0) +
                       Math.Cos(dLat1) * Math.Cos(dLat2) *
                       Math.Pow(Math.Sin(dLon / 2.0), 2.0);

            // Intermediate result c (great circle distance in Radians).
            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            // Distance.
            const Double kEarthRadiusMs = 6376500;
            dDistance = kEarthRadiusMs * c;

            return dDistance;
        }

        #endregion

        #region Object overrides
        public override int GetHashCode()
        {
            return Latitude.GetHashCode() ^ Longitude.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FastCastCoordinate)) return base.Equals(obj);
            return Equals(obj as FastCastCoordinate);
        }

        public override string ToString()
        {
            if (this == FastCastCoordinate.Unknown)
            {
                return "Unknown";
            }
            else
            {
                return Latitude.ToString("G", CultureInfo.InvariantCulture) + ", " +
                       Longitude.ToString("G", CultureInfo.InvariantCulture);
            }
        }

        #endregion

        #region IEquatable
        public bool Equals(FastCastCoordinate other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
        }
        #endregion

        #region Public static operators
        public static Boolean operator ==(FastCastCoordinate left, FastCastCoordinate right)
        {
            if (object.ReferenceEquals(left, null))
            {
                return object.ReferenceEquals(right, null);
            }
            return left.Equals(right);
        }

        public static Boolean operator !=(FastCastCoordinate left, FastCastCoordinate right)
        {
            return !(left == right);
        }
        #endregion
    }
}
