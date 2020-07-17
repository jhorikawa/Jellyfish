using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class RandomVectorComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the RandomVectorComponent class.
        /// </summary>
        public RandomVectorComponent()
          : base("Random Vector", "RandomVector",
              "Create normalized random direction vector.",
              "Jellyfish", "Vector")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Number", "N", "Number of random vectors.", GH_ParamAccess.item, 1);
            pManager.AddIntegerParameter("Seed", "S", "Random seed.", GH_ParamAccess.item, 0);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddVectorParameter("Vectors", "V", "Random vectors.", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int num = 0;
            int seed = 0;

            if (!DA.GetData(0, ref num)) return;
            if (!DA.GetData(1, ref seed)) return;

            Random rnd = new Random(seed);

            List<Vector3d> vecs = new List<Vector3d>();
            for(int i=0; i<num; i++)
            {
                var x = rnd.NextDouble() - 0.5;
                var y = rnd.NextDouble() - 0.5;
                var z = rnd.NextDouble() - 0.5;

                var vec = new Vector3d(x, y, z);
                vec.Unitize();

                vecs.Add(vec);
            }

            DA.SetDataList(0, vecs);
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
            get { return new Guid("cf245056-a001-455c-87fb-98d934371463"); }
        }
    }
}