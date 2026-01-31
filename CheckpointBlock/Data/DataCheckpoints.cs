namespace CheckpointBlock.Data
{
    using System.IO;
    using System.Xml.Linq;
    using JumpKing;

    public class DataCheckpoints
    {
        private const int SetCount = ModEntry.SetCount;

        /// <summary>
        ///     All checkpoint sets.
        /// </summary>
        public CheckpointSet[] Sets { get; } = new CheckpointSet[SetCount];

        public static DataCheckpoints TryDeserialize()
        {
            var file = Path.Combine(
                Game1.instance.contentManager.root,
                "zebrasSaves",
                "checkpointBlock.sav");

            var data = new DataCheckpoints();
            if (!File.Exists(file))
            {
                return data;
            }

            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var doc = XDocument.Load(fs);
                var root = doc.Root;

                if (root == null)
                {
                    return data;
                }

                for (var i = 0; i < SetCount; i++)
                {
                    var element = root.Element($"Set{i + 1}");
                    if (element != null)
                    {
                        data.Sets[i] = CheckpointSet.FromXElement(element);
                    }
                }
            }

            return data;
        }

        public void SaveToFile()
        {
            var path = Path.Combine(
                Game1.instance.contentManager.root,
                "zebrasSaves");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var root = new XElement("CheckpointData");
            for (var i = 0; i < SetCount; i++)
            {
                if (this.Sets[i] == null)
                {
                    continue;
                }

                root.Add(this.Sets[i].ToXElement($"Set{i + 1}"));
            }

            var doc = new XDocument(root);

            using (var fs = new FileStream(
                       Path.Combine(path, "checkpointBlock.sav"),
                       FileMode.Create,
                       FileAccess.Write,
                       FileShare.None))
            {
                doc.Save(fs);
            }
        }
    }
}
