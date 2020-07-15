using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class PointToTextComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the PointToTextComponent class.
        /// </summary>
        public PointToTextComponent()
          : base("Point To Text", "PointToText",
              "Convert point to text.",
              "Jellyfish", "Utility")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Point", "P", "Point to convert to text.", GH_ParamAccess.item);
            pManager.AddTextParameter("Delimiter", "D", "Delimiter.", GH_ParamAccess.item, ",");
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Text", "T", "Converted text from point.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Point3d pt = Point3d.Origin;
            if (!DA.GetData(0, ref pt)) return;

            string deli = null;
            if (!DA.GetData(1, ref deli)) return;

            string pttext = pt.X + deli + pt.Y + deli + pt.Z;

            DA.SetData(0, pttext);
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
            get { return new Guid("dcffa1af-6c63-4c36-a0e7-e36205cdb71a"); }
        }
    }
}