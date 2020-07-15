using System;
using System.Collections.Generic;

using Grasshopper.Kernel.Data;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class TreeBranchIndexComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the TreeBranchIndexComponent class.
        /// </summary>
        public TreeBranchIndexComponent()
          : base("Tree Branch Index", "TreeBranchIndex",
              "Get DataTree branch using branch index.",
              "Jellyfish", "Tree")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "DataTree data.", GH_ParamAccess.tree);
            pManager.AddIntegerParameter("Branch Index", "I", "Branch index.", GH_ParamAccess.item, 0);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Branch", "B", "Picked branch.", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_Structure<IGH_Goo> tree;
            if (!DA.GetDataTree(0, out tree)) return;

            int index = 0;
            if (!DA.GetData(1, ref index)) return;
            
            List<IGH_Goo> branch = new List<IGH_Goo>();

            if(tree.Branches.Count > 0)
            {
                index = Math.Max(Math.Min(index, tree.Branches.Count - 1), 0);
                branch = tree.Branches[index];
            }

            DA.SetDataList(0, branch);
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
            get { return new Guid("8c82b2e7-1d09-4a63-b14d-5aa1c58b7d74"); }
        }
    }
}