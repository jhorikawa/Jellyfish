using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class MakePairComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MakePairComponent class.
        /// </summary>
        public MakePairComponent()
          : base("Make Pair", "MakePair",
              "Make a consecutive pair of items from a list.",
              "Jellyfish", "Tree")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "List / DataTree to make a pair from.", GH_ParamAccess.tree);
            pManager.AddIntegerParameter("Pair Number", "P", "Pair number.", GH_ParamAccess.item, 2);
            pManager.AddBooleanParameter("Loop", "L", "Loop.", GH_ParamAccess.item, true);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "Paired data.", GH_ParamAccess.tree);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_Structure<IGH_Goo> dataTree = new GH_Structure<IGH_Goo>();
            int pair = 0;
            bool loop = false;

            if (!DA.GetDataTree(0, out dataTree)) return;
            if (!DA.GetData(1, ref pair)) return;
            if (!DA.GetData(2, ref loop)) return;

            pair = Math.Max(1, pair);

            GH_Structure<IGH_Goo> outTree = new GH_Structure<IGH_Goo>();
            for(int i=0; i < dataTree.Branches.Count; i++)
            {
                var branch = dataTree.Branches[i];
                var path = dataTree.Paths[i];

                int endIndex = branch.Count;
                if (!loop)
                {
                    endIndex -= (pair - 1);
                }
                
                for(int n=0; n<endIndex; n++)
                {
                    var npath = path.AppendElement(n);
                    for (int t = 0; t < pair; t++)
                    {
                        outTree.Append(branch[(n + t) % branch.Count], npath);
                    }
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
            get { return new Guid("7505a360-b247-4d6b-9ba6-7dae944e4b4c"); }
        }
    }
}