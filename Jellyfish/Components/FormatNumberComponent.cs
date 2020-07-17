using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class FormatNumberComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FormatNumberComponent class.
        /// </summary>
        public FormatNumberComponent()
          : base("Format Number", "FormatNumber",
              "Format number with digit input.",
              "Jellyfish", "Text")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Number", "N", "Number to format.", GH_ParamAccess.item, 100);
            pManager.AddTextParameter("Format", "F", "Format.", GH_ParamAccess.item, "0000.00");
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Formatted Number", "F", "Formatted Number", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double num = 0;
            string format = null;
            if (!DA.GetData(0, ref num)) return;
            if (!DA.GetData(1, ref format)) return;

            string ftext = num.ToString(format);

            DA.SetData(0, ftext);
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
            get { return new Guid("33ea83f5-6de6-4263-b40b-a411bafebdfd"); }
        }
    }
}