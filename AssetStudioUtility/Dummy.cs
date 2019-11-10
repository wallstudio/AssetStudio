using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssetStudio
{
    class Fbx
    {
        public class Dummy1
        {
            internal void Export(string path, IImported imported, bool eulerFilter, float filterPrecision, bool allNodes, bool skins, bool animation, bool blendShape, bool castToBone, float boneSize, float scaleFactor, int versionIndex, bool isAscii)
            {
                throw new NotImplementedException();
            }
        }

        public static Dummy1 Exporter { get; internal set; }

        internal static Vector3 QuaternionToEuler(Quaternion quaternion)
        {
            throw new NotImplementedException();
        }
    }
}
