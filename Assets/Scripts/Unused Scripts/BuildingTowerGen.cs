using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModuleGenerator
{

    public class BuildingTowerGen : MonoBehaviour
    {

        #region
        public BuildingProfile myProfile;
        public Transform basePrefab;
        public int recursionLevel = 0;
        private int maxLevel = 3;

        private BuildingModuleGen moduleManager;
        private Renderer myRenderer;
        private GameObject myMesh;
        private MeshFilter myMeshFilter;
        #endregion


        public void SetProfile(BuildingProfile profile)
        {
            myProfile = profile;
            maxLevel = myProfile.maxHeight;
        }

        public void Initialize(int recLevel, GameObject mesh)
        {

            recursionLevel = recLevel;
            myMesh = mesh;
            maxLevel = myProfile.maxHeight;
        }

        #region Methods

        private void Awake()
        {
            myRenderer = GetComponent<Renderer>();
            myMeshFilter = GetComponent<MeshFilter>();
        }

        private void Start()
        {
            int x = Mathf.RoundToInt(transform.position.x + 7.0f);
            int y = Mathf.RoundToInt(transform.position.y);
            int z = Mathf.RoundToInt(transform.position.z + 7.0f);
            moduleManager = BuildingModuleGen.Instance;

            Transform child;
            if (recursionLevel == 0)
            {
                if (!moduleManager.CheckSlot(x, y, z))
                {
                    int objNum = myProfile.groundModules.Length;   // Number of available modules
                    myMesh = myProfile.groundModules[Random.Range(0, objNum)];
                    moduleManager.SetSlot(x, y, z, true);
                }
            }
            else
            {
                Destroy(gameObject);
            }

          //  myMeshFilter.mesh = myMesh;


            if (recursionLevel < maxLevel)
            {
                if (recursionLevel == maxLevel - 1)
                {
                    if (!moduleManager.CheckSlot(x, y + 1, z))
                    {
                        child = Instantiate(basePrefab,transform.position + Vector3.up*1.05f, Quaternion.identity,this.transform);
                        int objNum = myProfile.roofModules.Length;

                        child.GetComponent<BuildingTowerGen>().Initialize(recursionLevel + 1, myProfile.roofModules[Random.Range(0,objNum)]);

                        moduleManager.SetSlot(x,y+1,z, true);




                    }

                }
                if (!moduleManager.CheckSlot(x, y + 1, z))
                {
                    child = Instantiate(basePrefab, transform.position + Vector3.up * 1.05f, Quaternion.identity, this.transform);
                    int objNum = myProfile.mainModules.Length;

                    child.GetComponent<BuildingTowerGen>().Initialize(recursionLevel + 1, myProfile.mainModules[Random.Range(0, objNum)]);

                    moduleManager.SetSlot(x, y + 1, z, true);
                }
            }

        }



        private void Update()
        {
            if (transform.position.y < -5f)
            {
                Destroy(gameObject);
            }
        }

        #endregion

    }

}


