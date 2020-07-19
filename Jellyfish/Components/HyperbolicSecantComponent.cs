using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class HyperbolicSecantComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the HyperbolicSecantComponent class.
        /// </summary>
        public HyperbolicSecantComponent()
          : base("Hyperbolic Secant", "SecH",
              "Hyperbolic secant.",
              "Jellyfish", "Math")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("X", "X", "Hyperbolic secant parameter.", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Y", "Y", "Evaluated value.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double param = 0;
            if (!DA.GetData(0, ref param)) return;

            var val = 1.0 / Math.Cosh(param);

            DA.SetData(0, val);
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
            get { return new Guid("01e450ac-a5df-48c7-9efa-6947e0f048fd"); }
        }
    }
}