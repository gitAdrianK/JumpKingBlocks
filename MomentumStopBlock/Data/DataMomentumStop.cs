namespace MomentumStopBlock.Data
{
    using System;
    using System.IO;
    using System.Xml.Linq;
    using JumpKing;

    public class DataMomentumStop
    {
        private DataMomentumStop() => this.Screen = -1;

        /// <summary>
        ///     The Screen that the screen stop block has last stopped the player on.
        /// </summary>
        public int Screen { get; set; }

        public static DataMomentumStop TryDeserialize()
        {
            var file = Path.Combine(
                Game1.instance.contentManager.root,
                "zebrasSaves",
                "momentumStopBlock.sav");
            if (!File.Exists(file))
            {
                return new DataMomentumStop();
            }

            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var doc = XDocument.Load(fs);
                var root = doc.Root;

                return new DataMomentumStop
                {
                    Screen = int.Parse(root?.Element("Screen")?.Value ?? throw new InvalidOperationException())
                };
            }
        }

        /// <summary>
        ///     Saves the data to file.
        /// </summary>
        public void SaveToFile()
        {
            var path = Path.Combine(
                Game1.instance.contentManager.root,
                "zebrasSaves");
            if (!Directory.Exists(path))
            {
                _ = Directory.CreateDirectory(path);
            }

            var doc = new XDocument(
                new XElement("MomentumStopData",
                    new XElement("Screen", this.Screen)));

            using (var fs = new FileStream(
                       Path.Combine(path, "momentumStopBlock.sav"),
                       FileMode.Create,
                       FileAccess.Write,
                       FileShare.None))
            {
                doc.Save(fs);
            }
        }
    }
}
