namespace WirelessPointerDriver;

public class Position3D {

    public double X { get; private set; }
    public double Y { get; private set; }
    public double Z { get; private set; }

    public Position3D(List<string> rawPositions) { 
        Dictionary<string, double> positionObjects = [];

        foreach (string pos in rawPositions) {
            string posCopy = pos.Replace("{", "");
            posCopy = posCopy.Replace("}", "");
            string[] components = posCopy.Split(':');

            if (!double.TryParse(components[1], out double value))
                value = 0.0;

            positionObjects.Add(components[0], value);

        }

        X = positionObjects["x"];
        Y = positionObjects["y"];
        Z = positionObjects["z"];

    }

    public override string ToString()
    {
        return $"{X}, {Y}, {Z}";
    }

}