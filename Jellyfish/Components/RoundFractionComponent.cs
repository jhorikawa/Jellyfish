using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class RoundFractionComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the RoundFractionComponent class.
        /// </summary>
        public RoundFractionComponent()
          : base("Round Fraction", "RoundFraction",
              "Round value with specified decimal index.",
              "Jellyfish", "Math")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Number", "N", "Number.", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Decimal Index", "D", "Decimal index.", GH_ParamAccess.item);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Number", "N", "Number.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double num = 0;
            int deci = 0;
            if(!DA.GetData(0, ref num)) return;
            if (!DA.GetData(1, ref deci)) return;

            var coef = Math.Pow(10, deci);
            var val = num > 0 ? Math.Floor((num * coef) + 0.5) / coef : Math.Ceiling((num * coef) - 0.5) / coef;

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
            get { return new Guid("bb3b92e5-d6cd-4a7e-9689-bae67997f6bb"); }
        }
    }
}