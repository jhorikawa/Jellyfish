using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class ConsecutiveAdditionComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ConsecutiveAdditionComponent class.
        /// </summary>
        public ConsecutiveAdditionComponent()
          : base("Consecutive Addition", "ConsecutiveAddition",
              "Add numbers in a list consecutively.",
              "Jellyfish", "Math")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Numbers", "N", "Number list.", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Consecutive Numbers", "C", "Consecutively added numbers.", GH_ParamAccess.list);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<double> vals = new List<double>();
            if (!DA.GetDataList(0, vals)) return;

            double lastVal = 0;
            List<double> outputList = new List<double>();
            for(int i=0; i<vals.Count; i++)
            {
                lastVal += vals[i];
                outputList.Add(lastVal);
            }

            DA.SetDataList(0, outputList);
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
            get { return new Guid("1bfa5b78-2ca5-4e41-a84a-7d535bc94b6a"); }
        }
    }
}