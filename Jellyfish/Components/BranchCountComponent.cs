using System;
using System.Collections.Generic;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class BranchCountComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the BranchCountComponent class.
        /// </summary>
        public BranchCountComponent()
          : base("Branch Count", "BranchCount",
              "Get branch count.",
              "Jellyfish", "Tree")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "DataTree datas.", GH_ParamAccess.tree);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Count", "C", "Branch count.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_Structure<IGH_Goo> tree;
            if (!DA.GetDataTree(0, out tree)) return;

            DA.SetData(0, tree.Branches.Count);
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
            get { return new Guid("fca945d0-46eb-4d5e-b616-70599bc1bd1a"); }
        }
    }
}