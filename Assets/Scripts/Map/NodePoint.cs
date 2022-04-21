using System;
[System.Serializable]
public class NodePoint : IEquatable<NodePoint>
{
    public int x;
    public int y;

    public NodePoint(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public bool Equals(NodePoint other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return x == other.x && y == other.y;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((NodePoint)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (x * 397) ^ y;
        }
    }
    // * End of Auto generated

    public override string ToString()
    {
        return $"({x}, {y})";
    }
}
