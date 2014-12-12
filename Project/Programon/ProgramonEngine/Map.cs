using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    /// <summary>
    /// A class to create maps
    /// </summary>
    public class Map
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public int SizeX { get; private set; }
        public int SizeY { get; private set; }

        public Dictionary<Vector2, Node> MapDictionary { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapDictionary"/> class.
        /// </summary>
        public Map()
        {
            this.Id = 0;
            this.Name = "test";
            this.SizeX = 1;
            this.SizeY = 1;
            this.MapDictionary = new Dictionary<Vector2, Node>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapDictionary"/> class.
        /// </summary>
        /// <param name="id">The identifier of the map.</param>
        /// <param name="name">The name of the map.</param>
        /// <param name="sizeX">The width of the map.</param>
        /// <param name="sizeY">The height of the map.</param>
        /// <param name="nodes">The nodes of the map.</param>
        public Map(int id, string name, int sizeX, int sizeY, Dictionary<Vector2, Node> map)
        {
            this.Id = id;
            this.Name = name;
            this.SizeX = sizeX;
            this.SizeY = sizeY;
            this.MapDictionary = map;
        }
    }
}
