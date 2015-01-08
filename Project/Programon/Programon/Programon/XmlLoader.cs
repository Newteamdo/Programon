using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProgramonEngine;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Programon
{
    /// <summary>
    /// A class to load Settings and a Map.
    /// </summary>
    public static class XmlLoader
    {
        /// <summary>
        /// Loads the settings.
        /// </summary>
        /// <param name="mainWindow">The main window.</param>
        /// <param name="spriteDrawer">The sprite drawer.</param>
        /// <param name="xmlLocation">The XML location.</param>
        public static void LoadSettings(MainWindow mainWindow, SpriteDrawer spriteDrawer, string xmlLocation)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlLocation);

            string width;
            width = xDoc.SelectSingleNode("Config/Resolution/width").InnerText;

            string height = xDoc.SelectSingleNode("Config/Resolution/height").InnerText;

            int screenWidth = 800;
            int screenHeight = 600;

            if (int.TryParse(width, out screenWidth) && int.TryParse(height, out screenHeight))
            {
                spriteDrawer.SetWindowSize(screenWidth, screenHeight);
            }
            else
            {
                spriteDrawer.SetWindowSize(screenWidth, screenHeight);
            }

            string volumeText = xDoc.SelectSingleNode("Config/Mastervolume").InnerText;

            double volumeValue;
            if (double.TryParse(volumeText, out volumeValue))
            {
                mainWindow.VolumeLevel = volumeValue;
            }
            else
            {
                mainWindow.VolumeLevel = 100;
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        /// <param name="mainWindow">The main window.</param>
        /// <param name="spriteDrawer">The sprite drawer.</param>
        /// <param name="xmlLocation">The XML location.</param>
        public static void SaveSettings(MainWindow mainWindow, SpriteDrawer spriteDrawer, string xmlLocation)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlLocation);

            xDoc.SelectSingleNode("Config/Resolution/width").InnerText = spriteDrawer.BufferSize.Width.ToString();
            xDoc.SelectSingleNode("Config/Resolution/height").InnerText = spriteDrawer.BufferSize.Height.ToString();

            xDoc.SelectSingleNode("Config/Mastervolume").InnerText = mainWindow.VolumeLevel.ToString();
            xDoc.Save(xmlLocation);
        }

        /// <summary>
        /// Loads the map.
        /// </summary>
        /// <param name="mainWindow">The main window.</param>
        /// <param name="xmlLocation">The XML location.</param>
        /// <returns></returns>
        public static Map LoadMap(MainWindow mainWindow, string xmlLocation)
        {
            XmlDocument mapFile = new XmlDocument();

            mapFile.Load(xmlLocation);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlLocation);

            XmlNode mapNode = xmlDoc.SelectSingleNode("Map");
            int mapId = Convert.ToInt16(mapNode.Attributes["Id"].Value);
            string mapName = mapNode.Attributes["Name"].Value;
            int mapSizeX = 50;
            int mapSizeY = 50;


            XmlNodeList nodeList = xmlDoc.SelectNodes("Map/Node");

            Dictionary<Vector2, Node> nodes = new Dictionary<Vector2, Node>();

            foreach (XmlNode xmlNode in nodeList)
            {
                int xPos = Convert.ToInt32(xmlNode.Attributes["PosX"].Value);
                int yPos = Convert.ToInt32(xmlNode.Attributes["PosY"].Value);
                bool walkable = Convert.ToBoolean(xmlNode.Attributes["Walkable"].Value);

                string texture = xmlNode.Attributes["Texture"].Value;

                Node node = new Node(new Vector2(xPos, yPos), mainWindow.Content.Load<Texture2D>(texture), walkable);

                nodes.Add(node.Transform.Position, node);
            }

            return new Map(mapId, mapName, mapSizeX, mapSizeY, nodes);
        }
    }
}
