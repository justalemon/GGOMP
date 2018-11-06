namespace GGO.Shared
{
    public class Location
    {
        /// <summary>
        /// The X position.
        /// </summary>
        public float X;
        /// <summary>
        /// The Y position.
        /// </summary>
        public float Y;
        /// <summary>
        /// The Z position.
        /// </summary>
        public float Z;
        /// <summary>
        /// The rotation.
        /// </summary>
        public float R;

        public Location(float PX, float PY, float PZ, float PR)
        {
            X = PX;
            Y = PY;
            Z = PZ;
            R = PR;
        }
    }
}
