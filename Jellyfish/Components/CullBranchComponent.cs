using System;
using System.Collections.Generic;

using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class CullBranchComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CullBranchComponent class.
        /// </summary>
        public CullBranchComponent()
          : base("Cull Branch", "CullBranch",
              "Cull branch using branch index.",
              "Jellyfish", "Tree")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "DataTree datas.", GH_ParamAccess.tree);
            pManager.AddIntegerParameter("Indexes", "I", "Branch indexes to cull.", GH_ParamAccess.list, 0);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "Culled data", GH_ParamAccess.tree);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_Structure<IGH_Goo> tree;
            List<int> indexes = new List<int>();
            if (!DA.GetDataTree(0, out tree)) return;
            if (!DA.GetDataList(1, indexes)) return;

            DataTree<object> outTree = new DataTree<object>();
            for(int i=0; i<tree.Branches.Count; i++)
            {
                if (!indexes.Contains(i))
                {
                    var path = tree.Paths[i];

                    outTree.AddRange(tree.Branches[i], path);
                }
            }
            DA.SetDataTree(0, outTree);

        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0a614989-83a2-4025-8cf5-cfa5984375ea"); }
        }
    }
}