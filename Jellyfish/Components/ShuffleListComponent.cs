using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class ShuffleListComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ShuffleListComponent class.
        /// </summary>
        public ShuffleListComponent()
          : base("Shuffle List", "ShuffleList",
              "Shuffle list.",
              "Jellyfish", "Tree")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("List", "L", "List to shuffle.", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Seed", "S", "Random seed for shuffling.", GH_ParamAccess.item, 0);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Shuffled List", "S", "Shuffled list.", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var list = new List<object>();
            int seed = 0;

            if (!DA.GetDataList(0, list)) return;
            if (!DA.GetData(1, ref seed)) return;

            Random rnd = new Random(seed);
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            DA.SetDataList(0, list);
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
            get { return new Guid("71c17405-dc8b-435b-b65b-20c77d9a2b18"); }
        }
    }
}