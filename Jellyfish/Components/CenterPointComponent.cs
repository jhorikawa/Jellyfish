using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;

namespace Jellyfish.Components
{
    public class CenterPointComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CenterPointComponent class.
        /// </summary>
        public CenterPointComponent()
          : base("Center Point", "CenterPoint",
              "Get center point of bounding box of input geometry.",
              "Jellyfish", "Utility")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGeometryParameter("Geometry", "G", "Geometry to get center point.", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Center", "C", "Center point of input geometry.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_GeometricGoo shape = null;
            if (!DA.GetData<IGH_GeometricGoo>(0, ref shape)) return;

            GeometryBase geo = null;
            if (shape is Mesh || shape is GH_Mesh ||
               shape is Brep || shape is GH_Brep ||
               shape is Surface || shape is GH_Surface ||
               shape is Curve || shape is GH_Curve ||
               shape is GH_Box || shape is GH_Line)
            {
                geo = GH_Convert.ToGeometryBase(shape);
            }
            else
            {
                return;
            }

            var bbox = geo.GetBoundingBox(true);

            DA.SetData(0, bbox.Center);
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
            get { return new Guid("49352d0f-c638-4fbf-9ecc-95c603aa0c8f"); }
        }
    }
}