using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ModuleGenerator
{
    public enum blockType { Quarters, Infirmary, Armory, Generators, Storage, Stairways, WedgeRoof, CornerRoof };

    public class BuildingModuleGen : MonoBehaviour
    {

        #region Fields
        private static BuildingModuleGen _instance;

        public Mesh[] meshArray;
        public GameObject buildingPrefab;

        private bool[,,] moduleArray = new bool[15, 15, 15];

        public static BuildingModuleGen Instance
        {
            get
            {
                return _instance;
            }
        }



        #endregion

        #region BlockPositionPlacement & BlockType Selection
        public bool CheckSlot(int x, int y, int z)
        {
            if (x < 0 || x > 14 || y < 0 || y > 14 || z < 0 || z > 14) return true;
            else
            {
                return moduleArray[x, y, z];
            }

        }

        public void SetSlot(int x, int y, int z, bool occupied)
        {
            if (!(x < 0 || x > 14 || y < 0 || y > 14 || z < 0 || z > 14))
            {
                moduleArray[x, y, z] = occupied;
            }

        }

        #endregion

    }

}



