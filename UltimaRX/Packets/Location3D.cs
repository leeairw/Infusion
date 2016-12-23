using System;

namespace UltimaRX.Packets
{
    public struct Location3D
    {
        public Location3D(ushort x, ushort y, byte z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector operator -(Location3D location1, Location3D location2)
        {
            return new Vector((short) (location1.X - location2.X), (short) (location1.Y - location2.Y),
                (byte) (location1.Z - location2.Z));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Location3D))
            {
                var otherLocation = (Location3D) obj;
                return Equals(otherLocation);
            }

            return false;
        }

        public bool Equals(Location3D other) => X == other.X && Y == other.Y && Z == other.Z;

        public static bool operator ==(Location3D location1, Location3D location2) => location1.Equals(location2);

        public static bool operator !=(Location3D location1, Location3D location2) => !location1.Equals(location2);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode*397) ^ Y.GetHashCode();
                hashCode = (hashCode*397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public ushort X { get; }
        public ushort Y { get; }
        public byte Z { get; }

        public override string ToString() => $"{X}, {Y}, {Z}";

        public Location3D LocationInDirection(Direction direction)
        {
            Vector directionVector;

            if (direction == Direction.East)
                directionVector = Vector.EastVector;
            else if (direction == Direction.North)
                directionVector = Vector.NorthVector;
            else if (direction == Direction.Northeast)
                directionVector = Vector.NortheastVector;
            else if (direction == Direction.Northwest)
                directionVector = Vector.NorthwestVector;
            else if (direction == Direction.Southeast)
                directionVector = Vector.SoutheastVector;
            else if (direction == Direction.Southwest)
                directionVector = Vector.SouthwestVector;
            else if (direction == Direction.South)
                directionVector = Vector.SouthVector;
            else if (direction == Direction.West)
                directionVector = Vector.WestVector;
            else
                throw new ArgumentOutOfRangeException(nameof(direction), $"Unknown direction {direction}");

            return new Location3D((ushort) (X + directionVector.X), (ushort) (Y + directionVector.Y),
                (byte) (Z + directionVector.Z));
        }
    }
}